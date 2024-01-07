using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftOption : MonoBehaviour
{
    public Recipe optionRecipe;

    public Text resultName;
    public Transform materialGrid;
    public GameObject resultSlot;
    public GameObject CraftSlotPrefeb;
    public GameObject ItemCountMenuPrefab;
    public Button CraftButton;

    public int maxAmount = 999;

    public List<Item> lackItem = new List<Item>();

    public void SetOption(Recipe setRecipe) {
        optionRecipe = setRecipe;
        resultName.text = optionRecipe.result.item.ItemName;
        resultSlot.GetComponent<CraftSlot>().SetupSlot(optionRecipe.result.item, optionRecipe.result.itemLevel, optionRecipe.result.itemQuality, optionRecipe.result.count);
        if(lackItem.Count == 0) {
            CraftButton.interactable = true;
        }

        for(int i=0; i<optionRecipe.material.Length; i++) {
            CraftSlot materialSlot = Instantiate(CraftSlotPrefeb, materialGrid).GetComponent<CraftSlot>();
            materialSlot.SetupSlot(optionRecipe.material[i].item, optionRecipe.material[i].itemLevel, optionRecipe.material[i].itemQuality, optionRecipe.material[i].count);
            if(lackItem.Contains(optionRecipe.material[i].item)) {
                materialSlot.MarkingLack();
            }
        }
    }
    public void OpenCountMenu() {
        if(maxAmount == 1) {
            CraftItem(1);
        }
        else if(maxAmount > 1) {
            ItemCountMenu countMenu = Instantiate(ItemCountMenuPrefab,Input.mousePosition, Quaternion.identity, transform.parent.parent.parent.transform).GetComponent<ItemCountMenu>();
            countMenu.maxAmount = maxAmount;
            Debug.Log(transform.parent);
            countMenu.target = transform;
        }
    }
    
    public void CraftItem(int count) {
        for(int i=0; i<optionRecipe.material.Length; i++) {
            InventoryManager.ReduceItem(InventoryManager.FindItemIDInPlayerBag(optionRecipe.material[i].item), "MyBag", optionRecipe.material[i].count * count);
        }
        InventoryManager.AddItem(optionRecipe.result.item, optionRecipe.result.itemLevel, optionRecipe.result.count * count, optionRecipe.result.itemQuality);
        CraftingManager.UpdateOptions();
    }
}
