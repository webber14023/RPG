using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackClass : MonoBehaviour
{
    public float delayTime;
    public float attackDelay;
    public float destroyTime;

    private string target;
    public GameObject Hitbox;
    public GameObject HitboxEffect;
    public BuffStatus attackDelayBuff;

    Collider2D HitboxCollider;
    AbilityStats stats;
    Animator anim;
    float timer;

    bool canAttack;
    private List<Collider2D> attackedTarget = new List<Collider2D>();

    Vector3 Effectscale;

    void Start() {
        Effectscale = Hitbox.transform.localScale / delayTime;
        HitboxEffect.transform.localScale = Vector3.zero;
        HitboxCollider = Hitbox.transform.GetComponent<Collider2D>();
        anim = transform.GetComponent<Animator>();
        stats = GetComponent<AbilityStats>();
        
        target = transform.parent.CompareTag("Player")? "Enemy": "Player";
        
    }

    void Update() {
        if(delayTime > 0){
            HitboxEffect.transform.localScale += Effectscale * Time.deltaTime;
            delayTime -= Time.deltaTime;
            if(delayTime <= 0) {
                HitboxEffect.transform.localScale = Hitbox.transform.localScale;
                HitboxCollider.enabled = true;
                anim.SetBool("Hit",true);
            }
        }
        else {
            Attack();
        }
    }

    void Attack(){
        if(timer > 0f) {
            timer -= Time.deltaTime;
        }
        else if(timer <= 0f) {
            timer = attackDelay;
            Debug.Log("attack");
            attackedTarget.Clear();
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.CompareTag(target) && !attackedTarget.Contains(other)) {
            attackedTarget.Add(other);
            int currentDamage = (int)(stats.abilityDamage*Random.Range(0.9f,1.1f));
            other.GetComponent<CharacterStats>().TakeDamage(currentDamage, stats.isAttackDamage, transform.parent.GetComponent<CharacterStats>(), Vector2.zero);
        }

    }

    void Die() {
        Destroy(gameObject, destroyTime);
    }
}
