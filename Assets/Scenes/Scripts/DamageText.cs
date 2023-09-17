using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    public Text damageText; 

    public void SetDamageText(int damage)
    {
        damageText.text = damage.ToString();
        StartCoroutine("clearText");
    }

    IEnumerator clearText() {
        yield return new WaitForSeconds(0.5f);
        destoryDamageFont();
    }
    
    void destoryDamageFont(){
        Destroy(this.gameObject);
    }
}