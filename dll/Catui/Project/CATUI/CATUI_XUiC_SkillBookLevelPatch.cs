using System;
using HarmonyLib;

// Token: 0x02000014 RID: 20
[HarmonyPatch]
public class XUiC_SkillBookLevelPatch
{
	// Token: 0x06000036 RID: 54 RVA: 0x00005838 File Offset: 0x00003A38
	[HarmonyPrefix]
	[HarmonyPatch(typeof(XUiC_SkillBookLevel), "GetBindingValueInternal")]
	public static bool GetBindingValueInternalPrefix(string _bindingName, ref string _value, ref bool __result, XUiC_SkillBookLevel __instance)
	{
		bool flag = __instance.CurrentSkill != null && __instance.perk != null;
		EntityPlayerLocal entityPlayer = __instance.xui.playerUI.entityPlayer;
		bool flag2 = false;
		bool flag3 = flag;
		if (flag3)
		{
			flag2 = (__instance.perk != null && __instance.perk.Level > 0);
		}
		bool result;
		if (!(_bindingName == "CATUI_SkillStat"))
		{
			result = true;
		}
		else
		{
			_value = "notbuy";
			bool flag4 = flag2;
			if (flag4)
			{
				_value = "bought";
			}
			__result = true;
			result = false;
		}
		return result;
	}
}
