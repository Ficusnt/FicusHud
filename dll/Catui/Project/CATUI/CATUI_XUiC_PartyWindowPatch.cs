using System;
using HarmonyLib;

// Token: 0x0200000E RID: 14
[HarmonyPatch]
public class XUiC_PartyWindowPatch
{
	// Token: 0x06000029 RID: 41 RVA: 0x00005188 File Offset: 0x00003388
	[HarmonyPrefix]
	[HarmonyPatch(typeof(XUiC_PartyWindow), "GetBindingValueInternal")]
	public static bool GetBindingValueInternalPrefix(string _bindingName, ref string _value, ref bool __result, XUiC_PartyWindow __instance)
	{
		bool result;
		if (!(_bindingName == "CATUI_PartyWindowPositionY"))
		{
			result = true;
		}
		else
		{
			int num = 130;
			int num2 = 56;
			_value = num.ToString();
			bool flag = __instance.player != null && __instance.player.Party != null && __instance.player.Party.MemberList != null;
			if (flag)
			{
				int count = __instance.player.Party.MemberList.Count;
				bool flag2 = count > 0;
				if (flag2)
				{
					_value = (num + (count - 1) * num2).ToString();
				}
			}
			else
			{
				bool flag3 = __instance.player != null && __instance.player.Party != null && __instance.player.Companions != null;
				if (flag3)
				{
					int count2 = __instance.player.Companions.Count;
					bool flag4 = count2 > 0;
					if (flag4)
					{
						_value = (num + count2 * num2).ToString();
					}
				}
			}
			__result = true;
			result = false;
		}
		return result;
	}
}
