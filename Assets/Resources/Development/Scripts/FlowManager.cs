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
    public static FlowManager Instance;

    [SerializeField]
    public PipeTypes pipeTypes;

    public ObjectTypeEnum currentType;
    public float fillTime;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public Pipes GetCurrentPipes()
    {
        int index = (int)currentType;

        return pipeTypes.pipeTypes[index];
    }
}
