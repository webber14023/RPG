using System.Collections.Generic;
using System.Linq;
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

    public AudioClip SellItemSound;
    public AudioClip BuyItemSound;

    public AudioSource SoundSource;

    List<Item> allItem = new List<Item>();

    void Awake() {
        if(intance != null)
            Destroy(this);
        intance = this;
    }
    private void OnEnable() {
        allItem = ((Item[])Resources.FindObjectsOfTypeAll(typeof(Item))).ToList();
        RefreshItem();
    }

    public static void UpdateItemInfo(string itemDiscription) {
        intance.itemInformation.text = itemDiscription;
    }

    public static Inventory GetItemLocation(string loactionName) {
        Dictionary<string, Inventory> Location = new Dictionary<string, Inventory>() {
            {"MyBag", intance.myBag},
            {"Equipment", intance.equipment}
        };
        if(Location.TryGetValue(loactionName, out Inventory value)) {
            return value;
        }
        return null;
    }

    public static int FindEmptyID(Inventory targetInv) {
        for(int i=0; i < targetInv.itemList.Count; i++){
            if(targetInv.itemList[i] == null) {
                return i;
            }
        }
        return -1;
    }

    public static void QuickEquip(int slotID, string location, string type) {
        Debug.Log("QuickEquip");
        Inventory itemLocation = GetItemLocation(location);
        int equipmentID = intance.equipTypes.IndexOf(type);

        var temp = itemLocation.itemList[slotID];
        var tempData = itemLocation.itemListData[slotID];
        itemLocation.itemList[slotID] = intance.equipment.itemList[equipmentID];
        intance.equipment.itemList[equipmentID] = temp;
        itemLocation.itemListData[slotID] = intance.equipment.itemListData[equipmentID];
        intance.equipment.itemListData[equipmentID] = tempData;
        RefreshItem();
        EquipmentManager.UpdateEquipmentStats();
        EquipmentManager.PlayEquipSound();
    }

    public static void BuyItem(Item item, int level, string Quality, int count) {
        CharacterStats stats = PlayerMove.GetPlayerStats();
        int realPrize = (int)Mathf.Round(item.prize * Mathf.Pow(item.prizePerLv, level)) * count;
        if(stats.money >= realPrize) {
            if(FindEmptyID(intance.myBag) != -1) {
                AddItem(item, level, count, Quality);
                stats.money -= realPrize;
            }
        }
        intance.SoundSource.PlayOneShot(intance.BuyItemSound);
        PlayerMove.UpdatePlayerUI();
    }

    public static void SellItem(int slotID, string location, int SellCount) {
        CharacterStats stats = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<CharacterStats>();
        Inventory itemLocation = GetItemLocation(location);
        int itemPrize = itemLocation.itemList[slotID].prize;
        int realPrize = (int)Mathf.Round(itemPrize * Mathf.Pow(itemLocation.itemList[slotID].prizePerLv, itemLocation.itemListData[slotID].itemLevel) * 0.8f);
        stats.money += realPrize * SellCount;
        ReduceItem(slotID, location, SellCount);
        intance.SoundSource.PlayOneShot(intance.SellItemSound);
        RefreshItem();
        EquipmentManager.UpdateEquipmentStats();
    }

    /*public static void DropItem(int slotID, string location) {
        Inventory itemLocation = GetItemLocation(location);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        ItemOnWorld dropitemData = Instantiate((GameObject)Resources.Load("items/itemPrefab"), player.transform.position + (Vector3)Random.insideUnitCircle * 2, Quaternion.identity).GetComponent<ItemOnWorld>();
        dropitemData.setItemData(itemLocation.itemList[slotID], itemLocation.itemListData[slotID].itemLevel, itemLocation.itemListData[slotID].count);
        itemLocation.itemList[slotID] = null;
        itemLocation.itemListData[slotID] = new Inventory.Itemdata();
        RefreshItem();
        EquipmentManager.UpdateEquipmentStats();
    }*/
    public static void DropItem(int slotID, Inventory itemLocation) {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        ItemOnWorld dropitemData = Instantiate((GameObject)Resources.Load("items/itemPrefab"), player.transform.position, Quaternion.identity).GetComponent<ItemOnWorld>();
        dropitemData.transform.GetComponent<Rigidbody2D>().velocity = (Vector3)Random.insideUnitCircle * 2;
        dropitemData.setItemData(itemLocation.itemList[slotID], itemLocation.itemListData[slotID].itemLevel, itemLocation.itemListData[slotID].count, itemLocation.itemListData[slotID].itemQuality);
        itemLocation.itemList[slotID] = null;
        itemLocation.itemListData[slotID] = new Inventory.Itemdata();
        RefreshItem();
        EquipmentManager.UpdateEquipmentStats();
    }

    public static void DeleteItem(int slotID, string location) {
        Inventory itemLocation = GetItemLocation(location);
        itemLocation.itemList[slotID] = null;
        itemLocation.itemListData[slotID] = new Inventory.Itemdata();
        RefreshItem();
        EquipmentManager.UpdateEquipmentStats();
    }

    public static void ReduceItem(int slotID, string location, int amount) {
        Inventory itemLocation = GetItemLocation(location);
        var tempData = itemLocation.itemListData[slotID];
        if(tempData.count > amount) {
            tempData.count -= amount;
            itemLocation.itemListData[slotID] = tempData;
        }
        else {
            DeleteItem(slotID, location);
        }
        RefreshItem();
    }
    
    public static void SplitItem(int slotID, string location, int amount) {
        Inventory itemLocation = GetItemLocation(location);
        int emptyID = FindEmptyID(itemLocation);
        if(emptyID == -1)
            return;
        var orgItemData = itemLocation.itemListData[slotID];
        orgItemData.count -= amount;
        intance.myBag.itemListData[slotID] = orgItemData;
        AddNewItem(itemLocation.itemList[slotID], orgItemData.itemLevel, amount, orgItemData.itemQuality, emptyID);
        RefreshItem();

    }

    public static void AddItem(Item item, int Level, int count, string Quality) {
        if(intance.myBag.itemList.Contains(item) && item.isStackable) {
            int itemID = intance.myBag.itemList.IndexOf(item);
            var temp = intance.myBag.itemListData[itemID];
            temp.count += count;
            intance.myBag.itemListData[itemID] = temp;
            RefreshItem();

            return;
        }
        int EmptyID = FindEmptyID(intance.myBag);

        if (EmptyID == -1)
            return;
        if(!intance.myBag.itemList.Contains(item) || !item.isStackable) {
            intance.myBag.itemList[EmptyID] = item;
            var temp = intance.myBag.itemListData[EmptyID];
            
            temp.itemLevel = item.baseLevel == 0? Level: item.baseLevel;
            temp.itemQuality = item.baseQuality == ""? Quality: item.baseQuality;
            temp.count = count;
            intance.myBag.itemListData[EmptyID] = temp;
        }
        RefreshItem();
    }

    public static void AddNewItem(Item item, int Level, int count, string Quality ,int slotID) {
        intance.myBag.itemList[slotID] = item;
        var temp = intance.myBag.itemListData[slotID];
        temp.itemLevel = Level;
        temp.itemQuality = Quality;
        temp.count = count;
        intance.myBag.itemListData[slotID] = temp;
    }

    public static void SwitchItem(int FirstslotID, Inventory FirstLocation, int SecondSlotID, Inventory SecondLocation) {
        var tempItem = FirstLocation.itemList[FirstslotID];
        var tempItemData = FirstLocation.itemListData[FirstslotID];
        FirstLocation.itemList[FirstslotID] = SecondLocation.itemList[SecondSlotID];
        FirstLocation.itemListData[FirstslotID] = SecondLocation.itemListData[SecondSlotID];
        SecondLocation.itemList[SecondSlotID] = tempItem;
        SecondLocation.itemListData[SecondSlotID] = tempItemData;

        RefreshItem();
        EquipmentManager.UpdateEquipmentStats();
    }

    public static int FindItemInPlayerBag(Item item) {
        if(intance.myBag.itemList.Contains(item)) {
            if(item.isStackable) {
                if(intance.myBag.itemListData[intance.myBag.itemList.IndexOf(item)].count > 0)
                    return intance.myBag.itemListData[intance.myBag.itemList.IndexOf(item)].count;
            }
            else
                return 1;

        }
        return -1;
    }

    public static int FindItemIDInPlayerBag(Item findItem) {
        return intance.myBag.itemList.IndexOf(findItem);;
    }

    public static Item FindItemByName(string ItemName) {
        return intance.allItem.Find(x => x.ItemName.Contains(ItemName));
    }

    public static Inventory[] GetAllInventory() {
        Inventory[] AllInv = {intance.myBag, intance.equipment};
        return AllInv;
    }

    public static void ClearAllInvData() {
        intance.myBag.itemList.Clear();
        intance.myBag.itemListData.Clear();
        intance.equipment.itemList.Clear();
        intance.equipment.itemListData.Clear();

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
