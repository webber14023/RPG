using UnityEngine;
using UnityEngine.UI;

public class Ability : ScriptableObject
{
    public int abilityID;
    public string abilityName;
    public Sprite abilityImage;
    public int AbilityLevel;
    [TextArea]
    public string abilityDes;
    public bool isUnlocked;
    public Ability[] preskills;
    public float cooldownTime;
    public float activeTime;
    public bool canSkip;

    public virtual void Activate(GameObject parent) {}
    public virtual void BeginCooldown(GameObject parent) {}
}
