using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public void ObstacleHit(float time)
    {
        StartCoroutine(HitCoroutine(time));
    }

    IEnumerator HitCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        this.gameObject.SetActive(false);
    }
}
