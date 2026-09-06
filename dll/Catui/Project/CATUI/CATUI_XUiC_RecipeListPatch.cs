using System;
using HarmonyLib;

// Token: 0x02000010 RID: 16
[HarmonyPatch]
public class XUiC_RecipeListPatch
{
	// Token: 0x0600002D RID: 45 RVA: 0x00005338 File Offset: 0x00003538
	[HarmonyPostfix]
	[HarmonyPatch(typeof(XUiC_RecipeList), "RefreshCurrentRecipes")]
	public static void RefreshCurrentRecipesPostfix(XUiC_RecipeList __instance)
	{
		bool flag = __instance != null;
		if (flag)
		{
			__instance.resortRecipes = true;
		}
	}

	// Token: 0x0600002E RID: 46 RVA: 0x00005358 File Offset: 0x00003558
	[HarmonyPrefix]
	[HarmonyPatch(typeof(XUiC_RecipeList), "CompareRecipeInfos")]
	public static bool CompareRecipeInfosPrefix(XUiC_RecipeList __instance, XUiC_RecipeList.RecipeInfo lhs, XUiC_RecipeList.RecipeInfo rhs, ref int __result)
	{
		bool flag = __instance == null || lhs.Equals(default(XUiC_RecipeList.RecipeInfo)) || rhs.Equals(default(XUiC_RecipeList.RecipeInfo)) || lhs.recipe == null || rhs.recipe == null;
		bool result;
		if (flag)
		{
			result = true;
		}
		else
		{
			bool flag2 = lhs.recipe.IsTracked != rhs.recipe.IsTracked;
			if (flag2)
			{
				__result = ((!lhs.recipe.IsTracked) ? 1 : -1);
				result = false;
			}
			else
			{
				bool flag3 = lhs.recipe.isChallenge != rhs.recipe.isChallenge;
				if (flag3)
				{
					__result = ((!lhs.recipe.isChallenge) ? 1 : -1);
					result = false;
				}
				else
				{
					bool flag4 = lhs.recipe.isQuest != rhs.recipe.isQuest;
					if (flag4)
					{
						__result = ((!lhs.recipe.isQuest) ? 1 : -1);
						result = false;
					}
					else
					{
						bool recipeIsFavorite = XUiM_Recipes.GetRecipeIsFavorite(__instance.xui, lhs.recipe);
						bool recipeIsFavorite2 = XUiM_Recipes.GetRecipeIsFavorite(__instance.xui, rhs.recipe);
						bool flag5 = recipeIsFavorite != recipeIsFavorite2;
						if (flag5)
						{
							__result = (recipeIsFavorite ? -1 : 1);
							result = false;
						}
						else
						{
							result = true;
						}
					}
				}
			}
		}
		return result;
	}
}
