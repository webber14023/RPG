using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseAttack", menuName = "Ability/Enemy/BasicAttack")]
public class EnemyBaseAttack : Ability
{
    public float attackRange;
    public GameObject AttackEffectPrefab;   //攻擊產生的特效
    Vector2 Derection;
    
    public override void Activate(GameObject parent) {
        Animator animator = parent.GetComponent<Animator>();
        CharacterStats characterStats = parent.GetComponent<CharacterStats>();

        characterStats.canControl = false;
        animator.SetTrigger("Attacking");
    }
    public override void BeginCooldown(GameObject parent) {
        CharacterStats characterStats = parent.GetComponent<CharacterStats>();
        Enemy controller = parent.GetComponent<Enemy>();

        Derection = (controller.target.position - parent.transform.position).normalized;
        float angle = Mathf.Atan2(Derection.y, Derection.x) * Mathf.Rad2Deg;
        GameObject attackEffect = Instantiate(AttackEffectPrefab, (Vector2)parent.transform.Find("AttackPoint").position + Derection.normalized * attackRange , Quaternion.Euler(new Vector3(0, 0, angle)), parent.transform);
        AbilityStats Stats = attackEffect.GetComponent<AbilityStats>();
        Stats.abilityDamage = characterStats.attackDamage;
        Stats.abilityknockBackPower = characterStats.knockBackPower;
        Stats.abilityDelayTime = 0.5f;
        characterStats.canControl = true;
    }
}
