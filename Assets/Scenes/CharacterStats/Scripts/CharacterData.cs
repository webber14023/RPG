using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Data", menuName = "Character Stats/Data")]
public class CharacterData : ScriptableObject
{
    public string CharacterName;
    public int maxHealth;
    public int currentHealth;
    public int attackDamage;
    public int abilityPower;
    public int attackArmor;
    public int magicArmor;
    public float speed;
    public float criticalChance;
    public float criticalMultiply;
    public float knockBackPower;
    public int abilityPoint;

    public int EquipHealth;
    public int EquipAttackDamage;
    public int EquipAbilityPower;
    public int EquipAttackArmor;
    public int EquipMagicArmor;
    public float EquipCriticalChance;
    public float EquipCriticalMultiply;
    public float EquipSpeed;

    public int money;

    public float AttackCooldown;

    [Header("Level")]
    public int level;
    public int maxExp;
    public int currentExp;
    public float HpPerLv;
    public float DamagePerLv;
    public float SpeedPerLv;
    public float ExpPerLv;
    public int abilityPointPerLv;

    /*public float attackMultiply;
    public float healthMultiply;
    public float moveSpeedMultiply;*/

    public dropItem[] dropItems;

    [System.Serializable]public struct dropItem {
        public Item item;
        public int Count;
        public float dropPercent;
    }

}
