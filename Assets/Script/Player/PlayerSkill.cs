using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PlayerSkill : MonoBehaviour
{
   


    private void Start()
    {
        Attack();
    }

    void Attack()
    {
        if (!atkspeed_flag)
        {
            StartCoroutine(Atkspeed_Flag_Coroutine());

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

                Vector3 dir = target_pos - this.transform.position;
                angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + -90f;

                GameObject ob = ObjectPoolingManager.instance.GetQueue(ObjectKind.ob);
                ob.transform.position = this.transform.position;
                ob.transform.rotation = Quaternion.Euler(0, 0, angle);
            }
           
        }

        Invoke("Attack", 0.1f);
    }

    bool atkspeed_flag;
    IEnumerator Atkspeed_Flag_Coroutine()
    {
        atkspeed_flag = true;
        yield return new WaitForSeconds(GetComponent<PlayerController>().atkspeed);
        atkspeed_flag = false;
    }
}
