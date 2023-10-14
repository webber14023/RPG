using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image hpImage;
    public Image hpEffectImage;
    public float EffectSpeed;

    private void Update() {
        if(hpEffectImage.fillAmount > hpImage.fillAmount) {
            hpEffectImage.fillAmount -= EffectSpeed*Time.deltaTime;
        }
    }
    public void UpdateHealthBar(float hp) {
        hpImage.fillAmount = hp;
    }

}
