using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour
{
    public bool isCasting = false;

    public AbilityHolder[] abilityHolders;
    public GameObject[] abilitySlots;
    public List<Image> AbilityIcon = new List<Image>();
    public List<Image> cooldownEffect = new List<Image>();
    public List<Text> cooldownText = new List<Text>();
    public List<Image> activeEffect = new List<Image>();

    private void Start() {
        for(int i=0; i<abilitySlots.Length; i++)
        {
            AbilityIcon.Add(abilitySlots[i].transform.GetChild(0).GetComponent<Image>());
            cooldownEffect.Add(abilitySlots[i].transform.GetChild(1).GetComponent<Image>());
            cooldownText.Add(abilitySlots[i].transform.GetChild(2).GetComponent<Text>());
            activeEffect.Add(abilitySlots[i].transform.GetChild(3).GetComponent<Image>());
        }
        UpdateAbilityIcon();
    }

    private void Update() {
        for(int i=0; i<abilityHolders.Length; i++)
        {
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

    public void UpdateAbilityIcon() {
        for(int i=0; i<abilityHolders.Length; i++)
        {
            AbilityIcon[i].sprite = abilityHolders[i].ability.abilityImage;
        }
    }
}
