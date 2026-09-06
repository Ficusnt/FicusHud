using System;
using System.Collections.Generic;

namespace SMXcore
{
	// Token: 0x02000020 RID: 32
	public class XUiC_BookList : XUiController
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00009A88 File Offset: 0x00007C88
		// (set) Token: 0x060000CF RID: 207 RVA: 0x00009AA0 File Offset: 0x00007CA0
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

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00009B04 File Offset: 0x00007D04
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x00009B0C File Offset: 0x00007D0C
		public XUiC_SkillListWindow SkillListWindow { get; set; }

		// Token: 0x060000D2 RID: 210 RVA: 0x00009B18 File Offset: 0x00007D18
		public override void Init()
		{
			base.Init();
			this.bookEntries = base.GetChildrenByType<XUiC_BookSkillEntry>(null);
			foreach (XUiC_BookSkillEntry xuiC_SkillEntry in this.bookEntries)
			{
				xuiC_SkillEntry.OnPress += this.XUiC_SkillEntry_OnPress;
				xuiC_SkillEntry.DisplayType = ProgressionClass.DisplayTypes.Book;
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00009B71 File Offset: 0x00007D71
		public void SelectFirstEntry()
		{
			this.SelectedEntry = this.bookEntries[0];
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00009B84 File Offset: 0x00007D84
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

		// Token: 0x060000D5 RID: 213 RVA: 0x00009BBC File Offset: 0x00007DBC
		internal int GetActiveCount()
		{
			return this.currentBookSkills.Count;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00009BD9 File Offset: 0x00007DD9
		public void RefreshSkillList()
		{
			this.UpdateSkillLists();
			this.RefreshSkillListEntries();
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00009BEC File Offset: 0x00007DEC
		private void UpdateSkillLists()
		{
			this.currentBookSkills.Clear();
			foreach (ProgressionValue progressionValue in this.skills)
			{
				ProgressionClass progressionClass = (progressionValue != null) ? progressionValue.ProgressionClass : null;
				bool flag = progressionClass == null || progressionClass.Name == null || !progressionClass.ValidDisplay(ProgressionClass.DisplayTypes.Book);
				if (!flag)
				{
					bool isBookGroup = progressionClass.IsBookGroup;
					if (isBookGroup)
					{
						this.currentBookSkills.Add(progressionValue);
					}
				}
			}
			this.currentBookSkills.Sort(ProgressionClass.ListSortOrderComparer.Instance);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00009CA4 File Offset: 0x00007EA4
		private void RefreshSkillListEntries()
		{
			XUiView viewComponent = ((XUiC_SkillWindowGroup)base.WindowGroup.Controller).skillBookInfoWindow.GetChildById("0").ViewComponent;
			this.SelectedEntry = null;
			XUiC_SkillEntry[] entries = this.bookEntries;
			this.PopulateSkillEntry(entries, this.currentBookSkills, viewComponent);
			bool flag = this.SelectedEntry == null;
			if (flag)
			{
				this.SelectedEntry = this.bookEntries[0];
				this.SelectedEntry.RefreshBindings(false);
				((XUiC_SkillWindowGroup)base.WindowGroup.Controller).CurrentSkill = this.SelectedEntry.Skill;
			}
			base.RefreshBindings(false);
			this.SkillListWindow.RefreshBindings(false);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00009D58 File Offset: 0x00007F58
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

		// Token: 0x060000DA RID: 218 RVA: 0x00009E78 File Offset: 0x00008078
		public override void OnOpen()
		{
			base.OnOpen();
			this.skills.Clear();
			base.xui.playerUI.entityPlayer.Progression.GetDict().CopyValuesTo(this.skills);
			this.RefreshSkillList();
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00009EC6 File Offset: 0x000080C6
		public override void OnClose()
		{
			base.OnClose();
			this.selectName = "";
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00009EDC File Offset: 0x000080DC
		public XUiC_SkillEntry GetEntryForSkill(ProgressionValue skill)
		{
			XUiC_SkillEntry[] array = new XUiC_SkillEntry[0];
			bool isBookGroup = skill.ProgressionClass.IsBookGroup;
			if (isBookGroup)
			{
				XUiC_SkillEntry[] array2 = this.bookEntries;
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

		// Token: 0x060000DD RID: 221 RVA: 0x00009F48 File Offset: 0x00008148
		public override bool GetBindingValueInternal(ref string value, string bindingName)
		{
			bool result;
			if (!(bindingName == "bookgroupcount"))
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

		// Token: 0x060000DE RID: 222 RVA: 0x00009F8C File Offset: 0x0000818C
		private int GetMaxItemCount()
		{
			int num = 0;
			foreach (ProgressionClass progressionClass in Progression.ProgressionClasses.Values)
			{
				bool flag = progressionClass == null || progressionClass.Name == null || !progressionClass.ValidDisplay(ProgressionClass.DisplayTypes.Book);
				if (!flag)
				{
					bool isBookGroup = progressionClass.IsBookGroup;
					if (isBookGroup)
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x04000085 RID: 133
		private List<ProgressionValue> skills = new List<ProgressionValue>();

		// Token: 0x04000086 RID: 134
		private List<ProgressionValue> currentBookSkills = new List<ProgressionValue>();

		// Token: 0x04000087 RID: 135
		private XUiC_BookSkillEntry[] bookEntries;

		// Token: 0x04000088 RID: 136
		private string selectName;

		// Token: 0x04000089 RID: 137
		private XUiC_SkillEntry selectedEntry;

		// Token: 0x0400008A RID: 138
		private XUiC_TextInput txtInput;
	}
}
