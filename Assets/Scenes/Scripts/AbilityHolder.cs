using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    public Ability ability;
    float cooldownTime;
    float activeTime;
    public KeyCode key;
    AbilityManager manager;

    enum AbilityState {
        ready,
        active,
        cooldown
    }
    AbilityState state = AbilityState.ready;

    private void Start() {
        manager = GetComponent<AbilityManager>();
    }

    void Update() {
        switch (state) {
            case AbilityState.ready:
                if(Input.GetKeyDown(key) && !manager.isCasting) {
                    ability.Activate(gameObject);
                    state = AbilityState.active;
                    manager.isCasting = true;
                    activeTime = ability.activeTime;
                }
            break;
            case AbilityState.active:
                if(activeTime >= 0) {
                    activeTime -= Time.deltaTime;
                }
                else {
                    ability.BeginCooldown(gameObject);
                    manager.isCasting = false;
                    state = AbilityState.cooldown;
                    cooldownTime = ability.cooldownTime;
                }
            break;
            case AbilityState.cooldown:
                if(cooldownTime >= 0) {
                    cooldownTime -= Time.deltaTime;
                }
                else {
                    state = AbilityState.ready;
                }
            break;
        }
        
    }
}
