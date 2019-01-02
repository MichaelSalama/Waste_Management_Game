using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField]
    private GameObject[] objectPrefabs;

    private List<GameObject> pooledObject = new List<GameObject>();

    public static ObjectPooler S;

    private void Start()
    {
        S = this;
    }
 
    public GameObject GetObject(ObjectTypeEnum type)
    {
        foreach (GameObject obj in pooledObject)
        {
            if (obj.GetComponent<ObjectType>().type == type && !obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        for (int i = 0; i < objectPrefabs.Length; i++)
        {
            if (objectPrefabs[i].GetComponent<ObjectType>().type == type)
            {
                GameObject newObject = Instantiate(objectPrefabs[i]);
                pooledObject.Add(newObject);
                newObject.GetComponent<ObjectType>().type = type;
                return newObject;
            }
        }
        return null;
    }

    public void ReleaseObject (GameObject gameObject)
    {
        gameObject.SetActive(false);
    }


}
