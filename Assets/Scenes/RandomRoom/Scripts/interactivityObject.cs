using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class interactivityObject : MonoBehaviour
{
    public KeyCode key;
    public string hintText;
    GameObject Hintkey;

    bool canInteract;

    public virtual void Start() {
        if(key == KeyCode.None)
            key = KeyCode.F;

        Hintkey = transform.GetChild(0).gameObject;
        Hintkey.transform.GetChild(1).GetComponent<Text>().text = key.ToString();
        Hintkey.transform.GetChild(2).GetComponent<Text>().text = hintText;
        Hintkey.SetActive(false);
    }
    
    private void Update() {
        if(canInteract && Input.GetKeyDown(key))
            Interact();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            canInteract = true;
            Hintkey.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            canInteract = false;
            Hintkey.SetActive(false);
        }
    }

    public virtual void Interact() {}
}
