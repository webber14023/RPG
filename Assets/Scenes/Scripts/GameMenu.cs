using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour
{
    public void SaveAndExit() {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<SaveLoadManager>().SaveByJson();
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameTitleManager>().BackToTitleScreen();
    }
}
