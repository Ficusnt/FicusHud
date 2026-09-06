using System;
using System.Collections.Generic;

namespace SMXcore
{
	// Token: 0x02000022 RID: 34
	public class XUiC_SkillList : XUiController
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x0000A5F0 File Offset: 0x000087F0
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x0000A608 File Offset: 0x00008808
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

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x0000A66C File Offset: 0x0000886C
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x0000A674 File Offset: 0x00008874
		public XUiC_SkillListWindow SkillListWindow { get; set; }

		// Token: 0x060000F6 RID: 246 RVA: 0x0000A680 File Offset: 0x00008880
		public override void Init()
		{
			base.Init();
			this.attributeEntries = base.GetChildrenByType<XUiC_AttributeSkillEntry>(null);
			foreach (XUiC_AttributeSkillEntry xuiC_SkillEntry in this.attributeEntries)
			{
				xuiC_SkillEntry.OnPress += this.XUiC_SkillEntry_OnPress;
				xuiC_SkillEntry.DisplayType = ProgressionClass.DisplayTypes.Standard;
			}
			this.perkEntries = base.GetChildrenByType<XUiC_PerkSkillEntry>(null);
			foreach (XUiC_PerkSkillEntry xuiC_SkillEntry2 in this.perkEntries)
			{
				xuiC_SkillEntry2.OnPress += this.XUiC_SkillEntry_OnPress;
				xuiC_SkillEntry2.DisplayType = ProgressionClass.DisplayTypes.Standard;
			}
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x0000A725 File Offset: 0x00008925
		public void SelectFirstEntry()
		{
			this.SelectedEntry = this.attributeEntries[0];
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x0000A738 File Offset: 0x00008938
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

		// Token: 0x060000F9 RID: 249 RVA: 0x0000A770 File Offset: 0x00008970
		internal int GetActiveCount()
		{
			return this.currentAttributeSkills.Count + this.currentPerkSkills.Count;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000A799 File Offset: 0x00008999
		public void RefreshSkillList()
		{
			this.UpdateSkillLists();
			this.RefreshSkillListEntries();
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000A7AC File Offset: 0x000089AC
		private void UpdateSkillLists()
		{
			this.currentAttributeSkills.Clear();
			this.currentPerkSkills.Clear();
			foreach (ProgressionValue progressionValue in this.skills)
			{
				ProgressionClass progressionClass = (progressionValue != null) ? progressionValue.ProgressionClass : null;
				bool flag = progressionClass == null || progressionClass.Name == null || !progressionClass.ValidDisplay(ProgressionClass.DisplayTypes.Standard);
				if (!flag)
				{
					bool isPerk = progressionClass.IsPerk;
					if (isPerk)
					{
						this.currentPerkSkills.Add(progressionValue);
					}
					else
					{
						bool flag2 = progressionClass.IsAttribute && !progressionClass.Name.Equals("attbooks") && !progressionClass.Name.Equals("attcrafting");
						if (flag2)
						{
							this.currentAttributeSkills.Add(progressionValue);
						}
					}
				}
			}
			this.currentAttributeSkills.Sort(ProgressionClass.ListSortOrderComparer.Instance);
			this.currentPerkSkills.Sort(ProgressionClass.ListSortOrderComparer.Instance);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000A8CC File Offset: 0x00008ACC
		private void RefreshSkillListEntries()
		{
			XUiView viewComponent = ((XUiC_SkillWindowGroup)base.WindowGroup.Controller).skillAttributeInfoWindow.GetChildById("0").ViewComponent;
			XUiView viewComponent2 = ((XUiC_SkillWindowGroup)base.WindowGroup.Controller).skillPerkInfoWindow.GetChildById("0").ViewComponent;
			this.SelectedEntry = null;
			XUiC_SkillEntry[] entries = this.attributeEntries;
			this.PopulateSkillEntry(entries, this.currentAttributeSkills, viewComponent);
			entries = this.perkEntries;
			this.PopulateSkillEntry(entries, this.currentPerkSkills, viewComponent2);
			bool flag = this.SelectedEntry == null;
			if (flag)
			{
				this.SelectedEntry = this.attributeEntries[0];
				this.SelectedEntry.RefreshBindings(false);
				((XUiC_SkillWindowGroup)base.WindowGroup.Controller).CurrentSkill = this.SelectedEntry.Skill;
			}
			base.RefreshBindings(false);
			this.SkillListWindow.RefreshBindings(false);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000A9BC File Offset: 0x00008BBC
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
					bool flag2 = !string.IsNullOrEmpty(this.selectName) && progressionValue.ProgressionClass.Name.Equals(this.selectName);
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

		// Token: 0x060000FE RID: 254 RVA: 0x0000AADC File Offset: 0x00008CDC
		public override void OnOpen()
		{
			base.OnOpen();
			this.skills.Clear();
			base.xui.playerUI.entityPlayer.Progression.GetDict().CopyValuesTo(this.skills);
			this.RefreshSkillList();
		}

		// Token: 0x060000FF RID: 255 RVA: 0x0000AB2A File Offset: 0x00008D2A
		public override void OnClose()
		{
			base.OnClose();
			this.selectName = "";
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0000AB40 File Offset: 0x00008D40
		public XUiC_SkillEntry GetEntryForSkill(ProgressionValue skill)
		{
			XUiC_SkillEntry[] array = new XUiC_SkillEntry[0];
			bool isAttribute = skill.ProgressionClass.IsAttribute;
			if (isAttribute)
			{
				XUiC_SkillEntry[] array2 = this.attributeEntries;
				array = array2;
			}
			else
			{
				bool isPerk = skill.ProgressionClass.IsPerk;
				if (isPerk)
				{
					XUiC_SkillEntry[] array2 = this.perkEntries;
					array = array2;
				}
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

		// Token: 0x04000093 RID: 147
		private List<ProgressionValue> skills = new List<ProgressionValue>();

		// Token: 0x04000094 RID: 148
		private List<ProgressionValue> currentAttributeSkills = new List<ProgressionValue>();

		// Token: 0x04000095 RID: 149
		private List<ProgressionValue> currentPerkSkills = new List<ProgressionValue>();

		// Token: 0x04000096 RID: 150
		private XUiC_PerkSkillEntry[] perkEntries;

		// Token: 0x04000097 RID: 151
		private XUiC_AttributeSkillEntry[] attributeEntries;

		// Token: 0x04000098 RID: 152
		private string selectName;

		// Token: 0x04000099 RID: 153
		private XUiC_SkillEntry selectedEntry;

		// Token: 0x0400009A RID: 154
		private XUiC_TextInput txtInput;
	}
}
