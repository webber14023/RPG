using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConversationManager : MonoBehaviour
{
    static ConversationManager intance;
    public GameObject ConversationPenal;
    public NpcData data;
    public char[] currentText;
    public Text NameText;
    public Text talkText;
    public Image talkImage;

    public AudioClip TalkSound;
    public AudioSource audioSource;

    Color Gray = new Color(0.2f, 0.2f, 0.2f);

    public float textSpeed;

    int charPoint, talkTextPoint;
    float timer;

    void Awake() {
        if(intance != null)
            Destroy(this);
        intance = this;
    }

    void Update()
    {
        if(ConversationPenal.activeSelf) {
            if(charPoint < currentText.Length) {
                if(Input.GetKeyDown(KeyCode.Mouse0)) {
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
                        audioSource.PlayOneShot(TalkSound, 0.33f);
                        charPoint++;
                    }
                }
                
            }
            else if(Input.GetKeyDown(KeyCode.Mouse0)) {
                Debug.Log("finishSentance");
                talkTextPoint++;
                talkText.text = "";
                charPoint = 0;
                if(talkTextPoint < data.Conversations.Count && !data.RandomSentence) {
                    currentText = data.Conversations[talkTextPoint].sentence.ToCharArray();
                    NameText.text = data.Conversations[talkTextPoint].isNpcTalk? data.NPCName : "Player";
                    intance.talkImage.color = data.Conversations[talkTextPoint].isNpcTalk? Color.white : Gray;
                }
                else {
                    if(data.AfterTalkFunction != null)
                        data.AfterTalkFunction.ActivateFunction();
                    ConversationPenal.SetActive(false);
                }
            }
        }
    }

    public static void SetConversation(NpcData setData) {
        intance.talkTextPoint = 0;
        intance.charPoint = 0;
        intance.ConversationPenal.SetActive(true);
        intance.data = setData;
        if(setData.RandomSentence) {
            intance.talkTextPoint = Random.Range(0, setData.Conversations.Count);
        }
        intance.currentText = setData.Conversations[intance.talkTextPoint].sentence.ToCharArray();
        intance.NameText.text = setData.Conversations[intance.talkTextPoint].isNpcTalk? setData.NPCName : "Player";
        intance.talkImage.color = setData.Conversations[intance.talkTextPoint].isNpcTalk? Color.white : intance.Gray;
        
    }
}
