using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DungeonPortal : interactivityObject
{
    public DungeonData dungeonData;

    public override void Start() {
        base.Start();
    }

    public override void Interact() {
        dungeonData.floor = 1;
        GameObject.FindGameObjectWithTag("Player").transform.GetComponent<CharacterStats>().ResetStats();
        PlayerPrefs.SetString("dungeonName", dungeonData.dungeonName);
        SceneManager.LoadScene("Dungeon");
    }
}
