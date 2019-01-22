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

        gameObject.GetComponent<ObjectMover>().enabled= false;
    }

    private void OnBecameInvisible()
    {
        ObjectPool.Despawn(this.gameObject);
    }
}

