using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject opt;
    public GameObject main;
    public void ChangeScene(int x)
    {
        SceneManager.LoadScene(x);
    }

    public void StartGame()
    {
        main.SetActive(true);
        opt.SetActive(false);
        SceneManager.LoadScene(1);
        PlayerPrefs.SetInt("BossStage", 1);
        PlayerPrefs.SetInt("PlayerHP", 100);
    }

    public void Options()
    {
        opt.SetActive(true);
        main.SetActive(false);
    }

    public void OptionsBack()
    {
        opt.SetActive(false);
        main.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
