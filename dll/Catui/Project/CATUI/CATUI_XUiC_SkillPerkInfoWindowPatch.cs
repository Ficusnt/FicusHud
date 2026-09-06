using System;
using HarmonyLib;

// Token: 0x02000019 RID: 25
[HarmonyPatch]
public class XUiC_SkillPerkInfoWindowPatch
{
	// Token: 0x06000041 RID: 65 RVA: 0x00006A20 File Offset: 0x00004C20
	[HarmonyPrefix]
	[HarmonyPatch(typeof(XUiC_SkillPerkInfoWindow), "GetBindingValueInternal")]
	public static bool GetBindingValueInternalPrefix(string _bindingName, ref string _value, ref bool __result, XUiC_SkillPerkInfoWindow __instance)
	{
		bool result;
		if (!(_bindingName == "CATUI_SkillGroupName"))
		{
			if (!(_bindingName == "CATUI_MaxSkillLevel"))
			{
				result = true;
			}
			else
			{
				_value = "0";
				bool flag = __instance.CurrentSkill != null;
				if (flag)
				{
					_value = __instance.CurrentSkill.ProgressionClass.MaxLevel.ToString();
				}
				__result = true;
				result = false;
			}
		}
		else
		{
			_value = "";
			bool flag2 = __instance.CurrentSkill != null;
			if (flag2)
			{
				bool isSkill = __instance.CurrentSkill.ProgressionClass.Parent.IsSkill;
				if (isSkill)
				{
					_value = Localization.Get(__instance.CurrentSkill.ProgressionClass.Parent.NameKey, false);
				}
			}
			__result = true;
			result = false;
		}
		return result;
	}
}
