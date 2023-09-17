using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Slider healthBar;
    public GameObject damageTextPrefab;
    private bool canMove;
    private Animator animator;
    private Rigidbody2D rb;

    private void Start() {
        currentHealth = maxHealth;
        UpdateHealthBar();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage, Vector2 knockBack) {
        rb.AddForce(knockBack, ForceMode2D.Impulse);
        animator.SetTrigger("getDamage");
        currentHealth -= damage;
        UpdateHealthBar();
        if (currentHealth <= 0) {
            animator.SetBool("isDeath",true);
        }
    }

    public void ShowDamageText(GameObject other, int damage) {
        GameObject damageTextObject = Instantiate(damageTextPrefab, other.transform.position, Quaternion.identity, transform);
        DamageText damageText = damageTextObject.GetComponent<DamageText>();
        damageText.SetDamageText(damage);
    }

    private void UpdateHealthBar() {
        Debug.Log((float)currentHealth / maxHealth);
        healthBar.value = (float)currentHealth / maxHealth;
    }

    private void Die() {
        Destroy(gameObject);
    }
}
