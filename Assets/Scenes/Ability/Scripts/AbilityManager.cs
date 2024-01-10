using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour
{
    public static AbilityManager intance;
    public bool isCasting = false;

    public PlayerAbilitysList PlayerAbilityData;
    
    public List<KeyCode> keyCodes = new List<KeyCode>();

    public List<AbilityHolder> abilityHolders = new List<AbilityHolder>();
    public GameObject[] abilitySlots;
    public List<Image> AbilityIcon = new List<Image>();
    public List<Image> cooldownEffect = new List<Image>();
    public List<Text> cooldownText = new List<Text>();
    public List<Image> activeEffect = new List<Image>();
    public List<Text> abilityKeyText = new List<Text>();

    void Awake() {
        if(intance != null)
            Destroy(this);
        intance = this;
    }

    private void Start() {
        /*for(int i=0; i<PlayerAbilityData.Abilitys.Count; i++) {
            Debug.Log(PlayerAbilityData.Abilitys[i].playerAbility);
            Debug.Log(PlayerAbilityData.Abilitys[i].playerAbility != null);
            if(PlayerAbilityData.Abilitys[i].playerAbility != null) {
                abilityHolders[i] = gameObject.AddComponent(typeof(AbilityHolder)) as AbilityHolder;
                abilityHolders[i].ability = PlayerAbilityData.Abilitys[i].playerAbility;
                abilityHolders[i].key = keyCodes[i];
            }
        }*/
        for(int i=0; i<abilitySlots.Length; i++) {
            AbilityIcon.Add(abilitySlots[i].transform.GetChild(0).GetComponent<Image>());
            cooldownEffect.Add(abilitySlots[i].transform.GetChild(1).GetComponent<Image>());
            cooldownText.Add(abilitySlots[i].transform.GetChild(2).GetComponent<Text>());
            activeEffect.Add(abilitySlots[i].transform.GetChild(3).GetComponent<Image>());
            abilityKeyText.Add(abilitySlots[i].transform.GetChild(4).GetComponent<Text>());
        }
        
        UpdateAbilityData();
        //UpdateAbilityIcon();
    }

    private void Update() {
        for(int i=0; i<abilityHolders.Count; i++)
        {
            if(abilityHolders[i] != null) {
                if(abilityHolders[i].cooldownTime > 0) {
                    cooldownEffect[i].fillAmount = abilityHolders[i].cooldownTime / abilityHolders[i].ability.cooldownTime;
                    cooldownText[i].text = abilityHolders[i].cooldownTime.ToString("F1");
                }
                else {
                    cooldownEffect[i].fillAmount = 0;
                    cooldownText[i].text = null;
                }

                if(abilityHolders[i].activeTime >= 0) {
                    activeEffect[i].fillAmount = abilityHolders[i].activeTime / abilityHolders[i].ability.activeTime;
                }
            }
        }
    }

    public void UpdateAbilityIcon() {
        for(int i=0; i<abilityHolders.Count; i++)
        {
            AbilityIcon[i].sprite = abilityHolders[i].ability.abilityImage;
            abilityKeyText[i].text = abilityHolders[i].key.ToString();
        }
    }

    public void UpdateAbilityData() {
        Debug.Log("updateData");
        for(int i=0; i<abilityHolders.Count; i++) {
            Destroy(abilityHolders[i]);
        }
        abilityHolders.Clear();

        for(int i=0; i<PlayerAbilityData.Abilitys.Count; i++) {
            if(PlayerAbilityData.Abilitys[i].playerAbility != null) {
                abilityHolders.Add(gameObject.AddComponent(typeof(AbilityHolder)) as AbilityHolder);
                abilityHolders[i].ability = PlayerAbilityData.Abilitys[i].playerAbility;
                abilityHolders[i].key = keyCodes[i];
            }
        }
        UpdateAbilityIcon();
    }

    public static void SetAbilityData(PlayerAbilitysList newData) {
        intance.PlayerAbilityData = newData;
        intance.UpdateAbilityData();
    }

    public static PlayerAbilitysList GetAbilitysList() {
        return intance.PlayerAbilityData;
    }
}
