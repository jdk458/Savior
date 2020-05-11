using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ob_nomal : MonoBehaviour
{
    [Header("오브")]
    [Range(15, 30)] public float speed = 15;
    [Range(0, 10)] public float atk = 1;

    [HideInInspector]
    public PlayerController player_status;
 
    private void FixedUpdate()
    {
        this.transform.Translate(Vector2.up * Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Contains("Enemy"))
        {
            collision.GetComponent<EnemyController>().Hit((int)(player_status.atk * atk));
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
