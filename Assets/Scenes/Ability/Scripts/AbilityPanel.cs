using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class AbilityPanel : MonoBehaviour
{
    public PlayerAbilitysList playerAbilitys;
    public AbilityManager abilityManager;
    public GameObject SlotPerfeb;

    public Ability selectAbility;
    public List<GameObject> AbilitySlots = new List<GameObject>();

    bool switchAbility;
    int switchAbilityID;

    public void SetAbilitySlots() {
        for(int i=0; i<AbilitySlots.Count; i++) {
            AbilitySlots[i].SetActive(false);
        }
        for(int i=0; i<playerAbilitys.Abilitys.Count; i++) {
            AbilitySlots[i].SetActive(true);
            AbilitySlots[i].transform.GetChild(0).GetComponent<Image>().sprite = playerAbilitys.Abilitys[i].playerAbility != null? playerAbilitys.Abilitys[i].playerAbility.abilityImage : null;
            AbilitySlots[i].name = i.ToString();
        }
    }

    public void ChangeAbility(GameObject target) {
        switchAbility = false;
        int AbilityID = int.Parse(target.name);
        for(int i=0; i<playerAbilitys.Abilitys.Count; i++) {
            if(playerAbilitys.Abilitys[i].playerAbility == selectAbility) {
                switchAbility = true;
                switchAbilityID = i;
                break;
            }
        }
        if(switchAbility) {
            Debug.Log("switch Ability");
            var selectTemp = playerAbilitys.Abilitys[AbilityID];
            var switchTemp = playerAbilitys.Abilitys[switchAbilityID];
            selectTemp.playerAbility = selectAbility;
            switchTemp.playerAbility = playerAbilitys.Abilitys[AbilityID].playerAbility;

            playerAbilitys.Abilitys[AbilityID] = selectTemp;
            playerAbilitys.Abilitys[switchAbilityID] = switchTemp;
        }
        else {
            var Data = playerAbilitys.Abilitys[AbilityID];
            Data.playerAbility = selectAbility;
            playerAbilitys.Abilitys[AbilityID] = Data;
        }

        abilityManager.UpdateAbilityData();

        transform.parent.gameObject.SetActive(false);
    }
}
