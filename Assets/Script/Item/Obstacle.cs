using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public void ObstacleHit()
    {
        StartCoroutine(HitCoroutine());
    }

    IEnumerator HitCoroutine()
    {
        this.GetComponent<Animator>().SetBool("obstacle", true);
        yield return new WaitForSeconds(0.7f);
        this.GetComponent<Animator>().SetBool("obstacle", false);
        this.gameObject.SetActive(false);
    }
}
