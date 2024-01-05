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
    int itemCount;

    SpriteRenderer sp;
    AudioSource Audio;

    public override void Start() {
        hintText = "撿起 " + ThisItem.ItemName;
        if(ThisItem.isStackable) {
            hintText += " x " + itemCount;
        }
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
        Hintkey.SetActive(false);
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
                    temp.count = itemCount;
                    playerInventory.itemListData[i] = temp;
            
                    break;
                }
            }
        }
        else {
            int itemID = playerInventory.itemList.IndexOf(ThisItem);
            var temp = playerInventory.itemListData[itemID];
            temp.count++;
            playerInventory.itemListData[itemID] = temp;
        }
        sp.enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        InventoryManager.RefreshItem();
        Destroy(gameObject,0.3f);

    }
    public void setItemData(Item itemData, int level, int Count) {
        ThisItem = itemData;
        itemLevel = level;
        itemCount = Count;
    }
}
