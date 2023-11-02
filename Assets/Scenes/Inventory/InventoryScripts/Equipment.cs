using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/New Equipment")]
public class Equipment : Item
{
    [Header("Equipment stats")]
    public float equipmentHp;
    public float equipmentSpeed;
    public int equipmentAttackDamage;
}
