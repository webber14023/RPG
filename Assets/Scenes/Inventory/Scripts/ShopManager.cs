using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    static ShopManager intance;
    public GameObject slotGrid;
    public GameObject slotPrefab;
    public GameObject PlayerBag;
    public GameObject ShopPanel;

    public List<GameObject> Slots = new List<GameObject>();
    
    void Awake() {
        if(intance != null)
            Destroy(this);
        intance = this;
    }

    public static void UpdateShop(Inventory shopInventory) {
        for(int i=0; i<intance.slotGrid.transform.childCount; i++) {
            Destroy(intance.slotGrid.transform.GetChild(i).gameObject);
        }
        intance.Slots.Clear();

        for(int i=0; i<shopInventory.itemList.Count; i++) {
            intance.Slots.Add(Instantiate(intance.slotPrefab));
            intance.Slots[i].transform.SetParent(intance.slotGrid.transform);
            intance.Slots[i].transform.localScale = new Vector3(1,1,1);
            ShopSlot slotData = intance.Slots[i].GetComponent<ShopSlot>();
            slotData.slotID = i;
            slotData.SetupSlot(shopInventory.itemList[i], shopInventory.itemListData[i].itemLevel, shopInventory.itemListData[i].itemQuality, shopInventory.itemListData[i].count);
        }
    }

    public static bool IsShopOpen() {
        return intance.ShopPanel.activeSelf;
    }

    public static void SetShopPanel(bool state) {
        intance.PlayerBag.SetActive(state);
        intance.ShopPanel.SetActive(state);
    }
}
