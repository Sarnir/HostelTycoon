using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailCamera : MonoBehaviour
{
    [HideInInspector]
    public Person Target;

    void Update()
    {
        if(Target != null)
        {
            transform.position = new Vector3(Target.CenterPosition.x, Target.CenterPosition.y, -10f);
        }
    }
}
