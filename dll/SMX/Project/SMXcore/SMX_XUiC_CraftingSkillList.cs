using System;
using System.Collections.Generic;

namespace SMXcore
{
	// Token: 0x02000021 RID: 33
	public class XUiC_CraftingSkillList : XUiController
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x0000A03C File Offset: 0x0000823C
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x0000A054 File Offset: 0x00008254
		public XUiC_SkillEntry SelectedEntry
		{
			get
			{
				return this.selectedEntry;
			}
			set
			{
				bool flag = this.selectedEntry != null;
				if (flag)
				{
					this.selectedEntry.IsSelected = false;
				}
				this.selectedEntry = value;
				bool flag2 = this.selectedEntry != null;
				if (flag2)
				{
					this.selectedEntry.IsSelected = true;
					base.xui.selectedSkill = this.selectedEntry.Skill;
				}
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x0000A0B8 File Offset: 0x000082B8
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x0000A0C0 File Offset: 0x000082C0
		public XUiC_SkillListWindow SkillListWindow { get; set; }

		// Token: 0x060000E4 RID: 228 RVA: 0x0000A0CC File Offset: 0x000082CC
		public override void Init()
		{
			base.Init();
			this.craftingSkillEntries = base.GetChildrenByType<XUiC_BookSkillEntry>(null);
			foreach (XUiC_BookSkillEntry xuiC_SkillEntry in this.craftingSkillEntries)
			{
				xuiC_SkillEntry.OnPress += this.XUiC_SkillEntry_OnPress;
				xuiC_SkillEntry.DisplayType = ProgressionClass.DisplayTypes.Book;
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0000A125 File Offset: 0x00008325
		public void SelectFirstEntry()
		{
			this.SelectedEntry = this.craftingSkillEntries[0];
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000A138 File Offset: 0x00008338
		private void XUiC_SkillEntry_OnPress(XUiController sender, int _mouseButton)
		{
			XUiC_SkillEntry xuiC_SkillEntry = sender as XUiC_SkillEntry;
			bool flag = xuiC_SkillEntry.Skill != null;
			if (flag)
			{
				this.SelectedEntry = xuiC_SkillEntry;
				this.selectName = "";
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000A170 File Offset: 0x00008370
		internal int GetActiveCount()
		{
			return this.currentCraftingSkills.Count;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0000A18D File Offset: 0x0000838D
		public void RefreshSkillList()
		{
			this.UpdateSkillLists();
			this.RefreshSkillListEntries();
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000A1A0 File Offset: 0x000083A0
		private void UpdateSkillLists()
		{
			this.currentCraftingSkills.Clear();
			foreach (ProgressionValue progressionValue in this.skills)
			{
				ProgressionClass progressionClass = (progressionValue != null) ? progressionValue.ProgressionClass : null;
				bool flag = progressionClass == null || progressionClass.Name == null || !progressionClass.ValidDisplay(ProgressionClass.DisplayTypes.Crafting);
				if (!flag)
				{
					bool isCrafting = progressionClass.IsCrafting;
					if (isCrafting)
					{
						this.currentCraftingSkills.Add(progressionValue);
					}
				}
			}
			this.currentCraftingSkills.Sort(ProgressionClass.ListSortOrderComparer.Instance);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000A258 File Offset: 0x00008458
		private void RefreshSkillListEntries()
		{
			XUiView viewComponent = ((XUiC_SkillWindowGroup)base.WindowGroup.Controller).skillCraftingInfoWindow.GetChildById("0").ViewComponent;
			this.SelectedEntry = null;
			XUiC_SkillEntry[] entries = this.craftingSkillEntries;
			this.PopulateSkillEntry(entries, this.currentCraftingSkills, viewComponent);
			bool flag = this.SelectedEntry == null;
			if (flag)
			{
				this.SelectedEntry = this.craftingSkillEntries[0];
				this.SelectedEntry.RefreshBindings(false);
				((XUiC_SkillWindowGroup)base.WindowGroup.Controller).CurrentSkill = this.SelectedEntry.Skill;
			}
			base.RefreshBindings(false);
			this.SkillListWindow.RefreshBindings(false);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x0000A30C File Offset: 0x0000850C
		private void PopulateSkillEntry(XUiC_SkillEntry[] entries, List<ProgressionValue> progressionValues, XUiView navRightTarget)
		{
			for (int i = 0; i < entries.Length; i++)
			{
				XUiC_SkillEntry xuiC_SkillEntry = entries[i];
				bool flag = i < progressionValues.Count && progressionValues[i] != null && Progression.ProgressionClasses.ContainsKey(progressionValues[i].Name);
				if (flag)
				{
					ProgressionValue progressionValue = progressionValues[i];
					xuiC_SkillEntry.Skill = progressionValue;
					bool flag2 = !string.IsNullOrEmpty(this.selectName) && progressionValue.ProgressionClass.Name == this.selectName;
					if (flag2)
					{
						this.SelectedEntry = xuiC_SkillEntry;
						((XUiC_SkillWindowGroup)base.WindowGroup.Controller).CurrentSkill = this.SelectedEntry.Skill;
					}
					else
					{
						xuiC_SkillEntry.IsSelected = false;
					}
					xuiC_SkillEntry.ViewComponent.Enabled = true;
					xuiC_SkillEntry.ViewComponent.NavRightTarget = navRightTarget;
					xuiC_SkillEntry.RefreshBindings(false);
				}
				else
				{
					xuiC_SkillEntry.Skill = null;
					xuiC_SkillEntry.IsSelected = false;
					xuiC_SkillEntry.ViewComponent.Enabled = false;
					xuiC_SkillEntry.RefreshBindings(false);
				}
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x0000A42C File Offset: 0x0000862C
		public override void OnOpen()
		{
			base.OnOpen();
			this.skills.Clear();
			base.xui.playerUI.entityPlayer.Progression.GetDict().CopyValuesTo(this.skills);
			this.RefreshSkillList();
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000A47A File Offset: 0x0000867A
		public override void OnClose()
		{
			base.OnClose();
			this.selectName = "";
		}

		// Token: 0x060000EE RID: 238 RVA: 0x0000A490 File Offset: 0x00008690
		public XUiC_SkillEntry GetEntryForSkill(ProgressionValue skill)
		{
			XUiC_SkillEntry[] array = new XUiC_SkillEntry[0];
			bool isCrafting = skill.ProgressionClass.IsCrafting;
			if (isCrafting)
			{
				XUiC_SkillEntry[] array2 = this.craftingSkillEntries;
				array = array2;
			}
			foreach (XUiC_SkillEntry xuiC_SkillEntry in array)
			{
				bool flag = xuiC_SkillEntry.Skill == skill;
				if (flag)
				{
					return xuiC_SkillEntry;
				}
			}
			return null;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000A4FC File Offset: 0x000086FC
		public override bool GetBindingValueInternal(ref string value, string bindingName)
		{
			bool result;
			if (!(bindingName == "craftingskillcount"))
			{
				result = base.GetBindingValueInternal(ref value, bindingName);
			}
			else
			{
				value = this.GetMaxItemCount().ToString();
				result = true;
			}
			return result;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x0000A540 File Offset: 0x00008740
		private int GetMaxItemCount()
		{
			int num = 0;
			foreach (ProgressionClass progressionClass in Progression.ProgressionClasses.Values)
			{
				bool flag = progressionClass == null || progressionClass.Name == null || !progressionClass.ValidDisplay(ProgressionClass.DisplayTypes.Crafting);
				if (!flag)
				{
					bool isCrafting = progressionClass.IsCrafting;
					if (isCrafting)
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x0400008C RID: 140
		private List<ProgressionValue> skills = new List<ProgressionValue>();

		// Token: 0x0400008D RID: 141
		private List<ProgressionValue> currentCraftingSkills = new List<ProgressionValue>();

		// Token: 0x0400008E RID: 142
		private XUiC_BookSkillEntry[] craftingSkillEntries;

		// Token: 0x0400008F RID: 143
		private string selectName;

		// Token: 0x04000090 RID: 144
		private XUiC_SkillEntry selectedEntry;

		// Token: 0x04000091 RID: 145
		private XUiC_TextInput txtInput;
	}
}
