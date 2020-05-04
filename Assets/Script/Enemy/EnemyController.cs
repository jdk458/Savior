using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [HideInInspector]
    public Transform player_transform;
    [Header("속도")]
    public float speed = 0.035f;

    private void FixedUpdate()
    {
        this.transform.position = Vector2.MoveTowards(this.transform.position, player_transform.position, speed);
    }
}
