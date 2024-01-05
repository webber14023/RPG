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
    private int currentCount;

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        currentItemID = originalParent.GetComponent<Slot>().slotID;
        FindLocation(transform.parent.parent.name, 0);
        if (eventData.button != PointerEventData.InputButton.Left && orgLocation.itemList[currentItemID].isStackable) {
            var data = orgLocation.itemListData[currentItemID];
            currentCount = data.count - (data.count / 2);
            data.count /= 2;
            transform.GetChild(1).GetComponent<Text>().text = currentCount.ToString();
            InventoryManager.RefreshItem();
        }
        transform.SetParent(transform.parent.parent.parent.parent);
        transform.position = eventData.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;  
        
    }

    public void FindLocation(string locationName, int num) {
        if (num == 0){
            switch(locationName)
            {
                case "MyBag":
                    orgLocation = myBag;
                    break;
                case "Equipment":
                    orgLocation = Equipment;
                    break;
            }
        }
        else {
            switch(locationName)
            {
                case "MyBag":
                    TargetLocation = myBag;
                    break;
                case "Equipment":
                    TargetLocation = Equipment;
                    break;
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Drag");
        /*if (eventData.button != PointerEventData.InputButton.Left) {
            return;
        }*/
        transform.position = eventData.position;
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("ENd DRAG");
        /*if (eventData.button != PointerEventData.InputButton.Left)
            return;*/

            //&& eventData.pointerCurrentRaycast.gameObject.CompareTag("slot")
        if(eventData.pointerCurrentRaycast.gameObject != null) {
            if(eventData.pointerCurrentRaycast.gameObject.name != "ItemImage")
                FindLocation(eventData.pointerCurrentRaycast.gameObject.transform.parent.name, 1);
            else
                FindLocation(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.parent.name, 1);
            
            if(eventData.pointerCurrentRaycast.gameObject.name == "ItemImage" && TargetLocation == orgLocation) {//判斷下面物體名字是 ItemImage 那麼互換位置
                int targetSlotID = eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID;

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
                InventoryManager.RefreshItem();
                return;
            }

            if(eventData.pointerCurrentRaycast.gameObject.name == "slot(Clone)" || eventData.pointerCurrentRaycast.gameObject.name == orgLocation.itemList[currentItemID].type) {
                //否則直接掛在檢測到的Slot下面
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
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
                return;
            }
            else if(eventData.pointerCurrentRaycast.gameObject.name == "DropArea") {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                ItemOnWorld dropitemData = Instantiate((GameObject)Resources.Load("items/itemPrefab"), player.transform.position + (Vector3)Random.insideUnitCircle * 2, Quaternion.identity).GetComponent<ItemOnWorld>();
                dropitemData.setItemData(orgLocation.itemList[currentItemID], orgLocation.itemListData[currentItemID].itemLevel, orgLocation.itemListData[currentItemID].count);
                orgLocation.itemList[currentItemID] = null;
                orgLocation.itemListData[currentItemID] = new Inventory.Itemdata();
                InventoryManager.RefreshItem();
                EquipmentManager.UpdateEquipmentStats();
                Destroy(gameObject);
                return;
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
