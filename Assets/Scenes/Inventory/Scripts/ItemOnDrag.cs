using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemOnDrag : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public Transform originalParent;
    public Inventory orgLocation;
    public Inventory TargetLocation;
    public Inventory myBag;
    public Inventory Equipment;
    private int currentItemID;//當前物品ID

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;
    
        orgLocation = InventoryManager.GetItemLocation(transform.parent.parent.name);
        originalParent = transform.parent;
        currentItemID = originalParent.GetComponent<Slot>().slotID;
        transform.SetParent(transform.parent.parent.parent.parent);
        transform.position = eventData.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;  
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) {
            return;
        }
        transform.position = eventData.position;
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        if(eventData.pointerCurrentRaycast.gameObject != null) {
            if(eventData.pointerCurrentRaycast.gameObject.name != "ItemImage")
                TargetLocation = InventoryManager.GetItemLocation(eventData.pointerCurrentRaycast.gameObject.transform.parent.name);
            else
                TargetLocation = InventoryManager.GetItemLocation(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.parent.name);
            
            if(eventData.pointerCurrentRaycast.gameObject.name == "ItemImage") {
                int targetSlotID = eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID;
                if(orgLocation.itemList[currentItemID] == orgLocation.itemList[targetSlotID]) {
                    var tempTargetData = orgLocation.itemListData[targetSlotID];
                    tempTargetData.count += orgLocation.itemListData[currentItemID].count;
                    orgLocation.itemListData[targetSlotID] = tempTargetData;
                    orgLocation.itemList[currentItemID] = null;
                    orgLocation.itemListData[currentItemID] = new Inventory.Itemdata();
                    InventoryManager.RefreshItem();
                }
                else
                    InventoryManager.SwitchItem(currentItemID, orgLocation, targetSlotID, TargetLocation);
                Destroy(gameObject);
                return;
                /*
                if(orgLocation.itemList[currentItemID] == orgLocation.itemList[targetSlotID]) {
                    var tempTargetData = orgLocation.itemListData[targetSlotID];
                    tempTargetData.count += orgLocation.itemListData[currentItemID].count;
                    orgLocation.itemListData[targetSlotID] = tempTargetData;
                    orgLocation.itemList[currentItemID] = null;
                    orgLocation.itemListData[currentItemID] = new Inventory.Itemdata();
                    Destroy(gameObject);
                    InventoryManager.RefreshItem();
                    return;
                }
                
                var temp = orgLocation.itemList[currentItemID];
                var tempData = orgLocation.itemListData[currentItemID];

                //itemList的物品存取位置改變
                orgLocation.itemList[currentItemID] = orgLocation.itemList[targetSlotID];
                orgLocation.itemList[targetSlotID] = temp;
                orgLocation.itemListData[currentItemID] = orgLocation.itemListData[targetSlotID];
                orgLocation.itemListData[targetSlotID] = tempData;

                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent);
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.position;
                eventData.pointerCurrentRaycast.gameObject.transform.parent.position = originalParent.position;
                eventData.pointerCurrentRaycast.gameObject.transform.parent.SetParent(originalParent);
                GetComponent<CanvasGroup>().blocksRaycasts = true;
                InventoryManager.RefreshItem();*/
            }

            if(eventData.pointerCurrentRaycast.gameObject.name == "slot(Clone)" || eventData.pointerCurrentRaycast.gameObject.name == orgLocation.itemList[currentItemID].type) {
                int targetSlotID = eventData.pointerCurrentRaycast.gameObject.GetComponent<Slot>().slotID;
                InventoryManager.SwitchItem(currentItemID, orgLocation, targetSlotID, TargetLocation);
                Destroy(gameObject);
                return;
                //否則直接掛在檢測到的Slot下面
                /*transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
                //itemList的物品存取位置改變
                if(eventData.pointerCurrentRaycast.gameObject.GetComponent<Slot>().slotID != currentItemID){
                    TargetLocation.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponent<Slot>().slotID] = orgLocation.itemList[currentItemID];
                    orgLocation.itemList[currentItemID] = null;
                    TargetLocation.itemListData[eventData.pointerCurrentRaycast.gameObject.GetComponent<Slot>().slotID] = orgLocation.itemListData[currentItemID];
                    orgLocation.itemListData[currentItemID] = new Inventory.Itemdata();
                    InventoryManager.RefreshItem();
                    EquipmentManager.UpdateEquipmentStats();
                }

                GetComponent<CanvasGroup>().blocksRaycasts = true;
                return;*/
            }
            else if(eventData.pointerCurrentRaycast.gameObject.name == "DropArea") {
                InventoryManager.DropItem(currentItemID, orgLocation);
                Destroy(gameObject);
                return;
                /*
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                ItemOnWorld dropitemData = Instantiate((GameObject)Resources.Load("items/itemPrefab"), player.transform.position + (Vector3)Random.insideUnitCircle * 2, Quaternion.identity).GetComponent<ItemOnWorld>();
                dropitemData.setItemData(orgLocation.itemList[currentItemID], orgLocation.itemListData[currentItemID].itemLevel, orgLocation.itemListData[currentItemID].count);
                orgLocation.itemList[currentItemID] = null;
                orgLocation.itemListData[currentItemID] = new Inventory.Itemdata();
                InventoryManager.RefreshItem();
                EquipmentManager.UpdateEquipmentStats();
                Destroy(gameObject);
                return;*/
            }
        }
        transform.SetParent(originalParent);
        transform.position = originalParent.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void stopDrag() {
        transform.SetParent(originalParent);
        transform.position = originalParent.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
