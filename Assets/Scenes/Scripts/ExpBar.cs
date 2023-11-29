using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    public Image ExpImage;

    public void UpdateExpBar(float Exp) {
        ExpImage.fillAmount = Exp;
    }
}
