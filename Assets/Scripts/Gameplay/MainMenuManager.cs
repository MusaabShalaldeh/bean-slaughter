using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }
}
