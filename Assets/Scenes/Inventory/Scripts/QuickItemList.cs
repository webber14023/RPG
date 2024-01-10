using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerAbilityList", menuName = "Inventory/New QuickItemList")]
public class QuickItemList : ScriptableObject
{
    public List<ItemData> itemDatas = new List<ItemData>();

    [System.Serializable]public struct ItemData {
        public Item item;
        public string itemLocation;
        public int itemID;
    }
}