using UnityEngine;
using UnityEngine.UI;

public class Ability : ScriptableObject
{
    public int abilityID;
    public string abilityName;
    public Sprite abilityImage;
    public int AbilityLevel;
    public int AbilityMaxLevel;
    [TextArea]
    public string abilityDes;
    public bool isUnlocked;
    public Ability[] preskills;
    public float cooldownTime;
    public float activeTime;

    public bool canSkip;
    public float attackDistance; //enemy

    [Header("Stats")]
    public bool isAttackDamage;
    public float damagePercentage;
    public float BaseDamagePercentage;
    public float BaseCooldownTime;
    public float damagePerLevel;
    public float cooldownTimePerLevel;


    public virtual void Activate(GameObject parent) {}
    public virtual void BeginCooldown(GameObject parent) {}
}
