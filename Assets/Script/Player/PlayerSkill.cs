using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerSkill : MonoBehaviour
{
    List<SkillInfo> player_skill = new List<SkillInfo>();

    public SkillInfo GetPlayerskill(string skillindex)
    {
        for (int i = 0; i < player_skill.Count; i++)
        {
            if (player_skill[i].skill_index == skillindex)
            {
                return player_skill[i];
            }
        }
        return null;
    }

    public void Insert_Playerskill(string skillindex)
    {
        SkillInfo skill = new SkillInfo();
        skill.skill_index = skillindex;
        player_skill.Add(skill);
    }

    public void LevelUp_Playerskill(string skillindex, int index)
    {
        for (int i = 0; i < player_skill.Count; i++)
        {
            if (player_skill[i].skill_index == skillindex)
            {
                switch (index)
                {
                    case 0:
                        player_skill[i].skill_level00++;
                        break;
                    case 1:
                        player_skill[i].skill_level01++;
                        break;
                    case 2:
                        player_skill[i].skill_level02++;
                        break;
                    case 3:
                        player_skill[i].skill_level03++;
                        break;
                }
            }
        }
    }

    private void Start()
    {
        Skill_006();
    }

    public void Skill_006()
    {
        StartCoroutine(Skill_006_Coroutine());
    }
    IEnumerator Skill_006_Coroutine()
    {
        RaycastHit2D enemy = Physics2D.CircleCast(this.transform.position, 5f, Vector2.zero);

        while (enemy && enemy.transform.tag != "Enemy")
        {
            enemy = Physics2D.CircleCast(this.transform.position, 5f, Vector2.zero);
            yield return null;
        }

        GameObject skill = ObjectPoolingManager.instance.GetQueue(ObjectKind.skill);
        skill.GetComponent<Animator>().SetBool("Skill_006", true);
        skill.transform.position = this.transform.position;
        Vector3 dir = enemy.transform.position - this.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        skill.transform.rotation = Quaternion.Euler(skill.transform.rotation.x, skill.transform.rotation.y, angle );

        while (true)
        {
            yield return new WaitForFixedUpdate();
            skill.transform.position = Vector2.MoveTowards(skill.transform.position, enemy.transform.position, 0.5f);
            if (Vector2.Distance(skill.transform.position, enemy.transform.position) == 0)
            {
                skill.GetComponent<Animator>().SetBool("Skill_006", false);
                ObjectPoolingManager.instance.InsertQueue(skill, ObjectKind.skill);
                enemy.transform.GetComponent<EnemyController>().Damage(this.gameObject);
                break;
            }
        }
        Invoke("Skill_006", 3f);
    }
}
public class SkillInfo
{
    public string skill_index;
    public int skill_level00;
    public int skill_level01;
    public int skill_level02;
    public int skill_level03;
}
