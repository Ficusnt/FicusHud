using System;
using HarmonyLib;

// Token: 0x02000011 RID: 17
[HarmonyPatch]
public class XUiC_RecipeTrackerIngredientEntryPatch
{
	// Token: 0x06000030 RID: 48 RVA: 0x000054C8 File Offset: 0x000036C8
	[HarmonyPrefix]
	[HarmonyPatch(typeof(XUiC_RecipeTrackerIngredientEntry), "GetBindingValueInternal")]
	public static bool GetBindingValueInternalPrefix(string bindingName, ref string value, ref bool __result, XUiC_RecipeTrackerIngredientEntry __instance)
	{
		ItemStack ingredient = __instance.ingredient;
		bool flag = ingredient != null;
		int currentCount = __instance.currentCount;
		XUiC_RecipeTrackerIngredientsList owner = __instance.Owner;
		bool result;
		if (!(bindingName == "CATUI_IngredientCompleteColor"))
		{
			result = true;
		}
		else
		{
			value = "255,255,255";
			bool flag2 = flag;
			if (flag2)
			{
				value = ((currentCount >= ingredient.count * owner.Count) ? owner.completeColor : owner.incompleteColor);
			}
			__result = true;
			result = false;
		}
		return result;
	}
}
