using System;
using System.Reflection;
using HarmonyLib;
using UnityEngine;

// Token: 0x02000021 RID: 33
public class ModStartup : IModApi
{
	// Token: 0x0600005C RID: 92 RVA: 0x00007ED8 File Offset: 0x000060D8
	public void InitMod(Mod modInstance)
	{
		Assembly executingAssembly = Assembly.GetExecutingAssembly();
		Harmony harmony = new Harmony(executingAssembly.GetName().Name);
		harmony.PatchAll(executingAssembly);
		Debug.Log("<color=#00FF00>CATUI Applied.</color>");
		ModEvents.GameAwake.RegisterHandler(delegate(ref ModEvents.SGameAwakeData _data)
		{
			Constants.TrackedFriendColors = new Color[]
			{
				new Color32(byte.MaxValue, 173, 31, byte.MaxValue),
				new Color32(4, 254, 133, byte.MaxValue),
				new Color32(1, 239, byte.MaxValue, byte.MaxValue),
				new Color32(byte.MaxValue, 82, 82, byte.MaxValue),
				new Color32(89, 167, byte.MaxValue, byte.MaxValue),
				new Color32(231, 92, byte.MaxValue, byte.MaxValue),
				new Color32(byte.MaxValue, 235, 59, byte.MaxValue),
				new Color32(153, 110, byte.MaxValue, byte.MaxValue)
			};
		});
	}
}
