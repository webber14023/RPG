using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System;

public class CharacterStats : MonoBehaviour
{
    public CharacterData c_Data;
    public PlayerStatsPanel playerStatsPanel;

    private Rigidbody2D rb;
    private SpriteRenderer sp;
    private Animator animator;
    private AudioSource audioSource;
    private NavMeshAgent agent;
    private PlayerMove playerMove;
    private HealthBar healthBar;
    public GameObject hpBar;
    public GameObject expBar;
    public Text levelText;
    public GameObject damageTextPrefab;
    public GameObject hitEffect;
    public AudioClip hitSound;
    public GameObject DeadEffect;
    
    
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
    public int baseAbilityPower {
        get { if (c_Data != null) return c_Data.abilityPower; else return 0; }
        set { c_Data.abilityPower = value; }
    }
    public int baseAttackArmor {
        get { if (c_Data != null) return c_Data.attackArmor; else return 0; }
        set { c_Data.attackArmor = value; }
    }
    public int baseMagicArmor {
        get { if (c_Data != null) return c_Data.magicArmor; else return 0; }
        set { c_Data.magicArmor = value; }
    }
    public float baseSpeed {
        get { if (c_Data != null) return c_Data.speed; else return 0; }
        set { c_Data.speed = value; }
    }
    public float baseCriticalChance {
        get { if (c_Data != null) return c_Data.criticalChance; else return 0; }
        set { c_Data.criticalChance = value; }
    }
    public float baseCriticalMultiply {
        get { if (c_Data != null) return c_Data.criticalMultiply; else return 0; }
        set { c_Data.criticalMultiply = value; }
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
    public int EquipAbilityPower {
        get { if (c_Data != null) return c_Data.EquipAbilityPower; else return 0; }
        set { c_Data.EquipAbilityPower = value; }
    }
    public int EquipAttackArmor {
        get { if (c_Data != null) return c_Data.EquipAttackArmor; else return 0; }
        set { c_Data.EquipAttackArmor = value; }
    }
    public int EquipMagicArmor {
        get { if (c_Data != null) return c_Data.EquipMagicArmor; else return 0; }
        set { c_Data.EquipMagicArmor = value; }
    }
    public float EquipSpeed {
        get { if (c_Data != null) return c_Data.EquipSpeed; else return 0; }
        set { c_Data.EquipSpeed = value; }
    }
    public float EquipCriticalChance {
        get { if (c_Data != null) return c_Data.EquipCriticalChance; else return 0; }
        set { c_Data.EquipCriticalChance = value; }
    }
    public float EquipCriticalMultiply {
        get { if (c_Data != null) return c_Data.EquipCriticalMultiply; else return 0; }
        set { c_Data.EquipCriticalMultiply = value; }
    }
    public int money {
        get { if (c_Data != null) return c_Data.money; else return 0; }
        set { c_Data.money = value; }
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
    public int abilityPoint {
        get { if (c_Data != null) return c_Data.abilityPoint; else return 0; }
        set { c_Data.abilityPoint = value; }
    }

    public int maxHealth;
    public int currentHealth;
    public int attackDamage;
    public int abilityPower;
    public int attackArmor;
    public int magicArmor;
    public float speed;
    public float knockBackPower;

    public float attackMultiply;
    public float abilityMultiply;
    public float healthMultiply;
    public float moveSpeedMultiply;
    public float AD_Reduce;
    public float AP_Reduce;
    public float criticalChance;
    public float criticalMultiply;
    public bool canDamage;
    public bool canControl;
    public int maxExp;
    public int enemyLevel;

    private void Start() {
        attackMultiply = 1f;
        healthMultiply = 1f;
        moveSpeedMultiply = 1f;
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
            EquipmentManager.UpdateEquipmentStats();
            UpdateStats();
            currentHealth = maxHealth;
            UpdateUI();
            playerStatsPanel.UpdateStatsPanel();
        }
        else {
            enemySetLevel(enemyLevel);
            healthBar.SetHealthBar((float)currentHealth/maxHealth);
        }
        
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
        if(enemyLevel == 0) {
            maxHealth = (int)((float)((baseMaxHealth * Mathf.Pow(c_Data.HpPerLv, level) + EquipHealth) * healthMultiply));
            speed = (baseSpeed * Mathf.Pow(c_Data.SpeedPerLv, level) + EquipSpeed) * moveSpeedMultiply;
            attackDamage = (int)((float)((baseAttackDamage * Mathf.Pow(c_Data.DamagePerLv, level) + EquipAttackDamage) * attackMultiply));
            abilityPower = (int)((float)((baseAbilityPower + EquipAbilityPower) * attackMultiply));
            attackArmor = baseAttackArmor + EquipAttackArmor;
            magicArmor = baseMagicArmor + EquipMagicArmor;
            AD_Reduce = 200f / (200 + attackArmor);
            AP_Reduce = 200f / (200 + magicArmor);
            maxExp = (int)(baseExp * Mathf.Pow(c_Data.ExpPerLv, level));
            criticalChance = baseCriticalChance + EquipCriticalChance;
            criticalMultiply = baseCriticalMultiply + EquipCriticalMultiply;
            
            playerStatsPanel.UpdateStatsPanel();
        }
        else {
            maxHealth = (int)((float)((baseMaxHealth * Mathf.Pow(c_Data.HpPerLv, enemyLevel) + EquipHealth) * healthMultiply));
            speed = (baseSpeed * Mathf.Pow(c_Data.SpeedPerLv, enemyLevel) + EquipSpeed) * moveSpeedMultiply;
            attackDamage = (int)((float)((baseAttackDamage * Mathf.Pow(c_Data.DamagePerLv, enemyLevel) + EquipAttackDamage) * attackMultiply));
            attackArmor = baseAttackArmor + EquipAttackArmor;
            magicArmor = baseMagicArmor + EquipMagicArmor;
            AD_Reduce = 200f / (200 + attackArmor);
            AP_Reduce = 200f / (200 + magicArmor);
            maxExp = (int)(baseExp * Mathf.Pow(c_Data.ExpPerLv, enemyLevel));
            criticalChance = baseCriticalChance + EquipCriticalChance;
            criticalMultiply = baseCriticalMultiply + EquipCriticalMultiply;
        }
    }

    public void enemySetLevel(int lv) {
        enemyLevel = lv;
        maxHealth = (int)((float)((baseMaxHealth * Mathf.Pow(c_Data.HpPerLv, lv) + EquipHealth) * healthMultiply));
        speed = (baseSpeed * Mathf.Pow(c_Data.SpeedPerLv, lv) + EquipSpeed) * moveSpeedMultiply;
        attackDamage = (int)((float)((baseAttackDamage * Mathf.Pow(c_Data.DamagePerLv, lv) + EquipAttackDamage) * attackMultiply));
        abilityPower = (int)((float)((baseAbilityPower + EquipAbilityPower) * attackMultiply));
        attackArmor = baseAttackArmor + EquipAttackArmor;
        magicArmor = baseMagicArmor + EquipMagicArmor;
        AD_Reduce = 200f / (200 + attackArmor);
        AP_Reduce = 200f / (200 + magicArmor);
        maxExp = (int)(baseExp * Mathf.Pow(c_Data.ExpPerLv, lv));
        currentHealth = maxHealth;
    }
    
    public void TakeDamage(int damage, bool isAttackDamage, CharacterStats attacker, Vector2 knockBack) {
        rb.MovePosition(transform.position + (Vector3)knockBack);
        Instantiate(hitEffect, transform.transform.GetChild(0).position, Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(knockBack.y, knockBack.x) * Mathf.Rad2Deg)), transform);
        audioSource.PlayOneShot(hitSound);
        StartCoroutine(Hurt());
        
