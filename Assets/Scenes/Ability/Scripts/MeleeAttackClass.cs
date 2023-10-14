using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackClass : MonoBehaviour
{   
    public int damage;
    public float knockBackPower = 4f;
    Vector2 mouseDerection;
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        mouseDerection = PlayerMove.GetMouseDerection();
        float angle = Mathf.Atan2(mouseDerection.y, mouseDerection.x) * Mathf.Rad2Deg;
        
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemy"))
        {
            int currentDamage = (int)(damage*Random.Range(0.9f,1.1f));
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

}
