using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{
    public static ObjectPoolingManager instance;

    public Transform instantiate_pos;  
    public GameObject enemy_prefab = null;
    public Queue<GameObject> enemy_queue = new Queue<GameObject>();
    public GameObject ghost_prefab = null;
    public Queue<GameObject> ghost_queue = new Queue<GameObject>();
    public GameObject skill_prefab = null;
    public Queue<GameObject> skill_queue = new Queue<GameObject>();

    private void Awake()
    {
        instance = this;

        // enemy  
        for (int i = 0; i < 100; i++)
        {
            GameObject t_object = Instantiate(enemy_prefab, new Vector2(3000,3000), Quaternion.identity, instantiate_pos);
            enemy_queue.Enqueue(t_object);
            t_object.SetActive(false);
        }
        // ghost  
        for (int i = 0; i < 20; i++)
        {
            GameObject t_object = Instantiate(ghost_prefab, new Vector2(3000, 3000), Quaternion.identity, instantiate_pos);
            ghost_queue.Enqueue(t_object);
            t_object.SetActive(false);
        }
        // skill
        for (int i = 0; i < 40; i++)
        {
            GameObject t_object = Instantiate(skill_prefab, new Vector2(3000, 3000), Quaternion.identity, instantiate_pos);
            skill_queue.Enqueue(t_object);
            t_object.SetActive(false);
        }
    }

    // 사용한 오브젝트를 다시 큐에 집어 넣는 함수
    public void InsertQueue(GameObject p_object, ObjectKind obj)
    {
        if(obj == ObjectKind.enemy)
            enemy_queue.Enqueue(p_object);

        if (obj == ObjectKind.ghost)
            ghost_queue.Enqueue(p_object);

        if (obj == ObjectKind.skill)
            skill_queue.Enqueue(p_object);

        p_object.SetActive(false);
    }

    // 사용하기위해 큐에서 오브젝트를 꺼내오는 함수
    public GameObject GetQueue(ObjectKind obj)
    {
        GameObject t_object = null;

        if (obj == ObjectKind.enemy)
            t_object = enemy_queue.Dequeue();

        if (obj == ObjectKind.ghost)
            t_object = ghost_queue.Dequeue();

        if (obj == ObjectKind.skill)
            t_object = skill_queue.Dequeue();

        t_object.SetActive(true);
        return t_object;
    }
}

public enum ObjectKind
{
    enemy,
    ghost,
    skill
}
