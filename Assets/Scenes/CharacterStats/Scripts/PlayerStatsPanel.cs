using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsPanel : MonoBehaviour
{
    public CharacterStats stats;
    [TextArea]
    public string exampleText;
    public Text statsText;

    public void UpdateStatsPanel() {
        Dictionary<string, string> statsValue = new Dictionary<string, string>() {
            {"HP", KiloFormat(stats.currentHealth).ToString() + "/" + KiloFormat(stats.maxHealth).ToString()},
            {"LV", stats.level.ToString()},
            {"EXP", stats.CurrentExp.ToString() + "/" + stats.maxExp.ToString()},
            {"AD", stats.attackDamage.ToString()},
            {"AP", stats.abilityPower.ToString()},
            {"AR", stats.attackArmor.ToString() + " (" + (stats.AD_Reduce * 100).ToString("0.00") + "%)"},
            {"MR", stats.magicArmor.ToString() + " (" + (stats.AP_Reduce * 100).ToString("0.00") + "%)"},
            {"SPEED", stats.speed.ToString("0.00")},
            {"CritChance", (stats.criticalChance * 100).ToString("0.0") + "%"},
            {"CritMult", (stats.criticalMultiply * 100).ToString("0.0") + "%"}
        };

        string temp = "", value;
        string[] textSplit = exampleText.Split('*');

        for(int i=0; i<textSplit.Length; i++) {
            if(statsValue.TryGetValue(textSplit[i], out value))
                temp += value;
            else
                temp += textSplit[i];
        }
        statsText.text = temp;
    }

    public static string KiloFormat(int num)
    {
        if (num >= 100000000)
            return (num / 1000000).ToString("#,0 M");

        if (num >= 10000000)
            return (num / 1000000).ToString("0.#") + " M";

        if (num >= 100000)
            return (num / 1000).ToString("#,0 K");

        if (num >= 10000)
            return (num / 1000).ToString("0.#") + " K";

        return num.ToString("#,0");
    }
}
