using System;
using HarmonyLib;

// Token: 0x02000013 RID: 19
[HarmonyPatch]
public class XUiC_SkillAttributeLevelPatch
{
	// Token: 0x06000034 RID: 52 RVA: 0x000055BC File Offset: 0x000037BC
	[HarmonyPrefix]
	[HarmonyPatch(typeof(XUiC_SkillAttributeLevel), "GetBindingValueInternal")]
	public static bool GetBindingValueInternalPrefix(string _bindingName, ref string _value, ref bool __result, XUiC_SkillAttributeLevel __instance)
	{
		int level = __instance.level;
		bool flag = __instance.CurrentSkill != null && __instance.CurrentSkill.ProgressionClass.MaxLevel >= __instance.level;
		EntityPlayerLocal entityPlayer = __instance.xui.playerUI.entityPlayer;
		bool flag2 = false;
		bool flag3 = false;
		bool flag4 = false;
		bool flag5 = false;
		bool flag6 = flag;
		if (flag6)
		{
			flag3 = (__instance.CurrentSkill.Level >= __instance.level);
			flag2 = (__instance.CurrentSkill.Level + 1 == __instance.level && __instance.CurrentSkill.Level + 1 <= __instance.CurrentSkill.CalculatedMaxLevel(entityPlayer));
			flag4 = (!flag3 && __instance.CurrentSkill.CalculatedLevel(entityPlayer) >= __instance.level);
			flag5 = (flag3 && __instance.CurrentSkill.CalculatedLevel(entityPlayer) < __instance.level);
		}
		bool result;
		if (!(_bindingName == "buyvisible"))
		{
			if (!(_bindingName == "CATUI_IsBuffed"))
			{
				if (!(_bindingName == "CATUI_IsNerfed"))
				{
					if (!(_bindingName == "CATUI_BuyCost"))
					{
						if (!(_bindingName == "CATUI_SkillStat"))
						{
							result = true;
						}
						else
						{
							_value = "notbuy";
							bool flag7 = flag3;
							if (flag7)
							{
								_value = "bought";
							}
							else
							{
								bool flag8 = flag2;
								if (flag8)
								{
									_value = "buy";
								}
							}
							__result = true;
							result = false;
						}
					}
					else
					{
						_value = "0";
						bool flag9 = __instance.CurrentSkill != null;
						if (flag9)
						{
							_value = __instance.CurrentSkill.ProgressionClass.CalculatedCostForLevel(level).ToString();
						}
						__result = true;
						result = false;
					}
				}
				else
				{
					_value = "fasle";
					bool flag10 = __instance.CurrentSkill != null && flag3 && flag5;
					if (flag10)
					{
						_value = "true";
					}
					__result = true;
					result = false;
				}
			}
			else
			{
				_value = "fasle";
				bool flag11 = __instance.CurrentSkill != null && flag4;
				if (flag11)
				{
					_value = "true";
				}
				__result = true;
				result = false;
			}
		}
		else
		{
			_value = flag.ToString();
			bool flag12 = __instance.CurrentSkill != null;
			if (flag12)
			{
				int num = __instance.CurrentSkill.ProgressionClass.CalculatedCostForLevel(__instance.level);
				bool flag13 = num <= 0;
				if (flag13)
				{
					_value = "false";
				}
			}
			__result = true;
			result = false;
		}
		return result;
	}
}
