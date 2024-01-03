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

    List<string> equipTypes = new List<string>() {"Head", "Body", "Feet", "Ring", "Weapon", "OffHand", "Accessory", "Neck"};
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

    public void QuickEquip(int slotID, string location, string type) {
        Dictionary<string, Inventory> Location = new Dictionary<string, Inventory>() {
            {"MyBag", myBag},
            {"Equipment", equipment}
        };
        Inventory itemLocation = Location[location];
        int equipmentID = equipTypes.IndexOf(type);

        var temp = itemLocation.itemList[slotID];
        var tempData = itemLocation.itemListData[slotID];
        itemLocation.itemList[slotID] = equipment.itemList[equipmentID];
        equipment.itemList[equipmentID] = temp;
        itemLocation.itemListData[slotID] = equipment.itemListData[equipmentID];
        equipment.itemListData[equipmentID] = tempData;
        RefreshItem();
        EquipmentManager.UpdateEquipmentStats();
    }

    public void BuyItem(Item item, int level, string Quality) {
        CharacterStats stats = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<CharacterStats>();
        if(stats.money >= item.prize) {
            bool success = AddNewItem(item, level, 1, Quality);
            if(success) {
                stats.money -= item.prize;
            }
        }
        PlayerMove.UpdatePlayerUI();

    }

    public void SellItem(int slotID, string location) {
        CharacterStats stats = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<CharacterStats>();
        Dictionary<string, Inventory> Location = new Dictionary<string, Inventory>() {
            {"MyBag", myBag},
            {"Equipment", equipment}
        };
        Inventory itemLocation = Location[location];
        stats.money += itemLocation.itemList[slotID].prize;
        itemLocation.itemList[slotID] = null;
        itemLocation.itemListData[slotID] = new Inventory.Itemdata();
        RefreshItem();
        EquipmentManager.UpdateEquipmentStats();
        
    }

    public bool AddNewItem(Item item, int Level, int count, string Quality) {
        bool bagfull = true;
        if(myBag.itemList.Contains(item)) {
            int itemID = myBag.itemList.IndexOf(item);
            var temp = myBag.itemListData[itemID];
            temp.count += count;
            myBag.itemListData[itemID] = temp;
        }
        for(int i=0; i < myBag.itemList.Count; i++){
            if(myBag.itemList[i] == null) {
                bagfull = false;
                break;
            }
        }
        if(!myBag.itemList.Contains(item) || !item.isStackable) {
            if (bagfull)
                return false;

            for(int i = 0; i < myBag.itemList.Count; i++) {
                if(myBag.itemList[i] == null) {
                    myBag.itemList[i] = item;
                    var temp = myBag.itemListData[i];
                    temp.itemLevel = Level;
                    temp.itemQuality = Quality;
                    myBag.itemListData[i] = temp;
                    break;
                }
            }
        }
        InventoryManager.RefreshItem();
        return true;
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
            intance.equipmentSlots[i].GetComponent<Slot>().SetupSlot(intance.equipment.itemList[i], intance.equipment.itemListData[i].itemLevel, intance.equipment.itemListData[i].itemQuality, intance.equipment.itemListData[i].count);
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
            intance.myBagSlots[i].GetComponent<Slot>().SetupSlot(intance.myBag.itemList[i], intance.myBag.itemListData[i].itemLevel, intance.myBag.itemListData[i].itemQuality, intance.myBag.itemListData[i].count);
        }

    }
}
