using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackClass : MonoBehaviour
{
    public float delayTime;
    public float attackDelay;
    public float destroyTime;
    public float knockBackPower;
    
    public BuffStatus[] Buffs;

    private string target;
    public GameObject Hitbox;
    public GameObject HitboxEffect;
    public BuffStatus attackDelayBuff;
    public AudioClip soundEffect;

    Collider2D HitboxCollider;
    AbilityStats stats;
    Animator anim;
    AnimatorClipInfo [] ClipInfo; 
    float timer, destroyTimer, readyAnimTime;

    bool canAttack;
    private List<Collider2D> attackedTarget = new List<Collider2D>();

    Vector3 Effectscale;
    AudioSource audioSource;

    void Start() {
        Effectscale = Hitbox.transform.localScale / delayTime;
        HitboxEffect.transform.localScale = Vector3.zero;
        HitboxCollider = Hitbox.transform.GetComponent<Collider2D>();
        anim = transform.GetComponent<Animator>();
        anim.enabled = false;
        ClipInfo = anim.GetCurrentAnimatorClipInfo(0);
        readyAnimTime = ClipInfo[0].clip.length;
        Debug.Log(readyAnimTime);
        audioSource = transform.GetComponent<AudioSource>();
        stats = GetComponent<AbilityStats>();
        destroyTime = stats.abilityDestroyTime;
        
        target = transform.parent.CompareTag("Player")? "Enemy": "Player";
        
    }

    void Update() {
        if(delayTime > 0){
            HitboxEffect.transform.localScale += Effectscale * Time.deltaTime;
            delayTime -= Time.deltaTime;
            if(delayTime <= readyAnimTime)
                anim.enabled = true;
            if(delayTime <= 0) {
                HitboxEffect.transform.localScale = Hitbox.transform.localScale;
                HitboxCollider.enabled = true;
                destroyTimer = destroyTime;
                anim.SetBool("Hit",true);
                audioSource.PlayOneShot(soundEffect);
            }
        }
        else {
            Attack();
            DestoryEffect();
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
    void DestoryEffect() {
        if(destroyTimer > 0f) {
            destroyTimer -= Time.deltaTime;
        }
        else if(destroyTimer <= 0f) {
            anim.SetBool("Destroy", true);
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.CompareTag(target) && !attackedTarget.Contains(other)) {
            attackedTarget.Add(other);
            BuffHolder BuffHolder = other.GetComponent<BuffHolder>();
            for(int i=0; i<Buffs.Length; i++) {
                BuffHolder.addBuff(Buffs[i]);
            }
            int currentDamage = (int)(stats.abilityDamage*Random.Range(0.9f,1.1f));
            Vector2 attackDerection = transform.position - other.transform.position;
            other.GetComponent<CharacterStats>().TakeDamage(currentDamage, stats.isAttackDamage, transform.parent.GetComponent<CharacterStats>(), attackDerection.normalized * knockBackPower);
        }

    }

    void Die() {
        Destroy(gameObject);
    }
}
