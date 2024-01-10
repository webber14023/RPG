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
    public List<ItemData> playerQuickItemList = new List<ItemData>();

    public List<AbilityData> playerAbilitys = new List<AbilityData>();
    public List<string> playerUseAbilityList = new List<string>();

    [System.Serializable]
    public class ItemData {
        public string itemName = "";
        public int itemCount;
        public int itemLevel;
        public string itemQuality = "";
    }
    [System.Serializable]
    public class AbilityData {
        public string abilityName = "";
        public int abilityLevel;
    }
}
