using System;
using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;

// Token: 0x02000020 RID: 32
[HarmonyPatch]
public class XUiViewPatch
{
	// Token: 0x06000056 RID: 86 RVA: 0x00007DDC File Offset: 0x00005FDC
	[HarmonyPostfix]
	[HarmonyPatch(typeof(XUiView), "ParseAttribute")]
	public static void ParseAttributePostfix(XUiView __instance, string _attribute, string _value)
	{
		bool flag = _attribute == "transform_scale";
		if (flag)
		{
			float num = 1f;
			float.TryParse(_value, out num);
			bool flag2 = num < 0.1f;
			if (flag2)
			{
				num = 0.1f;
			}
			bool flag3 = num > 3f;
			if (flag3)
			{
				num = 3f;
			}
			bool flag4 = XUiViewPatch.elementScales.ContainsKey(__instance);
			if (flag4)
			{
				XUiViewPatch.elementScales[__instance] = num;
			}
			else
			{
				XUiViewPatch.elementScales.Add(__instance, num);
			}
			__instance.IsDirty = true;
		}
	}

	// Token: 0x06000057 RID: 87 RVA: 0x00007E63 File Offset: 0x00006063
	[HarmonyPostfix]
	[HarmonyPatch(typeof(XUiView), "InitView")]
	public static void InitViewPostfix(XUiView __instance)
	{
		XUiViewPatch.ApplyScale(__instance);
	}

	// Token: 0x06000058 RID: 88 RVA: 0x00007E6D File Offset: 0x0000606D
	[HarmonyPostfix]
	[HarmonyPatch(typeof(XUiView), "UpdateData")]
	public static void UpdateDataPostfix(XUiView __instance)
	{
		XUiViewPatch.ApplyScale(__instance);
	}

	// Token: 0x06000059 RID: 89 RVA: 0x00007E78 File Offset: 0x00006078
	private static void ApplyScale(XUiView view)
	{
		float d;
		bool flag = XUiViewPatch.elementScales.TryGetValue(view, out d);
		if (flag)
		{
			bool flag2 = view.UiTransform != null;
			if (flag2)
			{
				view.UiTransform.localScale = Vector3.one * d;
			}
		}
	}

	// Token: 0x04000015 RID: 21
	private static readonly Dictionary<XUiView, float> elementScales = new Dictionary<XUiView, float>();
}
