using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    [Header("몬스터 정보")]
    public string name;
    public ObjectKind objectKind;
    Stage stage;
    MonsterType monsterType;
    [Range(0, 1)] float rigidTime;
    int atk;
    int hp; int current_hp;
    float speed;
    [Header("플레이어 정보")]
    public Transform player_transform;

    Rigidbody2D rigidbody2D;

    private void OnEnable()
    {
        stage = GameManager.instance.monsterManager.GetMonster(name).stage;
        monsterType = GameManager.instance.monsterManager.GetMonster(name).monsterType;
        rigidTime = GameManager.instance.monsterManager.GetMonster(name).rigidTime;
        atk = GameManager.instance.monsterManager.GetMonster(name).atk;
        hp = GameManager.instance.monsterManager.GetMonster(name).hp;
        speed = GameManager.instance.monsterManager.GetMonster(name).speed;

        rigidbody2D = this.GetComponent<Rigidbody2D>();
        current_hp = hp;
    }

    private void OnDisable()
    {
        hit_flag = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    bool hit_flag;
    private void FixedUpdate()
    {
        if (TimeManager.instance.GetTime())
            return;

        if (!hit_flag)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, player_transform.position, speed / 120f);
        }
    }

    public void Hit(int damage)
    {
        if (TimeManager.instance.GetTime())
            return;

        current_hp -= damage;
        if (current_hp <= 0)
        {
            GameObject smoke = ObjectPoolingManager.instance.GetQueue(ObjectKind.smoke);
            smoke.transform.position = this.transform.position;
            ObjectPoolingManager.instance.InsertQueue(this.gameObject, objectKind);
        }

        else
        {
            GameObject damage_obj = ObjectPoolingManager.instance.GetQueue(ObjectKind.nomal_damage);
            damage_obj.transform.position = this.transform.position;
            damage_obj.GetComponent<Damage>().DamageSet(damage);

            StartCoroutine(Hit_Coroutine());
        }
    }
    IEnumerator Hit_Coroutine()
    {
        hit_flag = true;
        yield return new WaitForSeconds(0.05f);
        hit_flag = false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag.Contains("Enemy"))
        {
            rigidbody2D.velocity = Vector2.zero;
        }

        if (collision.transform.tag.Contains("Player"))
        {
            if (TimeManager.instance.GetTime())
                return;

            collision.transform.GetComponent<PlayerController>().Hit(atk, gameObject);
            rigidbody2D.velocity = Vector2.zero;
        }
    }
}
