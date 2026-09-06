using System;
using HarmonyLib;

namespace SMXcore.HarmonyPatches
{
	// Token: 0x02000027 RID: 39
	[HarmonyPatch(typeof(BindingInfo))]
	public class BindingInfo_Harmony
	{
		// Token: 0x0600012E RID: 302 RVA: 0x0000C3A8 File Offset: 0x0000A5A8
		[HarmonyPostfix]
		[HarmonyPatch(3)]
		[HarmonyPatch(new Type[]
		{
			typeof(XUiView),
			typeof(string),
			typeof(string)
		})]
		public static void Constructor(BindingInfo __instance, XUiView _view, string _property, string _sourceText)
		{
			bool flag = _sourceText.Contains("craftingskillcount") || _sourceText.Contains("bookgroupcount");
			if (flag)
			{
				__instance.RefreshValue(true);
			}
		}
	}
}
