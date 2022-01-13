using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public void ChangeScene(int x)
    {
        SceneManager.LoadScene(x);
    }

    public void Options()
    {

    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
