using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicAttack", menuName = "Ability/BasicAttack")]
public class BasicAttack : Ability
{
    public float attackRange;
    public float knockBackPower;
    public GameObject AttackEffectPrefab;   //攻擊產生的特效
    Vector2 mouseDerection;
    
    public override void Activate(GameObject parent) {
        Animator animator = parent.GetComponent<Animator>();
        PlayerMove move = parent.GetComponent<PlayerMove>();
        CharacterStats characterStats = parent.GetComponent<CharacterStats>();
        SpriteRenderer sprite = parent.GetComponent<SpriteRenderer>();
        
        animator.SetTrigger("Attack" + move.Combo);
        move.comboTimer = 1.5f;
        move.Combo++;
        if(move.Combo > 2)
            move.Combo = 0;
        mouseDerection = PlayerMove.GetMouseDerection();
        move.canControl = false;
        sprite.flipX = mouseDerection.x >= 0? false: true;

        GameObject attackEffect = Instantiate(AttackEffectPrefab, (Vector2)parent.transform.Find("AttackPoint").position + mouseDerection.normalized * attackRange , Quaternion.identity, parent.transform);
        AbilityStats Stats = attackEffect.GetComponent<AbilityStats>();
        Stats.abilityDamage = isAttackDamage? (int)(characterStats.attackDamage * damagePercentage) : (int)(characterStats.abilityPower * damagePercentage);
        Stats.isAttackDamage = isAttackDamage;
        Stats.Derection = mouseDerection;
        Stats.abilityknockBackPower = knockBackPower;
    }

    public override void BeginCooldown(GameObject parent) {
        PlayerMove move = parent.GetComponent<PlayerMove>();

        move.canControl = true;
    }
}
