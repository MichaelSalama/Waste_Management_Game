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

    [HideInInspector]
    public bool begin;

    private bool beganMove;

    Transform planeTransMoving;

    Collider2D[] otherCols;
    SpriteRenderer[] renderers;
    Color color;

    [HideInInspector]
    public Color oldColor;

    [HideInInspector]
    public Vector3 end;

    FlowManager FM;

    bool stop;

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
        //if (stop)
        //{
        //    if (finishedMoving)
        //    {
        //        ResetPos();
        //        stop = false;
        //    }
        //}

        if (begin && otherCols != null && renderers != null && !finishedMoving)
        {
            for (int i = 0; i < otherCols.Length; i++)
            {
                if (otherCols[i] == null)
                {
                    continue;
                }
                bool touching = false;

                touching = otherCols[i].IsTouching(col);

                if (touching)
                {
                    DiscardColor(renderers[i]);
                }
            }
        }
    }

    public void SetDiscardData(SpriteRenderer[] sr, Collider2D[] cols, Vector4 color)
    {
        this.renderers = sr;
        this.color = color;
        otherCols = cols;
    }   

    public void DiscardColor(SpriteRenderer sr)
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

            StartCoroutine(MoveToEnd(FM.fillTime));
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

            i += rate * Time.fixedDeltaTime;

            Vector3 pos = Vector3.Lerp(oldPos, newPos, i);
            transform.localPosition = pos;

            yield return new WaitForSeconds(Time.fixedDeltaTime);
            counter += Time.fixedDeltaTime;
        }

        finishedMoving = true;
    }

    public void Reset()
    {
        ResetPos();
    }

    private void ResetPos()
    {
        StopAllCoroutines();
        transform.localPosition = beginPos.localPosition;

        finishedMoving = false;
        beganMove = false;
        begin = false;
    }
}
