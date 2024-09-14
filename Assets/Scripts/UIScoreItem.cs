using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class UIScoreItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI numberText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI killsText;
    [SerializeField] private TextMeshProUGUI assistsText;
    [SerializeField] private TextMeshProUGUI deathsText;

    public int Score = 0;
    public int KillsCount = 0;
    public int AssistsCount = 0;
    public int DeathsCount = 0;

    public TextMeshProUGUI ScoreText { get { return scoreText; } }

    public void SetText(string number, string name, string score, string kills, string assists, string deaths) 
    {
        nameText.text = name;
        scoreText.text = score;
        killsText.text = kills;
        assistsText.text = assists;
        deathsText.text = deaths;
        numberText.text = (transform.GetSiblingIndex() + 1).ToString();
    }

    public void SetValues(int score, int kills, int assists, int deaths) 
    {
        KillsCount = kills;
        DeathsCount = deaths;
        AssistsCount = assists;
        Score = score;
    }

    public void SetCurrentOrderNumber(Transform parent, int index) 
    {
        transform.SetSiblingIndex(index);
        numberText.text = (transform.GetSiblingIndex() + 1).ToString();
    }
}
