using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConversationManager : MonoBehaviour
{
    public NpcData data;
    public char[] currentText;
    public Text NameText;
    public Text talkText;
    public Image talkImage;

    public float textSpeed;

    int charPoint, talkTextPoint;
    float timer;

    void Start()
    {
        currentText = data.Conversations[talkTextPoint].sentence.ToCharArray();
        NameText.text = data.Conversations[talkTextPoint].isNpcTalk? data.NPCName : "Player";
    }

    void Update()
    {
        if(charPoint < currentText.Length) {
            if(Input.GetKeyDown(KeyCode.Mouse0)) {
                Debug.Log("快轉");
                Debug.Log(talkTextPoint);
                charPoint = currentText.Length;
                talkText.text = data.Conversations[talkTextPoint].sentence;
            }
            else {
                if(timer > 0f) {
                    timer -= Time.deltaTime;
                }
                else if(timer <= 0f) {
                    timer = textSpeed;
                    talkText.text += currentText[charPoint];
                    charPoint++;
                }
            }
            
        }
        else if(Input.GetKeyDown(KeyCode.Mouse0)) {
            Debug.Log("下一句");
            talkTextPoint++;
            talkText.text = "";
            charPoint = 0;
            if(talkTextPoint < data.Conversations.Count) {
                currentText = data.Conversations[talkTextPoint].sentence.ToCharArray();
                NameText.text = data.Conversations[talkTextPoint].isNpcTalk? data.NPCName : "Player";
            }
            else {
                gameObject.SetActive(false);
            }
        }
    }

    public void SetConversation(NpcData setData) {
        data = setData;
    }
}
