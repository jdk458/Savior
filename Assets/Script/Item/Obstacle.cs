using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    bool istrue;
    public ObjectKind objectKind;

    public void ObstacleHit()
    {
        StartCoroutine(HitCoroutine());
    }

    IEnumerator HitCoroutine()
    {
        this.GetComponent<Animator>().SetBool("obstacle", true);
        yield return new WaitForSeconds(0.7f);
        this.GetComponent<Animator>().SetBool("obstacle", false);
        this.gameObject.SetActive(false);
        istrue = (Random.value > 0.5f);
        drop_skill_item(istrue);
    }

    void drop_skill_item(bool istrue)
    {
        if(istrue == true)
        {
            GameObject skill_marble = ObjectPoolingManager.instance.GetQueue(ObjectKind.skill_marble);
            skill_marble.transform.position = gameObject.transform.position;
        }
    }
}
