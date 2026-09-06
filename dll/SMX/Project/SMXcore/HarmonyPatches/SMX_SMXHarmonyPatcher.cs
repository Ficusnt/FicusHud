using System;
using HarmonyLib;

namespace SMXcore.HarmonyPatches
{
	// Token: 0x0200002A RID: 42
	public static class SMXHarmonyPatcher
	{
		// Token: 0x06000134 RID: 308 RVA: 0x0000C684 File Offset: 0x0000A884
		public static Harmony GetHarmonyInstance()
		{
			bool flag = SMXHarmonyPatcher.harmony == null;
			if (flag)
			{
				SMXHarmonyPatcher.harmony = new Harmony("Harmony.SMXcore.Mod");
			}
			return SMXHarmonyPatcher.harmony;
		}

		// Token: 0x040000CA RID: 202
		public const string Id = "Harmony.SMXcore.Mod";

		// Token: 0x040000CB RID: 203
		private static Harmony harmony;
	}
}
