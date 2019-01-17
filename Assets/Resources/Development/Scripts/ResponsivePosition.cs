using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponsivePosition : MonoBehaviour
{
    public Vector2 startscreenSize;
    public Camera cam;
    public float percentOfX, percentOfY;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = cam.ScreenToWorldPoint(new Vector3(Screen.width * percentOfX, Screen.height * percentOfY, 10));
        startscreenSize.x = Screen.width;
        startscreenSize.y = Screen.height;

    }

    // Update is called once per frame
    void Update()
    {

        if (Screen.width != startscreenSize.x || Screen.height != startscreenSize.y)
        {
            this.transform.position = cam.ScreenToWorldPoint(new Vector3(Screen.width * percentOfX, Screen.height*percentOfY, 10));
            startscreenSize.x = Screen.width;
            startscreenSize.y = Screen.height;
        }

    }









}
