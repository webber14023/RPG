﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{   
    public int slotID;//空格ID 等於 物品ID
    public Item slotItem;
    public Image slotImage;
    public Text slotNum; 
    public string slotInfo;

    public GameObject itemInSlot;

    public void ItemOnClick() {
        InventoryManager.UpdateItemInfo(slotInfo);
    }

    public void SetupSlot(Item item) {
        if(item == null) {
            itemInSlot.SetActive(false);
            return;
        }
        slotImage.sprite = item.ItemImage;
        slotNum.text = item.ItemHeld.ToString();
        slotInfo = item.ItemInfo;
    }
}

