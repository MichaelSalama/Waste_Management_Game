using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearSpinningScript : MonoBehaviour
{
    public float gearSpeed;

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, gearSpeed) * Time.deltaTime * 20);
    }
}
