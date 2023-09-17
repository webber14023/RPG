using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BasicAttack : Ability
{
    public float attackRange;
    public GameObject AttackEffectPrefab;   //攻擊產生的特效
    private GameObject attack;
    
    public override void Activate(GameObject parent) {
        Rigidbody2D rb = parent.GetComponent<Rigidbody2D>();
        Animator animator = parent.GetComponent<Animator>();
        PlayerMove move = parent.GetComponent<PlayerMove>();
        
        animator.SetTrigger("Attacking");
        Vector2 mouseDerection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - parent.transform.position);
        move.canControl = false;
        GameObject attackEffect = Instantiate(AttackEffectPrefab, (Vector2)parent.transform.position + mouseDerection.normalized * attackRange, Quaternion.identity, parent.transform);
        attack = attackEffect;
        rb.velocity = Vector2.zero;//mouseDerection.normalized * attackRange * 2;
        attack.gameObject.GetComponent<Animator>().SetTrigger("isAttacking");
        attack.gameObject.GetComponent<CircleCollider2D>().enabled = true;

    }

    public override void BeginCooldown(GameObject parent) {
        Animator animator = parent.GetComponent<Animator>();
        PlayerMove move = parent.GetComponent<PlayerMove>();

        attack.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        move.canControl = true;
    }
}
