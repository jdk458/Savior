using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSelect : MonoBehaviour
{
    public UI2_Manager uI2_Manager;
    public Animator UI2_Ani;
    public PlayerController playerController;
    public Camera ui2_camera;

    [Header("SelectPannel")]
    public GameObject select01;
    public GameObject select02;

    int select_num01;
    int select_num02;

    bool select_flag;
    public void Update()
    {
        if (Input.GetMouseButtonUp(0) && !select_flag)
        {
            Vector2 mousePos = ui2_camera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 10);
            if (hit)
            {
                switch (hit.transform.name)
                {
                    case "캐릭터패시브":
                        select_num01 = 1;
                        hit.transform.GetComponent<Animator>().SetBool("click", true);
                        StartCoroutine(Ani_Coroutine("SkillSelect01_FadeOut", .9F, Select01Btn));
                        break;
                    case "공격패시브 ":
                        select_num01 = 2;
                        hit.transform.GetComponent<Animator>().SetBool("click", true);
                        StartCoroutine(Ani_Coroutine("SkillSelect01_FadeOut", .9F, Select01Btn));
                        break;
                    case "스킬패시브":
                        select_num01 = 3;
                        hit.transform.GetComponent<Animator>().SetBool("click", true);
                        StartCoroutine(Ani_Coroutine("SkillSelect01_FadeOut", .9F, Select01Btn));
                        break;
                    case "능력치선택1":
                        select_num02 = 1;
                        hit.transform.GetComponent<Animator>().SetBool("click", true);
                        Invoke("Select02Btn", 0.35f);
                        break;
                    case "능력치선택2":
                        select_num02 = 2;
                        hit.transform.GetComponent<Animator>().SetBool("click", true);
                        Invoke("Select02Btn", 0.35f);
                        break;
                    case "능력치선택3":
                        select_num02 = 3;
                        hit.transform.GetComponent<Animator>().SetBool("click", true);
                        Invoke("Select02Btn", 0.35f);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    IEnumerator Ani_Coroutine(string ani, float time, System.Action Callback = null)
    {
        select_flag = true;
        UI2_Ani.SetBool(ani, true);
        yield return new WaitForSeconds(time);
        UI2_Ani.SetBool(ani, false);
        select_flag = false;
        if (Callback != null)
        {
            Callback();
        }
    }

    void Select01Btn()
    {
        switch (select_num01)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
        }
        select01.SetActive(false);
        select02.SetActive(true);
        StartCoroutine(Ani_Coroutine("SkillSelect02_FadeIn", 1));
    }

    void Select02Btn()
    {
        switch (select_num01)
        {
            case 1:
                switch (select_num02)
                {
                    case 1:
                        playerController.character_lv_hp++;
                        break;
                    case 2:
                        playerController.character_lv_speed++;
                        break;
                    case 3:
                        playerController.character_lv_exp++;
                        break;
                }
                break;
            case 2:
                switch (select_num02)
                {
                    case 1:
                        playerController.attack_lv_atk++;
                        break;
                    case 2:
                        playerController.attack_lv_speed++;
                        break;
                    case 3:
                        playerController.attack_lv_count++;
                        break;
                }
                break;
            case 3:
                switch (select_num02)
                {
                    case 1:
                        playerController.skill_lv_atk++;
                        break;
                    case 2:
                        playerController.skill_lv_cooltime++;
                        break;
                    case 3:
                        playerController.skill_lv_getcount++;
                        break;
                }
                break;
        }
        uI2_Manager.SkillSelect_Exit();
    }
}
