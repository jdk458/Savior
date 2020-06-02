﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;
    public EnemyManager enemyManager;
    public Background_StageManager background_stageManager;
    public Item_Manager item_Manager;

    private void Awake()
    {
        instance = this;
    }

    public string currentStage;
    int currentStageInt = 1;

    public void NextStage()
    {
        TimeManager.instance.SetTime(true);

        currentStageInt++;
        if (currentStageInt == 6)
        {
            currentStage = "Boss";
        }
        else
        {
            currentStage = "Stage0" + currentStageInt;
            this.GetComponent<Animator>().SetBool("Next", true);
        }
    }

    public void NextStage_Start()
    {
        //장애물없애기, 배경 바꾸기, 몬스터 없애기, 몬스터 변경
        enemyManager.NextStage();
        background_stageManager.NextStage();
        item_Manager.NextStage();
    }

    public void NextStage_End()
    {
        TimeManager.instance.SetTime(false);
    }
}
