using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class CameraManager : MonoBehaviour
{

    public float horizontalResolution = 1920;
#if UNITY_EDITOR
    private void OnGUI()
    {
        SclaeFunction();
    }
#endif
#if UNITY_WEBGL
    private void Update()
    {
        SclaeFunction();
    }
#endif

    private void SclaeFunction()
    {
        float currentAspect = (float)Screen.width / (float)Screen.height;
        Camera.main.orthographicSize = horizontalResolution / currentAspect / 83.333f;
    }
}
