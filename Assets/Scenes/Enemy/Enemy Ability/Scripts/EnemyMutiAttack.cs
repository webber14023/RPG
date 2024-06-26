using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MutiAttack", menuName = "Ability/Enemy/MutiAttack")]
public class EnemyMutiAttack : Ability
{
    public float attackRange;
    public float DelayTime;
    public float areaAngle;
    public bool isTrack;
    public int attackCount;
    public GameObject AttackEffectPrefab;   //攻擊產生的特效
    Vector2 Derection;
    float angle, area;
    public bool isSpecialAttack;
    
    public override void Activate(GameObject parent) {
        Animator animator = parent.GetComponent<Animator>();
        animator.SetTrigger(isSpecialAttack?"SpecialAttack":"Attacking");
    }
    public override void BeginCooldown(GameObject parent) {
        CharacterStats characterStats = parent.GetComponent<CharacterStats>();
        Enemy controller = parent.GetComponent<Enemy>();
        area = areaAngle / (attackCount - 1);
        Derection = (controller.target.position - parent.transform.position).normalized;
        angle = (Mathf.Atan2(Derection.y, Derection.x) * Mathf.Rad2Deg) - (area * (attackCount / 2));
        
        for(int i=0; i<attackCount; i++) {
            GameObject attackEffect = Instantiate(AttackEffectPrefab, (Vector2)parent.transform.Find("AttackPoint").position + Derection.normalized * attackRange , Quaternion.Euler(new Vector3(0, 0, angle)), parent.transform);
            AbilityStats Stats = attackEffect.GetComponent<AbilityStats>();
            Stats.abilityDamage = isAttackDamage? (int)(characterStats.attackDamage * damagePercentage) : (int)(characterStats.abilityPower * damagePercentage);
            Stats.isAttackDamage = isAttackDamage;
            Stats.abilityknockBackPower = characterStats.knockBackPower;
            Stats.abilityDelayTime = DelayTime * (i + 1);
            Stats.isTrack = isTrack;
            Stats.Derection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin (angle * Mathf.Deg2Rad));
            angle += area;
        }
    }
}
