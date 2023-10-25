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
            intance.myBagSlots[i].GetComponent<Slot>().SetupSlot(intance.myBag.itemList[i]);
        }
        for(int i = 0; i < intance.equipment.itemList.Count; i++) {
            intance.equipmentSlots[i].GetComponent<Slot>().SetupSlot(intance.equipment.itemList[i]);
            if (intance.equipment.itemList[i] != null) {
                intance.equipmentSlots[i].transform.GetChild(1).gameObject.SetActive(true);
            }
            
        }
    }
}

//可以给每个物品一个id,碰撞物体的时候遍历背包里物体的id如果有，就直接在物品的数量上加一，可以提高游戏性能