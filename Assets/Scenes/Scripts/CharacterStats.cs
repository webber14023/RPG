using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Header("Stats")]
    public float maxHealth;
    public float currentHealth;
    public float speed;
    public int attackDamage;
    public float knockBackPower;
    

    public void ChangeStats(float addHp, int addDamage, float addSpeed) {
        maxHealth += addHp;
        attackDamage += addDamage;
        speed += addSpeed;
    }
}
