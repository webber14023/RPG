using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffHolder : MonoBehaviour
{
    public GameObject playerBuffsGrid;
    public GameObject BuffSlotPrefeb;
    public List<BuffStatus> buffs = new List<BuffStatus>();
    public List<GameObject> buffsSlot = new List<GameObject>();
    public List<Text> SlotTime = new List<Text>();
    public List<float> timer = new List<float>();

    public void Update() {
        for(int i=0; i<buffs.Count; i++) {
            timer[i] -= Time.deltaTime;
            SlotTime[i].text = timer[i].ToString("F1");
            if(timer[i] <= 0) {
                buffs[i].RemoveEffect(gameObject);
                Destroy(buffsSlot[i]);
                timer.RemoveAt(i);
                buffs.RemoveAt(i);
                SlotTime.RemoveAt(i);
                buffsSlot.RemoveAt(i);
                i--;
            }
        }
    }

    public void addBuff(BuffStatus buff) {
        buffs.Add(buff);
        timer.Add(buff.activeTime);
        buff.Effect(gameObject);
        buffsSlot.Add(Instantiate(BuffSlotPrefeb, playerBuffsGrid.transform));
        buffsSlot[buffsSlot.Count-1].transform.GetChild(0).GetComponent<Image>().sprite = buff.BuffImage;
        SlotTime.Add(buffsSlot[buffsSlot.Count-1].transform.GetChild(1).GetComponent<Text>());
    }
}
