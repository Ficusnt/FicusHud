using System;
using HarmonyLib;
using UnityEngine;

// Token: 0x02000008 RID: 8
[HarmonyPatch]
public class XUiC_EquipmentStackPatch
{
	// Token: 0x06000014 RID: 20 RVA: 0x00002FB8 File Offset: 0x000011B8
	[HarmonyPrefix]
	[HarmonyPatch(typeof(XUiController), "GetBindingValueInternal")]
	public static bool GetBindingValueInternalPrefix(string _bindingName, ref string _value, ref bool __result, XUiController __instance)
	{
		XUiC_EquipmentStack xuiC_EquipmentStack = __instance as XUiC_EquipmentStack;
		bool result;
		if (!(_bindingName == "CATUI_durabilityColor"))
		{
			if (!(_bindingName == "CATUI_durabilityFill"))
			{
				if (!(_bindingName == "CATUI_itemType"))
				{
					if (!(_bindingName == "CATUI_hasQuality"))
					{
						result = true;
					}
					else
					{
						_value = "false";
						bool flag = xuiC_EquipmentStack != null && xuiC_EquipmentStack.itemValue != null;
						if (flag)
						{
							_value = xuiC_EquipmentStack.itemValue.HasQuality.ToString();
						}
						__result = true;
						result = false;
					}
				}
				else
				{
					_value = "0";
					bool flag2 = xuiC_EquipmentStack != null && xuiC_EquipmentStack.itemValue != null;
					if (flag2)
					{
						_value = xuiC_EquipmentStack.SlotNumber.ToString();
					}
					__result = true;
					result = false;
				}
			}
			else
			{
				_value = "0";
				bool flag3 = xuiC_EquipmentStack != null && xuiC_EquipmentStack.itemValue != null;
				if (flag3)
				{
					_value = XUiC_EquipmentStackPatch.durabilityFillFormatter.Format(xuiC_EquipmentStack.itemValue.PercentUsesLeft);
				}
				__result = true;
				result = false;
			}
		}
		else
		{
			_value = "0,0,0,0";
			bool flag4 = xuiC_EquipmentStack != null && xuiC_EquipmentStack.itemValue != null;
			if (flag4)
			{
				Color32 v = QualityInfo.GetQualityColor((int)xuiC_EquipmentStack.itemValue.Quality);
				_value = XUiC_EquipmentStackPatch.durabilityColorFormatter.Format(v);
			}
			__result = true;
			result = false;
		}
		return result;
	}

	// Token: 0x06000015 RID: 21 RVA: 0x00003114 File Offset: 0x00001314
	[HarmonyPrefix]
	[HarmonyPatch(typeof(XUiC_EquipmentStack), "Update")]
	public static void Prefix(XUiController __instance, bool ___isDirty)
	{
		if (___isDirty)
		{
			__instance.RefreshBindings(false);
		}
	}

	// Token: 0x04000005 RID: 5
	[PublicizedFrom(EAccessModifier.Private)]
	public static CachedStringFormatterXuiRgbaColor durabilityColorFormatter = new CachedStringFormatterXuiRgbaColor();

	// Token: 0x04000006 RID: 6
	[PublicizedFrom(EAccessModifier.Private)]
	public static CachedStringFormatterFloat durabilityFillFormatter = new CachedStringFormatterFloat(null);
}
