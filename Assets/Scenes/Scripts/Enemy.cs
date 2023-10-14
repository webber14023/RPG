using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public GameObject hpBar;
    public GameObject damageTextPrefab;
    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer sp;

    private void Start() {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int damage, Vector2 knockBack) {
        rb.AddForce(knockBack, ForceMode2D.Impulse);
        StartCoroutine(Hurt());
        currentHealth -= damage;
        hpBar.GetComponent<HealthBar>().UpdateHealthBar((float)currentHealth/maxHealth);
        if (currentHealth <= 0) {
            animator.SetBool("isDeath",true);
        }
    }

    public void ShowDamageText(GameObject other, int damage) {
        GameObject damageTextObject = Instantiate(damageTextPrefab, other.transform.position, Quaternion.identity, transform);
        DamageText damageText = damageTextObject.GetComponent<DamageText>();
        damageText.SetDamageText(damage);
    }

    private void Die() {
        Destroy(gameObject);
    }
    IEnumerator Hurt()
    {
        sp.material.SetFloat("_FlashAmount", 1);
        yield return new WaitForSeconds(.1f);
        sp.material.SetFloat("_FlashAmount", 0);
    }

}
