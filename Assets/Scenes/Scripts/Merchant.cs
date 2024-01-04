using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant : interactivityObject
{
    public Inventory shopInventory;

    public override void Start() {
        base.Start();
    }

    public override void Interact() {
        ShopManager.UpdateShop(shopInventory);
        ShopManager.SetShopPanel(true);
    }
}
