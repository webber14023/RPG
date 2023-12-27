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

    void Start()
    {
        anim = GetComponent<Animator>();
        stats = transform.GetComponent<AbilityStats>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = stats.ActvateSound;
        audioSource.clip = Audios[Random.Range(0,Audios.Length-1)];
        target = transform.parent.CompareTag("Player")? "Enemy": "Player";
        audioSource.Play();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag(target)) {
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
