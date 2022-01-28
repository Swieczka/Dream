using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject main;
    public void ChangeScene(int x)
    {
        SceneManager.LoadScene(x);
    }

    public void StartGame()
    {
        main.SetActive(true);
        SceneManager.LoadScene(1);
        PlayerPrefs.SetInt("BossStage", 1);
        PlayerPrefs.SetInt("PlayerHP", 100);
    }

    public void OpenSection(GameObject obj)
    {
        obj.SetActive(true);
    }

    public void CloseSection(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
