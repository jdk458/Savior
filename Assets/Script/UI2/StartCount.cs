using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartCount : MonoBehaviour
{
    public StageManager stageManager;
    public TimeManager timeManager;

    public Text Stage_num;
    public Text Count;

    private float time = 0f;

    private void Awake()
    {
        timeManager.SetTime(true);
        time = 0f;
        Stage_num.text = stageManager.currentStage;

    }
    private void Update()
    {
        time += Time.deltaTime;

        if (time >= 3.0f)
        {
            Count.text = "Start";
            gameObject.SetActive(false);
            timeManager.SetTime(false);
        }
        else if (time >= 2.0f)
            Count.text = "1";
        else if (time >= 1.0f)
            Count.text = "2";
        else
            Count.text = "3";
    }
}
