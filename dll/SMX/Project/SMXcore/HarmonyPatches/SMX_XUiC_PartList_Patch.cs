using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;

namespace SMXcore.HarmonyPatches
{
	// Token: 0x0200002D RID: 45
	public static class XUiC_PartList_Patch
	{
		// Token: 0x06000139 RID: 313 RVA: 0x0000C9BC File Offset: 0x0000ABBC
		public static void PatchSetMainItemMethod()
		{
			Harmony harmonyInstance = SMXHarmonyPatcher.GetHarmonyInstance();
			MethodInfo methodInfo = AccessTools.Method(typeof(XUiC_PartList), "SetMainItem", null, null);
			MethodInfo methodInfo2 = AccessTools.Method(typeof(XUiC_PartList_Patch), "SetMainItem", null, null);
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
				harmonyInstance.Patch(methodInfo, null, new HarmonyMethod(methodInfo2), null, null, null);
			}
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000CA78 File Offset: 0x0000AC78
		public static void PatchSetSlotsMethod()
		{
			Harmony harmonyInstance = SMXHarmonyPatcher.GetHarmonyInstance();
			MethodInfo methodInfo = AccessTools.Method(typeof(XUiC_PartList), "SetSlots", null, null);
			MethodInfo methodInfo2 = AccessTools.Method(typeof(XUiC_PartList_Patch), "SetSlots", null, null);
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

		// Token: 0x0600013B RID: 315 RVA: 0x0000CB34 File Offset: 0x0000AD34
		public static void PatchSetSlotMethod()
		{
			Harmony harmonyInstance = SMXHarmonyPatcher.GetHarmonyInstance();
			MethodInfo methodInfo = AccessTools.Method(typeof(XUiC_PartList), "SetSlot", null, null);
			MethodInfo methodInfo2 = AccessTools.Method(typeof(XUiC_PartList_Patch), "SetSlot", null, null);
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

		// Token: 0x0600013C RID: 316 RVA: 0x0000CBF0 File Offset: 0x0000ADF0
		public static void SetMainItem(XUiC_PartList __instance, ItemStack itemStack)
		{
			XUiC_PartList xuiC_PartList = __instance as XUiC_PartList;
			bool flag = xuiC_PartList != null;
			if (flag)
			{
				xuiC_PartList.SetMainItem(itemStack);
			}
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000CC18 File Offset: 0x0000AE18
		public static bool SetSlots(XUiC_PartList __instance, ItemValue[] parts, int startIndex)
		{
			XUiC_PartList xuiC_PartList = __instance as XUiC_PartList;
			bool flag = xuiC_PartList != null;
			bool result;
			if (flag)
			{
				xuiC_PartList.SetSlots(parts, startIndex);
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000CC48 File Offset: 0x0000AE48
		public static bool SetSlot(XUiC_PartList __instance, ItemValue part, int index)
		{
			XUiC_PartList xuiC_PartList = __instance as XUiC_PartList;
			bool flag = xuiC_PartList != null;
			bool result;
			if (flag)
			{
				xuiC_PartList.SetSlot(part, index);
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}
	}
}
