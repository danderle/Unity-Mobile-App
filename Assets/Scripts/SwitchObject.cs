using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchObject : MonoBehaviour
{
    public Transform Target;

    private void OnCollisionEnter(Collision col)
    {
        if(Target != null)
            Destroy(Target.gameObject);
    }
}
