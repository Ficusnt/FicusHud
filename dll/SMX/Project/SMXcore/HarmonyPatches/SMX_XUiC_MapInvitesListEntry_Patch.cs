using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace SMXcore.HarmonyPatches
{
	// Token: 0x0200002B RID: 43
	public static class XUiC_MapInvitesListEntry_Patch
	{
		// Token: 0x06000135 RID: 309 RVA: 0x0000C6B8 File Offset: 0x0000A8B8
		public static void PatchUpdateSelectedMethod()
		{
			Harmony harmonyInstance = SMXHarmonyPatcher.GetHarmonyInstance();
			MethodInfo methodInfo = AccessTools.Method(typeof(XUiC_MapInvitesListEntry), "updateSelected", null, null);
			MethodInfo methodInfo2 = AccessTools.Method(typeof(XUiC_MapInvitesListEntry_Patch), "updateSelected", null, null);
			bool flag = false;
			IEnumerable<MethodBase> patchedMethods = harmonyInstance.GetPatchedMethods();
			foreach (MethodBase methodBase in patchedMethods)
			{
				bool flag2 = methodBase.Equals(methodInfo);
				if (flag2)
				{
					flag = true;
					break;
				}
			}
			bool flag3 = !flag;
			if (flag3)
			{
				harmonyInstance.Patch(methodInfo, new HarmonyMethod(methodInfo2), null, null, null, null);
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000C774 File Offset: 0x0000A974
		public static bool updateSelected(bool _bHover, XUiV_Sprite ___Background, bool ___m_bSelected)
		{
			bool flag = ___Background != null;
			if (flag)
			{
				if (___m_bSelected)
				{
					___Background.Color = new Color32(160, 160, 160, byte.MaxValue);
					___Background.SpriteName = "smxlib_window_button_background";
				}
				if (_bHover)
				{
					___Background.Color = new Color32(96, 96, 96, byte.MaxValue);
					___Background.SpriteName = "smxlib_window_button_background";
				}
				___Background.Color = new Color32(7, 7, 7, byte.MaxValue);
				___Background.SpriteName = "smxlib_window_button_background";
			}
			return false;
		}
	}
}
