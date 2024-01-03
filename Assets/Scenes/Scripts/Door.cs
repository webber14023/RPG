using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;

public class Door : interactivityObject
{
    public Transform target;

    public override void Start() {
        base.Start();
    }

    public override void Interact() {
        Camera.main.GetComponent<CameraMove>().BlackScreen();
        GameObject.FindGameObjectWithTag("Player").transform.position = target.position;
        Camera.main.transform.position = target.position + new Vector3 (0, 0, -10);
    }
}
