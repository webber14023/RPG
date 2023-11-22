using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class NextFloor : MonoBehaviour
{
    RoomGenerator genrator;

    private void Start() {
        genrator = transform.parent.GetComponent<RoomGenerator>();
    }
    private void OnTriggerStay2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            if (Input.GetKey(KeyCode.F)) {
                Debug.Log(genrator.Data.floor);
                genrator.Data.floor += 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
