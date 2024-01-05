using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

[CreateAssetMenu(fileName = "NPC", menuName = "NPC/Data")]
public class NpcData : ScriptableObject
{
    public string NPCName;
    public Sprite NPCImage;
    public List<Conversation> Conversations = new List<Conversation>();

    [System.Serializable]public struct Conversation {
        public bool isNpcTalk;
        
        [TextArea]
        public string sentence;
    }
}
