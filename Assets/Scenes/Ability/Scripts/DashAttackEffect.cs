using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DashAttackEffect : MonoBehaviour
{
    private string target;
    public float timer;
    float MaxTimer;
    public Color effectColor;
    
    AbilityStats stats;
    SpriteRenderer sp;
    AudioSource audioSource;

    void Start()
    {
        MaxTimer = timer;
        stats = transform.GetComponent<AbilityStats>();
        sp = transform.GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        target = transform.parent.CompareTag("Player")? "Enemy": "Player";
        
        audioSource.Play();
    }

    private void Update() {
        timer -= Time.deltaTime;
        sp.color = new Color(effectColor.r, effectColor.g, effectColor.b, timer / MaxTimer);
        if(timer <= 0f)
            Destroy(gameObject);

    }


    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag(target)) {
            CharacterStats character = other.GetComponent<CharacterStats>();
            if(character.canDamage) {
                int currentDamage = (int)(stats.abilityDamage*Random.Range(0.9f,1.1f));
                character.ShowDamageText(other.gameObject, currentDamage);
                character.TakeDamage(currentDamage,Vector2.zero);
            }
        }
    }
    
}
