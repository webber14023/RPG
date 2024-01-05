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
    
    public Ability[] abilitys;
    public AbilityTreeButton[] AbilityButtons;

    [Header("UI")]
    public Text AbilityNameText; 
    public Text AbilityLvText, AbilityDesText, AbilityPointText;

    private bool showMoreInfo;

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

    private void Update() {
        if(Input.GetKeyDown(KeyCode.LeftShift)) {
            showMoreInfo = true;
            DisplayAbilityInfo();
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift)) {
            showMoreInfo = false;
            DisplayAbilityInfo();
        }
    }

    public void UpgradeButton() {
        if(activeAbility == null || activeAbility.AbilityMaxLevel <= activeAbility.AbilityLevel) {
            return;
        }

        if(C_stats.abilityPoint > 0 && activeAbility.preskills.Length == 0) {
            if(activeAbility.preskills.Length == 0)
                UpgradeAbility();
            else {
                for(int i=0; i < activeAbility.preskills.Length; i++) {
                    
                }
            }
        }
    }

    public void EquipButton() {
        if(activeAbility == null || !activeAbility.isUnlocked) {
            return;
        }
        EquipAbilityPanel.parent.gameObject.SetActive(true);
        abilityPanel.selectAbility = activeAbility;
        abilityPanel.SetAbilitySlots();
    }

    public void ResetButton() {
        C_stats.abilityPoint = C_stats.c_Data.abilityPointPerLv * C_stats.level;
        for(int i=0; i<abilitys.Length; i++) {
            abilitys[i].AbilityLevel = 0;
            abilitys[i].isUnlocked = false;
            abilitys[i].damagePercentage = abilitys[i].BaseDamagePercentage;
            abilitys[i].cooldownTime = abilitys[i].BaseCooldownTime;
            abilitys[i].ResetAbility();
        }
        UpdatePointUI();
        DisplayAbilityLevel();
        if (activeAbility != null)
            DisplayAbilityInfo();
    }

    public void UpgradeAbility() {
        activeAbility.AbilityLevel++;
        activeAbility.damagePercentage = activeAbility.BaseDamagePercentage + activeAbility.damagePerLevel * activeAbility.AbilityLevel;
        activeAbility.cooldownTime = activeAbility.BaseCooldownTime + activeAbility.cooldownTimePerLevel * activeAbility.AbilityLevel;
        activeAbility.UpgradeAbility();
        C_stats.abilityPoint--;
        activeAbility.isUnlocked = true;
        activeButton.gameObject.transform.Find("Image").GetComponent<Image>().color = Color.white;
        activeButton.GetComponent<AbilityTreeButton>().LevelText.text = activeAbility.AbilityLevel.ToString();
        DisplayAbilityInfo();
        UpdatePointUI();
    }

    public void DisplayAbilityInfo() {
        Dictionary<string, string> AbilityValue = new Dictionary<string, string>() {
            {"Damage", (activeAbility.damagePercentage*100).ToString() + "%"},
            {"Cooldown", activeAbility.cooldownTime.ToString("0.00") + "秒"},
            {"AD", "物理傷害"},
            {"AP", "魔法傷害"},
            {"AttackCount", "Get"}
        };
        Dictionary<string, string> AbilityValuePerLv = new Dictionary<string, string>() {
            {"Damage", $"({activeAbility.damagePerLevel*100}%)"},
            {"Cooldown", $"({activeAbility.cooldownTimePerLevel}秒)"}
        };
        AbilityNameText.text = activeAbility.abilityName;
        AbilityLvText.text = "Skill Level : " + activeAbility.AbilityLevel + "/" + activeAbility.AbilityMaxLevel;
        string desText = "", value;
        string[] desTexts = activeAbility.abilityDes.Split('*');
        for(int i=0; i<desTexts.Length; i++) {
            if(AbilityValue.TryGetValue(desTexts[i],out value)){
                if(value != "Get") {
                    desText += value;
                    if(showMoreInfo && AbilityValuePerLv.TryGetValue(desTexts[i],out value))
                        desText += value;
                }
                else {
                    desText += GetAbilityValue(desTexts[i]);
                }
            }
            else
                desText += desTexts[i];
        }
        AbilityDesText.text = desText;
    }

    public string GetAbilityValue(string key) {
        if(key == "AttackCount") {
            MutiAttack Temp = (MutiAttack)activeAbility;
            if(showMoreInfo) {
                return Temp.attackCount.ToString() + $"({Temp.attackCountPerLv})";
            }
            else {
                return Temp.attackCount.ToString();
            }
        }
        return null;
    }

    public void UpdatePointUI() {
        AbilityPointText.text = "Skill Point : " + C_stats.abilityPoint.ToString();
    }

    public void DisplayAbilityLevel() {
        for(int i=0; i < AbilityButtons.Length; i++) {
            AbilityButtons[i].LevelText.text = AbilityButtons[i].AbilityData.AbilityLevel.ToString();
            if(AbilityButtons[i].AbilityData.isUnlocked) {
                AbilityButtons[i].gameObject.transform.Find("Image").GetComponent<Image>().color = Color.white;
            }
            else
                AbilityButtons[i].gameObject.transform.Find("Image").GetComponent<Image>().color = new Color(0.33f, 0.33f, 0.33f);
        }
        
    }
}
