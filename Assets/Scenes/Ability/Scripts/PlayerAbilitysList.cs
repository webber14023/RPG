using System.Collections;
using System.Collections.Generic;
using Mono.Cecil.Cil;
using UnityEngine;


[CreateAssetMenu(fileName = "New PlayerAbilityList", menuName = "AbilityList/New PlayerAbilityList")]
public class PlayerAbilitysList : ScriptableObject
{
    public List<AbilityData> Abilitys = new List<AbilityData>();

    [System.Serializable]public struct AbilityData {
        public Ability playerAbility;
        public KeyCode abilityKey;
    }
}
