using System;
using System.Linq;
using Audio;
using HarmonyLib;

// Token: 0x02000007 RID: 7
[HarmonyPatch]
public class XUiC_CraftingQueuePatch
{
	// Token: 0x0600000F RID: 15 RVA: 0x00002CC0 File Offset: 0x00000EC0
	[HarmonyPrefix]
	[HarmonyPatch(typeof(XUiC_CraftingQueue), "AddRecipeToCraft")]
	public static bool AddRecipeToCraftPrefix(XUiC_CraftingQueue __instance, ref Recipe _recipe, ref int _count, ref float craftTime, ref bool isCrafting, ref float _oneItemCraftingTime, ref bool __result)
	{
		bool flag = _recipe == null || _count <= 0;
		bool result;
		if (flag)
		{
			__result = false;
			result = false;
		}
		else
		{
			bool shiftKeyPressed = InputUtils.ShiftKeyPressed;
			if (shiftKeyPressed)
			{
				XUiC_CraftingQueuePatch.AddToStartOfQueue(__instance, ref _recipe, ref _count, ref craftTime, ref isCrafting, ref _oneItemCraftingTime, ref __result);
				result = false;
			}
			else
			{
				int num = __instance.queueItems.Length;
				for (int i = num - 1; i >= 0; i--)
				{
					bool flag2 = __instance.AddRecipeToCraftAtIndex(i, _recipe, _count, craftTime, isCrafting, false, -1, -1, _oneItemCraftingTime);
					if (flag2)
					{
						__result = true;
						return false;
					}
				}
				__result = false;
				result = false;
			}
		}
		return result;
	}

	// Token: 0x06000010 RID: 16 RVA: 0x00002D60 File Offset: 0x00000F60
	private static void AddToStartOfQueue(XUiC_CraftingQueue inst, ref Recipe _recipe, ref int _count, ref float craftTime, ref bool isCrafting, ref float _oneItemCraftingTime, ref bool __result)
	{
		__result = false;
		bool flag = _recipe == null || _count <= 0;
		if (flag)
		{
			Manager.PlayInsidePlayerHead("ui_denied", -1, 0f, false, false);
		}
		else
		{
			XUiController[] queueItems = inst.queueItems;
			bool flag2 = queueItems.Cast<XUiC_RecipeStack>().All((XUiC_RecipeStack a) => a.HasRecipe());
			if (flag2)
			{
				Manager.PlayInsidePlayerHead("ui_denied", -1, 0f, false, false);
			}
			else
			{
				XUiC_CraftingQueuePatch.ShiftQueueItems(queueItems);
				XUiC_RecipeStack xuiC_RecipeStack = (XUiC_RecipeStack)queueItems[queueItems.Length - 1];
				xuiC_RecipeStack.SetRecipe(_recipe, _count, craftTime, false, -1, -1, _oneItemCraftingTime);
				xuiC_RecipeStack.IsDirty = true;
				__result = true;
			}
		}
	}

	// Token: 0x06000011 RID: 17 RVA: 0x00002E18 File Offset: 0x00001018
	[HarmonyPrefix]
	[HarmonyPatch(typeof(XUiC_CraftingQueue), "AddItemToRepair")]
	public static bool AddItemToRepairPrefix(XUiC_CraftingQueue __instance, ref float _repairTimeLeft, ref ItemValue _itemToRepair, ref int _amountToRepair, ref bool __result, ref XUiController[] ___queueItems)
	{
		bool flag = !InputUtils.ShiftKeyPressed;
		bool result;
		if (flag)
		{
			result = true;
		}
		else
		{
			bool flag2 = _itemToRepair == null || _amountToRepair <= 0;
			if (flag2)
			{
				Manager.PlayInsidePlayerHead("ui_denied", -1, 0f, false, false);
				__result = false;
				result = false;
			}
			else
			{
				bool flag3 = ___queueItems.Cast<XUiC_RecipeStack>().All((XUiC_RecipeStack a) => a.HasRecipe());
				if (flag3)
				{
					Manager.PlayInsidePlayerHead("ui_denied", -1, 0f, false, false);
					__result = false;
					result = false;
				}
				else
				{
					XUiC_CraftingQueuePatch.ShiftQueueItems(___queueItems);
					XUiC_RecipeStack xuiC_RecipeStack = (XUiC_RecipeStack)___queueItems[___queueItems.Length - 1];
					bool flag4 = xuiC_RecipeStack.SetRepairRecipe(_repairTimeLeft, _itemToRepair, _amountToRepair);
					if (flag4)
					{
						xuiC_RecipeStack.IsCrafting = true;
						xuiC_RecipeStack.IsDirty = true;
						__result = true;
					}
					else
					{
						__result = false;
					}
					result = false;
				}
			}
		}
		return result;
	}

	// Token: 0x06000012 RID: 18 RVA: 0x00002F08 File Offset: 0x00001108
	private static void ShiftQueueItems(XUiController[] queueItems)
	{
		bool flag = queueItems == null || queueItems.Length <= 1;
		if (!flag)
		{
			int num = queueItems.Length;
			for (int i = 1; i < num; i++)
			{
				XUiC_RecipeStack xuiC_RecipeStack = (XUiC_RecipeStack)queueItems[i];
				bool flag2 = xuiC_RecipeStack.HasRecipe();
				if (flag2)
				{
					xuiC_RecipeStack.IsCrafting = false;
					xuiC_RecipeStack.CopyTo((XUiC_RecipeStack)queueItems[i - 1]);
					queueItems[i - 1].IsDirty = true;
				}
			}
			XUiC_RecipeStack xuiC_RecipeStack2 = (XUiC_RecipeStack)queueItems[num - 1];
			xuiC_RecipeStack2.IsCrafting = false;
			xuiC_RecipeStack2.SetRecipe(null, 0, 0f, true, -1, -1, -1f);
		}
	}
}
