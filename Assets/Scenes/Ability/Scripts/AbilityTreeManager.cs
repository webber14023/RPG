using System.Collections;
using System.Collections.Generic;
using UnityEditor.Purchasing;
using UnityEngine;
using UnityEngine.UI;

public class AbilityTreeManager : MonoBehaviour
{
    public static AbilityTreeManager instance;
    public Ability activeAbility;
    public int AbilityPoint;

    public AbilityTreeButton[] AbilityButtons;
    [Header("UI")]
    public Text AbilityNameText; 
    public Text AbilityLvText, AbilityDesText, AbilityPointText;

    private void Awake() {
        if(instance == null) {
            instance = this;
        }
        else if(instance != this){
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        UpdatePointUI();
    }

    public void UpgradeButton() {
        if(activeAbility == null) {
            return;
        }

        if(AbilityPoint > 0 && activeAbility.preskills.Length == 0) {
            if(activeAbility.preskills.Length == 0)
                UpdateAbility();
            else {
                for(int i=0; i < activeAbility.preskills.Length; i++) {
                    
                }
            }
                
        }
    }
    public void UpdateAbility() {
        AbilityButtons[activeAbility.abilityID].GetComponent<Image>().color = Color.white;
        activeAbility.AbilityLevel++;
        AbilityPoint--;
        activeAbility.isUnlocked = true;
        DisplayAbilityInfo();
        UpdatePointUI();
    }
    public void DisplayAbilityInfo() {
        AbilityNameText.text = activeAbility.abilityName;
        AbilityLvText.text = "Skill Level : " + activeAbility.AbilityLevel;
        AbilityDesText.text = activeAbility.abilityDes;
    }

    public void UpdatePointUI() {
        AbilityPointText.text = "Skill Point : " + AbilityPoint;
    }
}
