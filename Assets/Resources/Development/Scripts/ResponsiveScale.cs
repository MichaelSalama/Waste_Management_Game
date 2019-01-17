using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponsiveScale : MonoBehaviour
{
    public Vector2 screenSize;
    public float modifierX, modifierY;
    public bool IsScaleWithSame=false;
    Vector3 initalScale;
    
    // Start is called before the first frame update
    void Start()
    {
        
        initalScale = transform.localScale;

    }

    // Update is called once per frame
    void Update()
    {
        if (IsScaleWithSame)
            ScaleBySameValues();
        else
            ScaleByDiffrentValues();
    }

    public void ScaleBySameValues()
    {
        modifierX = initalScale.x * screenSize.y / screenSize.x * Screen.width / Screen.height;
        modifierY = initalScale.y * screenSize.x / screenSize.y * Screen.height / Screen.width;
        if (modifierX >= modifierY)
            this.transform.localScale = new Vector3(modifierX, modifierX, 1);
        else
            this.transform.localScale = new Vector3(modifierY, modifierY, 1);

    }

    public void ScaleByDiffrentValues()
    {
        modifierX = initalScale.x * (screenSize.y) / screenSize.x * Screen.width / Screen.height;
        modifierY = initalScale.y * (screenSize.x) / (screenSize.y) * Screen.height / Screen.width;
        this.transform.localScale = new Vector3(modifierX, modifierY, 1);
    }
}
