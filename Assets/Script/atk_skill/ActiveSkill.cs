using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSkill : MonoBehaviour
{
    [Range(1,10)] public float skillDamage;
    [Range(1, 10)] public int atk_num; // 타격수
    [Range(0.1f,60)] public float colltime;


    public void SkillCall(GameObject target, int atk)
    {
        this.transform.position = target.transform.position;
        StartCoroutine(SkillCall_Coroutine(target, atk));
    }

    IEnumerator SkillCall_Coroutine(GameObject target, int atk)
    {
        for (int i = 0; i < atk_num; i++)
        {
            target.GetComponent<EnemyController>().Hit((int)(atk * skillDamage));
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
