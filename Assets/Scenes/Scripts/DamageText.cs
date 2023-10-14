using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    public Text damageText;
    public float speed;

    private void Start() {
        Destroy(this.gameObject,0.5f);
    }
    private void Update() {
        transform.position += new Vector3(0, speed * Time.deltaTime, 0);
    }

    public void SetDamageText(int damage)
    {
        damageText.text = damage.ToString();
    }

}