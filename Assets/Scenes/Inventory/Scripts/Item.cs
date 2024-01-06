using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/New Item")]
public class Item : ScriptableObject
{
    public string ItemName;
    public string type;
    public Sprite ItemImage;
    public bool isStackable;
    public int baseLevel;
    public string baseQuality;
    public int prize = 100;
    public float prizePerLv = 1.05f;
    [TextArea]
    public string ItemInfo;

    public virtual void ItemFunction(string loaction, int itemID) {}
}

