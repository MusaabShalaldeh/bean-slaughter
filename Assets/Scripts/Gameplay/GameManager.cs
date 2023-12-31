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
    public GameObject ResultsScreen;
    public TMP_Text achievedScoreText;
    public TMP_Text maxScoreText;
    public TMP_Text earnedCoinsText;

    // Private Variables
    int score;

    void Start()
    {
        OnGameStart();
    }

    public void OnGameStart()
    {
        Time.timeScale = 1.0f;
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "New Game Started");

        UserData.instance.VisualizeNumber(coinsText, UserData.instance.coins);
        UserData.instance.VisualizeNumber(scoreText, score);
    }

    public void OnGameEnd()
    {
        Time.timeScale = 0.0f;
        UserData.instance.UpdateScore(score);
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Score Achieved", score);

        UserData.instance.SaveData();
        ;
        UserData.instance.VisualizeNumber(maxScoreText, UserData.instance.score, "Max Score: ");
        UserData.instance.VisualizeNumber(achievedScoreText, score, "Achieved Score: ");
        UserData.instance.VisualizeNumber(earnedCoinsText, UserData.instance.coins, "Coins Earned: ");

        ResultsScreen.SetActive(true);
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

    public void LoadMainMenu()
    {
        SceneLoader.instance.LoadLevel(0);
    }
}
