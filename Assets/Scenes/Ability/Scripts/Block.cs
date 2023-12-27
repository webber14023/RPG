using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Block", menuName = "Ability/Block")]
public class Block : Ability
{
    public override void Activate(GameObject parent) { 
        Animator animator = parent.GetComponent<Animator>();
        CharacterStats characterStats = parent.GetComponent<CharacterStats>();
        PlayerMove move = parent.GetComponent<PlayerMove>();
        SpriteRenderer sprite = parent.GetComponent<SpriteRenderer>();
        Vector2 mouseDerection = PlayerMove.GetMouseDerection();
        characterStats.AD_Reduce = 0.99f;
        animator.SetBool("Block", true);
        move.canControl = false;
        sprite.flipX = mouseDerection.x >= 0? false: true;
    }
    
    public override void BeginCooldown(GameObject parent) {
        Animator animator = parent.GetComponent<Animator>();
        CharacterStats characterStats = parent.GetComponent<CharacterStats>();
        PlayerMove move = parent.GetComponent<PlayerMove>();
        characterStats.AD_Reduce = 0f;
        animator.SetBool("Block", false);
        move.canControl = true;

    }

}
