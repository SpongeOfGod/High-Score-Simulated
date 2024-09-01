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

    public TextMeshProUGUI ScoreText { get { return scoreText; } }

    public int Score = 0; 

    public void setText(string number, string name, string score) 
    {
        nameText.text = name;
        scoreText.text = score;
        numberText.text = (transform.GetSiblingIndex() + 1).ToString();
        if(int.TryParse(score, out int scoreInt)) 
        {
            Score = scoreInt;
        }
    }
    public void SetCurrentOrderNumber(Transform parent, int index) 
    {
        transform.SetSiblingIndex(index);
        numberText.text = (transform.GetSiblingIndex() + 1).ToString();
    }
}
