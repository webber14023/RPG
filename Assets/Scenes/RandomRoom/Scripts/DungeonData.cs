using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DungeonData", menuName = "Dungeon/DungeonData")]
public class DungeonData : ScriptableObject
{
    public string dungeonName;
    public int floor;
    public int maxFloor;
    public int historyFloor;
    public int enemyCount;
    public GameObject[] dungeonEnemy;
}
