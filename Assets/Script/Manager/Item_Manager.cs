using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Manager : MonoBehaviour
{
    [Header("구슬 스폰")]
    public Transform item_spawn;

    public Transform player;

    [Header("초기 스폰 타임")]
    public float spawn_time = 3f;

    SpriteRenderer render;

    GameObject exp_marble;
    GameObject hp_marble;
    GameObject skill_marble;

    //나중에 itemtype테이블에서 값읽기
    int rand_lv_num;

    private void Start()
    {
        render = GetComponent<SpriteRenderer>();
        exp_marble_Spawn();
        hp_marble_Spawn();
    }

    void exp_marble_Spawn()
    {
        exp_marble = ObjectPoolingManager.instance.GetQueue(ObjectKind.exp_marble);
        exp_marble.GetComponent<Item>().player = player;
        int rand_pos_num = Random.RandomRange(0, item_spawn.childCount);
        rand_lv_num = Random.RandomRange(1, 4);
        exp_marble.transform.position = item_spawn.GetChild(rand_pos_num).transform.position;
        Invoke("exp_marble_Spawn", spawn_time);
    }

    void hp_marble_Spawn()
    {
        hp_marble = ObjectPoolingManager.instance.GetQueue(ObjectKind.hp_marble);
        hp_marble.GetComponent<Item>().player = player;
        int rand_pos_num = Random.RandomRange(0, item_spawn.childCount);
        rand_lv_num = Random.RandomRange(1, 4);
        hp_marble.transform.position = item_spawn.GetChild(rand_pos_num).transform.position;
        Invoke("hp_marble_Spawn", spawn_time);
    }
}
