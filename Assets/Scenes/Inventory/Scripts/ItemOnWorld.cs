using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnWorld : interactivityObject
{
    public Inventory playerInventory;
    public Transform PickColider;
    [SerializeField]AudioClip dropSound;
    [SerializeField]AudioClip pickSound;
    Item ThisItem;
    bool bagfull;
    int itemLevel;
    int itemCount;
    string itemQuality;

    SpriteRenderer sp;
    AudioSource Audio;

    public override void Start() {
        hintText = "撿起 " + ThisItem.ItemName;
        key = KeyCode.Mouse0;
        ColliderTag = "PlayerPickCollider";
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
        int EmptyID = InventoryManager.FindEmptyID(InventoryManager.GetItemLocation("MyBag"));
        if(EmptyID == -1) {
            if(InventoryManager.FindItemIDInPlayerBag(ThisItem) == -1 || !ThisItem.isStackable) 
                return;
        }
        PickColider.GetComponent<BoxCollider2D>().enabled = false;
        InventoryManager.AddItem(ThisItem, itemLevel, itemCount, "優良");
        sp.enabled = false;
        Destroy(gameObject,0.3f);

    }
    public void setItemData(Item itemData, int level, int Count, string Quality) {
        ThisItem = itemData;
        itemLevel = level;
        itemCount = Count;
        itemQuality = Quality;

    }
}
