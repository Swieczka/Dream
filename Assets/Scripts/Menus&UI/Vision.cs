using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Vision : MonoBehaviour
{
    public bool ChangeScene;
    public MenuManager menumanager;
    public bool CanChangeScene;
    public GameObject DialogBox;
    public TextMeshProUGUI DialogBoxText;
    [TextArea]
    public string[] Dialouges;
    public int DialoguesLength;
    public int nextSceneIndex;
    void Start()
    {
        DialoguesLength = Dialouges.Length;
       DialogBox.SetActive(false);
        CanChangeScene = false;
        StartCoroutine(VisionTimer(3f));
        StartCoroutine(VisionDialogue(3f));
    }

    void Update()
    {
    }

    IEnumerator VisionDialogue(float time)
    {
        for(int i=0;i<DialoguesLength;i++)
        {
            yield return new WaitForSeconds(time);
            DialogBoxText.text = Dialouges[i];
        }
        yield return new WaitForSeconds(time);
        if (!ChangeScene)
        {
            menumanager.ChangeScene(nextSceneIndex);
        }
        else if(ChangeScene)
        {
            yield return new WaitForSeconds(time);
            DialogBox.SetActive(false);
        }
    }

    IEnumerator VisionTimer(float time)
    {
        yield return new WaitForSeconds(time);
        DialogBox.SetActive(true);
    }
}
