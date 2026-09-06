using System;
using System.Collections.Generic;
using HarmonyLib;

// Token: 0x02000016 RID: 22
[HarmonyPatch]
public class XUiC_SkillCraftingInfoWindowPatch
{
	// Token: 0x0600003A RID: 58 RVA: 0x00005AE0 File Offset: 0x00003CE0
	[HarmonyPrefix]
	[HarmonyPatch(typeof(XUiC_SkillCraftingInfoWindow), "UpdateSkill")]
	public static bool UpdateSkill_Prefix(XUiC_SkillCraftingInfoWindow __instance)
	{
		ProgressionValue currentSkill = __instance.CurrentSkill;
		XUiC_ItemActionList actionItemList = __instance.actionItemList;
		int skillsPerPage = __instance.skillsPerPage;
		XUiC_Paging pager = __instance.pager;
		XUiWindowGroup windowGroup = __instance.windowGroup;
		List<XUiC_SkillCraftingInfoEntry> levelEntries = __instance.levelEntries;
		bool flag = currentSkill != null && actionItemList != null;
		if (flag)
		{
			actionItemList.SetCraftingActionList(XUiC_ItemActionList.ItemActionListTypes.Skill, __instance);
		}
		int num = ((pager != null) ? pager.GetPage() : 0) * skillsPerPage;
		ProgressionClass progressionClass = (currentSkill != null) ? currentSkill.ProgressionClass : null;
		bool flag2 = progressionClass != null && progressionClass.DisplayDataList != null;
		bool result;
		if (flag2)
		{
			XUiC_SkillEntry entryForSkill = windowGroup.Controller.GetChildByType<XUiC_SkillList>().GetEntryForSkill(currentSkill);
			List<ProgressionClass.DisplayData> list = new List<ProgressionClass.DisplayData>();
			for (int i = 0; i < progressionClass.DisplayDataList.Count; i++)
			{
				bool flag3 = progressionClass.DisplayDataList[i].UnlockDataList != null;
				if (flag3)
				{
					for (int j = 0; j < progressionClass.DisplayDataList[i].UnlockDataList.Count; j++)
					{
						ProgressionClass.DisplayData displayData = progressionClass.DisplayDataList[i];
						ProgressionClass.DisplayData.UnlockData unlockData = displayData.UnlockDataList[j];
						list.Add(new ProgressionClass.DisplayData
						{
							CustomHasQuality = displayData.CustomHasQuality,
							CustomIcon = displayData.CustomIcon,
							CustomIconTint = displayData.CustomIconTint,
							item = displayData.item,
							Owner = displayData.Owner,
							QualityStarts = displayData.QualityStarts,
							UnlockDataList = new List<ProgressionClass.DisplayData.UnlockData>(),
							UnlockDataList = 
							{
								unlockData
							},
							ItemName = unlockData.ItemName
						});
					}
				}
			}
			foreach (XUiC_SkillCraftingInfoEntry xuiC_SkillCraftingInfoEntry in levelEntries)
			{
				ProgressionClass.DisplayData data = (list.Count > num) ? list[num] : null;
				xuiC_SkillCraftingInfoEntry.Data = data;
				xuiC_SkillCraftingInfoEntry.IsDirty = true;
				bool flag4 = entryForSkill != null;
				if (flag4)
				{
					xuiC_SkillCraftingInfoEntry.ViewComponent.NavLeftTarget = entryForSkill.ViewComponent;
				}
				num++;
			}
			result = false;
		}
		else
		{
			foreach (XUiC_SkillCraftingInfoEntry xuiC_SkillCraftingInfoEntry2 in levelEntries)
			{
				xuiC_SkillCraftingInfoEntry2.Data = null;
				xuiC_SkillCraftingInfoEntry2.IsDirty = true;
			}
			result = false;
		}
		return result;
	}

	// Token: 0x0600003B RID: 59 RVA: 0x00005DA4 File Offset: 0x00003FA4
	[HarmonyPrefix]
	[HarmonyPatch(typeof(XUiC_SkillCraftingInfoWindow), "Entry_OnPress")]
	public static bool Entry_OnPress_Prefix(XUiC_SkillCraftingInfoWindow __instance, XUiController _sender, int _mouseButton)
	{
		XUi xui = _sender.xui;
		XUiC_SkillCraftingInfoEntry xuiC_SkillCraftingInfoEntry = _sender as XUiC_SkillCraftingInfoEntry;
		object obj;
		if (xuiC_SkillCraftingInfoEntry == null)
		{
			obj = null;
		}
		else
		{
			ProgressionClass.DisplayData data = xuiC_SkillCraftingInfoEntry.Data;
			obj = ((data != null) ? data.GetUnlockItem(0) : null);
		}
		bool flag = obj == null;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			xui.playerUI.windowManager.CloseIfOpen("looting");
			List<XUiC_RecipeList> childrenByType = xui.GetChildrenByType<XUiC_RecipeList>();
			XUiC_RecipeList xuiC_RecipeList = null;
			for (int i = 0; i < childrenByType.Count; i++)
			{
				bool flag2 = childrenByType[i].WindowGroup != null && childrenByType[i].WindowGroup.isShowing;
				if (flag2)
				{
					xuiC_RecipeList = childrenByType[i];
					break;
				}
			}
			bool flag3 = xuiC_RecipeList == null;
			if (flag3)
			{
				XUiC_WindowSelector.OpenSelectorAndWindow(xui.playerUI.entityPlayer, "crafting");
				xuiC_RecipeList = xui.GetChildByType<XUiC_RecipeList>();
			}
			if (xuiC_RecipeList != null)
			{
				xuiC_RecipeList.SetRecipeDataByItem(xuiC_SkillCraftingInfoEntry.Data.GetUnlockItem(0).Id);
			}
			result = false;
		}
		return result;
	}
}
