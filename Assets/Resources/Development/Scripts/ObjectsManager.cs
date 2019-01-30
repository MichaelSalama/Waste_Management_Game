using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsManager : MonoBehaviour
{
    public List<GameObject> glassList = new List<GameObject>();
    public List<GameObject> metalList = new List<GameObject>();
    public List<GameObject> paperList = new List<GameObject>();
    public List<GameObject> plasticList = new List<GameObject>();

    public GameObject currentObj;
    public GameObject previousObj;

    public List<GameObject> lights = new List<GameObject>();



    int randNo = 0;
    int rand = 0;

    private void Start()
    {
        StartCoroutine(WaitforTime());
    }

    GameObject InstantiateNewObject()
    {
        randNo = Random.Range(1, 5);

        if (previousObj == null)
        {
            previousObj = ObjectPool.Spawn(glassList[1], transform.position, Quaternion.identity);
        }


        if (randNo == 1 && previousObj.GetComponent<DragHandler>().type != ObjectTypeEnum.Glass)
        {
            rand = Random.Range(1, glassList.Count);
            currentObj = ObjectPool.Spawn(glassList[rand], transform.position, Quaternion.identity);
            //greenLight.gameObject.SetActive(true);
            LightsHandler();
            FlowManager.Instance.currentType = ObjectTypeEnum.Glass;

        }
        else if (randNo == 2 && previousObj.GetComponent<DragHandler>().type != ObjectTypeEnum.Metal)
        {
            rand = Random.Range(1, metalList.Count);
            currentObj = ObjectPool.Spawn(metalList[rand], transform.position, Quaternion.identity);
            //redLight.gameObject.SetActive(true);
            LightsHandler();
            FlowManager.Instance.currentType = ObjectTypeEnum.Metal;
        }
        if (randNo == 3 && previousObj.GetComponent<DragHandler>().type != ObjectTypeEnum.Paper)
        {
            rand = Random.Range(1, paperList.Count);
            currentObj = ObjectPool.Spawn(paperList[rand], transform.position, Quaternion.identity);
            //yellowLight.gameObject.SetActive(true);
            LightsHandler();
            FlowManager.Instance.currentType = ObjectTypeEnum.Paper;
        }
        if (randNo == 4 && previousObj.GetComponent<DragHandler>().type != ObjectTypeEnum.Plastic)
        {
            rand = Random.Range(1, plasticList.Count);
            currentObj = ObjectPool.Spawn(plasticList[rand], transform.position, Quaternion.identity);
            //blueLight.gameObject.SetActive(true);
            LightsHandler();
            FlowManager.Instance.currentType = ObjectTypeEnum.Plastic;
        }
        return currentObj;
    }

    public void LightsHandler()
    {
        foreach (var obj in lights)
        {
            obj.gameObject.SetActive(false);
        if (obj.GetComponent<ObjectType>().type == currentObj.GetComponent<DragHandler>().type)
            {
                obj.gameObject.SetActive(true);
            }
        }
    }

   

    IEnumerator WaitforTime()
    {
        yield return new WaitForSeconds(2);
        previousObj = InstantiateNewObject();
        StartCoroutine(WaitforTime());
    }
}