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
    public AudioClip EquipSound;
    public AudioSource audioSource;

    EquipmentStats Equipmentstats;

    void Awake() {  
        if(intance != null)
            Destroy(this);
        intance = this;
    }

    void Start() {  
        Stats = GetComponent<CharacterStats>();
        audioSource = GetComponent<AudioSource>();
    }

    public static void UpdateEquipmentStats() {
        intance.Stats.ResetEquipmentsStats();
        for(int i = 0; i < intance.Inventory.equipmentSlots.Count; i++) {
            intance.Equipmentstats = intance.Inventory.equipmentSlots[i].GetComponent<EquipmentStats>();
            if(intance.Equipmentstats != null){ 
                intance.Stats.EquipHealth += intance.Equipmentstats.equipmentHp;
                intance.Stats.EquipAttackDamage += intance.Equipmentstats.equipmentAttackDamage;
                intance.Stats.EquipAbilityPower += intance.Equipmentstats.equipmentAbilityPower;
                intance.Stats.EquipSpeed += intance.Equipmentstats.equipmentSpeed;
                intance.Stats.EquipAttackArmor += intance.Equipmentstats.equipmentAttackArmor;
                intance.Stats.EquipMagicArmor += intance.Equipmentstats.equipmentMagicArmor;
                intance.Stats.EquipCriticalChance += intance.Equipmentstats.equipmentCriticalChance;
                intance.Stats.EquipCriticalMultiply += intance.Equipmentstats.equipmentCriticalMultiply;
            }
        }

        float hpPersent = (float)intance.Stats.currentHealth / intance.Stats.maxHealth;
        intance.Stats.UpdateStats();
        intance.Stats.currentHealth = (int)(intance.Stats.maxHealth * hpPersent);
        intance.Stats.UpdateUI();
        intance.Equipmentstats = null;
    }

    public static void PlayEquipSound() { 
        intance.audioSource.PlayOneShot(intance.EquipSound);
    }

}
