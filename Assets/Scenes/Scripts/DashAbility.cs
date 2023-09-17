using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DashAbility : Ability
{
    public float dashForse;

    public override void Activate(GameObject parent)
    {
        Rigidbody2D rb = parent.GetComponent<Rigidbody2D>();
        Animator animator = parent.GetComponent<Animator>();
        PlayerMove move = parent.GetComponent<PlayerMove>();
        Vector2 mouseDerection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - parent.transform.position);

        move.canControl = false;
        animator.SetBool("isDashing", true);
        rb.velocity = (Vector2)mouseDerection.normalized * dashForse;
    }

    public override void BeginCooldown(GameObject parent)
    {
        Animator animator = parent.GetComponent<Animator>();
        PlayerMove move = parent.GetComponent<PlayerMove>();

        animator.SetBool("isDashing", false);
        move.canControl = true;
    }
}
