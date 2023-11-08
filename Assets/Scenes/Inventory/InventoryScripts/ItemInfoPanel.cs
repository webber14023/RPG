using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoPanel : MonoBehaviour
{
    public Text itemName;
    public Text itemLevel;
    public Text itemQuality;
    public Text itemInfo;

    void Update()
    {
        transform.position = Input.mousePosition;
        if(Input.GetKey(KeyCode.Mouse0)) {
            Destroy(gameObject);
        }
    }

    public void UpdateItemInfo(string Name, string Info, int Level, string Quality) {
        itemName.text = Name;
        itemLevel.text = "道具等級 : " + Level.ToString();
        itemQuality.text = Quality;
        itemInfo.text = Info;
    }
}
