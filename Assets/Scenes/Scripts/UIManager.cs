using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject[] UIPanels;
    public KeyCode[] Keys;
    GameObject currentPanel;
    KeyCode currentKey;
    AbilityManager manager;
    PlayerMove move;

    private void Start() {
        manager = GetComponent<AbilityManager>();
        move = GetComponent<PlayerMove>();
        DetectUI();
    }

    void Update()
    {
        DetectUI();
        if(currentPanel == null) {
            for(int i=0; i<UIPanels.Length; i++) {
                OpenUI(UIPanels[i], Keys[i]);
            }
        }
        else {
            CloseUI(currentPanel, currentKey);
        }
    }

    void OpenUI(GameObject UI, KeyCode key) {
        if(Input.GetKeyDown(key)) {
            currentPanel = UI;
            currentKey = key;
            UI.SetActive(true);
            manager.isCasting = true;
            move.canControl = false;
            PlayerMove.PlayerInteractSet(true);
        }  
    }
    void CloseUI(GameObject UI, KeyCode key) {
        if(Input.GetKeyDown(key) || (key != KeyCode.None && Input.GetKeyDown(KeyCode.Escape))) {
            currentPanel = null;
            currentKey = KeyCode.None;
            UI.SetActive(false);
            manager.isCasting = false;
            move.canControl = true;
            ShopManager.SetShopPanel(false);
            CraftingManager.SetPanel(false);
            PlayerMove.PlayerInteractSet(false);
        }  
    }
    void DetectUI() {
        for(int i=0; i<UIPanels.Length; i++) {
            if(UIPanels[i].activeSelf) {
                currentPanel = UIPanels[i];
                currentKey = Keys[i];
                manager.isCasting = true;
                move.canControl = false;
                break;
            }
        }
    }

}
