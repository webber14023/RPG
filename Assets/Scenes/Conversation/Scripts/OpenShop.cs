using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="OpenShop",menuName = "NPC/Function/OpenShop")]
public class OpenShop : NpcFunction
{
    public Inventory ShopData;

    public override void ActivateFunction() {
        ShopManager.SetShopPanel(true);
        ShopManager.UpdateShop(ShopData);
    }
}
