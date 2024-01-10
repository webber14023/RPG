using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[CreateAssetMenu(fileName = "New RegenItem", menuName = "Inventory/New RegenItem")]
public class RegenItem : Item
{
    public int regenValue;
    public BuffStatus DeBuff;

    public override void ItemFunction(string location, int itemID)
    {
        BuffHolder buffHolder = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<BuffHolder>();
        if(!buffHolder.buffs.Contains(DeBuff)) {
            InventoryManager.ReduceItem(itemID, location, 1);
            CharacterStats playerStats = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<CharacterStats>();
            playerStats.RegenHP(regenValue);
            buffHolder.addBuff(DeBuff);
        }
    }
}
