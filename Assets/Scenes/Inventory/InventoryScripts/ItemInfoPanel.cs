using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoPanel : MonoBehaviour
{
    public Text itemName;
    public Text itemLevel;
    public Text itemQuality;
    public Text itemInfo;

    void Update()
    {
        transform.position = Input.mousePosition;
        if(Input.GetKey(KeyCode.Mouse0)) {
            Destroy(gameObject);
        }
    }

    public void UpdateItemInfo(Item item, Transform slot, int Level, string Quality) {
        string Information = item.ItemInfo + "\n\n";
        if(item.type != null) {
            Equipment equipment = (Equipment)item;
            EquipmentStats stats = slot.GetComponent<EquipmentStats>();
            if(equipment.equipmentHp != 0) Information += "最大生命 : " + stats.equipmentHp + "\n";
            if(equipment.equipmentSpeed != 0) Information += "移動速度 : " + stats.equipmentSpeed + "\n";
            if(equipment.equipmentAttackDamage != 0) Information +="攻擊傷害 : " +  stats.equipmentAttackDamage + "\n";
        }

        itemName.text = item.ItemName;
        itemLevel.text = "道具等級 : " + Level.ToString();
        itemQuality.text = Quality;
        itemInfo.text = Information;
    }
}
