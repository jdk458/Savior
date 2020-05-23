﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ob_nomal : MonoBehaviour
{
    [Header("오브")]
    [Range(15, 30)] public float speed = 15;
    [Range(0, 10)] public float atk = 1;

    [HideInInspector]
    public PlayerController player_status;
    [HideInInspector]
    public List<Transform> targetList = new List<Transform>();
    int iCount;

    private void FixedUpdate()
    {
        if (targetList.Count < 1)
        {
            Destroy();
            return;
        }


        this.transform.position = Vector2.MoveTowards(this.transform.position, targetList[iCount].position, 0.3f);
        if (Vector2.Distance(transform.position, targetList[iCount].position) == 0)
        {
            targetList[iCount].GetComponent<EnemyController>().Hit((int)(player_status.atk * atk));
            iCount++;
            if (iCount == targetList.Count)
            {
                iCount = 0;
                Destroy();
            }
        }
    }

  

    private void OnEnable()
    {
        Invoke("Destroy", 10);
    }

    void Destroy()
    {
        ObjectPoolingManager.instance.InsertQueue(this.gameObject, ObjectKind.ob);
    }
}