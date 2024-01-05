using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[CreateAssetMenu(fileName = "New RegenItem", menuName = "Inventory/New RegenItem")]
public class RegenItem : Item
{
    public int regenValue;

    public override void ItemFunction(string location, int itemID)
    {
        GameObject.FindGameObjectWithTag("InventoryManager").transform.GetComponent<InventoryManager>().ReduceItem(itemID, location, 1);
        CharacterStats playerStats = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<CharacterStats>();
        playerStats.RegenHP(regenValue);
        
    }
}
