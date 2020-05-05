using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [HideInInspector]
    public Transform player_transform;
    [Header("속도")]
    public float speed = 0.035f;

    Rigidbody2D rigidbody2D;

    private void OnEnable()
    {
        rigidbody2D = this.GetComponent<Rigidbody2D>();
     
    }
    private void FixedUpdate()
    {
        if (!damage_flag)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, player_transform.position, speed);
        }
    }

    void test()
    {
        ObjectPoolingManager.instance.InsertQueue(this.gameObject, ObjectKind.enemy );
    }

    private void OnDisable()
    {
        damage_flag = false;
        this.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    public void Damage(GameObject target)
    {
        StartCoroutine(Damage_Coroutine(target));
    }
    bool damage_flag;
    IEnumerator Damage_Coroutine(GameObject target)
    {
        damage_flag = true;

        Vector3 dir = player_transform.position - this.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 180f;
     

        this.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 150);
        yield return new WaitForSeconds(0.2f);
        this.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);

        float draft = 5f;
       
        if (target.tag.Contains("Draft"))
        {
            if (337.5f < angle || angle <= 22.5) // right   
                rigidbody2D.velocity = new Vector2(draft, 0);
            if (22.5f < angle || angle <= 67.5f) // under_right   
                rigidbody2D.velocity = new Vector2(draft, -draft);
            if (67.5f < angle || angle <= 112.5f) // under
                rigidbody2D.velocity = new Vector2(0, -draft);
            if (112.5f < angle || angle <= 157.5f) // under_left
                rigidbody2D.velocity = new Vector2(-draft, -draft);
            if (157.5f < angle || angle <= 202.5f) // left
                rigidbody2D.velocity = new Vector2(-draft, 0);
            if (202.5f < angle || angle <= 247.5f) // up_left
                rigidbody2D.velocity = new Vector2(-draft, draft);
            if (247.5f < angle || angle <= 292.5f) // up
                rigidbody2D.velocity = new Vector2(0, draft);
            if (292.5f < angle || angle <= 337.5f) // up_right
                rigidbody2D.velocity = new Vector2(draft, draft);
        }
        yield return new WaitForSeconds(0.2f);
        rigidbody2D.velocity = Vector2.zero;
        damage_flag = false;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Contains("Attack"))
        {
            Damage(collision.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag.Contains("Enemy"))
        {
            rigidbody2D.velocity = Vector2.zero;
        }
    }
}
