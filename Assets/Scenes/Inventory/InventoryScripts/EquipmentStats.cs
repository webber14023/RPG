using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentStats : MonoBehaviour
{
    public int equipmentHp;
    public float equipmentSpeed;
    public int equipmentAttackDamage;

    public void SetEquipmentStats(Equipment equipment, int Level) {
        equipmentHp = (int)Mathf.Round(equipment.equipmentHp * Mathf.Pow(equipment.equipmentHpPerLv, Level));
        equipmentSpeed = Mathf.Round(equipment.equipmentSpeed * Mathf.Pow(equipment.equipmentSpeedPerLv, Level));
        equipmentAttackDamage = (int)Mathf.Round(equipment.equipmentAttackDamage * Mathf.Pow(equipment.equipmentAttackDamagePerLv, Level));
    }
    
}
