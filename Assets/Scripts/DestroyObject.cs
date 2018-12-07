using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public float ForceRequired = 10.0f;

    private void OnCollisionEnter(Collision col)
    {
        if(col.impulse.magnitude > ForceRequired)
        {
            Destroy(gameObject);
        }
    }
}
