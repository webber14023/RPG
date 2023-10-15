﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        FindLocation(transform.parent.parent.name, 0);
        originalParent = transform.parent;
        currentItemID = originalParent.GetComponent<Slot>().slotID;
        transform.SetParent(transform.parent.parent.parent);
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
                default:
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
                default:
                    break;
            }
        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        FindLocation(eventData.pointerCurrentRaycast.gameObject.transform.parent.name, 1);
        Debug.Log(eventData.pointerCurrentRaycast.gameObject.name == "slot(Clone)");
        Debug.Log(eventData.pointerCurrentRaycast.gameObject.name == orgLocation.itemList[currentItemID].type);

        if(eventData.pointerCurrentRaycast.gameObject != null) {
            if(eventData.pointerCurrentRaycast.gameObject.name == "ItemImage" && TargetLocation == orgLocation) {//判斷下面物體名字是 ItemImage 那麼互換位置
                //itemList的物品存取位置改變
                var temp = orgLocation.itemList[currentItemID];
                orgLocation.itemList[currentItemID] = orgLocation.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID];
                orgLocation.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID] = temp;

                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent);
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.position;
                eventData.pointerCurrentRaycast.gameObject.transform.parent.position = originalParent.position;
                eventData.pointerCurrentRaycast.gameObject.transform.parent.SetParent(originalParent);
                GetComponent<CanvasGroup>().blocksRaycasts = true;
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
                }
                if(TargetLocation == Equipment)
                    EquipmentManager.UpdateEquipmentStats();

                GetComponent<CanvasGroup>().blocksRaycasts = true;
                return;
            }

        }
        transform.SetParent(originalParent);
        transform.position = originalParent.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
