using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AddStats", menuName = "Buff/AddStats")]
public class AddStats : BuffStatus
{
    [Header("stats")]
    public int Attack;
    public int Health;
    public float MoveSpeed;
    
    public override void Effect(GameObject parent) {
        CharacterStats stats = parent.GetComponent<CharacterStats>();
        stats.EquipAttackDamage += Attack;
        stats.EquipHealth += Health;
        stats.EquipSpeed += MoveSpeed;
        stats.UpdateStats();

    }
    public override void RemoveEffect(GameObject parent) {
        CharacterStats stats = parent.GetComponent<CharacterStats>();
        stats.EquipAttackDamage -= Attack;
        stats.EquipHealth -= Health;
        stats.EquipSpeed -= MoveSpeed;
        stats.UpdateStats();
    }
}
