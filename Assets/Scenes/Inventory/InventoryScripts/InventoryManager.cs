using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    static InventoryManager intance;
    
    public Inventory myBag;
    public GameObject slotGrid;
    // public Slot slotPrefab;
    public GameObject emptySlot;
    public Text itemInformation;

    public List<GameObject> slots = new List<GameObject>();

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

    /*public static void CreateNewItem(Item item) {
        Slot newItem = Instantiate(intance.slotPrefab,intance.transform.position,Quaternion.identity);
        newItem.gameObject.transform.SetParent(intance.slotGrid.transform);
        newItem.slotItem = item;
        newItem.slotImage.sprite = item.ItemImage;
        newItem.slotNum.text = item.ItemHeld.ToString();
    }*/

    public static void RefreshItem() {
        //循環刪除slotGrid下的子集物體
        for(int i = 0; i < intance.slotGrid.transform.childCount; i++) {
            Destroy(intance.slotGrid.transform.GetChild(i).gameObject);
            intance.slots.Clear();
        }
        //重新生成對應myBag裡面物品的slot
        for(int i = 0; i < intance.myBag.itemList.Count; i++) {
            //CreateNewItem(intance.myBag.itemList[i]);
            intance.slots.Add(Instantiate(intance.emptySlot));
            intance.slots[i].transform.SetParent(intance.slotGrid.transform);
            intance.slots[i].transform.localScale = new Vector3(1,1,1);
            intance.slots[i].GetComponent<Slot>().slotID = i;
            intance.slots[i].GetComponent<Slot>().SetupSlot(intance.myBag.itemList[i]);
        }
    }
}

//可以给每个物品一个id,碰撞物体的时候遍历背包里物体的id如果有，就直接在物品的数量上加一，可以提高游戏性能