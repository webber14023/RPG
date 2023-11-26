using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image hpImage;
    public Image hpEffectImage;
    public Image BackGroundImage;
    public float EffectSpeed;

    private void Update() {
        if(hpImage.fillAmount > 0) {
            if(hpEffectImage.fillAmount >= hpImage.fillAmount) {
                hpEffectImage.fillAmount -= EffectSpeed*Time.deltaTime;
                if(hpEffectImage.fillAmount < hpImage.fillAmount)
                    hpEffectImage.fillAmount = hpImage.fillAmount;
            }
        }
        else {
            hpImage.enabled = false;
            hpEffectImage.enabled = false;
            if (BackGroundImage != null)
                BackGroundImage.enabled = false;
        }
    }
    public void UpdateHealthBar(float hp) {
        hpImage.fillAmount = hp;
    }
    public void SetHealthBar(float hp) {
        hpImage.fillAmount = hp;
        hpEffectImage.fillAmount = hp;
    }
    

}
