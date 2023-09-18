using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject myBag;
    public GameObject DungeonMap;
    public KeyCode OpenBag_Key;
    public KeyCode OpenDungeonMap_key;
    
    void Update()
    {
        OpenMyBag();
        OpenMap();
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
