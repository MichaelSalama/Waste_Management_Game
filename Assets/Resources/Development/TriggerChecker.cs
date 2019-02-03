using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChecker : MonoBehaviour
{
    public ObjectTypeEnum boxType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Filler"))
        {
            CheckState(collision.gameObject);
        }
        foreach (var obj in GameManager.Instance.glow)
        {
            obj.gameObject.SetActive(false);
        }
        
    }

    public void CheckState(GameObject obj)
    {
        if (obj.gameObject.GetComponent<DragHandler>().type == boxType)
        {
            //success
            GameManager.Instance.IncreaseScore();
            ObjectPool.Despawn(obj.gameObject);
        }
        else
        {
            //fail
            GameManager.Instance.DecreaseSCore();
            ObjectPool.Despawn(obj.gameObject);
        }
    }
}
