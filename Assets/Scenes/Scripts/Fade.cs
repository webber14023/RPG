using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public Image EffectImage;
    public float maxAlpha;
    public float EffectSpeed;

    void Update()
    {
        if(EffectImage.color.a > 0f) {
            EffectImage.color -= new Color (0f, 0f, 0f, EffectSpeed);
        }
    }

    public void FadeEffect() {
        EffectImage.color = new Color(1f, 1f, 1f, maxAlpha);
    }
}
