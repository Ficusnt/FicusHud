using System;
using HarmonyLib;

// Token: 0x02000018 RID: 24
[HarmonyPatch]
public class XUiC_SkillListPatch
{
	// Token: 0x0600003F RID: 63 RVA: 0x000066D8 File Offset: 0x000048D8
	[HarmonyPrefix]
	[HarmonyPatch(typeof(XUiC_SkillList), "updateFilteredList")]
	public static bool Prefix(XUiC_SkillList __instance)
	{
		__instance.currentSkills.Clear();
		string a = __instance.Category.Trim();
		bool flag = __instance.filterText != "";
		foreach (ProgressionValue progressionValue in __instance.skills)
		{
			ProgressionClass progressionClass = (progressionValue != null) ? progressionValue.ProgressionClass : null;
			bool flag2 = progressionClass == null || !progressionClass.ValidDisplay(__instance.DisplayType) || progressionClass.Name == null || progressionClass.IsBook || progressionClass.Hidden || (flag && !progressionClass.NameKey.ContainsCaseInsensitive(__instance.filterText) && !Localization.Get(progressionClass.NameKey, false).ContainsCaseInsensitive(__instance.filterText));
			if (!flag2)
			{
				bool flag3 = a == "" || a.EqualsCaseInsensitive(progressionClass.Name);
				if (flag3)
				{
					__instance.currentSkills.Add(progressionValue);
				}
				else
				{
					ProgressionClass parent = progressionClass.Parent;
					bool flag4 = parent != null && parent != progressionClass;
					bool flag5 = progressionClass.IsCrafting || (progressionClass.IsSkill && progressionClass.Parent.Name == "LearnByUseName");
					bool flag6 = a.EqualsCaseInsensitive(progressionClass.Parent.Name);
					bool flag7 = progressionClass.IsPerk || progressionClass.IsBookGroup;
					bool flag8 = a.EqualsCaseInsensitive(progressionClass.Parent.Parent.Name);
					bool flag9 = flag4 && ((flag5 && flag6) || (flag7 && flag8));
					if (flag9)
					{
						__instance.currentSkills.Add(progressionValue);
					}
				}
			}
		}
		__instance.currentSkills.Sort(ProgressionClass.ListSortOrderComparer.Instance);
		bool flag10 = __instance.filterText == "";
		if (flag10)
		{
			for (int i = 0; i < __instance.currentSkills.Count; i++)
			{
				bool isAttribute = __instance.currentSkills[i].ProgressionClass.IsAttribute;
				if (isAttribute)
				{
					while (i % __instance.skillEntries.Length != 0)
					{
						__instance.currentSkills.Insert(i, null);
						i++;
					}
				}
			}
		}
		XUiC_Paging pagingControl = __instance.pagingControl;
		if (pagingControl != null)
		{
			pagingControl.SetLastPageByElementsAndPageLength(__instance.currentSkills.Count, __instance.skillEntries.Length);
		}
		bool flag11 = string.IsNullOrEmpty(__instance.selectName);
		bool result;
		if (flag11)
		{
			result = false;
		}
		else
		{
			for (int j = 0; j < __instance.currentSkills.Count; j++)
			{
				bool flag12 = __instance.currentSkills[j].Name == __instance.selectName;
				if (flag12)
				{
					XUiC_Paging pagingControl2 = __instance.pagingControl;
					if (pagingControl2 != null)
					{
						pagingControl2.SetPage(j / __instance.skillEntries.Length);
					}
					break;
				}
			}
			result = false;
		}
		return result;
	}
}
