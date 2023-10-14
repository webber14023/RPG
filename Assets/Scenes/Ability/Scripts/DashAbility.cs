using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="DashAbility",menuName = "Ability/DashAbility")]
public class DashAbility : Ability
{
    public float dashForse;

    public override void Activate(GameObject parent)
    {
        Rigidbody2D rb = parent.GetComponent<Rigidbody2D>();
        Animator animator = parent.GetComponent<Animator>();
        PlayerMove move = parent.GetComponent<PlayerMove>();
        Vector2 dashDerection;
        move.canControl = false;
        animator.SetBool("isDashing", true);

        if(move.input == Vector2.zero)
            dashDerection = new Vector2(1,0);
        else
            dashDerection = move.input;
            
        rb.velocity = dashDerection * dashForse;
    }

    public override void BeginCooldown(GameObject parent)
    {
        Animator animator = parent.GetComponent<Animator>();
        PlayerMove move = parent.GetComponent<PlayerMove>();

        animator.SetBool("isDashing", false);
        move.canControl = true;
    }
}
