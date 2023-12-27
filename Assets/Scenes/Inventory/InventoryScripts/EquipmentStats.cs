using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentStats : MonoBehaviour
{
    public int equipmentHp;
    public float equipmentSpeed;
    public int equipmentAttackDamage;
    public int equipmentAbilityPower;
    public int equipmentAttackArmor;
    public int equipmentMagicArmor;
    public float equipmentCriticalChance;

    public void SetEquipmentStats(Equipment equipment, int Level) {
        equipmentHp = (int)Mathf.Round(equipment.equipmentHp * Mathf.Pow(equipment.equipmentHpPerLv, Level));
        equipmentSpeed = equipment.equipmentSpeed * Mathf.Pow(equipment.equipmentSpeedPerLv, Level);
        equipmentAttackDamage = (int)Mathf.Round(equipment.equipmentAttackDamage * Mathf.Pow(equipment.equipmentAttackDamagePerLv, Level));
        equipmentAbilityPower = (int)Mathf.Round(equipment.equipmentAbilityPower * Mathf.Pow(equipment.equipmentAbilityPowerPerLv, Level));
        equipmentAttackArmor = (int)Mathf.Round(equipment.equipmentAttackArmor * Mathf.Pow(equipment.equipmentAttackArmorPerLv, Level));
        equipmentMagicArmor = (int)Mathf.Round(equipment.equipmentMagicArmor * Mathf.Pow(equipment.equipmentMagicArmorPerLv, Level));
        equipmentCriticalChance = equipment.equipmentCriticalChance * Mathf.Pow(equipment.equipmentCriticalChancePerLv, Level);
    }
    
}
