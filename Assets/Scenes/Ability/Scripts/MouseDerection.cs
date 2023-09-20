using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDerection : MonoBehaviour
{
    public GameObject target;
    Vector2 mouseDerection;
    public Vector2 GetMouseDerection()
    {
        mouseDerection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - target.transform.position).normalized;
        return mouseDerection;
    }
}
