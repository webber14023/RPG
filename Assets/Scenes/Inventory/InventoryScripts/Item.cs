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
    public int ItemHeld;
    public int prize;
    [TextArea]
    public string ItemInfo;
}

