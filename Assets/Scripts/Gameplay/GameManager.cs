using GameAnalyticsSDK;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region singleton
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    [Header("UI References")]
    public TMP_Text coinsText;
    public TMP_Text scoreText;

    // Private Variables
    int score;

    void Start()
    {
        OnGameStart();
    }

    public void OnGameStart()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "New Game Started");

        UserData.instance.VisualizeNumber(coinsText, UserData.instance.coins);
        UserData.instance.VisualizeNumber(scoreText, score);
    }

    public void OnGameEnd()
    {
        UserData.instance.UpdateScore(score);
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Score Achieved", score);

        UserData.instance.SaveData();
        SceneManager.LoadScene(0);
    }

    public void EarnCoin(int amount)
    {
        UserData.instance.AddCoins(amount);
        UserData.instance.VisualizeNumber(coinsText, UserData.instance.coins);
    }

    public void EarnScore(int amount)
    {
        score += amount;
        UserData.instance.VisualizeNumber(scoreText, score);
    }
}
