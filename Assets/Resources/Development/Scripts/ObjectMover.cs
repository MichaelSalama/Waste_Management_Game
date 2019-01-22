using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public IntVariable speed;
    void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            transform.position += Vector3.right * Time.deltaTime* speed.value;
        }
    }
}
