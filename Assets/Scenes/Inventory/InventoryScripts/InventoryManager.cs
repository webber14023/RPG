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
    // public Slot slotPrefab;
    public GameObject emptySlot;
    public Text itemInformation;

    public List<GameObject> myBagSlots = new List<GameObject>();
    public List<GameObject> equipmentSlots = new List<GameObject>();

    void Awake() {
        if(intance != null)
            Destroy(this);
        intance = this;
    }
    private void OnEnable() {
        RefreshItem();
        intance.itemInformation.text = "";
    }

    public static void UpdateItemInfo(string itemDiscription) {
        intance.itemInformation.text = itemDiscription;
    }


    public static void RefreshItem() {
        //循環刪除slotGrid下的子集物體
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
        for(int i = 0; i < intance.equipment.itemList.Count; i++) {
            Debug.Log(intance.equipment.itemList[i]);
            if (intance.equipment.itemList[i] != null) {
                Debug.Log("Add data");
                intance.equipmentSlots[i].transform.GetChild(1).gameObject.SetActive(true);
                intance.equipmentSlots[i].GetComponent<Slot>().SetupSlot(intance.equipment.itemList[i], intance.equipment.itemListData[i].itemLevel, intance.equipment.itemListData[i].itemQuality);
            }
            else if(intance.equipmentSlots[i].GetComponent<Slot>().slotItem != null) {
                Debug.Log("clear data");
                intance.equipmentSlots[i].GetComponent<Slot>().clearItemData();
                intance.equipmentSlots[i].GetComponent<Slot>().SetupSlot(null, 0, null);
                intance.equipmentSlots[i].transform.GetChild(1).gameObject.SetActive(false);
                
            }
            
        }
    }
}
