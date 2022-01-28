using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
    public MenuManager menumanager;
    public bool CanChangeScene;
    void Start()
    {
        CanChangeScene = false;
        StartCoroutine(VisionTimer(5f));
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && CanChangeScene)
        {
            menumanager.NextScene();
        }
    }



    IEnumerator VisionTimer(float time)
    {
        Debug.Log("aa");
        yield return new WaitForSeconds(time);
        Debug.Log("bbb");
        CanChangeScene = true;
    }
}
