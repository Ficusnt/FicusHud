using System;
using HarmonyLib;

// Token: 0x02000012 RID: 18
[HarmonyPatch]
public class XUiC_RecipeTrackerWindowPatch
{
	// Token: 0x06000032 RID: 50 RVA: 0x00005550 File Offset: 0x00003750
	[HarmonyPrefix]
	[HarmonyPatch(typeof(XUiC_RecipeTrackerWindow), "GetBindingValueInternal")]
	public static bool GetBindingValueInternalPrefix(string bindingName, ref string value, ref bool __result, XUiC_RecipeTrackerWindow __instance)
	{
		XUiC_RecipeTrackerIngredientsList ingredientList = __instance.ingredientList;
		Recipe currentRecipe = __instance.currentRecipe;
		bool result;
		if (!(bindingName == "CATUI_ListCount"))
		{
			result = true;
		}
		else
		{
			value = "0";
			bool flag = currentRecipe != null;
			if (flag)
			{
				value = ingredientList.GetActiveIngredientCount().ToString();
			}
			__result = true;
			result = false;
		}
		return result;
	}
}
