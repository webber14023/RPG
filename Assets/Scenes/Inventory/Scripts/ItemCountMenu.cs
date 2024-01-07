using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class ItemCountMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Transform target;
    public InputField SplitField;
    public Slider SplitSlider;
    public Text HintText;
    public int maxAmount;
        
    bool isInside;
    int amount = 1;

    string mode;

    private void Start() {
        if(ShopManager.IsShopOpen()) {
            mode = "Shop";
        }
        else if(CraftingManager.IsPenalOpen()) {
            mode = "Craft";
        }
    }
    
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Mouse0) && !isInside)
            Destroy(gameObject, 0.1f);
    }

    public void OnPointerEnter(PointerEventData pointEventData) {
        isInside = true;
    }

    public void OnPointerExit(PointerEventData pointEventData) {
        isInside = false;
    }
        
    public void ChangeAmount(Text Input) {
        amount = Int32.Parse(Input.text);
        if(amount > maxAmount)
            amount = maxAmount;
        if(amount <= 0)
            amount = 1;
        
        SplitField.text = amount.ToString();
        SplitSlider.value = (float)amount / maxAmount;
    }

    public void ChangeAmount(Slider Input) {
        amount = (int)(maxAmount * Input.value);
        if(amount > maxAmount)
            amount = maxAmount;
        if(amount <= 0)
            amount = 1;

        SplitField.text = amount.ToString();
    }

    public void ConfirmButton() {
        if(mode == "Shop") {
            target.GetComponent<ShopSlot>().BuyItem(amount);
        }
        else if(mode == "Craft") {
            target.GetComponent<CraftOption>().CraftItem(amount);
        }
        Destroy(gameObject);
    }
}
