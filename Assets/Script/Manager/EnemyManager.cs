using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("플레이어 타겟")]
    public Transform player_transform;
    [Header("적 스폰")]
    public Transform enemy_spawn;
    [Header("초기 스폰 타임")]
    public float spawn_time = 1f;

    private void Start()
    {
        Spawn();
    }

    void Spawn()
    {
     //   GameObject enemy_obj = ObjectPoolingManager.instance.GetQueue(ObjectKind.enemy);
     //   enemy_obj.GetComponent<EnemyController>().player_transform = player_transform;
        int rand_pos_num = Random.RandomRange(0, enemy_spawn.childCount);
//        enemy_obj.transform.position = enemy_spawn.GetChild(rand_pos_num).transform.position;

        Invoke("Spawn", spawn_time);
       // spawn_time -= 0.01f;
    }

}
