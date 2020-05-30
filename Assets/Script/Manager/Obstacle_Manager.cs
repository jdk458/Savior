using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Obstacle_Manager : MonoBehaviour
{
    public Transform player;

    [Header("초기 스폰 타임")]
    public float spawn_time = 3f;

    int currentStage;

    SpriteRenderer render;

    GameObject obstacle;

    private void Start()
    {
        currentStage = 1;
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

    public void NextStage()
    {
        currentStage++;
        RaycastHit2D[] obstacle_hit_list = Physics2D.CircleCastAll(player.position, 100, Vector2.zero);

        for (int i = 0; i < obstacle_hit_list.Length; i++)
        {
            if (obstacle_hit_list[i] && obstacle_hit_list[i].transform.tag.Contains("Obstacle"))
            {
                ObjectPoolingManager.instance.InsertQueue(obstacle_hit_list[i].transform.gameObject, obstacle_hit_list[i].transform.GetComponent<Obstacle>().objectKind);
            }
        }
    }
}
