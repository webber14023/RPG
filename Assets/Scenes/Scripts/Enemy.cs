using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.AI;
using System.Collections.Generic;

public enum EnemyStates {GUARD, PATROL, CHASE, DEAD }

public class Enemy : MonoBehaviour
{
    private EnemyStates enemyStates;
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
        AbilityHolder = AbilityHolders[Random.Range(0,AbilityHolders.Length)];
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
    }

    private void Update() {
        SwitchStates();
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
            currentAttackRange = attackRange + 0.5f;
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
        Debug.Log(readyAbility);
        Debug.Log(readyAbility.Count);
        if(readyAbility.Count != 0)
            AbilityHolder = readyAbility[Random.Range(0,readyAbility.Count - 1)];
        else {
            AbilityHolder = AbilityHolders[0];
            for(int i=0; i<AbilityHolders.Length; i++) {
                if(AbilityHolders[i].cooldownTime <= AbilityHolder.cooldownTime)
                    AbilityHolder = AbilityHolders[i];
            }
        }
    }

    void PrepareAttack() {
        if(timer <= 0){
            AbilityHolder.activeAbility = true;
            timer = AttackCooldown;
        }
        else {
            timer -= Time.deltaTime;
        }
    }

    void Attack() {
        Debug.Log("attack");
        AbilityHolder.activeTime = 0;
    }

    bool FoundPlayer() {
        if(targetDistance < sightRadius && targetStats.currentHealth > 0)
            return true;
        else
            return false;
    }

    void DropItem() {
        for(int i=0; i < stats.c_Data.dropItems.Length; i++) {
            if(Random.Range(0,100) <= stats.c_Data.dropItems[i].dropPercent) {
                for(int j=0; j<Random.Range(1, stats.c_Data.dropItems[i].Count); j++) {
                    Debug.Log("DropItem");
                    Instantiate((GameObject)Resources.Load("items/itemPrefab"), transform.position, Quaternion.identity, transform.parent.parent).GetComponent<ItemOnWorld>().setItemData(stats.c_Data.dropItems[i].item);

                }
            }
        }
    }


    private void Die() {
        targetStats.AddExp(stats.c_Data.maxExp);
        DropItem();
        Destroy(gameObject);
    }
}
