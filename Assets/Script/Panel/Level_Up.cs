using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Level_Up : MonoBehaviour
{
    [HideInInspector] public int select_num;

    public GameObject select1;
    public GameObject black_panel;
    public GameObject character_s;
    public GameObject attack;
    public GameObject skill;

    public GameObject select2;
    public GameObject character_s_back;
    public GameObject attack_back;
    public GameObject skill_back;

    public PlayerController player;

    bool click_flag;

    public void Panel_Pop()
    {
        black_panel.SetActive(true);
        select1.SetActive(true);
        select2.SetActive(false);
    }
    public void Onclick(int num)
    {
        if (click_flag) return;
        click_flag = true;
        select_num = num;
        character_s.GetComponent<Image>().DOFade(0,1.0f).OnComplete(()=> {
            select2.SetActive(true);
            character_s_back.GetComponent<Image>().DOFade(1, 1.0f);
            select1.SetActive(false);
        });
        attack.GetComponent<Image>().DOFade(0, 1.0f).OnComplete(() => {
            attack_back.GetComponent<Image>().DOFade(1, 1.0f);
        });
        skill.GetComponent<Image>().DOFade(0, 1.0f).OnComplete(() => {
            skill_back.GetComponent<Image>().DOFade(1, 1.0f);
        });
        
    }
    public void OnClick2(int num)
    {
        switch (select_num)
        {
            case 0:
                switch (num)
                {
                    case 0:
                        player.character_lv_speed++;
                        break;
                    case 1:
                        player.character_lv_exp++;
                        break;
                    case 2:
                        player.character_lv_hp++;
                        break;
                    default:
                        break;
                }
                break;
            case 1:
                switch (num)
                {
                    case 0:
                        player.attack_lv_atk++;
                        break;
                    case 1:
                        player.attack_lv_speed++;
                        break;
                    case 2:
                        player.attack_lv_count++;
                        break;
                    default:
                        break;
                }
                break;
            case 2:
                switch (num)
                {
                    case 0:
                        player.skill_lv_atk++;
                        break;
                    case 1:
                        player.skill_lv_cooltime++;
                        break;
                    case 2:
                        player.skill_lv_getcount++;
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
        character_s_back.GetComponent<Image>().DOFade(0, 1.0f);
        attack_back.GetComponent<Image>().DOFade(0, 1.0f);
        skill_back.GetComponent<Image>().DOFade(0, 1.0f).OnComplete(()=> { select2.SetActive(false); black_panel.SetActive(false); });
    }
}
