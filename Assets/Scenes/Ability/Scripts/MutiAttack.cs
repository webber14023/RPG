using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MutiAttack", menuName = "Ability/MutiAttack")]
public class MutiAttack : Ability
{
    public float attackRange;
    public float attackGapTime;
    public float areaAngle;
    public float knockBackPower;
    public GameObject AttackEffectPrefab;   //攻擊產生的特效
    public int attackCount;
    public int baseAttackCount;
    public float attackCountPerLv;
    Vector2 mouseDerection;
    
    float area, angle;
    
    public override void Activate(GameObject parent) {
        Animator animator = parent.GetComponent<Animator>();
        PlayerMove move = parent.GetComponent<PlayerMove>();
        SpriteRenderer sprite = parent.GetComponent<SpriteRenderer>();
        CharacterStats characterStats = parent.GetComponent<CharacterStats>();
        mouseDerection = PlayerMove.GetMouseDerection();
        area = areaAngle / (attackCount - 1);
        angle = (Mathf.Atan2(mouseDerection.y, mouseDerection.x) * Mathf.Rad2Deg) - (area * (attackCount / 2));
        move.canControl = false;
        
        for(int i=0; i<attackCount; i++) {
            animator.SetTrigger("Attack" + move.Combo);
            move.comboTimer = 1.5f;
            move.Combo++;
            if(move.Combo > 2)
                move.Combo = 0;
            if (areaAngle == 0) {
                mouseDerection = PlayerMove.GetMouseDerection();
            }
            else {
                mouseDerection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin (angle * Mathf.Deg2Rad));
                angle += area;
            }
            sprite.flipX = mouseDerection.x >= 0? false: true;
            GameObject attackEffect = Instantiate(AttackEffectPrefab, (Vector2)parent.transform.Find("AttackPoint").position + mouseDerection * attackRange, Quaternion.identity, parent.transform);
            AbilityStats Stats = attackEffect.GetComponent<AbilityStats>();
            Stats.abilityDamage = isAttackDamage? (int)(characterStats.attackDamage * damagePercentage) : (int)(characterStats.abilityPower * damagePercentage);
            Stats.isAttackDamage = isAttackDamage;
            Stats.Derection = mouseDerection;
            Stats.abilityknockBackPower = knockBackPower;
            Stats.abilityDelayTime = attackGapTime * i;
        }

    }

    public override void BeginCooldown(GameObject parent) {
        Animator animator = parent.GetComponent<Animator>();
        PlayerMove move = parent.GetComponent<PlayerMove>();

        move.canControl = true;
    }

    public override void ResetAbility() {
        attackCount = baseAttackCount;
    }

    public override void UpgradeAbility() {
        attackCount = baseAttackCount + (int)(attackCountPerLv * AbilityLevel);
    }
}
