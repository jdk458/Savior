using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_Manager : MonoBehaviour
{
    public Transform player;

    [Header("초기 스폰 타임")]
    public float spawn_time = 3f;

    SpriteRenderer render;

    GameObject obstacle;

    private void Start()
    {
        render = GetComponent<SpriteRenderer>();
        obstacle_Spawn();
    }

    void obstacle_Spawn()
    {
        obstacle = ObjectPoolingManager.instance.GetQueue(ObjectKind.obstacle);
        float X = Random.Range(player.position.x-70, player.position.x + 70);
        float Y = Random.Range(player.position.y - 70, player.position.y + 70);
        obstacle.transform.position = new Vector3(X, Y, 0);
        Invoke("obstacle_Spawn", spawn_time);
    }
}
