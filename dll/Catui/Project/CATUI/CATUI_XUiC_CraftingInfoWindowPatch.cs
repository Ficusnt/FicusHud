using System;
using System.Reflection;
using HarmonyLib;
using UnityEngine;

// Token: 0x02000006 RID: 6
[HarmonyPatch]
public class XUiC_CraftingInfoWindowPatch
{
	// Token: 0x0600000A RID: 10 RVA: 0x0000285C File Offset: 0x00000A5C
	[HarmonyPatch(typeof(XUiC_CraftingInfoWindow), "SetSelectedButtonByType")]
	[HarmonyPostfix]
	public static void SetSelectedButtonByType_Postfix(XUiC_CraftingInfoWindow __instance)
	{
		int num = (int)AccessTools.Field(typeof(XUiC_CraftingInfoWindow), "TabType").GetValue(__instance);
		XUiController childById = __instance.GetChildById("statButton");
		((XUiV_Button)childById.ViewComponent).Selected = (num == 3);
	}

	// Token: 0x0600000B RID: 11 RVA: 0x000028AC File Offset: 0x00000AAC
	[HarmonyPatch(typeof(XUiC_CraftingInfoWindow), "Init")]
	[HarmonyPostfix]
	public static void Init_Postfix(XUiC_CraftingInfoWindow __instance)
	{
		XUiController childById = __instance.GetChildById("statButton");
		childById.OnPress += delegate(XUiController sender, int mouseButton)
		{
			AccessTools.Field(typeof(XUiC_CraftingInfoWindow), "TabType").SetValue(__instance, 3);
			AccessTools.Method(typeof(XUiC_CraftingInfoWindow), "SetSelectedButtonByType", null, null).Invoke(__instance, new object[]
			{
				3
			});
			AccessTools.Field(typeof(XUiC_CraftingInfoWindow), "IsDirty").SetValue(__instance, true);
		};
	}

	// Token: 0x0600000C RID: 12 RVA: 0x000028EC File Offset: 0x00000AEC
	[HarmonyPrefix]
	[HarmonyPatch(typeof(XUiC_CraftingInfoWindow), "GetBindingValueInternal")]
	public static bool GetBindingValueInternalPrefix(string bindingName, ref string value, ref bool __result, XUiC_CraftingInfoWindow __instance)
	{
		bool flag = __instance == null;
		bool result;
		if (flag)
		{
			result = false;
		}
		else if (!(bindingName == "showStatTab"))
		{
			if (!(bindingName == "showstats"))
			{
				bool flag2 = bindingName.StartsWith("itemstattitle");
				if (flag2)
				{
					result = XUiC_CraftingInfoWindowPatch.GetStat(bindingName, "itemstattitle", ref value, ref __result, __instance, delegate(ItemValue entry, DisplayInfoEntry displayInfo)
					{
						bool flag6 = displayInfo.TitleOverride != null;
						string result2;
						if (flag6)
						{
							result2 = displayInfo.TitleOverride;
						}
						else
						{
							result2 = UIDisplayInfoManager.Current.GetLocalizedName(displayInfo.StatType);
						}
						return result2;
					});
				}
				else
				{
					bool flag3 = bindingName.StartsWith("itemstat");
					result = (!flag3 || XUiC_CraftingInfoWindowPatch.GetStat(bindingName, "itemstat", ref value, ref __result, __instance, (ItemValue entry, DisplayInfoEntry displayInfo) => XUiM_ItemStack.GetStatItemValueTextWithCompareInfo(entry, ItemValue.None, __instance.xui.playerUI.entityPlayer, displayInfo, false, true)));
				}
			}
			else
			{
				value = "false";
				object value2 = typeof(XUiC_CraftingInfoWindow).GetField("TabType", BindingFlags.Instance | BindingFlags.Public).GetValue(__instance);
				value = ((int)value2 == 3).ToString();
				__result = true;
				result = false;
			}
		}
		else
		{
			value = "false";
			bool flag4 = __instance.recipe != null;
			if (flag4)
			{
				int selectedCraftingTier = __instance.selectedCraftingTier;
				int itemValueType = __instance.recipe.itemValueType;
				ItemValue itemValue = new ItemValue(itemValueType, selectedCraftingTier, selectedCraftingTier, false, null, 1f);
				ItemDisplayEntry displayStatsForTag = UIDisplayInfoManager.Current.GetDisplayStatsForTag(itemValue.ItemClass.IsBlock() ? Block.list[itemValue.type].DisplayType : itemValue.ItemClass.DisplayType);
				bool flag5 = ((itemValue != null) ? itemValue.ItemClass : null) != null && displayStatsForTag != null;
				if (flag5)
				{
					value = (displayStatsForTag.DisplayStats.Count > 0).ToString();
				}
			}
			__result = true;
			result = false;
		}
		return result;
	}

	// Token: 0x0600000D RID: 13 RVA: 0x00002ADC File Offset: 0x00000CDC
	private static bool GetStat(string bindingName, string prefix, ref string value, ref bool __result, XUiC_CraftingInfoWindow instance, Func<ItemValue, DisplayInfoEntry, string> valueGenerator)
	{
		bool result;
		try
		{
			bool flag = instance == null || string.IsNullOrEmpty(bindingName) || instance.recipe == null;
			if (flag)
			{
				value = string.Empty;
				__result = true;
				result = false;
			}
			else
			{
				int num;
				bool flag2 = bindingName.Length <= prefix.Length || !int.TryParse(bindingName.Substring(prefix.Length), out num);
				if (flag2)
				{
					value = string.Empty;
					__result = true;
					result = false;
				}
				else
				{
					int selectedCraftingTier = instance.selectedCraftingTier;
					int itemValueType = instance.recipe.itemValueType;
					ItemValue itemValue = new ItemValue(itemValueType, selectedCraftingTier, selectedCraftingTier, false, null, 1f);
					bool flag3 = ((itemValue != null) ? itemValue.ItemClass : null) == null;
					if (flag3)
					{
						value = string.Empty;
						__result = true;
						result = false;
					}
					else
					{
						ItemDisplayEntry displayStatsForTag = UIDisplayInfoManager.Current.GetDisplayStatsForTag(itemValue.ItemClass.IsBlock() ? Block.list[itemValue.type].DisplayType : itemValue.ItemClass.DisplayType);
						bool flag4 = displayStatsForTag == null || displayStatsForTag.DisplayStats == null || num < 0 || num >= displayStatsForTag.DisplayStats.Count;
						if (flag4)
						{
							value = string.Empty;
							__result = true;
							result = false;
						}
						else
						{
							DisplayInfoEntry arg = displayStatsForTag.DisplayStats[num];
							value = valueGenerator(itemValue, arg);
							__result = true;
							result = false;
						}
					}
				}
			}
		}
		catch (Exception ex)
		{
			Debug.LogError(string.Concat(new string[]
			{
				"[CATUI] XUiC_CraftingInfoWindowPatch: Error GetStat '",
				bindingName,
				"': ",
				ex.Message,
				"\n",
				ex.StackTrace
			}));
			value = string.Empty;
			__result = true;
			result = false;
		}
		return result;
	}

	// Token: 0x04000004 RID: 4
	public const int TABTYPE_STAT = 3;
}
