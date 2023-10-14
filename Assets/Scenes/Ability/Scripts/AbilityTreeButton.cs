using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AbilityTreeButton : MonoBehaviour, IPointerClickHandler
{
    public Ability AbilityData;
    
    public void OnPointerClick(PointerEventData eventData){
        AbilityTreeManager.instance.activeAbility = AbilityData;
        AbilityTreeManager.instance.DisplayAbilityInfo();
    }
}
