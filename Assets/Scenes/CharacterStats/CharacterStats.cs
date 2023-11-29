using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CharacterStats : MonoBehaviour
{
    public CharacterData c_Data;
    
    private Rigidbody2D rb;
    private SpriteRenderer sp;
    private Animator animator;
    private AudioSource audioSource;
    private NavMeshAgent agent;
    private PlayerMove playerMove;
    private HealthBar healthBar;
    public GameObject hpBar;
    public GameObject expBar;
    public GameObject damageTextPrefab;
    public GameObject hitEffect;
    
    
    public int baseMaxHealth {
        get { if (c_Data != null) return c_Data.maxHealth; else return 0; }
        set { c_Data.maxHealth = value; }
    }
    public int baseCurrentHealth {
        get { if (c_Data != null) return c_Data.currentHealth; else return 0; }
        set { c_Data.currentHealth = value; }
    }
    public int baseAttackDamage {
        get { if (c_Data != null) return c_Data.attackDamage; else return 0; }
        set { c_Data.attackDamage = value; }
    }
    public float baseSpeed {
        get { if (c_Data != null) return c_Data.speed; else return 0; }
        set { c_Data.speed = value; }
    }
    public float baseKnockBackPower {
        get { if (c_Data != null) return c_Data.knockBackPower; else return 0; }
        set { c_Data.knockBackPower = value; }
    }
    public int EquipHealth {
        get { if (c_Data != null) return c_Data.EquipHealth; else return 0; }
        set { c_Data.EquipHealth = value; }
    }
    public int EquipAttackDamage {
        get { if (c_Data != null) return c_Data.EquipAttackDamage; else return 0; }
        set { c_Data.EquipAttackDamage = value; }
    }
    public float EquipSpeed {
        get { if (c_Data != null) return c_Data.EquipSpeed; else return 0; }
        set { c_Data.EquipSpeed = value; }
    }
    public int baseExp {
        get { if (c_Data != null) return c_Data.maxExp; else return 0; }
        set { c_Data.maxExp = value; }
    }
    public int CurrentExp {
        get { if (c_Data != null) return c_Data.currentExp; else return 0; }
        set { c_Data.currentExp = value; }
    }
    public int level {
        get { if (c_Data != null) return c_Data.level; else return 0; }
        set { c_Data.level = value; }
    }


    public int maxHealth;
    public int currentHealth;
    public int attackDamage;
    public float speed;
    public float knockBackPower;

    public float attackMultiply;
    public float healthMultiply;
    public float moveSpeedMultiply;
    public float DamageReduce;

    public bool canDamage;
    public bool canControl;

    int maxExp;

    public int enemyLevel;

    private void Start() {
        attackMultiply = 1f;
        healthMultiply = 1f;
        moveSpeedMultiply = 1f;
        DamageReduce = 0f;
        canDamage = true;
        canControl = true;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        playerMove = GetComponent<PlayerMove>();
        healthBar = hpBar.GetComponent<HealthBar>();
        if(enemyLevel == 0) {
            UpdateStats();
            currentHealth = baseCurrentHealth;
        }
        else
            enemySetLevel(enemyLevel);
        UpdateUI();
    }

    public void ChangeStats(int addHp, int addDamage, float addSpeed) {
        baseMaxHealth += addHp;
        baseAttackDamage += addDamage;
        baseSpeed += addSpeed;
    }

    public void UpdateStats() {
        /*maxHealth = (int)((float)((baseMaxHealth + EquipHealth + c_Data.HpPerLv * level) * healthMultiply));
        speed = (baseSpeed + EquipSpeed + c_Data.SpeedPerLv * level) * moveSpeedMultiply;
        attackDamage = (int)((float)((baseAttackDamage + EquipAttackDamage + c_Data.DamagePerLv * level) * attackMultiply));*/

        maxHealth = (int)((float)((baseMaxHealth * Mathf.Pow(c_Data.HpPerLv, level) + EquipHealth) * healthMultiply));
        speed = (baseSpeed * Mathf.Pow(c_Data.SpeedPerLv, level) + EquipSpeed) * moveSpeedMultiply;
        attackDamage = (int)((float)((baseAttackDamage * Mathf.Pow(c_Data.DamagePerLv, level) + EquipAttackDamage) * attackMultiply));
        maxExp = (int)(baseExp * Mathf.Pow(c_Data.ExpPerLv, level));
    }

    public void enemySetLevel(int lv) {
        maxHealth = (int)((float)((baseMaxHealth * Mathf.Pow(c_Data.HpPerLv, lv) + EquipHealth) * healthMultiply));
        speed = (baseSpeed * Mathf.Pow(c_Data.SpeedPerLv, lv) + EquipSpeed) * moveSpeedMultiply;
        attackDamage = (int)((float)((baseAttackDamage * Mathf.Pow(c_Data.DamagePerLv, lv) + EquipAttackDamage) * attackMultiply));
        maxExp = (int)(baseExp * Mathf.Pow(c_Data.ExpPerLv, lv));
        currentHealth = maxHealth;
    }
    
    public void TakeDamage(int damage, Vector2 knockBack) {
        rb.MovePosition(transform.position + (Vector3)knockBack);
        Instantiate(hitEffect, transform.transform.GetChild(0).position, Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(knockBack.y, knockBack.x) * Mathf.Rad2Deg)), transform);
        audioSource.Play();
        StartCoroutine(Hurt());
        currentHealth -= (int)(damage * (1f -DamageReduce));
        healthBar.UpdateHealthBar((float)currentHealth/maxHealth);
        playerMove?.PlayerHurt();
        if (currentHealth <= 0) {
            animator.SetBool("isDeath",true);
        }

    }

    public void ShowDamageText(GameObject target, int damage) {
        GameObject damageTextObject = Instantiate(damageTextPrefab, target.transform.position, Quaternion.identity, transform);
        DamageText damageText = damageTextObject.GetComponent<DamageText>();
        damageText.SetDamageText((int)(damage * (1f -DamageReduce)));
    }

    public void ResetStats() {
        baseCurrentHealth = maxHealth;
    }

    public void UpdateUI() {
        healthBar.SetHealthBar((float)currentHealth/maxHealth);
        if(expBar != null)
            expBar.GetComponent<ExpBar>().UpdateExpBar((float)CurrentExp/maxExp);
    }

    public void Upgrade() {
        UpdateStats();
        currentHealth = maxHealth;
        UpdateUI();
    }

    public void AddExp(int exp) {
        CurrentExp += exp;
        if(CurrentExp >= maxExp) {
            while(CurrentExp >= maxExp) {
                CurrentExp -= maxExp;
                level++;
                maxExp = (int)(baseExp * Mathf.Pow(c_Data.ExpPerLv, level));
            }
            Upgrade();
        }
        expBar.GetComponent<ExpBar>().UpdateExpBar((float)CurrentExp/maxExp);
    }

    IEnumerator Hurt()
    {
        sp.material.SetFloat("_FlashAmount", 1);
        if(agent != null) {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
        }
        //canDamage = false;
        canControl = false;

        yield return new WaitForSeconds(.1f);

        sp.material.SetFloat("_FlashAmount", 0);
        canDamage = true;
        canControl = true;
    }
}
