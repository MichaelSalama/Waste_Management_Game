using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChecker : MonoBehaviour
{
    ObjectTypeEnum boxType;

    private void Awake()
    {
        boxType = gameObject.GetComponent<ObjectType>().type;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Filler"))
        {
            CheckState(collision.gameObject);
        }
    }

    public void CheckState(GameObject obj)
    {
        if (obj.gameObject.GetComponent<ObjectType>().type == boxType)
        {
            //success
            //trigger event here to game manager to increase the score.
            GameManager.Instance.IncreaseScore();
            //return to pool>>>>>>>
            Destroy(obj.gameObject);
        }
        else
        {
            //fail
            GameManager.Instance.DecreaseSCore();
            //return to pool>>>>>>>
            Destroy(obj.gameObject);
        }
    }
}
