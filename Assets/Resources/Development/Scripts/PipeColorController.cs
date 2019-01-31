using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeColorController : MonoBehaviour
{
    public class DiscardData
    {
        public DiscardData()
        {

        }

        public int index;
        public Discard current;
    }

    Collider2D[] col;
    SpriteRenderer[] sr;

    DiscardData[] discards;
    bool[] discardsStarted;

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

        discards = new DiscardData[FM.pipeTypes.pipeTypes.Count];
        discardsStarted = new bool[FM.pipeTypes.pipeTypes.Count];

        for (int i = 0; i < discards.Length; i++)
        {
            discards[i] = new DiscardData();
            discards[i].index = 0;
            discards[i].current = FM.pipeTypes.pipeTypes[i].pipes[0];
        }

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
            //if (!discardsStarted[(int)objectType])
            //{
            //    var current = discards[(int)objectType];
            //    current.index = 0;
            //    current.current = FM.pipeTypes.pipeTypes[(int)objectType].pipes[current.index];

            //    var oldC = FM.pipeTypes.pipeTypes[(int)objectType].fillColor;

            //    for (int i = 0; i < FM.pipeTypes.pipeTypes[(int)objectType].pipes.Count; i++)
            //    {
            //        var cd = FM.pipeTypes.pipeTypes[(int)objectType].pipes[i];
            //        cd.oldColor = oldC;
            //    }

            //    discardsStarted[(int)objectType] = true;
            //    StartCoroutine(BeginFlow(current, (int)objectType));
            //}
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
            Color col = FM.pipeTypes.pipeTypes[(int)objectType].pipes[i].oldColor;
            currentDiscard = FM.GetCurrentPipes().pipes[i];
            currentDiscard.end = pos;
            currentDiscard.oldColor = col;
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

    public IEnumerator BeginFlow(DiscardData current, int type)
    {       
        while (true)
        {
            if (current.current.finishedMoving)
            {
                for (int i = 0; i < discards.Length - 1; i++)
                {
                    if (i == type)
                    {
                        continue;
                    }

                        FM.pipeTypes.pipeTypes[i].pipes[current.index].Reset();
                    
                }

                Color oc = current.current.oldColor;
                current.current.oldColor = FM.pipeTypes.pipeTypes[type].fillColor;

                current.index++;

                if (current.index > FM.pipeTypes.pipeTypes[type].pipes.Count - 1)
                {
                    current.index = FM.pipeTypes.pipeTypes[type].pipes.Count - 1;
                    discardsStarted[type] = false;

                    yield return null;
                }

                current.current = FM.pipeTypes.pipeTypes[type].pipes[current.index];
            }

            current.current.SetDiscardData(sr, col, FM.pipeTypes.pipeTypes[type].fillColor);
            current.current.begin = true;

            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    public void Reset(SpriteRenderer sr)
    {
        sr.material.SetVector("_DiscardPlane", new Vector4(0, 0, 0, 0));
        sr.material.SetFloat("_UseDiscardPlane", 0);
        sr.material.SetVector("_DiscardedPixelsColor", new Vector4(0, 0, 0, 0));
    }
}
