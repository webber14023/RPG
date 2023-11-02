using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicAttack", menuName = "Ability/BasicAttack")]
public class BasicAttack : Ability
{
    public float attackRange;
    public GameObject AttackEffectPrefab;   //攻擊產生的特效
    Vector2 mouseDerection;
    
    public override void Activate(GameObject parent) {
        Rigidbody2D rb = parent.GetComponent<Rigidbody2D>();
        Animator animator = parent.GetComponent<Animator>();
        PlayerMove move = parent.GetComponent<PlayerMove>();
        CharacterStats characterStats = parent.GetComponent<CharacterStats>();
        
        animator.SetTrigger("Attacking");
        mouseDerection = PlayerMove.GetMouseDerection();
        move.canControl = false;

        for(int i=0; i<1000; i++) {
            GameObject attackEffect = Instantiate(AttackEffectPrefab, (Vector2)parent.transform.Find("AttackPoint").position + mouseDerection.normalized * attackRange + Random.insideUnitCircle, Quaternion.identity, parent.transform);
            AbilityStats Stats = attackEffect.GetComponent<AbilityStats>();
            Stats.abilityDamage = characterStats.attackDamage;
            Stats.abilityknockBackPower = characterStats.knockBackPower;
        }
        rb.velocity = Vector2.zero;

    }

    public override void BeginCooldown(GameObject parent) {
        Animator animator = parent.GetComponent<Animator>();
        PlayerMove move = parent.GetComponent<PlayerMove>();

        move.canControl = true;
    }
}
