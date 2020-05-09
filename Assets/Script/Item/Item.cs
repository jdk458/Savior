﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Item : MonoBehaviour
{
    public Transform player;

    float search_range;

    private void Start()
    {
        search_range = player.GetComponent<PlayerController>().item_range;
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.CircleCast(this.transform.position, search_range, Vector2.zero);
        if (hit && hit.transform.tag.Contains("Player"))
        {
            this.transform.DOMove(player.position, 0.3f);
        }
    
    }
}
