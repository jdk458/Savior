using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Manager : MonoBehaviour
{
    public Transform player;

    [Header("초기 스폰 타임")]
    public float spawn_time = 3f;
    [Header("생성 범위")]
    public float range = 50f;

    GameObject exp_marble;
    GameObject hp_marble;
    public GameObject treasure_obj;

    private void Start()
    {
        exp_marble_Spawn();
        hp_marble_Spawn();
        Treasure_Spawn();
    }

    void exp_marble_Spawn()
    {
        float marble_num = Random.value;
        ObjectKind marble_type = ObjectKind.exp_marble_small;
        if (marble_num < 0.1f)
            marble_type = ObjectKind.exp_marble_large;
        else if (marble_num < 0.3f)
            marble_type = ObjectKind.exp_marble_middle;
        else
            marble_type = ObjectKind.exp_marble_small;

        exp_marble = ObjectPoolingManager.instance.GetQueue(marble_type);
        exp_marble.GetComponent<Item>().player = player;
        float X = Random.Range(player.position.x - range, player.position.x + range);
        float Y = Random.Range(player.position.y - range, player.position.y + range);
        exp_marble.transform.position = new Vector3(X,Y,0);
        Invoke("exp_marble_Spawn", spawn_time);
    }

    void hp_marble_Spawn()
    {
        hp_marble = ObjectPoolingManager.instance.GetQueue(ObjectKind.hp_marble);
        hp_marble.GetComponent<Item>().player = player;
        float X = Random.Range(player.position.x - range, player.position.x + range);
        float Y = Random.Range(player.position.y - range, player.position.y + range);
        hp_marble.transform.position = new Vector3(X, Y, 0);
        Invoke("hp_marble_Spawn", spawn_time);
    }

    void Treasure_Spawn()
    {
        float X = Random.Range(player.position.x - range, player.position.x + range);
        float Y = Random.Range(player.position.y - range, player.position.y + range);
        while ((Mathf.Abs(player.position.x - X) < 20 && Mathf.Abs(player.position.y - Y) < 20))
        {
            X = Random.Range(player.position.x - range, player.position.x + range);
            Y = Random.Range(player.position.y - range, player.position.y + range);
        }
        Instantiate(treasure_obj, new Vector3(X, Y, 0), Quaternion.identity);
        Invoke("Treasure_Spawn", spawn_time);
    }

    public void NextStage()
    {
        RaycastHit2D[] hit_list = Physics2D.CircleCastAll(player.position, 100, Vector2.zero);

        for (int i = 0; i < hit_list.Length; i++)
        {
            if (hit_list[i] && hit_list[i].transform.tag.Contains("Treasure"))
            {
                Destroy(hit_list[i].transform.gameObject);
            }
            if (hit_list[i] && hit_list[i].transform.name.Contains("exp_marble_large"))
            {
                ObjectPoolingManager.instance.InsertQueue(hit_list[i].transform.gameObject, ObjectKind.exp_marble_large);
            }
            if (hit_list[i] && hit_list[i].transform.name.Contains("exp_marble_middle"))
            {
                ObjectPoolingManager.instance.InsertQueue(hit_list[i].transform.gameObject, ObjectKind.exp_marble_middle);
            }
            if (hit_list[i] && hit_list[i].transform.name.Contains("exp_marble_small"))
            {
                ObjectPoolingManager.instance.InsertQueue(hit_list[i].transform.gameObject, ObjectKind.exp_marble_small);
            }
            if (hit_list[i] && hit_list[i].transform.name.Contains("hp"))
            {
                ObjectPoolingManager.instance.InsertQueue(hit_list[i].transform.gameObject, ObjectKind.hp_marble);
            }
        }
    }
}
