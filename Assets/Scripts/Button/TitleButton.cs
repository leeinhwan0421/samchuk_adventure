using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class TitleButton : MonoBehaviour
{
    // Title 1 , Game 2, HTP 3, Credit 4

    public void LoadTitleScene()
    {
        LoadingSceneManager.LoadScene(0);
    }

    public void LoadGameScene()
    {
        LoadingSceneManager.LoadScene(1);
    }

    public void LoadHowToPlayScene()
    {
        LoadingSceneManager.LoadScene(2);
    }

    public void LoadCreditScene()
    {
        LoadingSceneManager.LoadScene(3);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
