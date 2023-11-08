using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName ="DashAbility",menuName = "Ability/DashAbility")]
public class DashAbility : Ability
{
    public float dashForse;

    public override void Activate(GameObject parent)
    {
        PlayerMove move = parent.GetComponent<PlayerMove>();
        CharacterStats stats = parent.GetComponent<CharacterStats>();
        if(move.input != Vector2.zero) {
            SpriteRenderer Sprite = parent.GetComponent<SpriteRenderer>();
            Rigidbody2D rb = parent.GetComponent<Rigidbody2D>();
            Animator animator = parent.GetComponent<Animator>();
            Vector2 dashDerection;
            animator.SetBool("isDashing", true);
            stats.canDamage = false;
            dashDerection = move.input;

            if(move.input.x < 0)
                Sprite.flipX = true;
            else
                Sprite.flipX = false;
                
            rb.velocity = dashDerection * dashForse;
        }

    }

    public override void BeginCooldown(GameObject parent)
    {
        Animator animator = parent.GetComponent<Animator>();
        PlayerMove move = parent.GetComponent<PlayerMove>();
        CharacterStats stats = parent.GetComponent<CharacterStats>();

        animator.SetBool("isDashing", false);
        stats.canDamage = true;
        move.canControl = true;
    }
}
