﻿using System.Collections;
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
    public GameObject[] fireSkill;
    public GameObject[] lightSkill;

    [Header("현재 소지한 스킬")]
    public List<String> player_skill = new List<string>();


    private void Start()
    {
        origin_nomal_atk_Pos = nomal_atk.transform.position;
        Attack();

    }
    Vector2 origin_nomal_atk_Pos;
    void Attack()
    {
        if (TimeManager.instance.GetTime())
        {
            CancelInvoke("Attack");
            Invoke("Attack", 0.1f);
            return;
        }

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
                Transform tempEnemy_transform = this.transform;

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
                    enemies = Physics2D.CircleCastAll(temp_list[i - 1].position, 1.5f, Vector2.zero);

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

        CancelInvoke("Attack");
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



    public void Get(string skillname)
    {
        Invoke(skillname,0);
        player_skill.Add(skillname);
    }

    bool water01_flag;
    public void Water01()
    {
        if (TimeManager.instance.GetTime())
        {
            Invoke("Water01", 0.1f);
            return;
        }

        if (!water01_flag)
        {
            StartCoroutine(Water01_Flag_Coroutine());

            List<float> enemy_distance_list = new List<float>();
            RaycastHit2D[] enemies = Physics2D.CircleCastAll(this.transform.position, waterSkill[1].GetComponent<ActiveSkill>().range, Vector2.zero);

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
                List<GameObject> enemy = new List<GameObject>();
                enemy.Add(enemy_list[enemy_index].transform.gameObject);
                water00_obj.GetComponent<ActiveSkill>().SkillCall(enemy, (int)(GetComponent<PlayerController>().skill_lv_atk * waterSkill[0].GetComponent<ActiveSkill>().lvOfDamage));
            }

        }

        Invoke("Water01", 0.5f);
    }
    IEnumerator Water01_Flag_Coroutine()
    {
        water01_flag = true;
        yield return new WaitForSeconds(waterSkill[0].GetComponent<ActiveSkill>().colltime);
        water01_flag = false;
    }

    bool water02_flag;
    public void Water02()
    {
        if (TimeManager.instance.GetTime())
        {
            Invoke("Water02", 0.1f);
            return;
        }

        if (!water02_flag)
        {
            StartCoroutine(Water02_Flag_Coroutine());

            List<float> enemy_distance_list = new List<float>();
            RaycastHit2D[] enemies = Physics2D.CircleCastAll(this.transform.position, waterSkill[1].GetComponent<ActiveSkill>().range , Vector2.zero);

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

                GameObject skillAactive = Instantiate(waterSkill[1], target_pos, Quaternion.identity);
                List<GameObject> enemy = new List<GameObject>();
                enemy.Add(enemy_list[enemy_index].transform.gameObject);
                skillAactive.GetComponent<ActiveSkill>().SkillCall(enemy, (int)(GetComponent<PlayerController>().skill_lv_atk * waterSkill[1].GetComponent<ActiveSkill>().lvOfDamage));
            }

        }

        Invoke("Water02", 0.5f);
    }
    IEnumerator Water02_Flag_Coroutine()
    {
        water02_flag = true;
        yield return new WaitForSeconds(waterSkill[1].GetComponent<ActiveSkill>().colltime);
        water02_flag = false;
    }

    bool water03_flag;
    public void Water03()
    {
        if (TimeManager.instance.GetTime())
        {
            Invoke("Water03", 0.1f);
            return;
        }

        if (!water03_flag)
        {
            StartCoroutine(Water03_Flag_Coroutine());

            StartCoroutine(Water03_Coroutine());
        }

        Invoke("Water03", 0.5f);
    }
    IEnumerator Water03_Flag_Coroutine()
    {
        water03_flag = true;
        yield return new WaitForSeconds(waterSkill[2].GetComponent<ActiveSkill>().colltime);
        water03_flag = false;
    }

    IEnumerator Water03_Coroutine()
    {
        float time = 0.3f;

        float width = 1;
        float height = 1;

        float length = 5;

        Vector2 currentPos = this.transform.position;

        Instantiate(waterSkill[2], currentPos, Quaternion.identity).GetComponent<ActiveSkill>().playerInfo = this.GetComponent<PlayerController>();

        yield return new WaitForSeconds(time);

        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                switch (j)
                {
                    case 0: // 오른쪽
                        Instantiate(waterSkill[2], new Vector2(currentPos.x + (width * i) , currentPos.y ), Quaternion.identity).GetComponent<ActiveSkill>().playerInfo = this.GetComponent<PlayerController>();
                        break;
                    case 1: // 왼쪽
                        Instantiate(waterSkill[2], new Vector2(currentPos.x - (width * i), currentPos.y), Quaternion.identity).GetComponent<ActiveSkill>().playerInfo = this.GetComponent<PlayerController>();
                        break;
                    case 2: // 위
                        Instantiate(waterSkill[2], new Vector2(currentPos.x, currentPos.y + (height * i)), Quaternion.identity).GetComponent<ActiveSkill>().playerInfo = this.GetComponent<PlayerController>();
                        break;
                    case 3: // 아래
                        Instantiate(waterSkill[2], new Vector2(currentPos.x, currentPos.y - (height * i)), Quaternion.identity).GetComponent<ActiveSkill>().playerInfo = this.GetComponent<PlayerController>();
                        break;
                }
            }
            yield return new WaitForSeconds(time);
        }
      
    }


    bool water04_flag;
    public void Water04()
    {
        if (TimeManager.instance.GetTime())
        {
            Invoke("Water04", 0.1f);
            return;
        }

        if (!water04_flag)
        {
            StartCoroutine(Water04_Flag_Coroutine());

            List<float> enemy_distance_list = new List<float>();
            RaycastHit2D[] enemies = Physics2D.CircleCastAll(this.transform.position, waterSkill[3].GetComponent<ActiveSkill>().range, Vector2.zero);

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

                GameObject skillAactive = Instantiate(waterSkill[3], target_pos, Quaternion.identity);
                skillAactive.GetComponent<ActiveSkill>().playerInfo = this.GetComponent<PlayerController>();
            }

        }

        Invoke("Water04", 0.5f);
    }
    IEnumerator Water04_Flag_Coroutine()
    {
        water04_flag = true;
        yield return new WaitForSeconds(waterSkill[3].GetComponent<ActiveSkill>().colltime);
        water04_flag = false;
    }

    bool water05_flag;
    public void Water05()
    {
        if (TimeManager.instance.GetTime())
        {
            Invoke("Water05", 0.1f);
            return;
        }

        RaycastHit2D[] enemies = Physics2D.BoxCastAll(this.transform.position, new Vector2(16,9), 0,Vector2.zero);

        List<GameObject> enemy_list = new List<GameObject>();
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].transform.gameObject.tag.Contains("Enemy"))
            {
                enemy_list.Add(enemies[i].transform.gameObject);
            }
        }

        if (!water05_flag)
        {
            StartCoroutine(Water05_Flag_Coroutine());

            GameObject activeSkill = Instantiate(waterSkill[4], this.transform.position, Quaternion.identity);
            activeSkill.GetComponent<ActiveSkill>().UpdatePosSet(GetComponent<PlayerController>().theCam);
            if (enemy_list.Count > 0)
                activeSkill.GetComponent<ActiveSkill>().SkillCall(enemy_list, (int)(GetComponent<PlayerController>().skill_lv_atk * waterSkill[4].GetComponent<ActiveSkill>().lvOfDamage));
        }

        Invoke("Water05", 0.5f);
    }
    IEnumerator Water05_Flag_Coroutine()
    {
        water05_flag = true;
        yield return new WaitForSeconds(waterSkill[4].GetComponent<ActiveSkill>().colltime);
        water05_flag = false;
    }

    bool fire01_flag;
    public void Fire01()
    {
        if (TimeManager.instance.GetTime())
        {
            Invoke("Fire01", 0.1f);
            return;
        }

        if (!fire01_flag)
        {
            StartCoroutine(Fire01_Flag_Coroutine());

            List<float> enemy_distance_list = new List<float>();
            RaycastHit2D[] enemies = Physics2D.CircleCastAll(this.transform.position, fireSkill[1].GetComponent<ActiveSkill>().range, Vector2.zero);

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

                GameObject fire00_obj = Instantiate(fireSkill[0], target_pos, Quaternion.identity);
                List<GameObject> enemy = new List<GameObject>();
                enemy.Add(enemy_list[enemy_index].transform.gameObject);
                fire00_obj.GetComponent<ActiveSkill>().SkillCall(enemy, (int)(GetComponent<PlayerController>().skill_lv_atk * fireSkill[0].GetComponent<ActiveSkill>().lvOfDamage));
            }

        }

        Invoke("Fire01", 0.5f);
    }
    IEnumerator Fire01_Flag_Coroutine()
    {
        fire01_flag = true;
        yield return new WaitForSeconds(fireSkill[0].GetComponent<ActiveSkill>().colltime);
        fire01_flag = false;
    }

    bool fire02_flag;
    public void Fire02()
    {
        if (TimeManager.instance.GetTime())
        {
            Invoke("Fire02", 0.1f);
            return;
        }

        if (!fire02_flag)
        {
            StartCoroutine(Fire02_Flag_Coroutine());

            List<float> enemy_distance_list = new List<float>();
            RaycastHit2D[] enemies = Physics2D.CircleCastAll(this.transform.position, fireSkill[1].GetComponent<ActiveSkill>().range, Vector2.zero);

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

                GameObject skillAactive = Instantiate(fireSkill[1], target_pos, Quaternion.identity);
                List<GameObject> enemy = new List<GameObject>();
                enemy.Add(enemy_list[enemy_index].transform.gameObject);
                skillAactive.GetComponent<ActiveSkill>().SkillCall(enemy, (int)(GetComponent<PlayerController>().skill_lv_atk * fireSkill[1].GetComponent<ActiveSkill>().lvOfDamage));
            }

        }

        Invoke("Fire02", 0.5f);
    }
    IEnumerator Fire02_Flag_Coroutine()
    {
        fire02_flag = true;
        yield return new WaitForSeconds(fireSkill[1].GetComponent<ActiveSkill>().colltime);
        fire02_flag = false;
    }

    bool fire03_flag;
    public void Fire03()
    {
        if (TimeManager.instance.GetTime())
        {
            Invoke("Fire03", 0.1f);
            return;
        }

        if (!fire03_flag)
        {
            StartCoroutine(Fire03_Flag_Coroutine());

            StartCoroutine(Fire03_Coroutine());
        }

        Invoke("Fire03", 0.5f);
    }
    IEnumerator Fire03_Flag_Coroutine()
    {
        fire03_flag = true;
        yield return new WaitForSeconds(fireSkill[2].GetComponent<ActiveSkill>().colltime);
        fire03_flag = false;
    }

    IEnumerator Fire03_Coroutine()
    {
        float time = 0.3f;

        float width = 1;
        float height = 1;

        float length = 5;

        Vector2 currentPos = this.transform.position;

        Instantiate(fireSkill[2], currentPos, Quaternion.identity).GetComponent<ActiveSkill>().playerInfo = this.GetComponent<PlayerController>();

        yield return new WaitForSeconds(time);

        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                switch (j)
                {
                    case 0: // 오른쪽
                        Instantiate(fireSkill[2], new Vector2(currentPos.x + (width * i), currentPos.y), Quaternion.identity).GetComponent<ActiveSkill>().playerInfo = this.GetComponent<PlayerController>();
                        break;
                    case 1: // 왼쪽
                        Instantiate(fireSkill[2], new Vector2(currentPos.x - (width * i), currentPos.y), Quaternion.identity).GetComponent<ActiveSkill>().playerInfo = this.GetComponent<PlayerController>();
                        break;
                    case 2: // 위
                        Instantiate(fireSkill[2], new Vector2(currentPos.x, currentPos.y + (height * i)), Quaternion.identity).GetComponent<ActiveSkill>().playerInfo = this.GetComponent<PlayerController>();
                        break;
                    case 3: // 아래
                        Instantiate(fireSkill[2], new Vector2(currentPos.x, currentPos.y - (height * i)), Quaternion.identity).GetComponent<ActiveSkill>().playerInfo = this.GetComponent<PlayerController>();
                        break;
                }
            }
            yield return new WaitForSeconds(time);
        }

    }


    bool fire04_flag;
    public void Fire04()
    {
        if (TimeManager.instance.GetTime())
        {
            Invoke("Fire04", 0.1f);
            return;
        }

        if (!fire04_flag)
        {
            StartCoroutine(Fire04_Flag_Coroutine());

            List<float> enemy_distance_list = new List<float>();
            RaycastHit2D[] enemies = Physics2D.CircleCastAll(this.transform.position, fireSkill[3].GetComponent<ActiveSkill>().range, Vector2.zero);

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

                GameObject skillAactive = Instantiate(fireSkill[3], target_pos, Quaternion.identity);
                skillAactive.GetComponent<ActiveSkill>().playerInfo = this.GetComponent<PlayerController>();
            }

        }

        Invoke("Fire04", 0.5f);
    }
    IEnumerator Fire04_Flag_Coroutine()
    {
        fire04_flag = true;
        yield return new WaitForSeconds(fireSkill[3].GetComponent<ActiveSkill>().colltime);
        fire04_flag = false;
    }

    bool fire05_flag;
    public void Fire05()
    {
        if (TimeManager.instance.GetTime())
        {
            Invoke("Fire05", 0.1f);
            return;
        }

        RaycastHit2D[] enemies = Physics2D.BoxCastAll(this.transform.position, new Vector2(16, 9), 0, Vector2.zero);

        List<GameObject> enemy_list = new List<GameObject>();
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].transform.gameObject.tag.Contains("Enemy"))
            {
                enemy_list.Add(enemies[i].transform.gameObject);
            }
        }

        if (!fire05_flag)
        {
            StartCoroutine(Fire05_Flag_Coroutine());

            GameObject activeSkill = Instantiate(fireSkill[4], this.transform.position, Quaternion.identity);
            activeSkill.GetComponent<ActiveSkill>().UpdatePosSet(GetComponent<PlayerController>().theCam);
            if (enemy_list.Count > 0)
                activeSkill.GetComponent<ActiveSkill>().SkillCall(enemy_list, (int)(GetComponent<PlayerController>().skill_lv_atk * fireSkill[4].GetComponent<ActiveSkill>().lvOfDamage));
        }

        Invoke("Fire05", 0.5f);
    }
    IEnumerator Fire05_Flag_Coroutine()
    {
        fire05_flag = true;
        yield return new WaitForSeconds(fireSkill[4].GetComponent<ActiveSkill>().colltime);
        fire05_flag = false;
    }

    bool light01_flag;
    public void Light01()
    {
        if (TimeManager.instance.GetTime())
        {
            Invoke("Fire01", 0.1f);
            return;
        }

        if (!light01_flag)
        {
            StartCoroutine(Light01_Flag_Coroutine());

            List<float> enemy_distance_list = new List<float>();
            RaycastHit2D[] enemies = Physics2D.CircleCastAll(this.transform.position, lightSkill[1].GetComponent<ActiveSkill>().range, Vector2.zero);

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

                GameObject light00_obj = Instantiate(lightSkill[0], target_pos, Quaternion.identity);
                List<GameObject> enemy = new List<GameObject>();
                enemy.Add(enemy_list[enemy_index].transform.gameObject);
                light00_obj.GetComponent<ActiveSkill>().SkillCall(enemy, (int)(GetComponent<PlayerController>().skill_lv_atk * lightSkill[0].GetComponent<ActiveSkill>().lvOfDamage));
            }

        }

        Invoke("Light01", 0.5f);
    }
    IEnumerator Light01_Flag_Coroutine()
    {
        light01_flag = true;
        yield return new WaitForSeconds(lightSkill[0].GetComponent<ActiveSkill>().colltime);
        light01_flag = false;
    }

    bool light02_flag;
    public void Light02()
    {
        if (TimeManager.instance.GetTime())
        {
            Invoke("Light02", 0.1f);
            return;
        }

        if (!light02_flag)
        {
            StartCoroutine(Light02_Flag_Coroutine());

            List<float> enemy_distance_list = new List<float>();
            RaycastHit2D[] enemies = Physics2D.CircleCastAll(this.transform.position, lightSkill[1].GetComponent<ActiveSkill>().range, Vector2.zero);

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

                GameObject skillAactive = Instantiate(lightSkill[1], target_pos, Quaternion.identity);
                List<GameObject> enemy = new List<GameObject>();
                enemy.Add(enemy_list[enemy_index].transform.gameObject);
                skillAactive.GetComponent<ActiveSkill>().SkillCall(enemy, (int)(GetComponent<PlayerController>().skill_lv_atk * lightSkill[1].GetComponent<ActiveSkill>().lvOfDamage));
            }

        }

        Invoke("Light02", 0.5f);
    }
    IEnumerator Light02_Flag_Coroutine()
    {
        light02_flag = true;
        yield return new WaitForSeconds(lightSkill[1].GetComponent<ActiveSkill>().colltime);
        light02_flag = false;
    }

    bool light03_flag;
    public void Light03()
    {
        if (TimeManager.instance.GetTime())
        {
            Invoke("Light03", 0.1f);
            return;
        }

        if (!light03_flag)
        {
            StartCoroutine(Light03_Flag_Coroutine());

            StartCoroutine(Light03_Coroutine());
        }

        Invoke("Light03", 0.5f);
    }
    IEnumerator Light03_Flag_Coroutine()
    {
        light03_flag = true;
        yield return new WaitForSeconds(lightSkill[2].GetComponent<ActiveSkill>().colltime);
        light03_flag = false;
    }

    IEnumerator Light03_Coroutine()
    {
        float time = 0.3f;

        float width = 1;
        float height = 1;

        float length = 5;

        Vector2 currentPos = this.transform.position;

        Instantiate(lightSkill[2], currentPos, Quaternion.identity).GetComponent<ActiveSkill>().playerInfo = this.GetComponent<PlayerController>();

        yield return new WaitForSeconds(time);

        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                switch (j)
                {
                    case 0: // 오른쪽
                        Instantiate(lightSkill[2], new Vector2(currentPos.x + (width * i), currentPos.y), Quaternion.identity).GetComponent<ActiveSkill>().playerInfo = this.GetComponent<PlayerController>();
                        break;
                    case 1: // 왼쪽
                        Instantiate(lightSkill[2], new Vector2(currentPos.x - (width * i), currentPos.y), Quaternion.identity).GetComponent<ActiveSkill>().playerInfo = this.GetComponent<PlayerController>();
                        break;
                    case 2: // 위
                        Instantiate(lightSkill[2], new Vector2(currentPos.x, currentPos.y + (height * i)), Quaternion.identity).GetComponent<ActiveSkill>().playerInfo = this.GetComponent<PlayerController>();
                        break;
                    case 3: // 아래
                        Instantiate(lightSkill[2], new Vector2(currentPos.x, currentPos.y - (height * i)), Quaternion.identity).GetComponent<ActiveSkill>().playerInfo = this.GetComponent<PlayerController>();
                        break;
                }
            }
            yield return new WaitForSeconds(time);
        }

    }


    bool light04_flag;
    public void Light04()
    {
        if (TimeManager.instance.GetTime())
        {
            Invoke("Light04", 0.1f);
            return;
        }

        if (!light04_flag)
        {
            StartCoroutine(Light04_Flag_Coroutine());

            List<float> enemy_distance_list = new List<float>();
            RaycastHit2D[] enemies = Physics2D.CircleCastAll(this.transform.position, lightSkill[3].GetComponent<ActiveSkill>().range, Vector2.zero);

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

                GameObject skillAactive = Instantiate(lightSkill[3], target_pos, Quaternion.identity);
                skillAactive.GetComponent<ActiveSkill>().playerInfo = this.GetComponent<PlayerController>();
            }

        }

        Invoke("Light04", 0.5f);
    }
    IEnumerator Light04_Flag_Coroutine()
    {
        light04_flag = true;
        yield return new WaitForSeconds(lightSkill[3].GetComponent<ActiveSkill>().colltime);
        light04_flag = false;
    }

    bool light05_flag;
    public void Light05()
    {
        if (TimeManager.instance.GetTime())
        {
            Invoke("Light05", 0.1f);
            return;
        }

        RaycastHit2D[] enemies = Physics2D.BoxCastAll(this.transform.position, new Vector2(16, 9), 0, Vector2.zero);

        List<GameObject> enemy_list = new List<GameObject>();
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].transform.gameObject.tag.Contains("Enemy"))
            {
                enemy_list.Add(enemies[i].transform.gameObject);
            }
        }

        if (!light05_flag)
        {
            StartCoroutine(Light05_Flag_Coroutine());

            GameObject activeSkill = Instantiate(lightSkill[4], this.transform.position, Quaternion.identity);
            activeSkill.GetComponent<ActiveSkill>().UpdatePosSet(GetComponent<PlayerController>().theCam);
            if (enemy_list.Count > 0)
                activeSkill.GetComponent<ActiveSkill>().SkillCall(enemy_list, (int)(GetComponent<PlayerController>().skill_lv_atk * lightSkill[4].GetComponent<ActiveSkill>().lvOfDamage));
        }

        Invoke("Light05", 0.5f);
    }
    IEnumerator Light05_Flag_Coroutine()
    {
        light05_flag = true;
        yield return new WaitForSeconds(lightSkill[4].GetComponent<ActiveSkill>().colltime);
        light05_flag = false;
    }

}
