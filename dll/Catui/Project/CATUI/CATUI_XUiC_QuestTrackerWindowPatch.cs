using System;
using Challenges;
using HarmonyLib;

// Token: 0x0200000F RID: 15
[HarmonyPatch]
public class XUiC_QuestTrackerWindowPatch
{
	// Token: 0x0600002B RID: 43 RVA: 0x000052AC File Offset: 0x000034AC
	[HarmonyPrefix]
	[HarmonyPatch(typeof(XUiC_QuestTrackerWindow), "GetBindingValueInternal")]
	public static bool GetBindingValueInternalPrefix(string bindingName, ref string value, ref bool __result, XUiC_QuestTrackerWindow __instance)
	{
		Quest currentQuest = __instance.currentQuest;
		Challenge currentChallenge = __instance.currentChallenge;
		bool result;
		if (!(bindingName == "CATUI_ListCount"))
		{
			result = true;
		}
		else
		{
			value = "0";
			bool flag = currentQuest != null;
			if (flag)
			{
				value = currentQuest.ActiveObjectives.ToString();
			}
			else
			{
				bool flag2 = currentChallenge != null;
				if (flag2)
				{
					value = currentChallenge.ActiveObjectives.ToString();
				}
			}
			__result = true;
			result = false;
		}
		return result;
	}
}
