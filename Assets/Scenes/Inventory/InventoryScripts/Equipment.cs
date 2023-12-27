using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/New Equipment")]
public class Equipment : Item
{
    [Header("Equipment stats")]
    public int equipmentHp;
    public float equipmentSpeed;
    public int equipmentAttackDamage;
    public int equipmentAbilityPower;
    public int equipmentAttackArmor;
    public int equipmentMagicArmor;
    public float equipmentCriticalChance;

    public float equipmentHpPerLv;
    public float equipmentSpeedPerLv;
    public float equipmentAttackDamagePerLv;
    public float equipmentAbilityPowerPerLv;
    public float equipmentAttackArmorPerLv;
    public float equipmentMagicArmorPerLv;
    public float equipmentCriticalChancePerLv;
}