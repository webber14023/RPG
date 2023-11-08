using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{   
    public int slotID;//空格ID 等於 物品ID
    public GameObject ItemDisciptionPrefab;
    public Item slotItem;
    public Image slotImage;
    public Text slotNum; 
    public string slotInfo;
    GameObject ItemPanel;
    int Level;
    string Quality;

    public GameObject itemInSlot;


    public void ItemOnClick() {
        InventoryManager.UpdateItemInfo(slotInfo);
    }


    public void OnPointerEnter(PointerEventData data)
    {
        if(!Input.GetKey(KeyCode.Mouse0)) {
            if (slotItem != null && ItemPanel == null) {
            ItemPanel = Instantiate(ItemDisciptionPrefab,Input.mousePosition, Quaternion.identity, transform.parent.parent.transform);
            ItemInfoPanel Info = ItemPanel.GetComponent<ItemInfoPanel>();
            Info.UpdateItemInfo(slotItem.ItemName, slotItem.ItemInfo, Level, Quality);
            }
        }
    }

    public void OnPointerExit(PointerEventData data)
    {
        if (slotItem != null)
            Destroy(ItemPanel);
    }

    public void SetupSlot(Item item, int itemLevel, string itemQuality) {
        if(item == null) {
            itemInSlot.SetActive(false);
            return;
        }
        slotImage.sprite = item.ItemImage;
        slotItem = item;
        Level = itemLevel;
        Quality = itemQuality;
        if(item.isStackable)
            slotNum.text = item.ItemHeld.ToString();
        else if(slotNum != null)
            slotNum.gameObject.SetActive(false);
        slotInfo = item.ItemInfo;
    }
}

