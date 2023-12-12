using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LeaveDungeon : interactivityObject
{

    public override void Start() {
        base.Start();
    }

    public override void Interact() {
        SceneManager.LoadScene("Vallage");
    }
}
