using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefeb;
    public int enemyAmount;
    public int minLevel;
    public int maxLevel;
    public float spownRange;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            Destroy(gameObject);
        }
    }
    private void OnDestroy() {
        Debug.Log("spown");
        for(int i=0; i<enemyAmount; i++) {
            int r = Random.Range(0,enemyPrefeb.Length);
            Vector2 position = new Vector2(Random.Range(0f,spownRange),Random.Range(0f,spownRange));
            GameObject enemy = Instantiate(enemyPrefeb[r], (Vector2)transform.position + position, Quaternion.identity);
        }
    }
}
