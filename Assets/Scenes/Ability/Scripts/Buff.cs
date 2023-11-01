using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "AddBuff", menuName = "Ability/AddBuff")]
public class Buff : Ability
{
    public BuffStatus buff;

    public override void Activate(GameObject parent) {
        BuffHolder buffholder = parent.GetComponent<BuffHolder>();
        buffholder.addBuff(buff);
    }
}
