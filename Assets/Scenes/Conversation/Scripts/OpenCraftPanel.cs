using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="OpenCraftPanel",menuName = "NPC/Function/OpenCraftPanel")]
public class OpenCraftPanel : NpcFunction
{
    public RecipeList Data;

    public override void ActivateFunction() {
        CraftingManager.OpenCraftingPanel(Data);
    }
}

