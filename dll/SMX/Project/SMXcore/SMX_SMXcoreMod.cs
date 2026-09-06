using System;
using System.Reflection;
using GearsAPI.Settings;
using GearsAPI.Settings.Global;
using GearsAPI.Settings.World;
using HarmonyLib;
using SMXcore.HarmonyPatches;

// Token: 0x02000002 RID: 2
public class SMXcoreMod : IModApi, IGearsModApi
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	public void InitMod(Mod _modInstance)
	{
		Log.Out("[SMXcore] Loading Patch");
		Harmony harmonyInstance = SMXHarmonyPatcher.GetHarmonyInstance();
		harmonyInstance.PatchAll(Assembly.GetExecutingAssembly());
		Log.Out("[SMXcore] Loaded Patch");
	}

	// Token: 0x06000002 RID: 2 RVA: 0x00002086 File Offset: 0x00000286
	public void InitMod(IGearsMod modInstance)
	{
	}

	// Token: 0x06000003 RID: 3 RVA: 0x0000208C File Offset: 0x0000028C
	public void OnGlobalSettingsLoaded(IModGlobalSettings modSettings)
	{
		IGlobalModSettingsTab tab = modSettings.GetTab("General");
		IGlobalModSettingsCategory category = tab.GetCategory("General");
		IGlobalValueSetting globalValueSetting = category.GetSetting("ForceSkipNews") as IGlobalValueSetting;
		SMXSettings.SkipNewsScreen(globalValueSetting, globalValueSetting.CurrentValue);
	}

	// Token: 0x06000004 RID: 4 RVA: 0x000020D0 File Offset: 0x000002D0
	public void OnWorldSettingsLoaded(IModWorldSettings worldSettings)
	{
	}

	// Token: 0x04000001 RID: 1
	public const string TAG = "SMXcore";
}
