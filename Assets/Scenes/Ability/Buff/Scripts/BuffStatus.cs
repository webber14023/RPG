using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffStatus : ScriptableObject
{
    public int BuffID;
    public string BuffName;
    public Sprite BuffImage;
    public string abilityDes;    
    public float activeTime;
    public GameObject effectPrefebs;
    public bool isDisplay;
    public virtual void Effect(GameObject parent) {}
    public virtual void RemoveEffect(GameObject parent) {}
}
