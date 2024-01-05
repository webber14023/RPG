using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Collections.Generic;

public enum EnemyStates {GUARD, PATROL, CHASE, DEAD}

public class Enemy : MonoBehaviour
{
    [SerializeField]private EnemyStates enemyStates;
    private EnemyAbilityHolder[] AbilityHolders;
    private EnemyAbilityHolder AbilityHolder;
    public Transform target;

    public float sightRadius;
    public float attackRange;
    float currentAttackRange;
    public bool canControl;
    [SerializeField]float AttackCooldown, timer;
    float targetDistance;

    [Header("UI")]
    public GameObject hpBar;
    public GameObject damageTextPrefab;
    private Animator animator;
    private SpriteRenderer sp;
    private CharacterStats stats;
    private CharacterStats targetStats;
    private NavMeshAgent agent;
    
    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        stats = GetComponent<CharacterStats>();
        AbilityHolders = GetComponents<EnemyAbilityHolder>();
        RandomAbility();
        //target = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetComponent<Transform>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        targetStats = target.GetComponent<CharacterStats>();
        stats.currentHealth = stats.baseMaxHealth;
        AttackCooldown = stats.c_Data.AttackCooldown;
        currentAttackRange = attackRange;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = stats.speed;
        canControl = true;
        //agent.isStopped = true;
        UpdateUI();
    }

    private void Update() {
        SwitchStates();
    }
    
    void UpdateUI() {
        hpBar.transform.Find("Name").GetComponent<Text>().text = stats.c_Data.name + "   Lv." + stats.enemyLevel;
    }

    void SwitchStates() {
        if(!animator.GetBool("isDeath")) {
            targetDistance = Vector2.Distance(transform.position, target.position);
            if(FoundPlayer()) {
                enemyStates = EnemyStates.CHASE;
            }
            else
                enemyStates = EnemyStates.GUARD;
        }
        else
            enemyStates = EnemyStates.DEAD;

        switch (enemyStates)
        {
            case EnemyStates.GUARD:
                Guard();
                break;
            case EnemyStates.PATROL:
                break;
            case EnemyStates.CHASE:
                ChaseTarget();
                break;
            case EnemyStates.DEAD:
                break;
            
        }
        if(timer > 0f)
            timer -= Time.deltaTime;
    }

    void ChaseTarget() {
        if(!canControl) {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
            return;
        }
        if(targetDistance > currentAttackRange) {
            //transform.position = Vector2.MoveTowards(transform.position, target.position, stats.speed* Time.deltaTime);
            agent.isStopped = false;
            currentAttackRange = attackRange;
            agent.SetDestination(target.position);
            agent.speed = stats.speed;
            animator.SetBool("Moving", true);
            
            sp.flipX = transform.position.x > target.position.x ? true : false;
        }
        else {
            PrepareAttack();
            currentAttackRange = attackRange + 0.7f;
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
            animator.SetBool("Moving", false);
        }
    }

    void Guard() {
        animator.SetBool("Moving", false);
        agent.velocity = Vector3.zero;
        //agent.isStopped = true;
    }

    public void RandomAbility() {
        List<EnemyAbilityHolder> readyAbility = new List<EnemyAbilityHolder>();
        for(int i=0; i<AbilityHolders.Length; i++) {
            if(AbilityHolders[i].cooldownTime <= 0)
                readyAbility.Add(AbilityHolders[i]);
        }
        if(readyAbility.Count != 0)
            AbilityHolder = readyAbility[Random.Range(0,readyAbility.Count - 1)];
        else {
            AbilityHolder = AbilityHolders[0];
            for(int i=0; i<AbilityHolders.Length; i++) {
                if(AbilityHolders[i].cooldownTime <= AbilityHolder.cooldownTime)
                    AbilityHolder = AbilityHolders[i];
            }
        }
        attackRange = AbilityHolder.ability.attackDistance;
    }

    void PrepareAttack() {
        if(timer <= 0){
            AbilityHolder.activeAbility = true;
            canControl = false;
            timer = AttackCooldown;
        }

    }

    void Attack() {
        AbilityHolder.activeTime = 0;
    }

    bool FoundPlayer() {
        if(targetDistance < sightRadius && targetStats.currentHealth > 0)
            return true;
        else
            return false;
    }

    void DropItem() {
        for(int i = 0; i < stats.c_Data.dropItems.Length; i++) {
            if(Random.Range(0,100) <= stats.c_Data.dropItems[i].dropPercent) {
                for(int j=0; j<Random.Range(1, stats.c_Data.dropItems[i].Count); j++) {
                    Transform dropitem = Instantiate((GameObject)Resources.Load("items/itemPrefab"), transform.position, Quaternion.identity, transform.parent.parent).transform;
                    dropitem.GetComponent<ItemOnWorld>().setItemData(stats.c_Data.dropItems[i].item, stats.enemyLevel, 1);
                    dropitem.GetComponent<Rigidbody2D>().velocity = (Vector3)Random.insideUnitCircle * 2;
                }
            }
        }
    }

    private void Die() {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        for(int i=0; i<AbilityHolders.Length; i++)
            AbilityHolders[i].enabled = false;
        
        int levelGap = targetStats.level - stats.enemyLevel;
        //float levelReduce = (levelGap >= -5 && levelGap <= 7)? levelGap > 0? (float)Mathf.Pow(1.15f, -levelGap) 
                                                             //: 1 + (float)(Mathf.Pow(1.05f ,-levelGap) % 1 / 2) :levelGap > 0? 0.3f: 1.2f;

        float levelReduce = (levelGap >= -5 && levelGap <= 7)? levelGap > 0? (float)Mathf.Pow(1.15f, -levelGap) 
                                                             : 1 + (float)(Mathf.Pow(1.05f ,-levelGap) % 1 / 2) :levelGap > 0? 0.3f: 1.2f;
        targetStats.AddExp((int)(stats.maxExp * levelReduce));
        DropItem();
    }

    private void DestroyGameObject() {
        Instantiate(stats.DeadEffect, transform.position, Quaternion.identity, transform.parent);
        Destroy(gameObject);
    }
}
