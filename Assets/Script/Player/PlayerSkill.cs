using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PlayerSkill : MonoBehaviour
{
    [Header("호롱불")]
    public GameObject nomal_atk;
    [Header("스킬 오브젝트")]
    public GameObject[] waterSkill;
    [Header("현재 소지한 스킬")]
    public List<String> player_skill = new List<string>();
    private void Start()
    {
        origin_nomal_atk_Pos = nomal_atk.transform.position;
        Attack();

    }
    Vector2 origin_nomal_atk_Pos;
    public int max_target = 3;
    void Attack()
    {
        if (!atkspeed_flag && nomal_atk.activeSelf)
        {
            StartCoroutine(Atkspeed_Flag_Coroutine());

            List<float> enemy_distance_list = new List<float>();
            RaycastHit2D[] enemies = Physics2D.CircleCastAll(this.transform.position, GetComponent<PlayerController>().range, Vector2.zero);
            List<RaycastHit2D> enemy_list = new List<RaycastHit2D>();

            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i].transform.gameObject.tag.Contains("Enemy"))
                {
                    enemy_list.Add(enemies[i]);
                }
            }
            if (enemy_list.Count > 0 )
            {
                GameObject ob = ObjectPoolingManager.instance.GetQueue(ObjectKind.ob);
                ob.transform.position = nomal_atk.transform.position;
                nomal_atk.SetActive(false);
                ob.GetComponent<Ob_nomal>().player_status = this.GetComponent<PlayerController>();
                Transform tmepEnemy_transform = this.transform;

                List<Transform> temp_list = new List<Transform>();
                for (int i = 0; i < enemy_list.Count; i++)
                {
                    if (enemy_list[i])
                    {
                        enemy_distance_list.Add(Vector2.Distance(this.transform.position, enemy_list[i].transform.position));
                    }
                }
                enemy_distance_list.Sort();
                for (int i = 0; i < enemy_distance_list.Count; i++)
                {
                    if (Vector2.Distance(this.transform.position, enemy_list[i].transform.position) ==
                      enemy_distance_list[0])
                    {
                        ob.GetComponent<Ob_nomal>().targetList.Add(enemy_list[i].transform);
                        temp_list.Add(enemy_list[i].transform);
                    }
                }

                for (int i = 1; i < this.GetComponent<PlayerController>().attack_lv_count; i++)
                {
                    enemy_distance_list.Clear();
                    enemy_list.Clear();
                    enemies = Physics2D.CircleCastAll(temp_list[i - 1].position, GetComponent<PlayerController>().range, Vector2.zero);

                    for (int j = 0; j < enemies.Length; j++)
                    {
                        if (enemies[j].transform.gameObject.tag.Contains("Enemy") && !temp_list.Contains(enemies[j].transform))
                        {
                            enemy_list.Add(enemies[j]);
                        }
                    }

                    if (enemy_list.Count < 1)
                        break;

                    for (int j = 0; j < enemy_list.Count; j++)
                    {
                        enemy_distance_list.Add(Vector2.Distance(temp_list[i - 1].position, enemy_list[j].transform.position));
                    }
                    enemy_distance_list.Sort();

                    for (int j = 0; j < enemy_distance_list.Count; j++)
                    {
                        if (Vector2.Distance(temp_list[i - 1].position, enemy_list[j].transform.position) ==
                          enemy_distance_list[0])
                        {
                            ob.GetComponent<Ob_nomal>().targetList.Add(enemy_list[j].transform);
                            temp_list.Add(enemy_list[j].transform);
                        }
                    }
                }
                
            }

        }

        Invoke("Attack", 0.1f);
    }

    bool atkspeed_flag;
    IEnumerator Atkspeed_Flag_Coroutine()
    {
        atkspeed_flag = true;
        yield return new WaitForSeconds(GetComponent<PlayerController>().atkspeed);
        nomal_atk.SetActive(true);
        nomal_atk.transform.localPosition = origin_nomal_atk_Pos;
        atkspeed_flag = false;
    }

    bool water00_flag;

    public void Get(string skillname)
    {
        //for (int i = 0; i < player_skill.Count; i++)
        //{
        //    if (player_skill[i].Contains(skillname))
        //        return;
        //}
        Invoke(skillname,0);
        player_skill.Add(skillname);

    }

    public void Water00()
    {
        if (!water00_flag)
        {
            StartCoroutine(Water00_Flag_Coroutine());

            List<float> enemy_distance_list = new List<float>();
            float angle = 0;

            RaycastHit2D[] enemies = Physics2D.CircleCastAll(this.transform.position, GetComponent<PlayerController>().range, Vector2.zero);

            List<RaycastHit2D> enemy_list = new List<RaycastHit2D>();
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i].transform.gameObject.tag.Contains("Enemy"))
                {
                    enemy_list.Add(enemies[i]);
                }
            }
            if (enemy_list.Count > 0)
            {
                for (int i = 0; i < enemy_list.Count; i++)
                {
                    if (enemy_list[i])
                    {
                        enemy_distance_list.Add(Vector2.Distance(this.transform.position, enemy_list[i].transform.position));
                    }
                }
                enemy_distance_list.Sort();
                int enemy_index = 0;
                for (int i = 0; i < enemy_list.Count; i++)
                {
                    if (Vector2.Distance(this.transform.position, enemy_list[i].transform.position) ==
                           enemy_distance_list[0])
                    {
                        enemy_index = i;
                    }
                }
                Vector3 target_pos = enemy_list[enemy_index].transform.position;

                GameObject water00_obj = Instantiate(waterSkill[0], target_pos, Quaternion.identity);
                water00_obj.GetComponent<ActiveSkill>().SkillCall(enemy_list[enemy_index].transform.gameObject, (int)GetComponent<PlayerController>().atk);
            }

        }

        Invoke("Water00", 0.1f);
    }
    IEnumerator Water00_Flag_Coroutine()
    {
        water00_flag = true;
        yield return new WaitForSeconds(waterSkill[0].GetComponent<ActiveSkill>().colltime);
        water00_flag = false;
    }
}
