using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;
    public EnemyManager enemyManager;

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
        //장애물 없애고, 배경바꾸고, 몬스터, 아이템 바꾸고, 타임 시작, 체력 유지, 경험치 유지
        enemyManager.NextStage();
    }

    public void NextStage_End()
    {
        TimeManager.instance.SetTime(false);
    }
}
