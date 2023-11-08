using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnWorld : MonoBehaviour
{
    public Item ThisItem;
    public Inventory playerInventory;
    public bool bagfull;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            AddNewItem();
        }
    }
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
                    temp.itemLevel = Random.Range(1,10);
                    temp.itemQuality = "優良";
                    playerInventory.itemListData[i] = temp;
                    Destroy(gameObject);
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
}
