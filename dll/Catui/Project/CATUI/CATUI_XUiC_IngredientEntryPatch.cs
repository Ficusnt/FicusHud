using System;
using System.Collections.Generic;
using HarmonyLib;

// Token: 0x0200000A RID: 10
[HarmonyPatch]
public class XUiC_IngredientEntryPatch
{
	// Token: 0x0600001C RID: 28 RVA: 0x00004AF4 File Offset: 0x00002CF4
	[HarmonyPrefix]
	[HarmonyPatch(typeof(XUiC_IngredientEntry), "GetBindingValueInternal")]
	public static bool GetBindingValueInternalPrefix(string bindingName, ref string value, ref bool __result, XUiC_IngredientEntry __instance)
	{
		bool flag = __instance.ingredient != null;
		bool result;
		if (!(bindingName == "CATUI_HasComplete"))
		{
			if (!(bindingName == "CATUI_InventoryHasRecipe"))
			{
				result = true;
			}
			else
			{
				value = "false";
				bool flag2 = flag;
				if (flag2)
				{
					List<Recipe> list = XUiM_Recipes.FilterRecipesByID(__instance.ingredient.itemValue.ItemClass.Id, XUiM_Recipes.GetRecipes());
					bool flag3 = list != null && list.Count > 0;
					if (flag3)
					{
						value = "true";
					}
				}
				__result = true;
				result = false;
			}
		}
		else
		{
			value = "false";
			XUiC_WorkstationMaterialInputGrid childByType = __instance.windowGroup.Controller.GetChildByType<XUiC_WorkstationMaterialInputGrid>();
			bool flag4 = !flag;
			if (flag4)
			{
				__result = true;
				result = false;
			}
			else
			{
				bool flag5 = childByType != null;
				int num;
				if (flag5)
				{
					bool materialBased = __instance.materialBased;
					if (materialBased)
					{
						num = childByType.GetWeight(__instance.material);
					}
					else
					{
						num = __instance.xui.PlayerInventory.GetItemCount(__instance.ingredient.itemValue);
					}
				}
				else
				{
					XUiC_WorkstationInputGrid childByType2 = __instance.windowGroup.Controller.GetChildByType<XUiC_WorkstationInputGrid>();
					bool flag6 = childByType2 != null;
					if (flag6)
					{
						num = childByType2.GetItemCount(__instance.ingredient.itemValue);
					}
					else
					{
						num = __instance.xui.PlayerInventory.GetItemCount(__instance.ingredient.itemValue);
					}
				}
				int num2 = __instance.ingredient.count * __instance.craftCountControl.Count;
				value = (num >= num2).ToString();
				__result = true;
				result = false;
			}
		}
		return result;
	}

	// Token: 0x0600001D RID: 29 RVA: 0x00004C94 File Offset: 0x00002E94
	[HarmonyPostfix]
	[HarmonyPatch(typeof(XUiC_IngredientEntry), "Init")]
	public static void InitPostfixProxy(XUiC_IngredientEntry __instance)
	{
		bool flag = XUiC_IngredientEntryPatch._patchedInstances.Contains(__instance);
		if (!flag)
		{
			XUiC_IngredientEntryPatch._patchedInstances.Add(__instance);
			XUiController childById = __instance.GetChildById("btnInventoryRecipe");
			bool flag2 = childById == null;
			if (!flag2)
			{
				childById.OnPress += delegate(XUiController _sender, int _mouseButton)
				{
					__instance.xui.playerUI.windowManager.CloseIfOpen("looting");
					XUiC_RecipeList xuiC_RecipeList = __instance.xui.GetChildrenByType<XUiC_RecipeList>().Find((XUiC_RecipeList recipeList) => recipeList.WindowGroup != null && recipeList.WindowGroup.isShowing);
					bool flag3 = xuiC_RecipeList == null;
					if (flag3)
					{
						XUiC_WindowSelector.OpenSelectorAndWindow(__instance.xui.playerUI.entityPlayer, "crafting");
						xuiC_RecipeList = __instance.xui.GetChildByType<XUiC_RecipeList>();
					}
					xuiC_RecipeList.SetRecipeDataByItem(__instance.ingredient.itemValue.ItemClass.Id);
					__instance.isDirty = true;
				};
			}
		}
	}

	// Token: 0x0400000E RID: 14
	private static readonly HashSet<XUiC_IngredientEntry> _patchedInstances = new HashSet<XUiC_IngredientEntry>();
}
