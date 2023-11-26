using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour
{
    public GameObject door;
    public GameObject Portal;
    public Transform spawnPoint;
    public List<GameObject> Enemys = new List<GameObject>();
    RoomGenerator genrator;
    
    bool EnterRoom;

        
    void Start()
    {
        EnterRoom = false;
        genrator = transform.parent.GetComponent<RoomGenerator>();
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
                Instantiate(Portal, spawnPoint.position, Quaternion.identity, transform.parent);
                transform.Find("RoomArea").gameObject.SetActive(false);
                EnterRoom = false;

            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            EnterRoom = true;
            door.SetActive(true);
            Debug.Log(genrator.Data.dungeonBoss);
            GameObject enemy = genrator.Data.dungeonBoss[Random.Range(0, genrator.Data.dungeonBoss.Length)];
            Enemys.Add(Instantiate(enemy, spawnPoint.position, Quaternion.identity, transform));
            transform.Find("RoomArea").gameObject.SetActive(false);
            
            //CameraMove.instance.ChangeTarget(transform);
        }

        
    }
}
