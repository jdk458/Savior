﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [Header("STATUS")]
    [Range(1, 10)] public float atk;
    [Range(3, 10)] public float range;
    [Range(0.5f, 3f)] public float atkspeed;

    /// <summary>
    /// 
    /// </summary>

    [Header("카메라")]
    public Transform theCam;
    [Header("조이스틱")]
    public Transform joystic_foreground;
    Vector2 joystic_localpos;
    [Header("속도")]
    public float speed;
    public float dash_move;
    [Header("슬래쉬")]
    public GameObject slash;

    List<Transform> enemy_transform_list = new List<Transform>();

    bool isRun_flag;


    private void FixedUpdate()
    {
        joystic_localpos = joystic_foreground.GetComponent<RectTransform>().localPosition;

        if (joystic_localpos != Vector2.zero)
        {
            if (!dash_flag) // 대쉬중에는 막아둔다
            {
                // 조이스틱 위치에 따른 플레이어 위치 
                this.transform.position = new Vector2(
                this.transform.position.x + (joystic_localpos.x * Time.fixedDeltaTime * speed),
                this.transform.position.y + (joystic_localpos.y * Time.fixedDeltaTime * speed));
            }


            if (!isRun_flag)   // 달리는 동작 
            {
                isRun_flag = true;
                this.GetComponent<Animator>().SetBool("isRun", true);
            }

            if(joystic_localpos.x < 0)
            {
                this.transform.rotation = Quaternion.Euler(this.transform.rotation.x, 180, this.transform.rotation.z);
            }
            else
            {
                this.transform.rotation = Quaternion.Euler(this.transform.rotation.x, 0, this.transform.rotation.z);
            }

            // 카메라 플레이어 따라가기 
            theCam.position = new Vector3(this.transform.position.x, this.transform.position.y, theCam.position.z);
        }
        else
        {
            if (isRun_flag) // 달리는 동작 멈추기 
            {
                isRun_flag = false;
                this.GetComponent<Animator>().SetBool("isRun", false);
            }
        }

      
    }

    public void OnClick_Dash()
    {
        if (Mathf.Abs(joystic_localpos.x) < 45 && Mathf.Abs(joystic_localpos.y) < 45)
        {
            return;
        }
        StartCoroutine(DashCoroutine());
    }

    bool dash_flag;
    IEnumerator DashCoroutine()
    {
        dash_flag = true;

        if (joystic_localpos.x > 45)
            this.transform.DOMoveX(this.transform.position.x + dash_move, 0.1f).SetEase(Ease.Linear);
        if (joystic_localpos.x < -45)
            this.transform.DOMoveX(this.transform.position.x - dash_move, 0.1f).SetEase(Ease.Linear);
        if (joystic_localpos.y > 45)
            this.transform.DOMoveY(this.transform.position.y + dash_move, 0.1f).SetEase(Ease.Linear);
        if (joystic_localpos.y < -45)
            this.transform.DOMoveY(this.transform.position.y - dash_move, 0.1f).SetEase(Ease.Linear);


        for (int i = 0; i < 5; i++)
        {
            GameObject ghost_obj = ObjectPoolingManager.instance.GetQueue(ObjectKind.ghost);
            ghost_obj.transform.position = this.transform.position;

            if (joystic_localpos.x < 0)
                ghost_obj.transform.rotation = Quaternion.Euler(this.transform.rotation.x, 180, this.transform.rotation.z);
            else
                ghost_obj.transform.rotation = Quaternion.Euler(this.transform.rotation.x, 0, this.transform.rotation.z);

            yield return new WaitForSeconds(0.1f / 5);

        }

        dash_flag = false;
    }


}
