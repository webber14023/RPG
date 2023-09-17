using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject traceObject;
    
    void Update()
    {
        transform.position = new Vector3 (traceObject.transform.position.x, traceObject.transform.position.y, -10f);
    }
}
