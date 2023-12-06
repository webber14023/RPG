using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName ="DashAttack",menuName = "Ability/DashAttack")]
public class DashAttack : Ability
{
    public float Dashdistance;
    public float damagePercentage;
    public GameObject AttackPrefeb;
    public LayerMask layerMark;
    
    RaycastHit2D hit;
    
    Vector2 orgPosition;
    Vector2 currentPosition;
    BoxCollider2D playerCollider;
    
    public override void Activate(GameObject parent)
    {
        Rigidbody2D rb = parent.GetComponent<Rigidbody2D>();
        playerCollider = parent.transform.Find("PlayerCollider").GetComponent<BoxCollider2D>();
        Vector3 mouseDerection = PlayerMove.GetMouseDerection().normalized;
        orgPosition = parent.transform.GetChild(0).position;
        playerCollider.enabled = false;
        hit = Physics2D.Raycast(parent.transform.GetChild(0).position, parent.transform.TransformDirection(mouseDerection), Dashdistance, layerMark);
        currentPosition = hit.collider? hit.point: orgPosition + (Vector2)mouseDerection * Dashdistance;
        rb.MovePosition(parent.transform.position + mouseDerection * Dashdistance);
    }

        
    public override void BeginCooldown(GameObject parent)
    {
        Vector2 dashPosition = currentPosition - orgPosition;
        CharacterStats characterStats = parent.GetComponent<CharacterStats>();

        playerCollider.enabled = true;
        GameObject attackEffect = Instantiate(AttackPrefeb, orgPosition, Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(dashPosition.y, dashPosition.x) * Mathf.Rad2Deg)), parent.transform);
        attackEffect.transform.localScale += new Vector3(Vector2.Distance(Vector2.zero, dashPosition), 0, 0);
        AbilityStats Stats = attackEffect.GetComponent<AbilityStats>();
        Stats.abilityDamage = (int)(characterStats.attackDamage * damagePercentage);
        Stats.abilityknockBackPower = characterStats.knockBackPower;
        Stats.abilityDelayTime = 0.2f;

    }
}
