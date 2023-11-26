using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class NextFloor : interactivityObject
{
    RoomGenerator genrator;

    public override void Start() {
        base.Start();
        genrator = transform.parent.GetComponent<RoomGenerator>();
    }

    public override void Interact() {
        genrator.Data.floor += 1;
        CharacterStats playerStats = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<CharacterStats>();
        playerStats.baseCurrentHealth = playerStats.currentHealth;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
