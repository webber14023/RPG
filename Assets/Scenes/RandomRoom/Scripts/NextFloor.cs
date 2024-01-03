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
        if(genrator == null)
            genrator = transform.parent.GetComponent<BossRoom>().genrator;
    }

    public override void Interact() {
        genrator.Data.floor += 1;
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        player.position = Vector3.zero;
        player.GetComponent<PlayerMove>().ClearAllAttackEffect();
        Camera.main.GetComponent<CameraMove>().BlackScreen();
        Camera.main.transform.position = new Vector3(0f, 0f, -10f);
        GameObject.FindGameObjectWithTag("RoomGenerator").transform.GetComponent<RoomGenerator>().GenerateAllRoom();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
