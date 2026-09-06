using System;
using HarmonyLib;

// Token: 0x02000004 RID: 4
[HarmonyPatch]
public class XUiC_CompanionEntryListPatch
{
	// Token: 0x06000006 RID: 6 RVA: 0x0000213C File Offset: 0x0000033C
	[HarmonyPrefix]
	[HarmonyPatch(typeof(XUiC_CompanionEntryList), "RefreshPartyList")]
	public static bool Prefix(XUiC_CompanionEntryList __instance)
	{
		int i = 0;
		EntityPlayer entityPlayer = __instance.xui.playerUI.entityPlayer;
		bool flag = entityPlayer.Companions != null;
		if (flag)
		{
			for (int j = 0; j < entityPlayer.Companions.Count; j++)
			{
				EntityAlive companion = entityPlayer.Companions[j];
				bool flag2 = i >= __instance.entryList.Count;
				if (flag2)
				{
					break;
				}
				__instance.entryList[i++].SetCompanion(companion);
			}
			while (i < __instance.entryList.Count)
			{
				__instance.entryList[i].SetCompanion(null);
				i++;
			}
		}
		else
		{
			for (int k = 0; k < __instance.entryList.Count; k++)
			{
				__instance.entryList[k].SetCompanion(null);
			}
		}
		return false;
	}
}
