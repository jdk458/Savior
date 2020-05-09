using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Damage : MonoBehaviour
{

    [Header("랜덤 horizontal 범위")] public float random_horizontal;
    [Header("높이")] public float height;

    public void DamageSet(int damage, bool isCritical = false)
    {
        Vector2 temp_pos = this.transform.position;
        this.transform.GetChild(0).GetComponent<Text>().text = damage.ToString();
        float randomX = Random.RandomRange(-random_horizontal, random_horizontal);
        this.transform.DOMove(new Vector2(temp_pos.x + randomX, temp_pos.y + height), 0.3f).OnComplete(()=>
        {
            if (!isCritical)
                ObjectPoolingManager.instance.InsertQueue(this.gameObject, ObjectKind.nomal_damage);
            else
                ObjectPoolingManager.instance.InsertQueue(this.gameObject, ObjectKind.critical_damage);
        });
        this.transform.GetChild(0).transform.DOScale(new Vector2(2f,  2f), 0.3f);
    }

    private void OnDisable()
    {
        this.transform.GetChild(0).transform.localScale = new Vector2(1, 1);
    }

   
}
