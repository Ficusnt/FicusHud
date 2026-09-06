using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Platform;

namespace SMXcore
{
	// Token: 0x0200001B RID: 27
	public class XUiC_SkillPerkInfoWindow : XUiC_InfoWindow
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00008D90 File Offset: 0x00006F90
		public ProgressionValue CurrentSkill
		{
			get
			{
				bool flag = base.xui.selectedSkill == null || !base.xui.selectedSkill.ProgressionClass.IsPerk;
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

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00008DE0 File Offset: 0x00006FE0
		// (set) Token: 0x060000BC RID: 188 RVA: 0x00008DF8 File Offset: 0x00006FF8
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

		// Token: 0x060000BD RID: 189 RVA: 0x00008E28 File Offset: 0x00007028
		public override void Init()
		{
			base.Init();
			base.GetChildrenByType<XUiC_SkillPerkLevel>(this.levelEntries);
			int num = 1;
			foreach (XUiC_SkillPerkLevel xuiC_SkillPerkLevel in this.levelEntries)
			{
				xuiC_SkillPerkLevel.ListIndex = num - 1;
				xuiC_SkillPerkLevel.Level = num++;
				xuiC_SkillPerkLevel.HiddenEntriesWithPaging = this.hiddenEntriesWithPaging;
				xuiC_SkillPerkLevel.MaxEntriesWithoutPaging = this.levelEntries.Count;
				xuiC_SkillPerkLevel.OnScroll += this.Entry_OnScroll;
				xuiC_SkillPerkLevel.OnHover += this.Entry_OnHover;
				xuiC_SkillPerkLevel.btnBuy.Controller.OnHover += this.Entry_OnHover;
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

		// Token: 0x060000BE RID: 190 RVA: 0x00008F64 File Offset: 0x00007164
		public void Entry_OnHover(XUiController _sender, bool _isOver)
		{
			XUiC_SkillPerkLevel xuiC_SkillPerkLevel = _sender as XUiC_SkillPerkLevel;
			bool flag = xuiC_SkillPerkLevel == null;
			if (flag)
			{
				xuiC_SkillPerkLevel = (_sender.Parent as XUiC_SkillPerkLevel);
			}
			bool flag2 = _isOver && xuiC_SkillPerkLevel != null;
			if (flag2)
			{
				this.HoveredLevel = xuiC_SkillPerkLevel.Level;
			}
			else
			{
				this.HoveredLevel = -1;
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00008FB8 File Offset: 0x000071B8
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

		// Token: 0x060000C0 RID: 192 RVA: 0x00009040 File Offset: 0x00007240
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
			foreach (XUiC_SkillPerkLevel xuiC_SkillPerkLevel in this.levelEntries)
			{
				xuiC_SkillPerkLevel.Level = num++;
				xuiC_SkillPerkLevel.IsDirty = true;
				bool flag2 = entryForSkill != null;
				if (flag2)
				{
					xuiC_SkillPerkLevel.btnBuy.NavLeftTarget = entryForSkill.ViewComponent;
				}
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00009128 File Offset: 0x00007328
		public void Pager_OnPageChanged()
		{
			this.IsDirty = true;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00009134 File Offset: 0x00007334
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

		// Token: 0x060000C3 RID: 195 RVA: 0x00009188 File Offset: 0x00007388
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

		// Token: 0x060000C4 RID: 196 RVA: 0x000091D8 File Offset: 0x000073D8
		public override void OnClose()
		{
			base.OnClose();
			XUiEventManager.Instance.OnSkillExperienceAdded -= this.Current_OnSkillExperienceAdded;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000091FC File Offset: 0x000073FC
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
				foreach (XUiC_SkillPerkLevel xuiC_SkillPerkLevel in this.levelEntries)
				{
					bool flag2 = xuiC_SkillPerkLevel.CurrentSkill != null && xuiC_SkillPerkLevel.Level == this.CurrentSkill.Level + 1;
					if (flag2)
					{
						xuiC_SkillPerkLevel.btnBuy.Controller.Pressed(-1);
						break;
					}
				}
			}
			base.Update(_dt);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00009368 File Offset: 0x00007568
		public void Current_OnSkillExperienceAdded(ProgressionValue _changedSkill, int _newXp)
		{
			bool flag = this.CurrentSkill == _changedSkill;
			if (flag)
			{
				this.IsDirty = true;
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x0000938C File Offset: 0x0000758C
		public override bool ParseAttribute(string _name, string _value, XUiController _parent)
		{
			bool flag = _name == "hidden_entries_with_paging";
			bool result;
			if (flag)
			{
				this.hiddenEntriesWithPaging = StringParsers.ParseSInt32(_value, 0, -1, NumberStyles.Integer);
				foreach (XUiC_SkillPerkLevel xuiC_SkillPerkLevel in this.levelEntries)
				{
					bool flag2 = xuiC_SkillPerkLevel != null;
					if (flag2)
					{
						xuiC_SkillPerkLevel.HiddenEntriesWithPaging = this.hiddenEntriesWithPaging;
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

		// Token: 0x060000C8 RID: 200 RVA: 0x00009440 File Offset: 0x00007640
		public override bool GetBindingValueInternal(ref string value, string bindingName)
		{
			EntityPlayerLocal entityPlayer = base.xui.playerUI.entityPlayer;
			uint num = global::<PrivateImplementationDetails>.ComputeStringHash(bindingName);
			if (num <= 1912580562U)
			{
				if (num <= 464048759U)
				{
					if (num != 443815844U)
					{
						if (num == 464048759U)
						{
							if (bindingName == "alwaysfalse")
							{
								value = "false";
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
				else if (num != 1275709072U)
				{
					if (num != 1283949528U)
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
										value = this.buyCostFormatter.Format(this.CurrentSkill.ProgressionClass.CalculatedCostForLevel(this.CurrentSkill.CalculatedLevel(entityPlayer) + 1));
									}
									else
									{
										value = this.expCostFormatter.Format((int)((1f - this.CurrentSkill.PercToNextLevel) * (float)this.CurrentSkill.ProgressionClass.CalculatedCostForLevel(this.CurrentSkill.CalculatedLevel(entityPlayer) + 1)));
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
				else if (bindingName == "maxSkillLevel")
				{
					value = ((this.CurrentSkill != null) ? this.maxSkillLevelFormatter.Format((float)ProgressionClass.GetCalculatedMaxLevel(entityPlayer, this.CurrentSkill)) : "0");
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
							bool flag3 = this.CurrentSkill != null && this.hoveredLevel != -1 && this.CurrentSkill.ProgressionClass.MaxLevel >= this.hoveredLevel;
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
							value = "";
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

		// Token: 0x04000078 RID: 120
		public XUiC_ItemActionList actionItemList;

		// Token: 0x04000079 RID: 121
		public int hiddenEntriesWithPaging = 1;

		// Token: 0x0400007A RID: 122
		public readonly List<XUiC_SkillPerkLevel> levelEntries = new List<XUiC_SkillPerkLevel>();

		// Token: 0x0400007B RID: 123
		public XUiC_Paging pager;

		// Token: 0x0400007C RID: 124
		public int skillsPerPage;

		// Token: 0x0400007D RID: 125
		public int hoveredLevel = -1;

		// Token: 0x0400007E RID: 126
		public readonly CachedStringFormatterFloat skillLevelFormatter = new CachedStringFormatterFloat(null);

		// Token: 0x0400007F RID: 127
		public readonly CachedStringFormatterFloat maxSkillLevelFormatter = new CachedStringFormatterFloat(null);

		// Token: 0x04000080 RID: 128
		public readonly CachedStringFormatter<int> buyCostFormatter = new CachedStringFormatter<int>((int _i) => _i.ToString() + " " + Localization.Get("xuiSkillPoints", false));

		// Token: 0x04000081 RID: 129
		public readonly CachedStringFormatter<int> expCostFormatter = new CachedStringFormatter<int>((int _i) => _i.ToString() + " " + Localization.Get("RewardExp_keyword", false));

		// Token: 0x04000082 RID: 130
		public readonly CachedStringFormatter<string, float, bool> attributeSubtractionFormatter = new CachedStringFormatter<string, float, bool>((string _s, float _f, bool _b) => _s + ": " + _f.ToCultureInvariantString("0.#") + (_b ? "%" : ""));

		// Token: 0x04000083 RID: 131
		public readonly CachedStringFormatter<string, float> attributeSetValueFormatter = new CachedStringFormatter<string, float>((string _s, float _f) => _s + ": " + _f.ToCultureInvariantString("0.#"));

		// Token: 0x04000084 RID: 132
		public readonly StringBuilder effectsStringBuilder = new StringBuilder();
	}
}
