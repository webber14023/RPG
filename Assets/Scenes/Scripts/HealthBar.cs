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

        if(hpImage.fillAmount <= 0) {
            hpImage.enabled = false;
            hpEffectImage.enabled = false;
            if (BackGroundImage != null)
                BackGroundImage.enabled = false;
        }

        else if(hpImage.fillAmount <= hpEffectImage.fillAmount) {
            hpImage.color = new Color(1f, 1f, 1f, 1f);
            hpEffectImage.color = Color.white;
            hpEffectImage.fillAmount -= EffectSpeed*Time.deltaTime;
            if(hpEffectImage.fillAmount < hpImage.fillAmount)
                hpEffectImage.fillAmount = hpImage.fillAmount;
        }
        else if(hpImage.fillAmount == hpEffectImage.fillAmount) {
            hpImage.color = new Color(1f, 1f, 1f, 1f);
            hpEffectImage.color = Color.white;
        }
    
        else if(hpImage.fillAmount > hpEffectImage.fillAmount) {
            hpImage.color = new Color(1f, 1f, 1f, 0.3f);
            hpEffectImage.color = Color.green;

            hpEffectImage.fillAmount += EffectSpeed*Time.deltaTime;
            if(hpEffectImage.fillAmount > hpImage.fillAmount)
                hpEffectImage.fillAmount = hpImage.fillAmount;
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
