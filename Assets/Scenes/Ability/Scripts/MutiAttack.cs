using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MutiAttack", menuName = "Ability/MutiAttack")]
public class MutiAttack : Ability
{
    public float attackRange;
    public GameObject AttackEffectPrefab;   //攻擊產生的特效
    public int attackCount;
    Vector2 mouseDerection;
    
    public override void Activate(GameObject parent) {
        Animator animator = parent.GetComponent<Animator>();
        PlayerMove move = parent.GetComponent<PlayerMove>();
        SpriteRenderer sprite = parent.GetComponent<SpriteRenderer>();
        CharacterStats characterStats = parent.GetComponent<CharacterStats>();
        
        animator.SetTrigger("Attack" + move.Combo);
        move.comboTimer = 1.5f;
        move.Combo++;
        if(move.Combo > 2)
            move.Combo = 0;
        mouseDerection = PlayerMove.GetMouseDerection();
        move.canControl = false;
        sprite.flipX = mouseDerection.x >= 0? false: true;

        for(int i=0; i<attackCount; i++) {
            GameObject attackEffect = Instantiate(AttackEffectPrefab, (Vector2)parent.transform.Find("AttackPoint").position + Random.insideUnitCircle * attackRange, Quaternion.identity, parent.transform);
            AbilityStats Stats = attackEffect.GetComponent<AbilityStats>();
            Stats.abilityDamage = isAttackDamage? (int)(characterStats.attackDamage * damagePercentage) : (int)(characterStats.abilityPower * damagePercentage);
            Stats.isAttackDamage = isAttackDamage;
            Stats.abilityknockBackPower = characterStats.knockBackPower;
            Stats.abilityDelayTime = (float)(i+1) / 10;
        }

    }

    public override void BeginCooldown(GameObject parent) {
        Animator animator = parent.GetComponent<Animator>();
        PlayerMove move = parent.GetComponent<PlayerMove>();

        move.canControl = true;
    }
}
