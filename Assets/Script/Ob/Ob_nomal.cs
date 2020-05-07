using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ob_nomal : MonoBehaviour
{
    [Header("오브")]
    [Range(15, 30)] public float speed = 15;
 
    private void FixedUpdate()
    {
        this.transform.Translate(Vector2.up * Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Contains("Enemy"))
        {
            collision.GetComponent<EnemyController>().Damage(this.gameObject);
            Destroy();
        }
    }

    private void OnEnable()
    {
        Invoke("Destroy", 10);
    }

    void Destroy()
    {
        ObjectPoolingManager.instance.InsertQueue(this.gameObject, ObjectKind.ob);
    }
}
