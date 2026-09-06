using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace SMXcore.HarmonyPatches
{
	// Token: 0x0200002C RID: 44
	public static class XUiC_MapWaypointListEntry_Patch
	{
		// Token: 0x06000137 RID: 311 RVA: 0x0000C824 File Offset: 0x0000AA24
		public static void PatchUpdateSelectedMethod()
		{
			Harmony harmonyInstance = SMXHarmonyPatcher.GetHarmonyInstance();
			MethodInfo methodInfo = AccessTools.Method(typeof(XUiC_MapWaypointListEntry), "updateSelected", null, null);
			MethodInfo methodInfo2 = AccessTools.Method(typeof(XUiC_MapWaypointListEntry_Patch), "updateSelected", null, null);
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

		// Token: 0x06000138 RID: 312 RVA: 0x0000C8E0 File Offset: 0x0000AAE0
		public static bool updateSelected(XUiC_MapWaypointListEntry __instance, ref bool _bHover, ref XUiV_Sprite ___Background, ref bool ___m_bSelected)
		{
			XUiV_Sprite xuiV_Sprite = ___Background;
			bool flag = xuiV_Sprite != null;
			if (flag)
			{
				bool flag2 = ___m_bSelected;
				if (flag2)
				{
					xuiV_Sprite.Color = new Color32(160, 160, 160, byte.MaxValue);
					xuiV_Sprite.SpriteName = "smxlib_window_button_background";
				}
				else
				{
					bool flag3 = _bHover;
					if (flag3)
					{
						xuiV_Sprite.Color = new Color32(96, 96, 96, byte.MaxValue);
						xuiV_Sprite.SpriteName = "smxlib_window_button_background";
					}
					else
					{
						xuiV_Sprite.Color = new Color32(7, 7, 7, byte.MaxValue);
						xuiV_Sprite.SpriteName = "smxlib_window_button_background";
					}
				}
			}
			__instance.Tracking.IsVisible = (__instance.Waypoint != null && __instance.Waypoint.bTracked);
			return false;
		}
	}
}
