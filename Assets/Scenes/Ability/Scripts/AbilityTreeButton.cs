using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityTreeButton : MonoBehaviour, IPointerClickHandler
{
    public Ability AbilityData;
    public Text LevelText;
    
    private void Start() {
        transform.GetChild(0).GetComponent<Image>().sprite = AbilityData.abilityImage;
    }

    public void OnPointerClick(PointerEventData eventData){
        AbilityTreeManager.intance.activeAbility = AbilityData;
        AbilityTreeManager.intance.activeButton = gameObject;
        AbilityTreeManager.intance.DisplayAbilityInfo();
    }
}
