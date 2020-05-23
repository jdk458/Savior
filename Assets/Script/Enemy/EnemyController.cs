using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [HideInInspector]
    public Transform player_transform;
    [Header("속도")]
    public float speed = 0.035f;
    [Header("체력")] public int max_hp;
    int current_hp;

    Rigidbody2D rigidbody2D;

    private void OnEnable()
    {
        current_hp = max_hp;
        rigidbody2D = this.GetComponent<Rigidbody2D>();
     
    }
    private void FixedUpdate()
    {
        if (!hit_flag)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, player_transform.position, speed);
        }
    }


    private void OnDisable()
    {
        hit_flag = false;
        this.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    bool hit_flag;
    public void Hit(int damage, bool isCritical = false)
    {
     
        current_hp -= damage;
        if (current_hp <= 0)
        {
            GameObject smoke = ObjectPoolingManager.instance.GetQueue(ObjectKind.smoke);
            smoke.transform.position = this.transform.position;
            ObjectPoolingManager.instance.InsertQueue(this.gameObject, ObjectKind.enemy);
        }

        else
        {
            if (!isCritical)
            {
                GameObject damage_obj = ObjectPoolingManager.instance.GetQueue(ObjectKind.nomal_damage);
                damage_obj.transform.position = this.transform.position;
                damage_obj.GetComponent<Damage>().DamageSet(damage, isCritical);
            }
            else
            {
                GameObject damage_obj = ObjectPoolingManager.instance.GetQueue(ObjectKind.critical_damage);
                damage_obj.transform.position = this.transform.position;
                damage_obj.GetComponent<Damage>().DamageSet(damage, isCritical);
            }
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
            collision.transform.GetComponent<PlayerController>().Hit(5, gameObject);
            rigidbody2D.velocity = Vector2.zero;
        }
    }

}
