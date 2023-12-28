using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    public Text damageText;
    public float speed;
    public Color AD_DamageColor;
    public Color AP_DamageColor;

    private void Start() {
        Destroy(this.gameObject,0.5f);
    }
    private void Update() {
        transform.position += new Vector3(0, speed * Time.deltaTime, 0);
    }

    public void SetDamageText(int damage, bool isAttackDamage, bool isCritical)
    {
        if(isAttackDamage) {
            damageText.color = AD_DamageColor;
        }
        else {
            damageText.color = AP_DamageColor;
        }
        if(!isCritical) {
            damageText.text = KiloFormat(damage);
        }
        else {
            damageText.text = KiloFormat(damage) + "!";
            transform.localScale *= 1.25f;
            damageText.color += new Color(0.1f, 0.1f, 0.1f);
        }
    }

    public static string KiloFormat(int num)
    {
        if (num >= 10000000)
            return (num / 1000000f).ToString("0.00") + "M";

        if (num >= 10000)
            return (num / 1000f).ToString("0.00") + "K";

        return num.ToString("#,0");
    } 

}