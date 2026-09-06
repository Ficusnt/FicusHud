using System;
using System.Collections.Generic;
using System.Globalization;
using Platform;

namespace SMXcore
{
	// Token: 0x02000018 RID: 24
	public class XUiC_SkillAttributeInfoWindow : XUiC_InfoWindow
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00006B8C File Offset: 0x00004D8C
		public ProgressionValue CurrentSkill
		{
			get
			{
				bool flag = base.xui.selectedSkill == null || !base.xui.selectedSkill.ProgressionClass.IsAttribute;
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

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00006BDC File Offset: 0x00004DDC
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00006BF4 File Offset: 0x00004DF4
		public int HoveredLevel
		{
			get
			{
				return this.hoveredLevel;
			}
			set
			{
				bool flag = this.hoveredLevel != value;
				if (flag)
				{
					this.hoveredLevel = value;
					base.RefreshBindings(false);
				}
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00006C24 File Offset: 0x00004E24
		public override void Init()
		{
			base.Init();
			base.GetChildrenByType<XUiC_SkillAttributeLevel>(this.levelEntries);
			int num = 1;
			foreach (XUiC_SkillAttributeLevel xuiC_SkillAttributeLevel in this.levelEntries)
			{
				xuiC_SkillAttributeLevel.ListIndex = num - 1;
				xuiC_SkillAttributeLevel.Level = num++;
				xuiC_SkillAttributeLevel.HiddenEntriesWithPaging = this.hiddenEntriesWithPaging;
				xuiC_SkillAttributeLevel.MaxEntriesWithoutPaging = this.levelEntries.Count;
				xuiC_SkillAttributeLevel.OnScroll += this.Entry_OnScroll;
				xuiC_SkillAttributeLevel.OnHover += this.Entry_OnHover;
				xuiC_SkillAttributeLevel.btnBuy.Controller.OnHover += this.Entry_OnHover;
			}
			this.actionItemList = base.GetChildByType<XUiC_ItemActionList>();
			this.skillsPerPage = this.levelEntries.Count - this.hiddenEntriesWithPaging;
			this.pager = base.GetChildByType<XUiC_Paging>();
			bool flag = this.pager != null;
			if (flag)
			{
				this.pager.OnPageChanged += this.Pager_OnPageChanged;
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00006D60 File Offset: 0x00004F60
		public void Entry_OnHover(XUiController _sender, bool _isOver)
		{
			XUiC_SkillAttributeLevel xuiC_SkillAttributeLevel = _sender as XUiC_SkillAttributeLevel;
			bool flag = xuiC_SkillAttributeLevel == null;
			if (flag)
			{
				xuiC_SkillAttributeLevel = (_sender.Parent as XUiC_SkillAttributeLevel);
			}
			bool flag2 = _isOver && xuiC_SkillAttributeLevel != null;
			if (flag2)
			{
				this.HoveredLevel = xuiC_SkillAttributeLevel.Level;
			}
			else
			{
				this.HoveredLevel = -1;
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00006DB4 File Offset: 0x00004FB4
		public void SkillChanged()
		{
			XUiC_Paging xuiC_Paging = this.pager;
			bool flag = xuiC_Paging != null;
			if (flag)
			{
				xuiC_Paging.SetLastPageByElementsAndPageLength((this.CurrentSkill != null && this.CurrentSkill.ProgressionClass.MaxLevel > this.levelEntries.Count) ? (this.CurrentSkill.ProgressionClass.MaxLevel - 1) : 0, this.skillsPerPage);
			}
			XUiC_Paging xuiC_Paging2 = this.pager;
			bool flag2 = xuiC_Paging2 != null;
			if (flag2)
			{
				xuiC_Paging2.Reset();
			}
			this.IsDirty = true;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00006E3C File Offset: 0x0000503C
		public void UpdateSkill()
		{
			bool flag = this.CurrentSkill != null && this.actionItemList != null;
			if (flag)
			{
				this.actionItemList.SetCraftingActionList(XUiC_ItemActionList.ItemActionListTypes.Skill, this);
			}
			XUiC_SkillEntry entryForSkill = this.windowGroup.Controller.GetChildByType<XUiC_SkillListWindow>().GetEntryForSkill(this.CurrentSkill);
			XUiC_Paging xuiC_Paging = this.pager;
			int num = ((xuiC_Paging != null) ? xuiC_Paging.GetPage() : 0) * this.skillsPerPage + 1;
			foreach (XUiC_SkillAttributeLevel xuiC_SkillAttributeLevel in this.levelEntries)
			{
				xuiC_SkillAttributeLevel.Level = num++;
				xuiC_SkillAttributeLevel.IsDirty = true;
				bool flag2 = entryForSkill != null;
				if (flag2)
				{
					xuiC_SkillAttributeLevel.btnBuy.NavLeftTarget = entryForSkill.ViewComponent;
				}
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00006F24 File Offset: 0x00005124
		public void Pager_OnPageChanged()
		{
			this.IsDirty = true;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00006F30 File Offset: 0x00005130
		public void Entry_OnScroll(XUiController _sender, float _delta)
		{
			bool flag = _delta > 0f;
			if (flag)
			{
				XUiC_Paging xuiC_Paging = this.pager;
				bool flag2 = xuiC_Paging == null;
				if (!flag2)
				{
					xuiC_Paging.PageDown();
				}
			}
			else
			{
				XUiC_Paging xuiC_Paging2 = this.pager;
				bool flag3 = xuiC_Paging2 == null;
				if (!flag3)
				{
					xuiC_Paging2.PageUp();
				}
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00006F84 File Offset: 0x00005184
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

		// Token: 0x0600008E RID: 142 RVA: 0x00006FD4 File Offset: 0x000051D4
		public override void OnClose()
		{
			base.OnClose();
			XUiEventManager.Instance.OnSkillExperienceAdded -= this.Current_OnSkillExperienceAdded;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00006FF8 File Offset: 0x000051F8
		public override void Update(float _dt)
		{
			bool isDirty = this.IsDirty;
			if (isDirty)
			{
				this.IsDirty = false;
				this.UpdateSkill();
				base.RefreshBindings(this.IsDirty);
			}
			bool flag = base.ViewComponent.UiTransform.gameObject.activeInHierarchy && this.CurrentSkill != null && !base.xui.playerUI.windowManager.IsInputActive() && ((PlatformManager.NativePlatform.Input.CurrentInputStyle != PlayerInputManager.InputStyle.Keyboard && base.xui.playerUI.playerInput.GUIActions.Inspect.WasPressed) || (PlatformManager.NativePlatform.Input.CurrentInputStyle == PlayerInputManager.InputStyle.Keyboard && base.xui.playerUI.playerInput.GUIActions.DPad_Up.WasPressed));
			if (flag)
			{
				foreach (XUiC_SkillAttributeLevel xuiC_SkillAttributeLevel in this.levelEntries)
				{
					bool flag2 = xuiC_SkillAttributeLevel.CurrentSkill != null && xuiC_SkillAttributeLevel.Level == this.CurrentSkill.Level + 1;
					if (flag2)
					{
						xuiC_SkillAttributeLevel.btnBuy.Controller.Pressed(-1);
						break;
					}
				}
			}
			base.Update(_dt);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00007164 File Offset: 0x00005364
		public void Current_OnSkillExperienceAdded(ProgressionValue _changedSkill, int _newXp)
		{
			bool flag = this.CurrentSkill == _changedSkill;
			if (flag)
			{
				this.IsDirty = true;
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00007188 File Offset: 0x00005388
		public override bool ParseAttribute(string _name, string _value, XUiController _parent)
		{
			bool flag = _name == "hidden_entries_with_paging";
			bool result;
			if (flag)
			{
				this.hiddenEntriesWithPaging = StringParsers.ParseSInt32(_value, 0, -1, NumberStyles.Integer);
				foreach (XUiC_SkillAttributeLevel xuiC_SkillAttributeLevel in this.levelEntries)
				{
					bool flag2 = xuiC_SkillAttributeLevel != null;
					if (flag2)
					{
						xuiC_SkillAttributeLevel.HiddenEntriesWithPaging = this.hiddenEntriesWithPaging;
					}
				}
				this.skillsPerPage = this.levelEntries.Count - this.hiddenEntriesWithPaging;
				result = true;
			}
			else
			{
				result = base.ParseAttribute(_name, _value, _parent);
			}
			return result;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0000723C File Offset: 0x0000543C
		public override bool GetBindingValueInternal(ref string value, string bindingName)
		{
			EntityPlayerLocal entityPlayer = base.xui.playerUI.entityPlayer;
			uint num = global::<PrivateImplementationDetails>.ComputeStringHash(bindingName);
			if (num <= 1912580562U)
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
					if (num == 1912580562U)
					{
						if (bindingName == "buycost")
						{
							value = "-- PTS";
							bool flag = this.CurrentSkill != null && this.CurrentSkill.Level < this.CurrentSkill.ProgressionClass.MaxLevel;
							if (flag)
							{
								bool flag2 = this.CurrentSkill.ProgressionClass.CurrencyType == ProgressionCurrencyType.SP;
								if (flag2)
								{
									value = this.buyCostFormatter.Format(this.CurrentSkill.ProgressionClass.CalculatedCostForLevel(this.CurrentSkill.Level + 1));
								}
								else
								{
									value = this.expCostFormatter.Format((int)((1f - this.CurrentSkill.PercToNextLevel) * (float)this.CurrentSkill.ProgressionClass.CalculatedCostForLevel(this.CurrentSkill.Level + 1)));
								}
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
			else if (num <= 3268933568U)
			{
				if (num != 2606420134U)
				{
					if (num == 3268933568U)
					{
						if (bindingName == "showPaging")
						{
							value = (this.CurrentSkill != null && this.CurrentSkill.ProgressionClass.MaxLevel > this.levelEntries.Count).ToString();
							return true;
						}
					}
				}
				else if (bindingName == "groupdescription")
				{
					value = ((this.CurrentSkill != null) ? Localization.Get(this.CurrentSkill.ProgressionClass.DescKey, false) : "");
					return true;
				}
			}
			else if (num != 3504806855U)
			{
				if (num != 4010384093U)
				{
					if (num == 4294521801U)
					{
						if (bindingName == "detailsdescription")
						{
							value = "";
							bool flag3 = this.CurrentSkill != null && this.hoveredLevel != -1;
							if (flag3)
							{
								using (List<MinEffectGroup>.Enumerator enumerator = this.CurrentSkill.ProgressionClass.Effects.EffectGroups.GetEnumerator())
								{
									while (enumerator.MoveNext())
									{
										MinEffectGroup minEffectGroup = enumerator.Current;
										bool flag4 = minEffectGroup.EffectDescriptions != null;
										if (flag4)
										{
											for (int i = 0; i < minEffectGroup.EffectDescriptions.Count; i++)
											{
												bool flag5 = this.hoveredLevel >= minEffectGroup.EffectDescriptions[i].MinLevel && this.hoveredLevel <= minEffectGroup.EffectDescriptions[i].MaxLevel;
												if (flag5)
												{
													value = ((!string.IsNullOrEmpty(minEffectGroup.EffectDescriptions[i].LongDescription)) ? minEffectGroup.EffectDescriptions[i].LongDescription : minEffectGroup.EffectDescriptions[i].Description);
													return true;
												}
											}
										}
									}
									return true;
								}
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
			}
			else if (bindingName == "groupname")
			{
				value = ((this.CurrentSkill != null) ? Localization.Get(this.CurrentSkill.ProgressionClass.NameKey, false) : "Skill Info");
				return true;
			}
			return base.GetBindingValueInternal(ref value, bindingName);
		}

		// Token: 0x04000058 RID: 88
		public XUiC_ItemActionList actionItemList;

		// Token: 0x04000059 RID: 89
		public int hiddenEntriesWithPaging = 2;

		// Token: 0x0400005A RID: 90
		public readonly List<XUiC_SkillAttributeLevel> levelEntries = new List<XUiC_SkillAttributeLevel>();

		// Token: 0x0400005B RID: 91
		public XUiC_Paging pager;

		// Token: 0x0400005C RID: 92
		public int skillsPerPage;

		// Token: 0x0400005D RID: 93
		public int hoveredLevel = -1;

		// Token: 0x0400005E RID: 94
		public readonly CachedStringFormatterFloat skillLevelFormatter = new CachedStringFormatterFloat(null);

		// Token: 0x0400005F RID: 95
		public readonly CachedStringFormatterFloat maxSkillLevelFormatter = new CachedStringFormatterFloat(null);

		// Token: 0x04000060 RID: 96
		public readonly CachedStringFormatter<int> buyCostFormatter = new CachedStringFormatter<int>((int _i) => _i.ToString() + " " + Localization.Get("xuiSkillPoints", false));

		// Token: 0x04000061 RID: 97
		public readonly CachedStringFormatter<int> expCostFormatter = new CachedStringFormatter<int>((int _i) => _i.ToString() + " " + Localization.Get("RewardExp_keyword", false));
	}
}
