using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoPanel : MonoBehaviour
{
    public Transform BackGround;
    public RectTransform infoTransform;
    public Transform Corner;
    public Text itemName;
    public Text itemLevel;
    public Text itemQuality;
    public Text itemPrize;
    public Text itemInfo;

    Vector3 cornerPos;
    Vector3 changePos;

    void Start() {
        transform.position = Input.mousePosition;
        cornerPos = Corner.position;
        cornerPos.x = BackGround.GetComponent<RectTransform>().rect.width;
        cornerPos.y = BackGround.GetComponent<RectTransform>().rect.height;
        if(cornerPos.x > Screen.width) 
            changePos.x += Screen.width - cornerPos.x;
        if(cornerPos.y < 0) 
            changePos.y -= cornerPos.y;

        transform.position += changePos;
    }

    void FixedUpdate()
    {
        transform.position = Input.mousePosition;
        cornerPos = Corner.position;
        changePos = new Vector3(0,0);
        if(cornerPos.x > Screen.width) 
            changePos.x += Screen.width - cornerPos.x;
        if(cornerPos.y < 0) 
            changePos.y -= cornerPos.y;

        transform.position += changePos;

        float itemInfoHeight = infoTransform.rect.height / 4;
        BackGround.GetComponent<RectTransform>().sizeDelta = new Vector2 (207, 108f + itemInfoHeight);

        if(Input.GetKey(KeyCode.Mouse0)) {
            Destroy(gameObject);
        }
    }

    public void UpdateItemInfo(Item item, Transform slot, int Level, string Quality) {
        string Information = item.ItemInfo + "\n\n";
        if(item.type != "" && item.type != "Functional") {
            Equipment equipment = (Equipment)item;
            EquipmentStats stats = slot.GetComponent<EquipmentStats>();
            if(equipment.equipmentHp != 0) Information += "最大生命 : " + stats.equipmentHp + "\n";
            if(equipment.equipmentSpeed != 0) Information += "移動速度 : " + stats.equipmentSpeed + "\n";
            if(equipment.equipmentAttackDamage != 0) Information +="物理攻擊 : " +  stats.equipmentAttackDamage + "\n";
            if(equipment.equipmentAbilityPower != 0) Information +="魔法攻擊 : " +  stats.equipmentAbilityPower + "\n";
            if(equipment.equipmentAttackArmor != 0) Information +="物理防禦 : " +  stats.equipmentAttackArmor + "\n";
            if(equipment.equipmentMagicArmor != 0) Information +="魔法防禦 : " +  stats.equipmentMagicArmor + "\n";
            if(equipment.equipmentCriticalChance != 0) Information +="爆擊率 : " +  stats.equipmentCriticalChance * 100 + "%\n";
            if(equipment.equipmentCriticalMultiply != 0) Information +="爆擊傷害 : " +  stats.equipmentCriticalMultiply * 100 + "%\n";
        }

        itemName.text = item.ItemName;
        itemLevel.text = "道具等級 : " + Level.ToString();
        itemQuality.text = Quality;
        if(slot.parent.name == "Shop Grid")
            itemPrize.text = "價格 : " + ((int)Mathf.Round(item.prize * Mathf.Pow(item.prizePerLv, Level))).ToString();
        else
            itemPrize.text = "價格 : " + ((int)Mathf.Round(item.prize * Mathf.Pow(item.prizePerLv, Level) * 0.8f)).ToString();
        itemInfo.text = Information;
        StartCoroutine(ChangeBGSize());

    }

    IEnumerator ChangeBGSize()
    {
        yield return new WaitForSeconds(.01f);
        float itemInfoHeight = infoTransform.rect.height / 4;
        BackGround.GetComponent<RectTransform>().sizeDelta = new Vector2 (207, 110f + itemInfoHeight);
    }
}
