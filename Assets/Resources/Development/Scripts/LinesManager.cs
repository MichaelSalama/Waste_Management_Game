using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinesManager : MonoBehaviour
{
    public static LinesManager Instance;

    public GameObject prefab;
    GameObject spawnedobj;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void SpawnAnewGameObject()
    {
        spawnedobj = ObjectPool.Spawn(prefab, transform.position, Quaternion.identity);
    }
}
