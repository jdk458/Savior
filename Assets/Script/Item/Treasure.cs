using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    public GameObject[] skillMable;
    public AudioSource open_AudioSource;
    public void TreasureOpen()
    {
        this.GetComponent<Animator>().SetBool("treasure", true);
        GameManager.instance.audioManager.EnvironVolume_Play(open_AudioSource);
    }

    public void Destroy()
    {
        this.GetComponent<Animator>().SetBool("treasure", false);
        int rand = Random.RandomRange(0, skillMable.Length);
        Instantiate(skillMable[rand], this.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
