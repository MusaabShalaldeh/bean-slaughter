using GameAnalyticsSDK;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UserData : MonoBehaviour
{
    #region singleton
    public static UserData instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    [Header("Data")]
    public int score;
    public int coins;

    void Start()
    {
        GameAnalytics.Initialize();

        score = PlayerPrefs.GetInt("score", 0);
        coins = PlayerPrefs.GetInt("coins", 0);
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("score", score);
        PlayerPrefs.SetInt("coins", coins);
    }

    public void UpdateScore(int achievedScore)
    {
        if (achievedScore > score)
            score = achievedScore;
    }

    public void AddCoins(int amount)
    {
        coins += amount;
    }

    public void RemoveCoins(int amount)
    {
        coins -= amount;
    }

    public void VisualizeNumber(TMP_Text txt, int num, string beforeText = "")
    {
        txt.text = beforeText + num.ToString();
    }
}
