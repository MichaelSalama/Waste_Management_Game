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

    private void Start()
    {
        SpawnAnewGameObject();
    }

    private void Update()
    {
        StartCoroutine(SpawnInterval());
        if (spawnedobj.gameObject.activeInHierarchy == false)
        {
            ObjectPool.Despawn(spawnedobj);
        }
    }

    IEnumerator SpawnInterval()
    {
        yield return new WaitForSeconds(1);
        SpawnAnewGameObject();

    }

    public void SpawnAnewGameObject()
    {
        spawnedobj = ObjectPool.Spawn(prefab, transform.position, Quaternion.identity);
    }


}
