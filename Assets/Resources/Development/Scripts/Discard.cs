using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Discard : MonoBehaviour
{
    [HideInInspector]
    public Collider2D col;

    public Transform beginPos;
    public Transform endPos;

    [HideInInspector]
    public bool finishedMoving;

    private bool beganMove;

    Transform planeTransMoving;

    SpriteRenderer sr;
    Color color;

    [HideInInspector]
    public Color oldColor;

    [HideInInspector]
    public Vector3 end;

    FlowManager FM;

    float fillTime;

    // Start is called before the first frame update
    void Start()
    {
        FM = FindObjectOfType<FlowManager>();
        col = GetComponent<Collider2D>();
        planeTransMoving = transform.GetChild(0).transform;
        transform.localPosition = beginPos.localPosition;
        end = beginPos.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (FM.fillTime != fillTime)
        {
            fillTime = FM.fillTime;
        }
    }

    public void SetDiscardData(SpriteRenderer sr, Vector4 color)
    {
        this.sr = sr;
        this.color = color;
    }   

    public void DiscardColor()
    {
        Vector3 normal = new Vector3(0, -1, 0);
        Quaternion rotation = planeTransMoving.rotation;
        normal = rotation * normal;

        Vector3 pos = planeTransMoving.position;

        Plane planeB;
        Plane planeM;
        Plane planeE;

        planeB = new Plane(normal, beginPos.localPosition);
        planeM = new Plane(normal, pos);

        if (rotation.eulerAngles.z > 100)
        {
            if (end.y < pos.y)
            {
                end = pos;
            }
        }
        else
        {
            if (end.y > pos.y)
            {
                end = pos;
            }
        }       

        planeE = new Plane(normal, end);

        sr.material.SetVector("_DiscardPlaneBegin", new Vector4(planeB.normal.x, planeB.normal.y, planeB.normal.z, planeB.distance));
        sr.material.SetVector("_DiscardPlaneMoving", new Vector4(planeM.normal.x, planeM.normal.y, planeM.normal.z, planeM.distance));
        sr.material.SetVector("_DiscardPlaneEnd", new Vector4(planeE.normal.x, planeE.normal.y, planeE.normal.z, planeE.distance));

        sr.material.SetFloat("_UseDiscardPlane", 1);

        sr.material.SetVector("_DiscardedPixelsColorBegin", color);
        sr.material.SetVector("_DiscardedPixelsColorEnd", oldColor);

        if (!beganMove)
        {
            beganMove = true;

            StartCoroutine(MoveToEnd(fillTime));
        }
    }

    public IEnumerator MoveToEnd(float duration)
    {
        float counter = 0;

        Vector3 oldPos = beginPos.localPosition;
        Vector3 newPos = endPos.localPosition;
        float rate = 1f / duration;
        float i = 0;
        while (counter < duration)
        {

            i += rate * Time.deltaTime;

            Vector3 pos = Vector3.Lerp(oldPos, newPos, i);
            transform.localPosition = pos;

            yield return new WaitForSeconds(Time.deltaTime);
            counter += Time.deltaTime;
        }

        finishedMoving = true;
    }

    public void ResetPos()
    {
        StopAllCoroutines();
        transform.localPosition = beginPos.localPosition;

        finishedMoving = false;
        beganMove = false;
    }
}
