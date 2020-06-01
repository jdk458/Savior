using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    bool istrue;
    public ObjectKind objectKind;

    public ObjectKind marble_type;

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
            float marble_type_num = Random.value;
            float marble_num = Random.value;
            marble_type = ObjectKind.skill_marble_fire01;
            if(marble_type_num < 0.3f)
            {
                if (marble_num < 0.1f)
                    marble_type = ObjectKind.skill_marble_fire05;
                else if (marble_num < 0.3f)
                    marble_type = ObjectKind.skill_marble_fire04;
                else if (marble_num < 0.4f)
                    marble_type = ObjectKind.skill_marble_fire03;
                else if (marble_num < 0.6f)
                    marble_type = ObjectKind.skill_marble_fire02;
                else
                    marble_type = ObjectKind.skill_marble_fire01;
            }
            else if (marble_type_num < 0.7f)
            {
                if (marble_num < 0.1f)
                    marble_type = ObjectKind.skill_marble_light05;
                else if (marble_num < 0.3f)
                    marble_type = ObjectKind.skill_marble_light04;
                else if (marble_num < 0.4f)
                    marble_type = ObjectKind.skill_marble_light03;
                else if (marble_num < 0.6f)
                    marble_type = ObjectKind.skill_marble_light02;
                else
                    marble_type = ObjectKind.skill_marble_light01;
            }
            else
            {
                if (marble_num < 0.1f)
                    marble_type = ObjectKind.skill_marble_water05;
                else if (marble_num < 0.3f)
                    marble_type = ObjectKind.skill_marble_water04;
                else if (marble_num < 0.4f)
                    marble_type = ObjectKind.skill_marble_water03;
                else if (marble_num < 0.6f)
                    marble_type = ObjectKind.skill_marble_water02;
                else
                    marble_type = ObjectKind.skill_marble_water01;
            }
            GameObject skill_marble = ObjectPoolingManager.instance.GetQueue(marble_type);
            skill_marble.transform.position = gameObject.transform.position;
        }
    }

}
