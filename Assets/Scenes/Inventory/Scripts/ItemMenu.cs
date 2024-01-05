using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Slot CurrentSlot;

    public Button useButton;
    public Button DropButton;
    public Button SplitButton;
    public Button SellButton;

    [Header("splitMenu")]
    public GameObject SplitMenuGameObject;
    public Text SplitMenuTitle;
    public InputField SplitField;
    public Slider SplitSlider;

    InventoryManager inventoryManager;

    bool isInside;
    int splitAmount = 1;
    int SplitmaxAmount;

    private void Start() {
        inventoryManager = GameObject.FindGameObjectWithTag("InventoryManager").transform.GetComponent<InventoryManager>();
        useButton.interactable = CurrentSlot.slotItem.type != "";
        DropButton.interactable = true;
        SplitButton.interactable = CurrentSlot.slotItem.isStackable;
        SellButton.interactable = ShopManager.IsShopOpen();
        SplitmaxAmount = Int32.Parse(CurrentSlot.slotNum.text) - 1;
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

    public void Use() {
        List<string> equipTypes = new List<string>() {"Head", "Body", "Feet", "Ring", "Weapon", "OffHand", "Accessory", "Neck"};
        if(equipTypes.Contains(CurrentSlot.slotItem.type)) {
            inventoryManager.QuickEquip(CurrentSlot.slotID, CurrentSlot.transform.parent.name, CurrentSlot.slotItem.type);
        }
        else if(CurrentSlot.slotItem.type == "Functional") {
            CurrentSlot.slotItem.ItemFunction(CurrentSlot.transform.parent.name, CurrentSlot.slotID);
        }
        Destroy(gameObject);
    }

    public void Drop() {
        inventoryManager.DropItem(CurrentSlot.slotID, CurrentSlot.transform.parent.name);
        Destroy(gameObject);
    }

    public void SplitMenu() {
        SplitMenuGameObject.SetActive(true);
        ChangeSplitAmount(1);
    }

    public void ChangeSplitAmount(int Input) {
        splitAmount = Input;
        if(splitAmount > SplitmaxAmount)
            splitAmount = SplitmaxAmount;
        if(splitAmount <= 0)
            splitAmount = 1;
        
        SplitField.text = splitAmount.ToString();
        SplitSlider.value = (float)splitAmount / SplitmaxAmount;
    }
    
    public void ChangeSplitAmount(Text Input) {
        splitAmount = Int32.Parse(Input.text);
        if(splitAmount > SplitmaxAmount)
            splitAmount = SplitmaxAmount;
        if(splitAmount <= 0)
            splitAmount = 1;
        
        SplitField.text = splitAmount.ToString();
        SplitSlider.value = (float)splitAmount / SplitmaxAmount;
    }

    public void ChangeSplitAmount(Slider Input) {
        splitAmount = (int)(SplitmaxAmount * Input.value);
        if(splitAmount > SplitmaxAmount)
            splitAmount = SplitmaxAmount;
        if(splitAmount <= 0)
            splitAmount = 1;
        
        SplitField.text = splitAmount.ToString();
    }
    
    public void Split() {
        if(SplitMenuTitle.text == "拆分數量")
            inventoryManager.SplitItem(CurrentSlot.slotID, CurrentSlot.transform.parent.name, splitAmount);
        else
            inventoryManager.SellItem(CurrentSlot.slotID, CurrentSlot.transform.parent.name, splitAmount);
        Destroy(gameObject);
    }

    public void Sell() {
        if(!CurrentSlot.slotItem.isStackable){
            inventoryManager.SellItem(CurrentSlot.slotID, CurrentSlot.transform.parent.name, 1);
            Destroy(gameObject);
        }
        else {
            SplitMenuGameObject.SetActive(true);
            SplitmaxAmount += 1;
            SplitMenuTitle.text = "銷售數量";
        }
    }
}