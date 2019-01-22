﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StripesTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        LinesManager.Instance.SpawnAnewGameObject();
    }

    private void OnBecameInvisible()
    {
        ObjectPool.Despawn(gameObject);
    }
}