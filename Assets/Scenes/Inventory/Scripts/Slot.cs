using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using System;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{   
    public int slotID;//空格ID 等於 物品ID
    public GameObject ItemDisciptionPrefab;
    public GameObject ItemMenuPrefab;
    public Item slotItem;
    public Image slotImage;
    public Text slotNum; 
    public string slotInfo;
    GameObject ItemPanel;
    GameObject ItemMenu;

    public int Level;
    string Quality;
    bool isClick, isEnter;
    float time;

    public GameObject itemInSlot;

    void Update() {
        if(time > 0)
            time -= Time.deltaTime;
            if(time <= 0) 
                isClick = false;
                
        if(slotItem != null && isEnter && Input.GetKeyDown(KeyCode.Mouse1) && !Input.GetKey(KeyCode.Mouse0)) {
            if(ItemMenu == null) {
                GameObject oldMenu = GameObject.FindGameObjectWithTag("ItemMenu");
                if(oldMenu != null) {
                    DestroyImmediate(oldMenu);
                }
                ItemMenu = Instantiate(ItemMenuPrefab,Input.mousePosition, Quaternion.identity, transform.parent.parent.transform);
                ItemMenu.transform.GetComponent<ItemMenu>().CurrentSlot = this;
                Destroy(ItemPanel);
            }
            else 
                Destroy(ItemMenu);

        }
    }

    public void ItemOnClick() {
        if(!isClick) {
            isClick = true;
            time = 0.33f;
        }
        else {
            if(slotItem.type == "Functional") {
                slotItem.ItemFunction(transform.parent.name, slotID);
            }
            else if(slotItem.type != null && transform.parent.name == "MyBag") {
                if(!ShopManager.IsShopOpen()) 
                    InventoryManager.QuickEquip(slotID, transform.parent.name, slotItem.type);
                else {
                    InventoryManager.SellItem(slotID, transform.parent.name, Int32.Parse(slotNum.text));
                }
            }
            
            return;
        }
            
    }

    public void OnPointerEnter(PointerEventData data)
    {
        isEnter = true;
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
        isEnter = false;
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
        Level = itemLevel;
        Quality = itemQuality;
        itemInSlot.SetActive(true);
        if(item.isStackable)
            slotNum.text = count.ToString();
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

