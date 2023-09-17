using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    public int damage = 10;
    public float knockBackPower = 4f;

    private void OnTriggerEnter2D(Collider2D other)
    {
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
}
