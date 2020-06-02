using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    public GameObject[] skillMable;
    public void TreasureOpen()
    {
        this.GetComponent<Animator>().SetBool("treasure", true);
    }

    public void Destroy()
    {
        this.GetComponent<Animator>().SetBool("treasure", false);
        int rand = Random.RandomRange(0, skillMable.Length);
        Instantiate(skillMable[rand], this.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
