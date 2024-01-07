using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class CraftSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{   
    public int slotID;//空格ID 等於 物品ID
    public GameObject ItemDisciptionPrefab;
    public Item slotItem;
    public Image slotImage;
    public Text slotNum; 
    public string slotInfo;
    GameObject ItemPanel;

    public int Level;
    string Quality;

    public GameObject itemInSlot;


    public void OnPointerEnter(PointerEventData data)
    {
        if(!Input.GetKey(KeyCode.Mouse0)) {
            if (slotItem != null && ItemPanel == null) {
            ItemPanel = Instantiate(ItemDisciptionPrefab,Input.mousePosition, Quaternion.identity, transform.parent.parent.parent.parent.parent.parent.parent);
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

    public void SetupSlot(Item item, int itemLevel, string itemQuality, int count) {
        if(item == null) {
            itemInSlot.SetActive(false);
            GetComponent<Image>().raycastTarget = true;
            return;
        }
        if(item.type != "" && item.type != "Functional") {
            EquipmentStats stats = gameObject.AddComponent(typeof(EquipmentStats)) as EquipmentStats;
            stats.SetEquipmentStats((Equipment)item, itemLevel);
        }
        GetComponent<Image>().raycastTarget = false;
        slotImage.sprite = item.ItemImage;
        slotItem = item;
        Level = item.baseLevel == 0? itemLevel: item.baseLevel;
        Quality = item.baseQuality == ""? itemQuality : item.baseQuality;
        itemInSlot.SetActive(true);
        if(item.isStackable)
            slotNum.text = count.ToString();
        else if(slotNum != null)
            slotNum.gameObject.SetActive(false);
        slotInfo = item.ItemInfo;
    }

    public void MarkingLack() {
        slotNum.color = Color.red;
        slotImage.color = new Color(0.5f, 0.5f, 0.5f);

    }
}

