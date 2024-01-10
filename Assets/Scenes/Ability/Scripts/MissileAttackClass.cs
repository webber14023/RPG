using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileAttackClass : MonoBehaviour
{
    public float speed;
    public float knockBackPower;
    public float destroyTime;
    public float delayTime;
    public bool isTrack;
    [Header("附加效果")]
    public BuffStatus[] Buffs;
    
    private string target;
    Vector2 Derection;
    float angle;
    bool fire;
    SpriteRenderer lineSp;

    Vector2 mouseDerection;
    Rigidbody2D rb;
    Animator anim;
    AbilityStats stats;
    AudioSource audioSource;
    
    List<Collider2D> attackedTarget = new List<Collider2D>();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        stats = GetComponent<AbilityStats>();
        audioSource = GetComponent<AudioSource>();
        lineSp = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        
        target = transform.parent.CompareTag("Player")? "Enemy": "Player";
        fire = false;
        delayTime = stats.abilityDelayTime;
        Derection = stats.Derection;
        isTrack = stats.isTrack;
        audioSource.pitch = Random.Range(0.9f,1.1f);
    }

    void FixedUpdate() {
        if(!anim.GetBool("Hit")) {
            if(delayTime > 0f) {
                if(stats.isTrack) {
                    if(target == "Enemy")
                        Derection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
                    else if(target == "Player" && isTrack) {
                        Enemy controller = transform.parent.GetComponent<Enemy>();
                        Derection = (controller.target.GetChild(0).position - transform.position).normalized;
                    }
                    angle = Mathf.Atan2(Derection.y, Derection.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
                    
                    rb.velocity = new Vector2(0, 0);
                    rb.AddForce(Derection.normalized * speed * 0.05f);
                }
                lineSp.color = new Color(255, 255, 255, (1f - (delayTime/stats.abilityDelayTime))/2f);
                delayTime -= Time.deltaTime;
            }
            else if (!fire) {
                /*if(target == "Enemy")
                    Derection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
                else if(target == "Player") {
                    Enemy controller = transform.parent.GetComponent<Enemy>();
                    Derection = (controller.target.position - transform.position).normalized;
                }*/
                angle = Mathf.Atan2(Derection.y, Derection.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
                Destroy(transform.GetChild(0).gameObject);
                fire = true;
                StartCoroutine(DestoryTimer());
                rb.AddForce(Derection.normalized * speed);
                
                audioSource.clip = stats.ActvateSound;
                audioSource.Play();
            }
        }
        else {
            if(!fire) {
                fire = true;
                Destroy(transform.GetChild(0).gameObject);
            }
            rb.velocity = new Vector2(0, 0);

        }
    }
    
    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Wall")) {
            anim.SetBool("Hit",true);
            audioSource.clip = stats.HitSound;
            audioSource.Play();
        }
           

        if (other.CompareTag(target) && !attackedTarget.Contains(other)) {
            attackedTarget.Add(other);
            CharacterStats characterStats = other.GetComponent<CharacterStats>();
            BuffHolder BuffHolder = other.GetComponent<BuffHolder>();
            if(characterStats.canDamage) {
                anim.SetBool("Hit",true);
                audioSource.clip = stats.HitSound;
                audioSource.Play();

                for(int i=0; i<Buffs.Length; i++) {
                BuffHolder.addBuff(Buffs[i]);
                }
                int currentDamage = (int)(stats.abilityDamage*Random.Range(0.9f,1.1f));
                Vector2 attackDerection = other.transform.GetChild(0).position - transform.position;
                characterStats.TakeDamage(currentDamage, stats.isAttackDamage, transform.parent.GetComponent<CharacterStats>(), attackDerection.normalized * knockBackPower);

            }
        }
    }
    private void Die() {
        Destroy(gameObject);
    }

    IEnumerator DestoryTimer()
    {
        yield return new WaitForSeconds(destroyTime);
        anim.SetBool("Hit",true);
    }
}
