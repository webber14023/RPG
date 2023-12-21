using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnWorld : interactivityObject
{
    public Inventory playerInventory;
    [SerializeField]AudioClip dropSound;
    [SerializeField]AudioClip pickSound;
    Item ThisItem;
    bool bagfull;
    int itemLevel;

    SpriteRenderer sp;
    AudioSource Audio;

    public override void Start() {
        base.Start();

        sp = GetComponent<SpriteRenderer>();
        Audio = GetComponent<AudioSource>();
        Audio.clip = dropSound;
        Audio.Play();
        sp.sprite = ThisItem.ItemImage;
    }

    public override void Interact() {
        Audio.clip = pickSound;
        Audio.Play();
        AddNewItem();

    }
    /*
    private void Start() {
        sp = GetComponent<SpriteRenderer>();
        Audio = GetComponent<AudioSource>();
        Audio.clip = dropSound;
        Audio.Play();
        sp.sprite = ThisItem.ItemImage;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            Audio.clip = pickSound;
            Audio.Play();
            AddNewItem();
        }
    }*/

    public void AddNewItem() {
        bagfull = true;
        for(int i=0; i < playerInventory.itemList.Count; i++){
            if(playerInventory.itemList[i] == null) {
                bagfull = false;
                break;
            }
        }
        if(!playerInventory.itemList.Contains(ThisItem) || !ThisItem.isStackable) {
            if (bagfull)
                return;

            for(int i = 0; i < playerInventory.itemList.Count; i++) {
                if(playerInventory.itemList[i] == null) {
                    playerInventory.itemList[i] = ThisItem;
                    var temp = playerInventory.itemListData[i];
                    temp.itemLevel = itemLevel;
                    temp.itemQuality = "優良";
                    playerInventory.itemListData[i] = temp;
                    sp.enabled = false;
                    GetComponent<BoxCollider2D>().enabled = false;
                    Destroy(gameObject,0.5f);
                    break;
                }
            }
        }
        else {
            ThisItem.ItemHeld += 1;
            Destroy(gameObject);
        }
        InventoryManager.RefreshItem();
    }
    public void setItemData(Item itemData, int level) {
        ThisItem = itemData;
        itemLevel = level;
    }
}
