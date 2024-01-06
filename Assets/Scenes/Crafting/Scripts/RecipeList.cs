using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New RecipeList", menuName = "Crafting/New RecipeList")]
public class RecipeList : ScriptableObject
{
    public List<Recipe> Recipes;
}
