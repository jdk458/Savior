using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Exp_Up : MonoBehaviour
{
    [Range(10, 100)] public int exp_amount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Contains("Player"))
        {
            collision.GetComponent<PlayerController>().Exp_Up(exp_amount);
            ObjectPoolingManager.instance.InsertQueue(this.gameObject, ObjectKind.exp_marble);
        }
    }
}
