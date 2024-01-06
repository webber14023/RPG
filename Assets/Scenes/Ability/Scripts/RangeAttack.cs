using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RangeAttack", menuName = "Ability/RangeAttack")]
public class RangeAttack : Ability
{
    public float attackRange;
    public float destroyTime;
    public float baseDestroyTime;
    public float destroyTimePerLv;
    public GameObject AttackEffectPrefab;   //攻擊產生的特效
    Vector2 mouseDerection;

    public override void Activate(GameObject parent) {
        Animator animator = parent.GetComponent<Animator>();
        PlayerMove move = parent.GetComponent<PlayerMove>();
        CharacterStats characterStats = parent.GetComponent<CharacterStats>();
        SpriteRenderer sprite = parent.GetComponent<SpriteRenderer>();
        
        animator.SetTrigger("Attack" + move.Combo);
        Debug.Log(move.Combo);
        move.comboTimer = 1.5f;
        move.Combo++;
        if(move.Combo > 2)
            move.Combo = 0; 
        mouseDerection = PlayerMove.GetMouseDerection();
        move.canControl = false;
        sprite.flipX = mouseDerection.x >= 0? false: true;

        GameObject attackEffect = Instantiate(AttackEffectPrefab, (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity, parent.transform);
        AbilityStats Stats = attackEffect.GetComponent<AbilityStats>();
        Stats.abilityDamage = isAttackDamage? (int)(characterStats.attackDamage * damagePercentage) : (int)(characterStats.abilityPower * damagePercentage);
        Stats.isAttackDamage = isAttackDamage;
        Stats.abilityknockBackPower = characterStats.knockBackPower;
        Stats.abilityDestroyTime = destroyTime;
        Stats.abilityDelayTime = 0.2f;
    }

    public override void BeginCooldown(GameObject parent) {
        PlayerMove move = parent.GetComponent<PlayerMove>();

        move.canControl = true;
    }

    public override void ResetAbility() {
        destroyTime = baseDestroyTime;
    }

    public override void UpgradeAbility() {
        destroyTime = baseDestroyTime + destroyTimePerLv * AbilityLevel;
    }
}
