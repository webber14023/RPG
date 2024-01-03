using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public static CameraMove instance;
    public float speed;
    public float shakeAmount = 0.7f;
    public float shakeTime;
    public GameObject blackEffect;
    public Transform target;

    void Start() {
        BlackScreen();
    }

    void Update()
    {
        if(target != null)
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), speed*Time.deltaTime);
        if(shakeTime > 0) {
            Vector2 randomPos = Random.insideUnitSphere * shakeAmount;
            transform.position += (Vector3)randomPos;
            shakeTime -= Time.deltaTime;
        }
    }

    public void ChangeTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void ShakeScreen() {
        shakeTime = 0.1f;
    }
    public void BlackScreen() {
        blackEffect.transform.GetComponent<Fade>().FadeEffect();
    }

}
