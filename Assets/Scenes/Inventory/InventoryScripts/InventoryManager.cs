using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    static InventoryManager intance;
    
    public Inventory myBag;
    public Inventory equipment;
    public GameObject slotGrid;
    public GameObject equipmentSlotGrid;
    // public Slot slotPrefab;
    public GameObject emptySlot;
    public GameObject emptyEquipmentSlot;
    public Text itemInformation;

    public List<GameObject> myBagSlots = new List<GameObject>();
    public List<GameObject> equipmentSlots = new List<GameObject>();

    string[] equipTypes = {"Head", "Body", "Feet", "Ring", "Weapon", "OffHand", "Accessory", "Neck"};
    public Sprite[] equipmentBg;

    void Awake() {
        if(intance != null)
            Destroy(this);
        intance = this;
    }
    private void OnEnable() {
        RefreshItem();
    }

    public static void UpdateItemInfo(string itemDiscription) {
        intance.itemInformation.text = itemDiscription;
    }


    public static void RefreshItem() {
        //循環刪除slotGrid下的子集物體
        for(int i = 0; i < intance.equipmentSlotGrid.transform.childCount; i++) {
            Destroy(intance.equipmentSlotGrid.transform.GetChild(i).gameObject);
            intance.equipmentSlots.Clear();
        }

        for(int i = 0; i < intance.equipment.itemList.Count; i++) {
            intance.equipmentSlots.Add(Instantiate(intance.emptyEquipmentSlot));
            intance.equipmentSlots[i].transform.SetParent(intance.equipmentSlotGrid.transform);
            intance.equipmentSlots[i].transform.localScale = new Vector3(1,1,1);
            intance.equipmentSlots[i].GetComponent<Slot>().slotID = i;
            intance.equipmentSlots[i].gameObject.name = intance.equipTypes[i];
            intance.equipmentSlots[i].transform.GetChild(0).GetComponent<Image>().sprite = intance.equipmentBg[i];
            intance.equipmentSlots[i].GetComponent<Slot>().SetupSlot(intance.equipment.itemList[i], intance.equipment.itemListData[i].itemLevel, intance.equipment.itemListData[i].itemQuality);
        }
        
        for(int i = 0; i < intance.slotGrid.transform.childCount; i++) {
            Destroy(intance.slotGrid.transform.GetChild(i).gameObject);
            intance.myBagSlots.Clear();
        }
        //重新生成對應myBag裡面物品的slot
        for(int i = 0; i < intance.myBag.itemList.Count; i++) {
            intance.myBagSlots.Add(Instantiate(intance.emptySlot));
            intance.myBagSlots[i].transform.SetParent(intance.slotGrid.transform);
            intance.myBagSlots[i].transform.localScale = new Vector3(1,1,1);
            intance.myBagSlots[i].GetComponent<Slot>().slotID = i;
            intance.myBagSlots[i].GetComponent<Slot>().SetupSlot(intance.myBag.itemList[i], intance.myBag.itemListData[i].itemLevel, intance.myBag.itemListData[i].itemQuality);
        }

    }
}
