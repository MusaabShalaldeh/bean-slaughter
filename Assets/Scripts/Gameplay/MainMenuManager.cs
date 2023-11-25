using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text coinsText;
    public TMP_Text scoreText;

    void Start()
    {
        Application.targetFrameRate = 60;
        UserData.instance.VisualizeNumber(coinsText, UserData.instance.coins);
        UserData.instance.VisualizeNumber(scoreText, UserData.instance.score);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
