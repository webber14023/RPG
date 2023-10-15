using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Header("Stats")]
    public float maxHealth;
    public float currentHealth;
    public float speed;
    public float attackDamage;
    

    public void ChangeStats(float addHp, float addDamage, float addSpeed) {
        maxHealth += addHp;
        attackDamage += addDamage;
        speed += addSpeed;
    }
}