        int levelGap = (level == 0? enemyLevel: level) - (attacker.level == 0? attacker.enemyLevel: attacker.level);
        float levelReduce = (levelGap >= -5 && levelGap <= 7)? levelGap > 0? (float)Mathf.Pow(1.05f, -levelGap) : 1 + (float)(Mathf.Pow(1.1f ,-levelGap) % 1 / 2) :
                                                                             levelGap > 0? 0.55f: 1.4f;
        bool isCritical = UnityEngine.Random.Range(0f,1f) <= attacker.criticalChance;
        int finalDamage = isAttackDamage? (int)(damage * AD_Reduce * levelReduce) : (int)(damage * AP_Reduce * levelReduce);
        if(isCritical) {
            finalDamage = (int)(finalDamage * attacker.criticalMultiply);
        }
        ShowDamageText(gameObject, finalDamage, isAttackDamage, isCritical);
        currentHealth -= finalDamage;
        healthBar.UpdateHealthBar((float)currentHealth/maxHealth);
        playerMove?.PlayerHurt();
        playerStatsPanel?.UpdateStatsPanel();

        if (currentHealth <= 0) {
            animator.SetBool("isDeath",true);
        }
    }

    public void RegenHP(int AddStats) {
        currentHealth += AddStats;
        if(currentHealth >= maxHealth)
            currentHealth = maxHealth;
        GameObject damageTextObject = Instantiate(damageTextPrefab, transform.position + (Vector3)UnityEngine.Random.insideUnitCircle, Quaternion.identity, transform);
        DamageText damageText = damageTextObject.GetComponent<DamageText>();
        healthBar.UpdateHealthBar((float)currentHealth/maxHealth);
        playerStatsPanel?.UpdateStatsPanel();
        damageText.setRegenText(AddStats);
        
    }

    public void ShowDamageText(GameObject target, int damage, bool isAttackDamage, bool isCritical) {
        GameObject damageTextObject = Instantiate(damageTextPrefab, target.transform.position + (Vector3)UnityEngine.Random.insideUnitCircle, Quaternion.identity, transform);
        DamageText damageText = damageTextObject.GetComponent<DamageText>();
        damageText.SetDamageText(damage, isAttackDamage, isCritical);
    }

    public void ResetStats() {
        baseCurrentHealth = maxHealth;
    }

    public void ResetEquipmentsStats() {
        EquipHealth = 0;
        EquipAttackDamage = 0;
        EquipAbilityPower = 0;
        EquipAttackArmor = 0;
        EquipMagicArmor = 0;
        EquipSpeed = 0f;
        EquipCriticalChance = 0f;
        EquipCriticalMultiply = 0f;
    }

    public void UpdateUI() {
        healthBar.SetHealthBar((float)currentHealth/maxHealth);
        PlayerMove.UpdatePlayerUI();
        if(expBar != null)
            expBar.GetComponent<ExpBar>().UpdateExpBar((float)CurrentExp/maxExp);
        if(levelText != null)
            levelText.text = "Lv." + c_Data.level;
            
    }

    public void Upgrade() {
        abilityPoint += c_Data.abilityPointPerLv;
        UpdateStats();
        UpdateUI();
        PlayerMove.PlayerUpgrade();
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
