using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearchManager : MonoBehaviour
{
    [Header("circle")] public GameObject[] circle;
    [Header("line")] public GameObject[] line;

    public void CheckFillAmount()
    {
        bool fillAmount_flag = true;
        for (int i = 0; i < circle.Length; i++)
        {
            if (circle[i].GetComponent<Image>().fillAmount != 1)
            {
                fillAmount_flag = false;
                break;
            }
        }
        if (fillAmount_flag) // 모든 원이 채워졌을 때
        {
            circle[0].GetComponent<Image>().fillAmount = 0;
            circle[0].GetComponent<Research>().if_fill_flag = false;
            circle[0].GetComponent<Research>().circle_flag = false;
            for (int i = 1; i < circle.Length; i++)
            {
                circle[i].GetComponent<Research>().PointUpCircle();
                circle[i].transform.GetChild(0).GetComponent<Image>().DOFade(0, 2f).OnComplete(()=> { circle[0].GetComponent<Research>().if_fill_flag = true; });
                circle[i].GetComponent<Research>().if_fill_flag = false;
                circle[i].transform.GetComponent<Image>().fillAmount = 0;
            }
            for (int i = 0; i < line.Length; i++)
            {
                line[i].GetComponent<Image>().fillAmount = 0;
            }
            GameManager.instance.userinfo.research_level++;
            GameManager.instance.userinfo.hp_research = false;
            GameManager.instance.userinfo.atk_research = false;
            GameManager.instance.userinfo.atkspeed_research = false;
            GameManager.instance.userinfo.speed_research = false;
            GameManager.instance.userinfo.item_research = false;
            GameManager.instance.userinfo.shield_research = false;
            GameManager.instance.userinfo.recovery_research = false;
            GameManager.instance.userinfo.skilldamage_research = false;
            GameManager.instance.userinfo.exp_research = false;
            GameManager.instance.userinfo.point_research = false;
        }
    }
}
