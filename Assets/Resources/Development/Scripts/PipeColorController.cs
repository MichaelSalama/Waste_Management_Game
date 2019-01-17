using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeColorController : MonoBehaviour
{
    Collider2D[] col;
    SpriteRenderer[] sr;

    Discard currentDiscard;
    int currentDiscardIndex = 0;

    public bool reset = false;

    FlowManager FM;
    ObjectTypeEnum objectType;
    Color oldColor;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponentsInChildren<SpriteRenderer>();
        col = GetComponentsInChildren<Collider2D>();
        FM = FindObjectOfType<FlowManager>();
        objectType = FM.currentType;

        for (int i = 0; i < sr.Length; i++)
        {
            sr[i].color = FM.GetCurrentPipes().fillColor;
            var col = sr[i].color;
            col.a = 1;
            sr[i].color = col;
        }
        
        currentDiscardIndex = 0;
        currentDiscard = FM.GetCurrentPipes().pipes[currentDiscardIndex];
        objectType = FM.currentType;
        oldColor = FM.GetCurrentPipes().fillColor;
        currentDiscard.oldColor = oldColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (reset)
        {
            for (int i = 0; i < FM.GetCurrentPipes().pipes.Count; i++)
            {
                FM.GetCurrentPipes().pipes[i].ResetPos();
            }

            for (int i = 0; i < sr.Length; i++)
            {
                Reset(sr[i]);
            }

            currentDiscardIndex = 0;
            reset = false;
        }


        if (objectType != ObjectTypeEnum.NULL && objectType != FM.currentType)
        {
            for (int i = 0; i <= currentDiscardIndex; i++)
            {
                Vector3 pos = FM.pipeTypes.pipeTypes[(int)objectType].pipes[i].end;
                currentDiscard = FM.GetCurrentPipes().pipes[i];
                currentDiscard.end = pos;
                currentDiscard.oldColor = oldColor;
            }

            objectType = FM.currentType;

            currentDiscardIndex = 0;
            currentDiscard = FM.GetCurrentPipes().pipes[currentDiscardIndex];
            
            oldColor = FM.GetCurrentPipes().fillColor;
        }
        else if (objectType == ObjectTypeEnum.NULL && objectType != FM.currentType)
        {
            objectType = FM.currentType;

            currentDiscardIndex = 0;
            currentDiscard = FM.GetCurrentPipes().pipes[currentDiscardIndex];

            oldColor = FM.GetCurrentPipes().fillColor;
            currentDiscard.oldColor = oldColor;
        }

        if (objectType != ObjectTypeEnum.NULL)
        {
            BeginFlow();
        }        
    }

    public void BeginFlow()
    {               
        for (int i = 0; i < FM.pipeTypes.pipeTypes.Count; i++)
        {
            if (FM.pipeTypes.pipeTypes[i].fillColor != FM.GetCurrentPipes().fillColor)
            {
                for (int j = 0; j <= currentDiscardIndex; j++)
                {
                    FM.pipeTypes.pipeTypes[i].pipes[j].ResetPos();
                }
            }
        }

        if (currentDiscard.finishedMoving)
        {
            Color oc = currentDiscard.oldColor;

            currentDiscardIndex++;

            if (currentDiscardIndex > FM.GetCurrentPipes().pipes.Count - 1)
            {
                currentDiscardIndex = FM.GetCurrentPipes().pipes.Count - 1;

                return;
            }

            currentDiscard = FM.GetCurrentPipes().pipes[currentDiscardIndex];
            currentDiscard.oldColor = oc;
        }

        for (int i = 0; i < col.Length; i++)
        {
            bool touching = false;

            touching = col[i].IsTouching(currentDiscard.col);

            if (touching)
            {
                currentDiscard.SetDiscardData(sr[i], FM.GetCurrentPipes().fillColor);
                currentDiscard.DiscardColor();
            }            
        }
    }


    public void Reset(SpriteRenderer sr)
    {
        sr.material.SetVector("_DiscardPlane", new Vector4(0, 0, 0, 0));
        sr.material.SetFloat("_UseDiscardPlane", 0);
        sr.material.SetVector("_DiscardedPixelsColor", new Vector4(0, 0, 0, 0));
    }
}
