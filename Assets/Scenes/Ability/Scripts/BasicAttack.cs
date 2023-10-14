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
        
        animator.SetTrigger("Attacking");
        mouseDerection = PlayerMove.GetMouseDerection();
        move.canControl = false;
        GameObject attackEffect = Instantiate(AttackEffectPrefab, (Vector2)parent.transform.Find("AttackPoint").position + mouseDerection.normalized * attackRange, Quaternion.identity, parent.transform);
        
        rb.velocity = Vector2.zero;

    }

    public override void BeginCooldown(GameObject parent) {
        Animator animator = parent.GetComponent<Animator>();
        PlayerMove move = parent.GetComponent<PlayerMove>();

        move.canControl = true;
    }
}
