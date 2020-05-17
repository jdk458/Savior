using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hp_Recovery : MonoBehaviour
{
    [Range(10, 100)] public int recovery_amount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Contains("Player"))
        {
            collision.GetComponent<PlayerController>().Hp_Recovery(recovery_amount);
            ObjectPoolingManager.instance.InsertQueue(this.gameObject, ObjectKind.hp_marble);
            Destroy(this.gameObject);
        }
    }
}
