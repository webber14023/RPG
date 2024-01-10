using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackClass : MonoBehaviour
{   
    public float knockBackPower;
    private string target;
    public AudioClip[] Audios;
    Vector2 Derection;
    Animator anim;
    AbilityStats stats;
    AudioSource audioSource;

    List<Collider2D> attackedTarget = new List<Collider2D>();

    void Start()
    {
        anim = GetComponent<Animator>();
        stats = transform.GetComponent<AbilityStats>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = stats.ActvateSound;
        audioSource.clip = Audios[Random.Range(0,Audios.Length-1)];
        target = transform.parent.CompareTag("Player")? "Enemy": "Player";
        anim.enabled = false;
        knockBackPower = stats.abilityknockBackPower;
        if(stats.abilityDelayTime == 0 || !stats.isTrack) {
            Derection = stats.Derection;
            float angle = Mathf.Atan2(Derection.y, Derection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            if(stats.abilityDelayTime == 0) {
                anim.enabled = true;
                audioSource.Play();
            }
        }
    }

    void Update() {
        if(stats.abilityDelayTime > 0) {
            stats.abilityDelayTime -= Time.deltaTime;
            if(stats.abilityDelayTime <= 0) {
                if(stats.isTrack) {
                    Derection = PlayerMove.GetMouseDerection();
                    float angle = Mathf.Atan2(Derection.y, Derection.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
                }
                anim.enabled = true;
                audioSource.Play();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag(target) && !attackedTarget.Contains(other)) {
            attackedTarget.Add(other);
            CharacterStats character = other.GetComponent<CharacterStats>();
            if(character.canDamage) {
                int currentDamage = (int)(stats.abilityDamage * Random.Range(0.9f,1.1f));
                Vector2 attackDerection = other.transform.position - transform.parent.position;
                character.TakeDamage(currentDamage, stats.isAttackDamage, transform.parent.GetComponent<CharacterStats>(), attackDerection.normalized * knockBackPower);
            }
        }
    }
    private void Die() {
        Destroy(gameObject);
    }

}
