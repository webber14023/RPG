using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class Spark : MonoBehaviour
{
    public float speed;
    public int damage;
    Vector2 mouseDerection;
    Rigidbody2D rb;
    Animator anim;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        mouseDerection = transform.parent.GetComponent<MouseDerection>().GetMouseDerection();
        mouseDerection.x *= Random.Range(0.1f,1.1f);
        mouseDerection.y *= Random.Range(0.1f,1.1f);
        float angle = Mathf.Atan2(mouseDerection.y, mouseDerection.x) * Mathf.Rad2Deg;
        Debug.Log(mouseDerection);
        
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        //rb.velocity = mouseDerection * speed;

    }

    void FixedUpdate() {
        if(!anim.GetBool("Hit"))
            rb.AddForce(mouseDerection.normalized * speed);
        else
            rb.velocity = new Vector2(0, 0);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Wall"))
            anim.SetBool("Hit",true);

        if (other.CompareTag("Enemy"))
        {
            anim.SetBool("Hit",true);
            int currentDamage = (int)(damage*Random.Range(0.9f,1.1f));
            Enemy enemy = other.GetComponent<Enemy>();
            Vector2 attackDerection = enemy.transform.position - transform.parent.position;
            enemy.ShowDamageText(other.gameObject, currentDamage);
            if (enemy != null)
            {
                enemy.TakeDamage(currentDamage,attackDerection.normalized*5);
            }
        }
    }
    private void Die() {
        Destroy(gameObject);
    }
}
