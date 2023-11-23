using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class interactivityObject : MonoBehaviour
{
    public KeyCode key;
    GameObject Hintkey;

    public virtual void Start() {
        Hintkey = transform.GetChild(0).gameObject;
        Hintkey.transform.GetChild(1).GetComponent<Text>().text = key.ToString();
        Hintkey.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Hintkey.SetActive(true);
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            if (Input.GetKey(key)) {
                Interact();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        Hintkey.SetActive(false);
    }

    public virtual void Interact() {}
}
