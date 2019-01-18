using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeColorController : MonoBehaviour
{
    Collider2D[] col;
    SpriteRenderer[] sr;

    Discard currentDiscard;
    Discard oldDiscard;
    int oldDiscardIndex = 0;
    int currentDiscardIndex = 0;

    FlowManager FM;
    ObjectTypeEnum objectType;
    ObjectTypeEnum oldObjectType;
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
        oldDiscardIndex = 0;

        currentDiscard = FM.GetCurrentPipes().pipes[currentDiscardIndex];
        oldDiscard = FM.GetCurrentPipes().pipes[currentDiscardIndex];

        objectType = FM.currentType;
        oldObjectType = FM.currentType;

        oldColor = FM.GetCurrentPipes().fillColor;
        currentDiscard.oldColor = oldColor;
    }

    // Update is called once per frame
    void Update()
    {      
        if (objectType != ObjectTypeEnum.NULL && objectType != FM.currentType)
        {
            oldColor = FM.pipeTypes.pipeTypes[(int)objectType].fillColor;
            oldDiscardIndex = currentDiscardIndex;
            oldDiscard = FM.pipeTypes.pipeTypes[(int)objectType].pipes[oldDiscardIndex];
            oldObjectType = objectType;

            for (int i = 0; i <= currentDiscardIndex; i++)
            {
                Vector3 pos = FM.pipeTypes.pipeTypes[(int)objectType].pipes[i].end;
                currentDiscard = FM.GetCurrentPipes().pipes[i];
                currentDiscard.end = pos;
                currentDiscard.oldColor = oldColor;
            }
            
            currentDiscardIndex = 0;
            currentDiscard = FM.GetCurrentPipes().pipes[currentDiscardIndex];            
        }
        else if (objectType == ObjectTypeEnum.NULL && objectType != FM.currentType)
        {

            currentDiscardIndex = 0;
            currentDiscard = FM.GetCurrentPipes().pipes[currentDiscardIndex];

            oldColor = FM.GetCurrentPipes().fillColor;
            currentDiscard.oldColor = oldColor;
        }

        if (objectType != ObjectTypeEnum.NULL)
        {
            BeginFlow();
        }

        objectType = FM.currentType;
    }

    public void BeginFlow()
    {
        if (objectType != FM.currentType)
        {
            for (int i = 0; i < FM.pipeTypes.pipeTypes.Count; i++)
            {
                for (int j = 0; j < FM.pipeTypes.pipeTypes[0].pipes.Count; j++)
                {
                    FM.pipeTypes.pipeTypes[i].pipes[j].Reset();
                }
            }
        }

        for (int i = 0; i <= oldDiscardIndex; i++)
        {
            Vector3 pos = FM.pipeTypes.pipeTypes[(int)oldObjectType].pipes[i].end;
            FM.pipeTypes.pipeTypes[(int)oldObjectType].pipes[i].end = pos;
        }

        for (int i = 0; i <= currentDiscardIndex; i++)
        {
            Vector3 pos = FM.pipeTypes.pipeTypes[(int)objectType].pipes[i].end;
            //Color col = FM.pipeTypes.pipeTypes[(int)objectType].pipes[i].oldColor;
            currentDiscard = FM.GetCurrentPipes().pipes[i];
            currentDiscard.end = pos;
            currentDiscard.oldColor = oldColor;
        }

        if (currentDiscard.finishedMoving)
        {
            Color oc = currentDiscard.oldColor;
            currentDiscard.oldColor = FM.GetCurrentPipes().fillColor;

            currentDiscardIndex++;

            if (currentDiscardIndex > FM.GetCurrentPipes().pipes.Count - 1)
            {
                currentDiscardIndex = FM.GetCurrentPipes().pipes.Count - 1;

                return;
            }

            currentDiscard = FM.GetCurrentPipes().pipes[currentDiscardIndex];
            //currentDiscard.oldColor = oc;
        }

        currentDiscard.SetDiscardData(sr, col, FM.GetCurrentPipes().fillColor);
        currentDiscard.begin = true;
        //for (int i = 0; i < col.Length; i++)
        //{
        //    bool touching = false;

        //    touching = col[i].IsTouching(currentDiscard.col);

        //    if (touching)
        //    {
        //        currentDiscard.DiscardColor();
        //    }            
        //}
    }


    public void Reset(SpriteRenderer sr)
    {
        sr.material.SetVector("_DiscardPlane", new Vector4(0, 0, 0, 0));
        sr.material.SetFloat("_UseDiscardPlane", 0);
        sr.material.SetVector("_DiscardedPixelsColor", new Vector4(0, 0, 0, 0));
    }
}
