using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    static EquipmentManager intance;
    
    public Transform Equipments;
    public InventoryManager Inventory;
    public CharacterStats Stats;

    public int maxHealth;
    public int attackDamage;
    public float speed;
    public float knockBackPower;

    EquipmentStats Equipmentstats;

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
        for(int i = 0; i < intance.Inventory.equipmentSlots.Count; i++) {
            intance.Equipmentstats = intance.Inventory.equipmentSlots[i].GetComponent<EquipmentStats>();
            if(intance.Equipmentstats != null){
                Debug.Log(intance.Equipments.GetChild(i).name + " " + i + " " + intance.Equipments.childCount);
                intance.Stats.EquipHealth += intance.Equipmentstats.equipmentHp;
                intance.Stats.EquipAttackDamage += intance.Equipmentstats.equipmentAttackDamage;
                intance.Stats.EquipSpeed += intance.Equipmentstats.equipmentSpeed;
            }
            
        }
        float hpPersent = (float)intance.Stats.currentHealth / intance.Stats.maxHealth;
        Debug.Log(hpPersent);
        intance.Stats.UpdateStats();
        intance.Stats.currentHealth = (int)(intance.Stats.maxHealth * hpPersent);
        intance.Stats.UpdateUI();
        intance.Equipmentstats = null;
    }

}
