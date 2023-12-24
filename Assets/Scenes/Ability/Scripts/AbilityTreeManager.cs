using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class AbilityTreeManager : MonoBehaviour
{
    public static AbilityTreeManager instance;
    public Ability activeAbility;
    public GameObject activeButton;
    public Transform EquipAbilityPanel;
    public AbilityPanel abilityPanel;
    public CharacterStats C_stats;
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
        abilityPanel = EquipAbilityPanel.GetComponent<AbilityPanel>();
        UpdatePointUI();
        DisplayAbilityLevel();
    }

    public void UpgradeButton() {
        if(activeAbility == null) {
            return;
        }

        if(C_stats.abilityPoint > 0 && activeAbility.preskills.Length == 0) {
            if(activeAbility.preskills.Length == 0)
                UpdateAbility();
            else {
                for(int i=0; i < activeAbility.preskills.Length; i++) {
                    
                }
            }
                
        }
    }

    public void EquipButton() {
        if(activeAbility == null) {
            return;
        }
        EquipAbilityPanel.parent.gameObject.SetActive(true);
        abilityPanel.selectAbility = activeAbility;
        abilityPanel.SetAbilitySlots();
    }

    public void UpdateAbility() {
        activeAbility.AbilityLevel++;
        C_stats.abilityPoint--;
        activeAbility.isUnlocked = true;
        activeButton.gameObject.transform.Find("Image").GetComponent<Image>().color = Color.white;
        activeButton.GetComponent<AbilityTreeButton>().LevelText.text = activeAbility.AbilityLevel.ToString();
        DisplayAbilityInfo();
        UpdatePointUI();
    }
    public void DisplayAbilityInfo() {
        AbilityNameText.text = activeAbility.abilityName;
        AbilityLvText.text = "Skill Level : " + activeAbility.AbilityLevel;
        AbilityDesText.text = activeAbility.abilityDes;
    }

    public void UpdatePointUI() {
        AbilityPointText.text = "Skill Point : " + C_stats.abilityPoint.ToString();
    }

    public void DisplayAbilityLevel() {
        for(int i=0; i < AbilityButtons.Length; i++)
        {
            AbilityButtons[i].LevelText.text = AbilityButtons[i].AbilityData.AbilityLevel.ToString();
            if(AbilityButtons[i].AbilityData.isUnlocked) {
                AbilityButtons[i].gameObject.transform.Find("Image").GetComponent<Image>().color = Color.white;
            }
        }
        
    }
}
