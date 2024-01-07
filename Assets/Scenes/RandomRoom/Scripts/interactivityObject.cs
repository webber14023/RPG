using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class interactivityObject : MonoBehaviour
{
    public KeyCode key;
    public string hintText;
    public GameObject Hintkey;
    public string ColliderTag = "Player";
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
        if(key != KeyCode.Mouse0) {
            if(canInteract && Input.GetKeyDown(key)) {
                Interact();
            }
        }
    }

    private void OnMouseOver() {
        Debug.Log("mouseIn");
        if(key == KeyCode.Mouse0 && canInteract) {
            if(Input.GetKeyDown(KeyCode.Mouse0)) {
                Interact();
                Debug.Log("clickDropItem");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag(ColliderTag)) {
            Debug.Log(PlayerMove.PlayerInteractStats());
            if(!PlayerMove.PlayerInteractStats()) {
                canInteract = true;
                Hintkey.SetActive(true);
                Hintkey.transform.rotation = Quaternion.identity;
            }
            else {
                canInteract = false;
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag(ColliderTag)) {
            canInteract = false;
            Hintkey.SetActive(false);
        }
    }

    public virtual void Interact() {}
}
