using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicAttack", menuName = "Ability/BasicAttack")]
public class BasicAttack : Ability
{
    public float attackRange;
    public GameObject AttackEffectPrefab;   //攻擊產生的特效
    public float damagePercentage;
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

        Vector2 Derection = PlayerMove.GetMouseDerection();
        float angle = Mathf.Atan2(Derection.y, Derection.x) * Mathf.Rad2Deg;
        GameObject attackEffect = Instantiate(AttackEffectPrefab, (Vector2)parent.transform.Find("AttackPoint").position + mouseDerection.normalized * attackRange , Quaternion.Euler(new Vector3(0, 0, angle)), parent.transform);
        AbilityStats Stats = attackEffect.GetComponent<AbilityStats>();
        Stats.abilityDamage = (int)(characterStats.attackDamage * damagePercentage);
        Stats.abilityknockBackPower = characterStats.knockBackPower;
        Stats.abilityDelayTime = 0.2f;
    }

    public override void BeginCooldown(GameObject parent) {
        PlayerMove move = parent.GetComponent<PlayerMove>();

        move.canControl = true;
    }
}
