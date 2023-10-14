using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnWorld : MonoBehaviour
{
    public Item ThisItem;
    public Inventory playerInventory;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            AddNewItem();
            Destroy(gameObject);
        }
    }
    public void AddNewItem() {
        if(!playerInventory.itemList.Contains(ThisItem) || !ThisItem.isStackable) {
            Debug.Log("Add New Item");
            for(int i = 0; i < playerInventory.itemList.Count; i++) {
                if(playerInventory.itemList[i] == null) {
                    playerInventory.itemList[i] = ThisItem;
                    break;
                }
            }
        }
        else {
            ThisItem.ItemHeld += 1;
        }
        InventoryManager.RefreshItem();
    }
}
