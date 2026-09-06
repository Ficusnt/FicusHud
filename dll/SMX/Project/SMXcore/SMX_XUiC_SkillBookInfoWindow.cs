using System;
using System.Collections.Generic;
using System.Globalization;

namespace SMXcore
{
	// Token: 0x02000019 RID: 25
	public class XUiC_SkillBookInfoWindow : XUiC_InfoWindow
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000094 RID: 148 RVA: 0x000077B8 File Offset: 0x000059B8
		public ProgressionValue CurrentSkill
		{
			[PublicizedFrom(EAccessModifier.Private)]
			get
			{
				bool flag = base.xui.selectedSkill == null || !base.xui.selectedSkill.ProgressionClass.IsBookGroup;
				ProgressionValue result;
				if (flag)
				{
					result = null;
				}
				else
				{
					result = base.xui.selectedSkill;
				}
				return result;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00007808 File Offset: 0x00005A08
		// (set) Token: 0x06000096 RID: 150 RVA: 0x00007820 File Offset: 0x00005A20
		public ProgressionValue HoveredPerk
		{
			get
			{
				return this.hoveredPerk;
			}
			set
			{
				bool flag = this.hoveredPerk != value;
				if (flag)
				{
					this.hoveredPerk = value;
					base.RefreshBindings(false);
				}
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00007850 File Offset: 0x00005A50
		public override void Init()
		{
			base.Init();
			base.GetChildrenByType<XUiC_SkillBookLevel>(this.perkEntries);
			int num = 1;
			foreach (XUiC_SkillBookLevel xuiC_SkillBookLevel in this.perkEntries)
			{
				xuiC_SkillBookLevel.ListIndex = num - 1;
				xuiC_SkillBookLevel.HiddenEntriesWithPaging = this.hiddenEntriesWithPaging;
				xuiC_SkillBookLevel.MaxEntriesWithoutPaging = this.perkEntries.Count;
				xuiC_SkillBookLevel.OnScroll += this.Entry_OnScroll;
			}
			this.actionItemList = base.GetChildByType<XUiC_ItemActionList>();
			this.skillsPerPage = this.perkEntries.Count - this.hiddenEntriesWithPaging;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00007918 File Offset: 0x00005B18
		public void SkillChanged()
		{
			this.IsDirty = true;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00007924 File Offset: 0x00005B24
		public void UpdateSkill()
		{
			bool flag = this.CurrentSkill != null && this.actionItemList != null;
			if (flag)
			{
				this.actionItemList.SetCraftingActionList(XUiC_ItemActionList.ItemActionListTypes.Skill, this);
			}
			bool flag2 = this.CurrentSkill != null;
			if (flag2)
			{
				base.xui.playerUI.entityPlayer.Progression.GetPerkList(this.perkList, this.CurrentSkill.Name);
			}
			XUiC_SkillEntry entryForSkill = this.windowGroup.Controller.GetChildByType<XUiC_SkillListWindow>().GetEntryForSkill(this.CurrentSkill);
			int num = 0;
			foreach (XUiC_SkillBookLevel xuiC_SkillBookLevel in this.perkEntries)
			{
				bool flag3 = num < this.perkList.Count;
				if (flag3)
				{
					xuiC_SkillBookLevel.Perk = this.perkList[num];
					xuiC_SkillBookLevel.Volume = num + 1;
					xuiC_SkillBookLevel.OnHover += this.Entry_OnHover;
					xuiC_SkillBookLevel.CompletionReward = (num == this.perkList.Count - 1);
					bool flag4 = entryForSkill != null;
					if (flag4)
					{
						xuiC_SkillBookLevel.ViewComponent.NavLeftTarget = entryForSkill.ViewComponent;
					}
				}
				else
				{
					xuiC_SkillBookLevel.Perk = null;
					xuiC_SkillBookLevel.Volume = -1;
					xuiC_SkillBookLevel.OnHover -= this.Entry_OnHover;
					xuiC_SkillBookLevel.CompletionReward = false;
				}
				num++;
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00007AB8 File Offset: 0x00005CB8
		public void Entry_OnHover(XUiController _sender, bool _isOver)
		{
			XUiC_SkillBookLevel xuiC_SkillBookLevel = _sender as XUiC_SkillBookLevel;
			bool flag = _isOver && xuiC_SkillBookLevel != null;
			if (flag)
			{
				this.HoveredPerk = xuiC_SkillBookLevel.Perk;
			}
			else
			{
				this.HoveredPerk = null;
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00007AF6 File Offset: 0x00005CF6
		public void Pager_OnPageChanged()
		{
			this.IsDirty = true;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00007B00 File Offset: 0x00005D00
		public void Entry_OnScroll(XUiController _sender, float _delta)
		{
			bool flag = _delta > 0f;
			if (flag)
			{
				XUiC_Paging xuiC_Paging = this.pager;
				if (xuiC_Paging != null)
				{
					xuiC_Paging.PageDown();
				}
			}
			else
			{
				XUiC_Paging xuiC_Paging2 = this.pager;
				if (xuiC_Paging2 != null)
				{
					xuiC_Paging2.PageUp();
				}
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00007B44 File Offset: 0x00005D44
		public override void OnOpen()
		{
			base.OnOpen();
			bool flag = this.actionItemList != null;
			if (flag)
			{
				this.actionItemList.SetCraftingActionList(XUiC_ItemActionList.ItemActionListTypes.Skill, this);
			}
			XUiEventManager.Instance.OnSkillExperienceAdded += this.Current_OnSkillExperienceAdded;
			this.IsDirty = true;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00007B94 File Offset: 0x00005D94
		public override void OnClose()
		{
			base.OnClose();
			XUiEventManager.Instance.OnSkillExperienceAdded -= this.Current_OnSkillExperienceAdded;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00007BB8 File Offset: 0x00005DB8
		public override void Update(float _dt)
		{
			bool isDirty = this.IsDirty;
			if (isDirty)
			{
				this.IsDirty = false;
				this.UpdateSkill();
				base.RefreshBindings(this.IsDirty);
			}
			base.Update(_dt);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00007BF8 File Offset: 0x00005DF8
		public void Current_OnSkillExperienceAdded(ProgressionValue _changedSkill, int _newXp)
		{
			bool flag = this.CurrentSkill == _changedSkill;
			if (flag)
			{
				this.IsDirty = true;
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00007C1C File Offset: 0x00005E1C
		public override bool ParseAttribute(string _name, string _value, XUiController _parent)
		{
			bool flag = _name == "hidden_entries_with_paging";
			bool result;
			if (flag)
			{
				this.hiddenEntriesWithPaging = StringParsers.ParseSInt32(_value, 0, -1, NumberStyles.Integer);
				foreach (XUiC_SkillBookLevel xuiC_SkillBookLevel in this.perkEntries)
				{
					bool flag2 = xuiC_SkillBookLevel != null;
					if (flag2)
					{
						xuiC_SkillBookLevel.HiddenEntriesWithPaging = this.hiddenEntriesWithPaging;
					}
				}
				this.skillsPerPage = this.perkEntries.Count - this.hiddenEntriesWithPaging;
				result = true;
			}
			else
			{
				result = base.ParseAttribute(_name, _value, _parent);
			}
			return result;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00007CD0 File Offset: 0x00005ED0
		public override bool GetBindingValueInternal(ref string value, string bindingName)
		{
			EntityPlayerLocal entityPlayer = base.xui.playerUI.entityPlayer;
			uint num = global::<PrivateImplementationDetails>.ComputeStringHash(bindingName);
			if (num <= 2606420134U)
			{
				if (num <= 1275709072U)
				{
					if (num != 443815844U)
					{
						if (num == 1275709072U)
						{
							if (bindingName == "maxSkillLevel")
							{
								value = ((this.CurrentSkill != null) ? this.maxSkillLevelFormatter.Format((float)ProgressionClass.GetCalculatedMaxLevel(entityPlayer, this.CurrentSkill)) : "0");
								return true;
							}
						}
					}
					else if (bindingName == "skillLevel")
					{
						value = ((this.CurrentSkill != null) ? this.skillLevelFormatter.Format(this.CurrentSkill.GetCalculatedLevel(entityPlayer)) : "0");
						return true;
					}
				}
				else if (num != 1283949528U)
				{
					if (num == 2606420134U)
					{
						if (bindingName == "groupdescription")
						{
							bool flag = this.CurrentSkill != null;
							if (flag)
							{
								value = Localization.Get(this.CurrentSkill.ProgressionClass.DescKey, false);
							}
							else
							{
								value = "";
							}
							return true;
						}
					}
				}
				else if (bindingName == "currentlevel")
				{
					value = Localization.Get("xuiSkillLevel", false);
					return true;
				}
			}
			else if (num <= 3504806855U)
			{
				if (num != 3268933568U)
				{
					if (num == 3504806855U)
					{
						if (bindingName == "groupname")
						{
							value = ((this.CurrentSkill != null) ? Localization.Get(this.CurrentSkill.ProgressionClass.NameKey, false) : "Skill Info");
							return true;
						}
					}
				}
				else if (bindingName == "showPaging")
				{
					value = "false";
					return true;
				}
			}
			else if (num != 4010384093U)
			{
				if (num == 4294521801U)
				{
					if (bindingName == "detailsdescription")
					{
						bool flag2 = this.CurrentSkill != null;
						if (flag2)
						{
							bool flag3 = this.hoveredPerk != null;
							if (flag3)
							{
								bool flag4 = string.IsNullOrEmpty(this.hoveredPerk.ProgressionClass.LongDescKey);
								if (flag4)
								{
									value = Localization.Get(this.hoveredPerk.ProgressionClass.DescKey, false);
								}
								else
								{
									value = Localization.Get(this.hoveredPerk.ProgressionClass.LongDescKey, false);
								}
							}
							else
							{
								value = Localization.Get(this.CurrentSkill.ProgressionClass.LongDescKey, false);
							}
						}
						else
						{
							value = "";
						}
						return true;
					}
				}
			}
			else if (bindingName == "groupicon")
			{
				value = ((this.CurrentSkill != null) ? this.CurrentSkill.ProgressionClass.Icon : "ui_game_symbol_skills");
				return true;
			}
			return base.GetBindingValueInternal(ref value, bindingName);
		}

		// Token: 0x04000062 RID: 98
		public XUiC_ItemActionList actionItemList;

		// Token: 0x04000063 RID: 99
		public int hiddenEntriesWithPaging = 1;

		// Token: 0x04000064 RID: 100
		public readonly List<XUiC_SkillBookLevel> perkEntries = new List<XUiC_SkillBookLevel>();

		// Token: 0x04000065 RID: 101
		public XUiC_Paging pager;

		// Token: 0x04000066 RID: 102
		public int skillsPerPage;

		// Token: 0x04000067 RID: 103
		public List<ProgressionValue> perkList = new List<ProgressionValue>();

		// Token: 0x04000068 RID: 104
		public ProgressionValue hoveredPerk;

		// Token: 0x04000069 RID: 105
		public readonly CachedStringFormatterFloat skillLevelFormatter = new CachedStringFormatterFloat(null);

		// Token: 0x0400006A RID: 106
		public readonly CachedStringFormatterFloat maxSkillLevelFormatter = new CachedStringFormatterFloat(null);

		// Token: 0x0400006B RID: 107
		public readonly CachedStringFormatter<int> buyCostFormatter = new CachedStringFormatter<int>((int _i) => _i.ToString() + " " + Localization.Get("xuiSkillPoints", false));

		// Token: 0x0400006C RID: 108
		public readonly CachedStringFormatter<int> expCostFormatter = new CachedStringFormatter<int>((int _i) => _i.ToString() + " " + Localization.Get("RewardExp_keyword", false));
	}
}
