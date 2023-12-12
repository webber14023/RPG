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

    public int maxHealth;
    public int attackDamage;
    public float speed;
    public float knockBackPower;

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
        Debug.Log("Update Equipment");
        intance.Stats.EquipHealth = 0;
        intance.Stats.EquipAttackDamage = 0;
        intance.Stats.EquipSpeed = 0;
        for(int i = 0; i < intance.Equipments.itemList.Count; i++) {
            if(intance.Equipments.itemList[i] != null){
                Equipment equipment = (Equipment)intance.Equipments.itemList[i];
                intance.Stats.EquipHealth += equipment.equipmentHp;
                intance.Stats.EquipAttackDamage += equipment.equipmentAttackDamage;
                intance.Stats.EquipSpeed += equipment.equipmentSpeed;
            }
            
        }
    }

}
