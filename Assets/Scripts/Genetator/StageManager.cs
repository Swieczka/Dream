using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public int StageIndex;
    private void Awake()
    {
        PlayerPrefs.SetInt("BossStage", StageIndex);
    }

    void Update()
    {
        
    }
}
