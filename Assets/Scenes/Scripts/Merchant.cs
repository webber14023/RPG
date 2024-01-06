using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant : interactivityObject
{
    public NpcData Data;

    public override void Start() {
        base.Start();
    }

    public override void Interact() {
        ConversationManager.SetConversation(Data);
    }
}
