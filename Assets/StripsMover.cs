using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StripsMover : MonoBehaviour
{
    Vector3 startPos;
    public Vector3 endPos;

    public float speed;

    public Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        endPos = transform.position + new Vector3(5, 0,0) ;
    }

    // Update is called once per frame
    void Update()
    {

        //gameObject.transform.Translate(18.11f,-0.13f,0f);
    }
}
