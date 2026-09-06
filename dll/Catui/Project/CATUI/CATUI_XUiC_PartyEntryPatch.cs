using System;
using HarmonyLib;

// Token: 0x0200000D RID: 13
[HarmonyPatch]
public class XUiC_PartyEntryPatch
{
	// Token: 0x06000027 RID: 39 RVA: 0x00005070 File Offset: 0x00003270
	[HarmonyPrefix]
	[HarmonyPatch(typeof(XUiC_PartyEntry), "GetBindingValueInternal")]
	public static bool Prefix(string bindingName, ref string value, ref bool __result, XUiC_PartyEntry __instance)
	{
		bool result;
		if (!(bindingName == "CATUI_Ping"))
		{
			if (!(bindingName == "CATUI_PingColor"))
			{
				result = true;
			}
			else
			{
				value = "0,0,0";
				bool flag = __instance.Player != null;
				if (flag)
				{
					int pingToServer = __instance.Player.pingToServer;
					bool flag2 = pingToServer > 0;
					if (flag2)
					{
						bool flag3 = pingToServer <= 150;
						if (flag3)
						{
							value = "67, 207, 124";
						}
						else
						{
							bool flag4 = pingToServer <= 500;
							if (flag4)
							{
								value = "255, 195, 0";
							}
							else
							{
								value = "255, 0, 0";
							}
						}
					}
				}
				__result = true;
				result = false;
			}
		}
		else
		{
			value = "-1";
			bool flag5 = __instance.Player != null;
			if (flag5)
			{
				int pingToServer2 = __instance.Player.pingToServer;
				bool flag6 = pingToServer2 > 0;
				if (flag6)
				{
					value = ((pingToServer2 > 1000) ? ">1000" : pingToServer2.ToString());
				}
			}
			__result = true;
			result = false;
		}
		return result;
	}
}
