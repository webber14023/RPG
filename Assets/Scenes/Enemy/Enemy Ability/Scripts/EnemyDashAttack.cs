using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName ="EnemyDashAttack",menuName = "Ability/Enemy/EnemyDashAttack")]
public class EnemyDashAttack : Ability
{
    public float Dashdistance;
    float currentDistance;
    public GameObject AttackPrefeb;
    public LayerMask layerMark;
    public Color effectColor;
    
    Vector2 orgPosition;
    BoxCollider2D playerCollider;
    RaycastHit2D hit;
    Vector3 dashDerection;

    public override void Activate(GameObject parent)
    {
        Animator animator = parent.GetComponent<Animator>();
        Enemy controller = parent.GetComponent<Enemy>();
        dashDerection = (controller.target.position - parent.transform.position).normalized;
        animator.SetTrigger("SpecialAttack");
    }
    public override void BeginCooldown(GameObject parent)
    {
        CharacterStats stats = parent.GetComponent<CharacterStats>();
        Rigidbody2D rb = parent.GetComponent<Rigidbody2D>();
        Enemy controller = parent.GetComponent<Enemy>();
        playerCollider = controller.target.Find("PlayerCollider").GetComponent<BoxCollider2D>();
        //dashDerection = (controller.target.position - parent.transform.position).normalized;

        hit = Physics2D.Raycast(parent.transform.GetChild(0).position, parent.transform.TransformDirection(dashDerection), Dashdistance, layerMark);
        

        orgPosition = parent.transform.GetChild(0).position;
        if(hit.collider != null) {
            parent.transform.position += dashDerection * hit.distance;
            currentDistance = hit.distance;
        }
        else {
            parent.transform.position += dashDerection * Dashdistance;
            currentDistance = Dashdistance;
        }
        
        Vector2 dashPosition = (Vector2)parent.transform.GetChild(0).position - orgPosition;
        CharacterStats characterStats = parent.GetComponent<CharacterStats>();
        GameObject attackEffect = Instantiate(AttackPrefeb, orgPosition, Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(dashPosition.y, dashPosition.x) * Mathf.Rad2Deg)), parent.transform);
        attackEffect.transform.localScale += new Vector3(currentDistance, 0, 0);
        
        AbilityStats Stats = attackEffect.GetComponent<AbilityStats>();
        Stats.abilityDamage = isAttackDamage? (int)(characterStats.attackDamage * damagePercentage) : (int)(characterStats.abilityPower * damagePercentage);
        Stats.isAttackDamage = isAttackDamage;
        Stats.abilityknockBackPower = characterStats.knockBackPower;
        attackEffect.GetComponent<DashAttackEffect>().effectColor = effectColor;
    }
    
}
