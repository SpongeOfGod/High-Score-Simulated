using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIScore : MonoBehaviour
{
    [Header("Score Variables")]
    [SerializeField] private List<UIScoreItem> items = new List<UIScoreItem>();
    [SerializeField] private UIScoreItem prefabScoreItem;
    [SerializeField] private int maxNumberOfPlayers = 10;
    [SerializeField] private OrderWays formToOrder = new OrderWays();

    [Header("ParentOfScore")]
    [SerializeField] private Transform parent;

    [Header("Button Press")]
    [SerializeField] private TMP_InputField inputName;
    [SerializeField] private TMP_InputField inputScore;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button clearListButton;
    [SerializeField] private Button orderByScore;
    [SerializeField] private Button orderByKills;
    [SerializeField] private Button orderByAssists;
    [SerializeField] private Button orderByDeaths;

    [Header("Animation")]
    [SerializeField] Transform parentCanvas;
    [SerializeField] Animator prefabTextItem;

    enum OrderWays 
    {
        byScore, byKills, byAssists, byDeaths
    }

    private void Awake()
    {
        confirmButton.onClick.AddListener(OnConfirmClick);
        clearListButton.onClick.AddListener(ResetList);
        orderByScore.onClick.AddListener(PrepareSortScore);
        orderByKills.onClick.AddListener(PrepareSortKills);
        orderByAssists.onClick.AddListener(PrepareSortAssists);
        orderByDeaths.onClick.AddListener(PrepareSortDeaths);
        formToOrder = OrderWays.byScore;
    }

    private void OnConfirmClick()
    {
        if(inputName.text != string.Empty && inputScore.text != string.Empty) 
        {
            string playerName = inputName.text;
            string playerScore = inputScore.text;

            TryAddPlayer((parent.childCount).ToString(), playerName, playerScore);
        } 
        else if(inputName.text != string.Empty || inputScore.text != string.Empty) 
        {
            inputName.text = string.Empty;
            inputScore.text = string.Empty;
        }
    }

    private void ResetList() 
    {
        foreach(UIScoreItem item in items) 
        {
            Destroy(item.gameObject);
        }
        items.Clear();
        CreateFalsePlayers();
    }

    private void PrepareSortKills() 
    {
        formToOrder = OrderWays.byKills;
        Sorting();
    }

    private void PrepareSortAssists()
    {
        formToOrder = OrderWays.byAssists;
        Sorting();
    }
    private void PrepareSortScore()
    {
        formToOrder = OrderWays.byScore;
        Sorting();
    }
    private void PrepareSortDeaths()
    {
        formToOrder = OrderWays.byDeaths;
        Sorting();
    }

    private void OnDestroy()
    {
        confirmButton.onClick.RemoveAllListeners();
    }

    private void Start()
    {
        CreateFalsePlayers();
    }

    private void CreateFalsePlayers()
    {
        for (int i = 0; i < maxNumberOfPlayers; i++)
        {
            int randomKills = Random.Range(0, 10);
            int randomDeaths = Random.Range(0, 10);
            int randomAssists = Random.Range(0, 10);

            int Score = (randomKills * 3) + randomAssists - randomDeaths;
            Score = Mathf.Clamp(Score, 0, 999);
            AddPlayer(i.ToString(), "Player " + i, Score, randomKills, randomAssists, randomDeaths);
        }
    }

    private void TryAddPlayer(string number, string name, string score) 
    {
        int.TryParse(score, out int addedScore);
        if(addedScore > items[items.Count - 1].Score) 
        {
            int randomKills = Random.Range(0, 10);
            int randomDeaths = Random.Range(0, 10);
            int randomAssists = Random.Range(0, 10);
            int Score = (randomKills * 3) + randomAssists - randomDeaths;

            AddPlayer(number, name, Score, randomKills, randomDeaths, randomAssists);
        } else 
        {
            Instantiate(prefabTextItem, parentCanvas);
        }
    }
    private void AddPlayer(string number, string name, int score, int kills, int assists, int deaths) 
    {
        UIScoreItem item = Instantiate(prefabScoreItem, parent);

        string scoreText = score.ToString();
        string killsText = kills.ToString();
        string assistsText = assists.ToString();
        string deathsText = deaths.ToString();

        item.SetText(number, name, scoreText, killsText, assistsText, deathsText);
        item.SetValues(score, kills, assists, deaths);
        items.Add(item);

        Sorting();

        if (items.Count > maxNumberOfPlayers)
        {
            Destroy(items[items.Count - 1].gameObject);
            items.RemoveAt(items.Count - 1);
        }
    }
    

    private void Sorting() 
    {
        UIScoreItem nextItem = null;
        int indexNext = -1;
        int numberSorted = 0;

        while (numberSorted < items.Count) 
        {
            for (int i = 0; i < items.Count - numberSorted; i++)
            {
                UIScoreItem currentItem = items[i];
                indexNext = i;

                if (i != items.Count - numberSorted - 1)
                {
                    indexNext = indexNext + 1;

                    nextItem = items[indexNext];
                    currentItem = items[i];

                    switch (formToOrder)
                    {
                        case OrderWays.byScore:

                            if (nextItem.Score > currentItem.Score)
                            {
                                (items[indexNext], items[i]) = (items[i], items[indexNext]);
                            }
                            break;

                        case OrderWays.byAssists:

                            if (nextItem.AssistsCount > currentItem.AssistsCount) 
                            {
                                (items[indexNext], items[i]) = (items[i], items[indexNext]);
                            }
                           break;

                        case OrderWays.byKills:
                            
                            if(nextItem.KillsCount > currentItem.KillsCount) 
                            {
                                (items[indexNext], items[i]) = (items[i], items[indexNext]);
                            }
                            break;

                        case OrderWays.byDeaths: 
                            
                            if(nextItem.DeathsCount < currentItem.DeathsCount) 
                            {
                                (items[indexNext], items[i]) = (items[i], items[indexNext]);
                            }
                            break;
                    }

                    items[i].SetCurrentOrderNumber(parent, i);
                    items[indexNext].SetCurrentOrderNumber(parent, indexNext);
                }
                else
                {
                    numberSorted++;
                }
            }
        }
    }
}
