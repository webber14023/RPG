using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemOnDrag : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public Transform originalParent;
    public Inventory myBag;
    private int currentItemID;//當前物品ID

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        currentItemID = originalParent.GetComponent<Slot>().slotID;
        transform.SetParent(transform.parent.parent);
        transform.position = eventData.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        //Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);//輸出鼠標當前位置下第一個碰到的物件名稱
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(eventData.pointerCurrentRaycast.gameObject != null) {
            if(eventData.pointerCurrentRaycast.gameObject.name == "ItemImage") {//判斷下面物體名字是 ItemImage 那麼互換位置
                //itemList的物品存取位置改變
                var temp = myBag.itemList[currentItemID];
                myBag.itemList[currentItemID] = myBag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID];
                myBag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID] = temp;

                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent);
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.position;
                eventData.pointerCurrentRaycast.gameObject.transform.parent.position = originalParent.position;
                eventData.pointerCurrentRaycast.gameObject.transform.parent.SetParent(originalParent);
                GetComponent<CanvasGroup>().blocksRaycasts = true;
                return;
            }
            if(eventData.pointerCurrentRaycast.gameObject.name == "slot(Clone)") {
                //否則直接掛在檢測到的Slot下面
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
                //itemList的物品存取位置改變
                if(eventData.pointerCurrentRaycast.gameObject.GetComponent<Slot>().slotID != currentItemID){
                    myBag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID] = myBag.itemList[currentItemID];
                    myBag.itemList[currentItemID] = null;
                }

                GetComponent<CanvasGroup>().blocksRaycasts = true;
                return;
            }
        }
        transform.SetParent(originalParent);
        transform.position = originalParent.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
