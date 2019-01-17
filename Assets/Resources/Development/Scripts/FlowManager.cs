using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pipes
{
    public List<Discard> pipes;
    public Color fillColor;
}

[System.Serializable]
public class PipeTypes
{
    public List<Pipes> pipeTypes;
}

public class FlowManager : MonoBehaviour
{
    [SerializeField]
    public PipeTypes pipeTypes;

    public ObjectTypeEnum currentType;
    public float fillTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Pipes GetCurrentPipes()
    {
        int index = (int)currentType;

        return pipeTypes.pipeTypes[index];
    }
}
