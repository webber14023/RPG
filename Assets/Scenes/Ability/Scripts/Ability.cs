using UnityEngine;

public class Ability : ScriptableObject
{
    public int abilityID;
    public string abilityName;
    public int AbilityLevel;
    [TextArea]
    public string abilityDes;
    public bool isUnlocked;
    public Ability[] preskills;
    public float cooldownTime;
    public float activeTime;

    public virtual void Activate(GameObject parent) {}
    public virtual void BeginCooldown(GameObject parent) {}
}
