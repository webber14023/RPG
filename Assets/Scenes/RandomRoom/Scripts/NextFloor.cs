using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class NextFloor : interactivityObject
{
    RoomGenerator genrator;

    public override void Start() {
        base.Start();
        genrator = transform.parent.GetComponent<RoomGenerator>();
    }

    public override void Interact() {
        Debug.Log(genrator.Data.floor);
        genrator.Data.floor += 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
