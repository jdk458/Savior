using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_follow : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy"||collision.tag == "Player"||collision.tag == "Attack") return;
        collision.transform.SetParent(this.gameObject.transform);
    }
}
