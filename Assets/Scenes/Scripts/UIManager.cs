using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject[] UIPanels;
    public KeyCode[] Keys;
    bool isOpenUI;
    AbilityManager manager;

    private void Start() {
        manager = GetComponent<AbilityManager>();
    }

    void Update()
    {
        for(int i=0; i<UIPanels.Length; i++) {
            ContralUI(UIPanels[i], Keys[i]);
        }
    }

    void ContralUI(GameObject UI, KeyCode key) {
        if(Input.GetKeyDown(key)) {
            UI.SetActive(!UI.activeSelf);
            manager.isCasting = true;
            isOpenUI = false;
            for(int i=0; i<UIPanels.Length; i++) {
                if(UIPanels[i].activeSelf) {
                    isOpenUI = true;
                    break;
                }
            }
            if(!isOpenUI)
                manager.isCasting = false;
            else
                manager.isCasting = true;
        }  
    }

}
