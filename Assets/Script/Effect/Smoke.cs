using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{

    public AudioSource explosion;
    public void Smoke_Destroy()
    {
        ObjectPoolingManager.instance.InsertQueue(this.gameObject, ObjectKind.smoke);
    }

    private void OnEnable()
    {
        GameManager.instance.audioManager.EnvironVolume_Play(explosion);
    }
}
