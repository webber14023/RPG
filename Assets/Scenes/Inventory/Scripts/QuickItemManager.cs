using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuickItemManager : MonoBehaviour
{
    static QuickItemManager intance;
    public Inventory data;
    public List<KeyCode> keys = new List<KeyCode>();

    public Image[] itemImages;
    
    void Awake() {
        if(intance != null)
            Destroy(this);
        intance = this;
    }

    private void Update() {
        for(int i=0; i<data.itemList.Count; i++) {
            if(data.itemList[i] != null && Input.GetKeyDown(keys[i])) {
                data.itemList[i].ItemFunction("QuickItemList", i);
                InventoryManager.RefreshItem();
            }
        }
        
    }
}
