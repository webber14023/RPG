using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public enum EnemyStates {GUARD, PATROL, CHASE, DEAD }

public class Enemy : MonoBehaviour
{
    private EnemyStates enemyStates;
    private EnemyAbilityHolder AbliltyHolder;
    public Transform target;

    public float sightRadius;
    public float attackRange;
    public bool canControl;
    float targetDistance;

    [Header("UI")]
    public GameObject hpBar;
    public GameObject damageTextPrefab;
    private Animator animator;
    private SpriteRenderer sp;
    private CharacterStats stats;

    private void Start() {
        animator = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        stats = GetComponent<CharacterStats>();
        AbliltyHolder = GetComponent<EnemyAbilityHolder>();
        target = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetComponent<Transform>();
        stats.currentHealth = stats.baseMaxHealth;
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
        }
        else
            enemyStates = EnemyStates.DEAD;

        switch (enemyStates)
        {
            case EnemyStates.GUARD:
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
        if(!stats.canControl) {
            return;
        }
        if(targetDistance > attackRange) {

            transform.position = Vector2.MoveTowards(transform.position, target.position, stats.speed* Time.deltaTime);
            animator.SetBool("Moving", true);
            if(transform.position.x > target.position.x) {

            }
            AbliltyHolder.activeAbility = false;
            sp.flipX = transform.position.x > target.position.x ? true : false;
        }
        else {
            AbliltyHolder.activeAbility = true;
            animator.SetBool("Moving", false);
        }
    }

    bool FoundPlayer() {
        if(targetDistance < sightRadius)
            return true;
        else
            return false;
    }


    private void Die() {
        Destroy(gameObject);
    }

}
