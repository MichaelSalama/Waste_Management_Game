using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invisiabilityScript : MonoBehaviour
{
    //public ObjectVariable parent;

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
