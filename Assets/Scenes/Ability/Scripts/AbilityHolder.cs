using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    public Ability ability;
    public float cooldownTime;
    public float activeTime;
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
                if(Input.GetKey(key) && !manager.isCasting) {
                    ability.Activate(gameObject);
                    state = AbilityState.active;
                    manager.isCasting = true;
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
                    manager.isCasting = false;
                    state = AbilityState.cooldown;
                    cooldownTime = ability.cooldownTime;
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
