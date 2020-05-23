using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{
    public static ObjectPoolingManager instance;

    [Header("아이템")]
    public Transform instantiate_pos;  
    public GameObject ob_prefab = null;
    public Queue<GameObject> ob_queue = new Queue<GameObject>();
    public GameObject smoke_prefab = null;
    public Queue<GameObject> smoke_queue = new Queue<GameObject>();
    public GameObject nomal_damage_prefab = null;
    public Queue<GameObject> nomal_damage_queue = new Queue<GameObject>();
    public GameObject critical_damage_prefab = null;
    public Queue<GameObject> critical_damage_queue = new Queue<GameObject>();
    public GameObject exp_marble_prefab = null;
    public Queue<GameObject> exp_marble_queue = new Queue<GameObject>();
    public GameObject hp_marble_prefab = null;
    public Queue<GameObject> hp_marble_queue = new Queue<GameObject>();
    public GameObject skill_marble_prefab = null;
    public Queue<GameObject> skill_marble_queue = new Queue<GameObject>();  
  
    [Header("캐릭터대쉬")]
    public GameObject idle_down = null;
    public Queue<GameObject> idle_queue = new Queue<GameObject>();
    public GameObject down_ver = null;
    public Queue<GameObject> down_ver_queue = new Queue<GameObject>();
    public GameObject ver = null;
    public Queue<GameObject> ver_queue = new Queue<GameObject>();
    public GameObject up = null;
    public Queue<GameObject> up_queue = new Queue<GameObject>();
    public GameObject up_ver = null;
    public Queue<GameObject> up_ver_queue = new Queue<GameObject>();

    [Header("몬스터")]
    public GameObject monster_stage01_01_pripab = null;
    public Queue<GameObject> monster_stage01_01_queue = new Queue<GameObject>();
    public GameObject monster_stage01_02_pripab = null;
    public Queue<GameObject> monster_stage01_02_queue = new Queue<GameObject>();
    public GameObject monster_stage01_03_pripab = null;
    public Queue<GameObject> monster_stage01_03_queue = new Queue<GameObject>();

    private void Awake()
    {
        instance = this;

        // ob  
        for (int i = 0; i < 100; i++)
        {
            GameObject t_object = Instantiate(ob_prefab, new Vector2(3000, 3000), Quaternion.identity, instantiate_pos);
            ob_queue.Enqueue(t_object);
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
        // exp_marble
        for (int i = 0; i < 100; i++)
        {
            GameObject t_object = Instantiate(exp_marble_prefab, new Vector2(3000, 3000), Quaternion.identity, instantiate_pos);
            exp_marble_queue.Enqueue(t_object);
            t_object.SetActive(false);
        }
        // hp_marble
        for (int i = 0; i < 100; i++)
        {
            GameObject t_object = Instantiate(hp_marble_prefab, new Vector2(3000, 3000), Quaternion.identity, instantiate_pos);
            hp_marble_queue.Enqueue(t_object);
            t_object.SetActive(false);
        }
        // skill_marble
        for (int i = 0; i < 50; i++)
        {
            GameObject t_object = Instantiate(skill_marble_prefab, new Vector2(3000, 3000), Quaternion.identity, instantiate_pos);
            skill_marble_queue.Enqueue(t_object);
            t_object.SetActive(false);
        }
        // idle_down
        for (int i = 0; i < 20; i++)
        {
            GameObject t_object = Instantiate(idle_down, new Vector2(3000, 3000), Quaternion.identity, instantiate_pos);
            idle_queue.Enqueue(t_object);
            t_object.SetActive(false);
        }
        // down_ver
        for (int i = 0; i < 20; i++)
        {
            GameObject t_object = Instantiate(down_ver, new Vector2(3000, 3000), Quaternion.identity, instantiate_pos);
            down_ver_queue.Enqueue(t_object);
            t_object.SetActive(false);
        }
        // ver
        for (int i = 0; i < 20; i++)
        {
            GameObject t_object = Instantiate(ver, new Vector2(3000, 3000), Quaternion.identity, instantiate_pos);
            ver_queue.Enqueue(t_object);
            t_object.SetActive(false);
        }
        // up
        for (int i = 0; i < 20; i++)
        {
            GameObject t_object = Instantiate(up, new Vector2(3000, 3000), Quaternion.identity, instantiate_pos);
            up_queue.Enqueue(t_object);
            t_object.SetActive(false);
        }
        // up_ver
        for (int i = 0; i < 20; i++)
        {
            GameObject t_object = Instantiate(up_ver, new Vector2(3000, 3000), Quaternion.identity, instantiate_pos);
            up_ver_queue.Enqueue(t_object);
            t_object.SetActive(false);
        }
        // monster_stage01_01
        for (int i = 0; i < 150; i++)
        {
            GameObject t_object = Instantiate(monster_stage01_01_pripab, new Vector2(3000, 3000), Quaternion.identity, instantiate_pos);
            monster_stage01_01_queue.Enqueue(t_object);
            t_object.SetActive(false);
        }
        // monster_stage01_02
        for (int i = 0; i < 150; i++)
        {
            GameObject t_object = Instantiate(monster_stage01_02_pripab, new Vector2(3000, 3000), Quaternion.identity, instantiate_pos);
            monster_stage01_02_queue.Enqueue(t_object);
            t_object.SetActive(false);
        }
        // monster_stage01_03
        for (int i = 0; i < 150; i++)
        {
            GameObject t_object = Instantiate(monster_stage01_03_pripab, new Vector2(3000, 3000), Quaternion.identity, instantiate_pos);
            monster_stage01_03_queue.Enqueue(t_object);
            t_object.SetActive(false);
        }
    }

    // 사용한 오브젝트를 다시 큐에 집어 넣는 함수
    public void InsertQueue(GameObject p_object, ObjectKind obj)
    {

        if (obj == ObjectKind.ob)
            ob_queue.Enqueue(p_object);

        if (obj == ObjectKind.smoke)
            smoke_queue.Enqueue(p_object);

        if (obj == ObjectKind.nomal_damage)
            nomal_damage_queue.Enqueue(p_object);

        if (obj == ObjectKind.critical_damage)
            critical_damage_queue.Enqueue(p_object);

        if (obj == ObjectKind.exp_marble)
            exp_marble_queue.Enqueue(p_object);

        if (obj == ObjectKind.hp_marble)
            hp_marble_queue.Enqueue(p_object);

        if (obj == ObjectKind.skill_marble)
           skill_marble_queue.Enqueue(p_object);

        if (obj == ObjectKind.idle_down)
            idle_queue.Enqueue(p_object);

        if (obj == ObjectKind.down_ver)
            down_ver_queue.Enqueue(p_object);

        if (obj == ObjectKind.ver)
           ver_queue.Enqueue(p_object);

        if (obj == ObjectKind.up)
           up_queue.Enqueue(p_object);

        if (obj == ObjectKind.up_ver)
            up_ver_queue.Enqueue(p_object);

        if (obj == ObjectKind.monster_stage01_01)
            monster_stage01_01_queue.Enqueue(p_object);

        if (obj == ObjectKind.monster_stage01_02)
            monster_stage01_02_queue.Enqueue(p_object);

        if (obj == ObjectKind.monster_stage01_03)
            monster_stage01_03_queue.Enqueue(p_object);

        p_object.SetActive(false);
    }

    // 사용하기위해 큐에서 오브젝트를 꺼내오는 함수
    public GameObject GetQueue(ObjectKind obj)
    {
        GameObject t_object = null;

        if (obj == ObjectKind.ob)
            t_object = ob_queue.Dequeue();

        if (obj == ObjectKind.smoke)
            t_object = smoke_queue.Dequeue();

        if (obj == ObjectKind.nomal_damage)
            t_object = nomal_damage_queue.Dequeue();

        if (obj == ObjectKind.critical_damage)
            t_object = critical_damage_queue.Dequeue();

        if (obj == ObjectKind.exp_marble)
            t_object = exp_marble_queue.Dequeue();

        if (obj == ObjectKind.hp_marble)
            t_object = hp_marble_queue.Dequeue();

        if (obj == ObjectKind.skill_marble)
            t_object = skill_marble_queue.Dequeue();

        if (obj == ObjectKind.idle_down)
            t_object = idle_queue.Dequeue();

        if (obj == ObjectKind.down_ver)
            t_object = down_ver_queue.Dequeue();

        if (obj == ObjectKind.ver)
            t_object = ver_queue.Dequeue();

        if (obj == ObjectKind.up)
            t_object = up_queue.Dequeue();

        if (obj == ObjectKind.up_ver)
            t_object = up_ver_queue.Dequeue();

        if (obj == ObjectKind.monster_stage01_01)
            t_object = monster_stage01_01_queue.Dequeue();

        if (obj == ObjectKind.monster_stage01_02)
            t_object = monster_stage01_02_queue.Dequeue();

        if (obj == ObjectKind.monster_stage01_03)
            t_object = monster_stage01_03_queue.Dequeue();

        t_object.SetActive(true);
        return t_object;
    }
}

public enum ObjectKind
{
    ob,
    smoke,
    nomal_damage,
    critical_damage,
    exp_marble,
    skill_marble,
    hp_marble,
    idle_down,
    down_ver,
    ver,
    up,
    up_ver,
    monster_stage01_01,
    monster_stage01_02,
    monster_stage01_03
}
