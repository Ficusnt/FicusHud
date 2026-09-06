using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;

namespace SMXcore.HarmonyPatches
{
	// Token: 0x02000029 RID: 41
	public static class ItemActionEntryShowPerk_Patch
	{
		// Token: 0x06000132 RID: 306 RVA: 0x0000C4C8 File Offset: 0x0000A6C8
		public static void PatchOnActivatedMethod()
		{
			Harmony harmonyInstance = SMXHarmonyPatcher.GetHarmonyInstance();
			MethodInfo methodInfo = AccessTools.Method(typeof(ItemActionEntryShowPerk), "OnActivated", null, null);
			MethodInfo methodInfo2 = AccessTools.Method(typeof(ItemActionEntryShowPerk_Patch), "OnActivated", null, null);
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

		// Token: 0x06000133 RID: 307 RVA: 0x0000C584 File Offset: 0x0000A784
		public static bool OnActivated(ItemActionEntryShowPerk __instance)
		{
			XUi xui = __instance.ItemController.xui;
			List<XUiC_SkillListWindow> childrenByType = xui.GetChildrenByType<XUiC_SkillListWindow>();
			bool flag = childrenByType.Count <= 0;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				xui.playerUI.windowManager.CloseIfOpen("looting");
				XUiC_SkillListWindow xuiC_SkillListWindow = null;
				foreach (XUiC_SkillListWindow xuiC_SkillListWindow2 in childrenByType)
				{
					bool flag2 = xuiC_SkillListWindow2.WindowGroup != null && xuiC_SkillListWindow2.WindowGroup.isShowing;
					if (flag2)
					{
						xuiC_SkillListWindow = xuiC_SkillListWindow2;
						break;
					}
				}
				bool flag3 = xuiC_SkillListWindow == null;
				if (flag3)
				{
					XUiC_WindowSelector.OpenSelectorAndWindow(xui.playerUI.entityPlayer, "skills");
					xuiC_SkillListWindow = xui.GetChildByType<XUiC_SkillListWindow>();
				}
				bool flag4 = xuiC_SkillListWindow != null;
				if (flag4)
				{
					xuiC_SkillListWindow.SetSelectedByUnlockData(__instance.unlockData);
				}
				result = false;
			}
			return result;
		}
	}
}
