using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private enum Direction{ up, down, left, right};
    private Direction direction;

    public GameObject doorLeft, doorRight, doorUp, doorDown;

    public bool roomLeft, roomRight, roomUp, roomDown, EnterRoom;
    public List<GameObject> Enemys = new List<GameObject>();
    int enemyCount;

    RoomGenerator genrator;
    
    void Start()
    {
        EnterRoom = false;
    }


    void Update()
    {
        if(EnterRoom == true) {
            for(int i=0; i<Enemys.Count; i++) {
                if(Enemys[i] == null) {
                    Enemys.RemoveAt(i);
                    i--;
                }
            }
            if(Enemys.Count == 0) {
                doorLeft.SetActive(false);
                doorRight.SetActive(false);
                doorUp.SetActive(false);
                doorDown.SetActive(false);
                EnterRoom = false;
            }
        }
    }

    public void SetDoor(int longwayRoomDirection, bool Flip)
    {
        if (Flip)
        {
            direction = (Direction)longwayRoomDirection;
            switch(direction)
            {
                case Direction.up:
                    roomDown = true;
                    break;
                case Direction.down:
                    roomUp = true;
                    break;
                case Direction.left:
                    roomRight = true;
                    break;
                case Direction.right:
                    roomLeft = true;
                    break;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            EnterRoom = true;
            doorLeft.SetActive(roomLeft);
            doorRight.SetActive(roomRight);
            doorUp.SetActive(roomUp);
            doorDown.SetActive(roomDown);
            spawnEnemy();
            transform.Find("RoomArea").gameObject.SetActive(false);
            
            //CameraMove.instance.ChangeTarget(transform);
        }
    }

    private void spawnEnemy() { 
        genrator = transform.parent.GetComponent<RoomGenerator>();
        enemyCount = genrator.Data.enemyCount;
        Transform spawnPoints = transform.GetChild(6).Find("EnemySpawners");
        for(int i=0; i<enemyCount; i++){
            GameObject enemy = genrator.Data.dungeonEnemy[Random.Range(0, genrator.Data.dungeonEnemy.Length)];

            Enemys.Add(Instantiate(enemy, spawnPoints.GetChild(Random.Range(0,spawnPoints.childCount)).position + (Vector3)Random.insideUnitCircle * 2, Quaternion.identity, transform));
            int level = genrator.Data.minLevel + (int)((float)(genrator.Data.maxLevel - genrator.Data.minLevel) / genrator.Data.maxFloor ) * genrator.Data.floor;
            Enemys[i].GetComponent<CharacterStats>().enemyLevel = level + level % 10 + Random.Range(-level % 10, 0);
        }
    }
}
