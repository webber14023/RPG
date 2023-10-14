using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityTreeButton : MonoBehaviour, IPointerClickHandler
{
    public Ability AbilityData;
    public Text LevelText;
    
    public void OnPointerClick(PointerEventData eventData){
        AbilityTreeManager.instance.activeAbility = AbilityData;
        AbilityTreeManager.instance.DisplayAbilityInfo();
    }
}
