using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Get : MonoBehaviour
{
    //스킬 획득 최대 갯수
    public int skill_num;

    public Queue<GameObject> skill_marble_queue = new Queue<GameObject>();

    private void Awake()
    {
        //나중에 테이블에서 받아오기(스킬획득 최대갯수)
        skill_num = 3;
    }
    public void Get(GameObject skill_item)
    {
        if (skill_marble_queue.Count < skill_num)
            skill_marble_queue.Enqueue(skill_item);
        else
            skill_marble_queue.Dequeue();
        ObjectPoolingManager.instance.InsertQueue(this.gameObject, ObjectKind.skill_marble);
        Destroy(gameObject);
    }
}
