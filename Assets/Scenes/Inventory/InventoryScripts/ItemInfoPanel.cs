using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoPanel : MonoBehaviour
{
    public Text itemName;
    public Text itemInfo;

    void Update()
    {
        transform.position = Input.mousePosition;
        if(Input.GetKey(KeyCode.Mouse0)) {
            Destroy(gameObject);
        }
    }

    public void UpdateItemInfo(string Name, string Info) {
        itemName.text = Name;
        itemInfo.text = Info;
    }
}
