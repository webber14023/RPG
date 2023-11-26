using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hurt : MonoBehaviour
{
    public Image hpEffectImage;
    public float EffectSpeed;

    void Update()
    {
        if(hpEffectImage.color.a > 0f) {
            hpEffectImage.color -= new Color (0f, 0f, 0f, EffectSpeed);
        }
    }

    public void HurtEffect() {
        hpEffectImage.color = new Color(1f, 1f, 1f, 0.5f);
    }
}
