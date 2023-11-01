using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject myBag;
    public GameObject DungeonMap;
    public GameObject AbilityTree;
    public KeyCode OpenBag_Key;
    public KeyCode OpenDungeonMap_key;
    public KeyCode OpenAbilityTree_key;
    AbilityManager manager;

    private void Start() {
        manager = GetComponent<AbilityManager>();
    }

    void Update()
    {
        OpenMyBag();
        OpenMap();
        if(myBag.activeSelf || DungeonMap.activeSelf)
            manager.isCasting = true;
        else
            manager.isCasting = false;
    }
    void ContralUI(GameObject UI, KeyCode key) {
        if(Input.GetKeyDown(key))
            UI.SetActive(UI.activeSelf);
    }

    void OpenMyBag()
    {
        if(Input.GetKeyDown(OpenBag_Key))
        {
            myBag.SetActive(!myBag.activeSelf);
        }
    }
    void OpenMap()
    {
        if(Input.GetKeyDown(OpenDungeonMap_key))
        {
            DungeonMap.SetActive(!DungeonMap.activeSelf);
        }
    }
}
