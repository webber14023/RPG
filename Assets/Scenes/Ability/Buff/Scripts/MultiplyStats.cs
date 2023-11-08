using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MultiplyStats", menuName = "Buff/MultiplyStats")]
public class MultiplyStats : BuffStatus
{
    [Header("stats")]
    public float AttackMultiply;
    public float HealthMultiply;
    public float MoveSpeedMultiply;
    
    public override void Effect(GameObject parent) {
        CharacterStats stats = parent.GetComponent<CharacterStats>();
        stats.attackMultiply += AttackMultiply;
        stats.healthMultiply += HealthMultiply;
        stats.moveSpeedMultiply += MoveSpeedMultiply;
        stats.UpdateStats();

    }
    public override void RemoveEffect(GameObject parent) {
        CharacterStats stats = parent.GetComponent<CharacterStats>();
        stats.attackMultiply -= AttackMultiply;
        stats.healthMultiply -= HealthMultiply;
        stats.moveSpeedMultiply -= MoveSpeedMultiply;
        stats.UpdateStats();

    }
}
