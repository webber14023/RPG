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

    public void OnPointerEnter(PointerEventData data)
    {
        if(!Input.GetKey(KeyCode.Mouse0)) {
            if (slotItem != null && ItemPanel == null) {
            ItemPanel = Instantiate(ItemDisciptionPrefab,Input.mousePosition, Quaternion.identity, transform.parent.parent.transform);
            ItemInfoPanel Info = ItemPanel.GetComponent<ItemInfoPanel>();
            Info.UpdateItemInfo(slotItem, transform, Level, Quality);
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
            GetComponent<Image>().raycastTarget = true;
            return;
        }
        if(item.type != null) {
            EquipmentStats stats = gameObject.AddComponent(typeof(EquipmentStats)) as EquipmentStats;
            stats.SetEquipmentStats((Equipment)item, itemLevel);
        }
        GetComponent<Image>().raycastTarget = false;
        slotImage.sprite = item.ItemImage;
        slotItem = item;
        Level = itemLevel;
        Quality = itemQuality;
        itemInSlot.SetActive(true);
        if(item.isStackable)
            slotNum.text = item.ItemHeld.ToString();
        else if(slotNum != null)
            slotNum.gameObject.SetActive(false);
        slotInfo = item.ItemInfo;
    }

    public void clearItemData() {
        slotItem = null;
        Level = 0;
        Quality = null;

    }
}

