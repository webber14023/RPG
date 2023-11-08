using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Data", menuName = "Character Stats/Data")]
public class CharacterData : ScriptableObject
{
    public int maxHealth;
    public int currentHealth;
    public int attackDamage;
    public float speed;
    public float knockBackPower;

    public int EquipHealth;
    public int EquipAttackDamage;
    public float EquipSpeed;

    /*public float attackMultiply;
    public float healthMultiply;
    public float moveSpeedMultiply;*/

}
