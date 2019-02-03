using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragHandler : MonoBehaviour
{
    public ObjectTypeEnum type; 

    private void OnMouseDrag()
    {
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = objPosition;

        foreach(var obj in GameManager.Instance.glow)
        {
            obj.gameObject.SetActive(false);
            if(obj.GetComponent<ObjectType>().type == type)
            {
                obj.gameObject.SetActive(true);
            }
        }

        gameObject.GetComponent<ObjectMover>().enabled= false;
    }

    

    private void OnBecameInvisible()
    {
        ObjectPool.Despawn(this.gameObject);
    }
}

