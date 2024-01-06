using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : MonoBehaviour
{
    static CraftingManager intance;

    public RecipeList recipeList;
    public GameObject PlayerBag;
    public GameObject CraftOptionsPrefeb;

    public GameObject CraftingPanel;
    public Transform CraftOptionsGrid;

    public AudioClip audioClip;
    public AudioSource audioSource;

    public bool HideOption;

    bool canCraft;

    void Awake() {
        if(intance != null)
            Destroy(this);
        intance = this;
    }
    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public static void UpdateOptions() {
        intance.audioSource.PlayOneShot(intance.audioClip);
        for(int i=0; i<intance.CraftOptionsGrid.childCount; i++) {
            Destroy(intance.CraftOptionsGrid.GetChild(i).gameObject);
        }

        for(int i=0; i<intance.recipeList.Recipes.Count; i++) {
            if(intance.HideOption) {
                intance.canCraft = true;
                for(int j=0; j<intance.recipeList.Recipes[i].material.Length; j++) {
                    if(!InventoryManager.FindItemInPlayerBag(intance.recipeList.Recipes[i].material[j].item, intance.recipeList.Recipes[i].material[j].count)) {
                        intance.canCraft = false;
                        break;
                    }
                }
                if(!intance.canCraft) {
                    continue;
                }
                CraftOption option = Instantiate(intance.CraftOptionsPrefeb, intance.CraftOptionsGrid).GetComponent<CraftOption>();
                option.SetOption(intance.recipeList.Recipes[i]);
            }
            else {
                CraftOption option = Instantiate(intance.CraftOptionsPrefeb, intance.CraftOptionsGrid).GetComponent<CraftOption>();
                for(int j=0; j<intance.recipeList.Recipes[i].material.Length; j++) {
                    if(!InventoryManager.FindItemInPlayerBag(intance.recipeList.Recipes[i].material[j].item, intance.recipeList.Recipes[i].material[j].count))
                        option.lackItem.Add(intance.recipeList.Recipes[i].material[j].item);
                }
                option.SetOption(intance.recipeList.Recipes[i]);
            }
        }
    }

    public static void OpenCraftingPanel(RecipeList newRecipeList) {
        intance.recipeList = newRecipeList;
        SetPanel(true);
        UpdateOptions();
    }

    public static void SetPanel(bool state) {
        intance.PlayerBag.SetActive(state);
        intance.CraftingPanel.SetActive(state);
    }

    public void ChangeShowMode(Toggle toggle) {
        HideOption = toggle.isOn;
        UpdateOptions();
    }

}
