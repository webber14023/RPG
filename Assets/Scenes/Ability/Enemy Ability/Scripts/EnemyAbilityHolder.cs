using UnityEngine;

public class EnemyAbilityHolder : MonoBehaviour
{
    public Ability ability;
    public float cooldownTime;
    public float activeTime;
    public bool activeAbility;

    enum AbilityState {
        ready,
        active,
        cooldown
    }
    AbilityState state = AbilityState.ready;

    private void Start() {
    }

    void Update() {
        switch (state) {
            case AbilityState.ready:
                if(activeAbility) {
                    ability.Activate(gameObject);
                    state = AbilityState.active;
                    activeTime = ability.activeTime;
                }
            break;
            case AbilityState.active:
                if(activeTime > 0) {
                    activeTime -= Time.deltaTime;
                    if (activeTime < 0)
                        activeTime = 0;
                }
                else {
                    ability.BeginCooldown(gameObject);
                    state = AbilityState.cooldown;
                    cooldownTime = ability.cooldownTime;
                    //activeAbility = false;
                }
            break;
            case AbilityState.cooldown:
                if(cooldownTime > 0) {
                    cooldownTime -= Time.deltaTime;
                    if (cooldownTime < 0)
                        cooldownTime = 0;
                }
                else {
                    state = AbilityState.ready;
                }
            break;
        }
    }

}
