using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{
    public static ObjectPoolingManager instance;

    public Transform instantiate_pos;  
    public GameObject enemy_prefab = null;
    public Queue<GameObject> enemy_queue = new Queue<GameObject>();
    public GameObject ob_prefab = null;
    public Queue<GameObject> ob_queue = new Queue<GameObject>();
    public GameObject ghost_prefab = null;
    public Queue<GameObject> ghost_queue = new Queue<GameObject>();
    public GameObject smoke_prefab = null;
    public Queue<GameObject> smoke_queue = new Queue<GameObject>();
    public GameObject nomal_damage_prefab = null;
    public Queue<GameObject> nomal_damage_queue = new Queue<GameObject>();
    public GameObject critical_damage_prefab = null;
    public Queue<GameObject> critical_damage_queue = new Queue<GameObject>();

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
        // ob  
        for (int i = 0; i < 100; i++)
        {
            GameObject t_object = Instantiate(ob_prefab, new Vector2(3000, 3000), Quaternion.identity, instantiate_pos);
            ob_queue.Enqueue(t_object);
            t_object.SetActive(false);
        }
        // ghost
        for (int i = 0; i < 20; i++)
        {
            GameObject t_object = Instantiate(ghost_prefab, new Vector2(3000, 3000), Quaternion.identity, instantiate_pos);
            ghost_queue.Enqueue(t_object);
            t_object.SetActive(false);
        }
        // smoke
        for (int i = 0; i < 20; i++)
        {
            GameObject t_object = Instantiate(smoke_prefab, new Vector2(3000, 3000), Quaternion.identity, instantiate_pos);
            smoke_queue.Enqueue(t_object);
            t_object.SetActive(false);
        }
        // nomal_damage
        for (int i = 0; i < 20; i++)
        {
            GameObject t_object = Instantiate(nomal_damage_prefab, new Vector2(3000, 3000), Quaternion.identity, instantiate_pos);
            nomal_damage_queue.Enqueue(t_object);
            t_object.SetActive(false);
        }
        // critical_damage
        for (int i = 0; i < 20; i++)
        {
            GameObject t_object = Instantiate(critical_damage_prefab, new Vector2(3000, 3000), Quaternion.identity, instantiate_pos);
            critical_damage_queue.Enqueue(t_object);
            t_object.SetActive(false);
        }
    }

    // 사용한 오브젝트를 다시 큐에 집어 넣는 함수
    public void InsertQueue(GameObject p_object, ObjectKind obj)
    {
        if(obj == ObjectKind.enemy)
            enemy_queue.Enqueue(p_object);

        if (obj == ObjectKind.ob)
            ob_queue.Enqueue(p_object);

        if (obj == ObjectKind.ghost)
            ghost_queue.Enqueue(p_object);

        if (obj == ObjectKind.smoke)
            smoke_queue.Enqueue(p_object);

        if (obj == ObjectKind.nomal_damage)
            nomal_damage_queue.Enqueue(p_object);

        if (obj == ObjectKind.critical_damage)
            critical_damage_queue.Enqueue(p_object);

        p_object.SetActive(false);
    }

    // 사용하기위해 큐에서 오브젝트를 꺼내오는 함수
    public GameObject GetQueue(ObjectKind obj)
    {
        GameObject t_object = null;

        if (obj == ObjectKind.enemy)
            t_object = enemy_queue.Dequeue();

        if (obj == ObjectKind.ob)
            t_object = ob_queue.Dequeue();

        if (obj == ObjectKind.ghost)
            t_object = ghost_queue.Dequeue();

        if (obj == ObjectKind.smoke)
            t_object = smoke_queue.Dequeue();

        if (obj == ObjectKind.nomal_damage)
            t_object = nomal_damage_queue.Dequeue();

        if (obj == ObjectKind.critical_damage)
            t_object = critical_damage_queue.Dequeue();

        t_object.SetActive(true);
        return t_object;
    }
}

public enum ObjectKind
{
    enemy,
    ob,
    ghost,
    smoke,
    nomal_damage,
    critical_damage
}
