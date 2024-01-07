using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class Save
{
    public int playerLevel;
    public int money;
    public int playerExp;
    public int AbilityPoint;

    public List<ItemData> playerBag = new List<ItemData>();
    public List<ItemData> PlayerEquipment = new List<ItemData>();

    [System.Serializable]
    public class ItemData {
        public string itemName = "";
        public int itemCount;
        public int itemLevel;
        public string itemQuality = "";
    }
}
