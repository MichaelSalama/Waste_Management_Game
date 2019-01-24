using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invisiabilityScript : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        ObjectPool.Despawn(gameObject);
    }
}
