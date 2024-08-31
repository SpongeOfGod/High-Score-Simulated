using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UIScore : MonoBehaviour
{
    [Header("Score Variables")]
    [SerializeField] private List<UIScoreItem> items = new List<UIScoreItem>();
    [SerializeField] private UIScoreItem prefabScoreItem;
    [SerializeField] private int maxNumberOfPlayers = 10;

    [Header("ParentOfScore")]
    [SerializeField] private Transform parent;

    [Header("Button Press")]
    [SerializeField] private TMP_InputField inputName;
    [SerializeField] private TMP_InputField inputScore;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button clearListButton;

    private void Awake()
    {
        confirmButton.onClick.AddListener(OnConfirmClick);
        clearListButton.onClick.AddListener(ResetList);
    }

    private void OnConfirmClick()
    {
        if(inputName.text != string.Empty && inputScore.text != string.Empty) 
        {
            string playerName = inputName.text;
            string playerScore = inputScore.text;

            AddPlayer((parent.childCount).ToString(), playerName, playerScore);
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
            string randomScore = Random.Range(1, 999).ToString();
            AddPlayer(i.ToString(), "Player " + i, randomScore);
        }
    }

    private void AddPlayer(string number, string name, string score) 
    {
        UIScoreItem item = Instantiate(prefabScoreItem, parent);
        item.setText(number, name, score);
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
        UIScoreItem previousItem = null;
        int indexPrev = -1;
        bool changeInOrder = false;
        bool listInOrder = false;

        while (!listInOrder) 
        {
            for (int i = 0; i < items.Count; i++)
            {
                UIScoreItem currentItem = items[i];
                indexPrev = i;

                if (indexPrev - 1 >= 0)
                {
                    indexPrev = indexPrev - 1;

                    previousItem = items[indexPrev];
                    currentItem = items[i];

                    if (previousItem != null && previousItem.Score < currentItem.Score)
                    {
                        items[indexPrev] = currentItem;
                        items[i] = previousItem;
                        changeInOrder = true;
                    }
                }

                items[i].SetCurrentOrderNumber(parent, i);
            }

            if (!changeInOrder) 
            {
                listInOrder = true;
            }

            changeInOrder = false;
        }
    }
}
