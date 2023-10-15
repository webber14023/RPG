using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    static EquipmentManager intance;
    
    public Inventory Equipments;
    public PlayerMove player;
    public CharacterStats Stats;

    void Awake() {  
        if(intance != null)
            Destroy(this);
        intance = this;
    }

    void Start() {
        Stats = GetComponent<CharacterStats>();
        UpdateEquipmentStats();
    }

    public static void UpdateEquipmentStats() {
        for(int i = 0; i < intance.Equipments.itemList.Count; i++) {
            if(intance.Equipments.itemList[i] != null){
                Equipment equipment = (Equipment)intance.Equipments.itemList[i];
                Debug.Log(equipment.equipmentAttackDamage);
                intance.Stats.ChangeStats(equipment.equipmentHp, equipment.equipmentAttackDamage, equipment.equipmentSpeed);
            }
            
        }
    }

}
