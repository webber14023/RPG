using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Crafting/New Recipe")]
public class Recipe : ScriptableObject
{
    public Itemdata result;
    public Itemdata[] material;

    [System.Serializable]public struct Itemdata {
        public Item item;
        public int itemLevel;
        public int count;
        public string itemQuality;
    }
}
