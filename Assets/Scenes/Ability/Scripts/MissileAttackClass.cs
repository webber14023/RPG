using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileAttackClass : MonoBehaviour
{
    public float speed;
    public int damage;
    public float knockBackPower = 4f;
    
    public float destroyTime;
    Vector2 mouseDerection;
    Rigidbody2D rb;
    Animator anim;
    AbilityStats stats;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        stats = transform.GetComponent<AbilityStats>();
        //mouseDerection = PlayerMove.GetMouseDerection();
        mouseDerection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        float angle = Mathf.Atan2(mouseDerection.y, mouseDerection.x) * Mathf.Rad2Deg;
        StartCoroutine(DestoryTimer());
        
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void FixedUpdate() {
        if(!anim.GetBool("Hit"))
            rb.AddForce(mouseDerection.normalized * speed);
        else
            rb.velocity = new Vector2(0, 0);

    }
    
    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Wall"))
            anim.SetBool("Hit",true);

        if (other.CompareTag("Enemy"))
        {
            anim.SetBool("Hit",true);
            int currentDamage = (int)((damage+stats.abilityDamage)*Random.Range(0.9f,1.1f));
            Enemy enemy = other.GetComponent<Enemy>();
            Vector2 attackDerection = enemy.transform.position - transform.parent.position;
            enemy.ShowDamageText(other.gameObject, currentDamage);
            if (enemy != null)
            {
                enemy.TakeDamage(currentDamage,attackDerection.normalized * knockBackPower);
            }
        }
    }
    private void Die() {
        Destroy(gameObject);
    }

    IEnumerator DestoryTimer()
    {
        yield return new WaitForSeconds(destroyTime);
        anim.SetBool("Hit",true);
    }
}
