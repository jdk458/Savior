using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public ObjectKind ob_kind;
    private void OnEnable()
    {
        StartCoroutine(Destroy_Coroutine());
    }

    IEnumerator Destroy_Coroutine()
    {
        yield return new WaitForSeconds(.6f);
        ObjectPoolingManager.instance.InsertQueue(this.gameObject, ob_kind);
    }
}
