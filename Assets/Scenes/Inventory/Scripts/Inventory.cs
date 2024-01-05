using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/New Inventory")]
public class Inventory : ScriptableObject
{
    public List<Item> itemList = new List<Item>();
    public List<Itemdata> itemListData = new List<Itemdata>();

    [System.Serializable]public struct Itemdata {
        public int itemLevel;
        public int count;
        public string itemQuality;
    }
}
