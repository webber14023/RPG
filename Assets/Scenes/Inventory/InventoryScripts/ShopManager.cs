using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameObject slotGrid;
    public GameObject slotPrefab;

    public Inventory testInv;

    public List<GameObject> Slots = new List<GameObject>();
    
    void Start() {
        UpdateShop(testInv);
    }

    public void UpdateShop(Inventory shopInventory) {
        for(int i=0; i<slotGrid.transform.childCount; i++) {
            Destroy(slotGrid.transform.GetChild(i).gameObject);
        }
        Slots.Clear();

        for(int i=0; i<shopInventory.itemList.Count; i++) {
            Slots.Add(Instantiate(slotPrefab));
            Slots[i].transform.SetParent(slotGrid.transform);
            Slots[i].transform.localScale = new Vector3(1,1,1);
            ShopSlot slotData = Slots[i].GetComponent<ShopSlot>();
            slotData.slotID = i;
            slotData.SetupSlot(shopInventory.itemList[i], shopInventory.itemListData[i].itemLevel, shopInventory.itemListData[i].itemQuality, shopInventory.itemListData[i].count);
        }

    }
}
