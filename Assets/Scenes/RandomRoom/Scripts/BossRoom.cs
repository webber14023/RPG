using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour
{
    public GameObject door;
    [SerializeField] GameObject Portal;
    [SerializeField] GameObject ExitPortal;
    public Transform spawnPoint;
    public List<GameObject> Enemys = new List<GameObject>();
    public RoomGenerator genrator;
    
    bool EnterRoom;

        
    void Start()
    {
        EnterRoom = false;
        genrator = transform.parent.GetComponent<RoomGenerator>();
        //genrator.Data.maxFloor = genrator.Data.floor;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)) {
            GameObject enemy = genrator.Data.dungeonBoss[Random.Range(0, genrator.Data.dungeonBoss.Length)];
            Enemys.Add(Instantiate(enemy, spawnPoint.position, Quaternion.identity, transform));
            int level = genrator.Data.minLevel + (int)((float)(genrator.Data.maxLevel - genrator.Data.minLevel) / genrator.Data.maxFloor * genrator.Data.floor);
            Enemys[0].GetComponent<CharacterStats>().enemyLevel = level + Random.Range(0, 2);
        }
        if(EnterRoom == true) {
            for(int i=0; i<Enemys.Count; i++) {
                if(Enemys[i] == null) {
                    Enemys.RemoveAt(i);
                    i--;
                }
            }
            if(Enemys.Count == 0) {
                if(genrator.Data.maxFloor != genrator.Data.floor)
                    Portal.SetActive(true);
                
                ExitPortal.SetActive(true);
                transform.Find("RoomArea").gameObject.SetActive(false);
                //EnterRoom = false;

            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(!EnterRoom && other.CompareTag("Player"))
        {
            EnterRoom = true;
            door.SetActive(true);
            transform.Find("RoomArea").gameObject.SetActive(false);

            GameObject enemy = genrator.Data.dungeonBoss[Random.Range(0, genrator.Data.dungeonBoss.Length)];
            Enemys.Add(Instantiate(enemy, spawnPoint.position, Quaternion.identity, transform));
            int level = genrator.Data.minLevel + (int)((float)(genrator.Data.maxLevel - genrator.Data.minLevel) / genrator.Data.maxFloor * genrator.Data.floor);
            Enemys[0].GetComponent<CharacterStats>().enemyLevel = level + Random.Range(0, 2);
            
            //CameraMove.instance.ChangeTarget(transform);
        }

        
    }
}
