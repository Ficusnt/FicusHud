using System;
using HarmonyLib;

// Token: 0x0200000B RID: 11
[HarmonyPatch]
public class XUiC_ItemInfoWindowPatch
{
	// Token: 0x06000020 RID: 32 RVA: 0x00004D1C File Offset: 0x00002F1C
	[HarmonyPrefix]
	[HarmonyPatch(typeof(XUiC_ItemInfoWindow), "GetBindingValueInternal")]
	public static bool GetBindingValueInternalPrefix(ref string value, string bindingName, ref bool __result, XUiC_ItemInfoWindow __instance)
	{
		bool result;
		if (!(bindingName == "CATUI_ItemUseTimesResidue"))
		{
			if (!(bindingName == "CATUI_ItemUseTimesMax"))
			{
				result = true;
			}
			else
			{
				ItemStack itemStack = __instance.itemStack;
				bool flag = itemStack.IsEmpty();
				if (flag)
				{
					value = "0";
				}
				else
				{
					bool flag2 = itemStack.itemValue.MaxUseTimes == 0;
					if (flag2)
					{
						value = "1";
					}
					else
					{
						value = itemStack.itemValue.MaxUseTimes.ToString("F0");
					}
				}
				__result = true;
				result = false;
			}
		}
		else
		{
			ItemStack itemStack2 = __instance.itemStack;
			bool flag3 = itemStack2.IsEmpty();
			if (flag3)
			{
				value = "0";
			}
			else
			{
				bool flag4 = itemStack2.itemValue.MaxUseTimes == 0;
				if (flag4)
				{
					value = "1";
				}
				else
				{
					value = (itemStack2.itemValue.MaxUseTimes - Convert.ToInt32(itemStack2.itemValue.UseTimes)).ToString("F0");
				}
			}
			__result = true;
			result = false;
		}
		return result;
	}
}
