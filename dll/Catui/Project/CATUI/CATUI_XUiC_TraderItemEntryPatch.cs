using System;
using HarmonyLib;

// Token: 0x0200001C RID: 28
[HarmonyPatch]
public class XUiC_TraderItemEntryPatch
{
	// Token: 0x06000047 RID: 71 RVA: 0x0000778C File Offset: 0x0000598C
	[HarmonyPrefix]
	[HarmonyPatch(typeof(XUiC_TraderItemEntry), "GetBindingValueInternal")]
	public static bool GetBindingValueInternalPrefix(string bindingName, ref string value, ref bool __result, XUiC_TraderItemEntry __instance)
	{
		bool result;
		if (!(bindingName == "CATUI_ItemName"))
		{
			if (!(bindingName == "CATUI_ItemCount"))
			{
				result = true;
			}
			else
			{
				value = "1";
				bool flag = __instance.item != null;
				if (flag)
				{
					value = __instance.item.count.ToString();
				}
				__result = true;
				result = false;
			}
		}
		else
		{
			value = "";
			bool flag2 = __instance.item != null;
			if (flag2)
			{
				value = __instance.itemClass.GetLocalizedItemName();
			}
			__result = true;
			result = false;
		}
		return result;
	}
}
