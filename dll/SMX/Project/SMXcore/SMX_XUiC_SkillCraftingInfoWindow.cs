using System;
using System.Collections.Generic;
using System.Globalization;

namespace SMXcore
{
	// Token: 0x0200001A RID: 26
	public class XUiC_SkillCraftingInfoWindow : XUiC_InfoWindow
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00008084 File Offset: 0x00006284
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x0000809C File Offset: 0x0000629C
		public XUiC_SkillCraftingInfoEntry SelectedEntry
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
				}
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x000080EC File Offset: 0x000062EC
		public ProgressionValue CurrentSkill
		{
			[PublicizedFrom(EAccessModifier.Private)]
			get
			{
				bool flag = base.xui.selectedSkill == null || !base.xui.selectedSkill.ProgressionClass.IsCrafting;
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

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x0000813C File Offset: 0x0000633C
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x00008154 File Offset: 0x00006354
		public ProgressionClass.DisplayData HoveredData
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

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00008184 File Offset: 0x00006384
		// (set) Token: 0x060000AA RID: 170 RVA: 0x0000819C File Offset: 0x0000639C
		public ProgressionClass.DisplayData SelectedData
		{
			get
			{
				return this.selectedLevel;
			}
			set
			{
				bool flag = this.selectedLevel != value;
				if (flag)
				{
					this.selectedLevel = value;
					base.RefreshBindings(false);
				}
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000AB RID: 171 RVA: 0x000081CC File Offset: 0x000063CC
		public ProgressionClass.DisplayData CurrentData
		{
			get
			{
				bool flag = this.hoveredLevel != null;
				ProgressionClass.DisplayData result;
				if (flag)
				{
					result = this.hoveredLevel;
				}
				else
				{
					result = this.selectedLevel;
				}
				return result;
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000081FC File Offset: 0x000063FC
		public override void Init()
		{
			base.Init();
			base.GetChildrenByType<XUiC_SkillCraftingInfoEntry>(this.levelEntries);
			int num = 1;
			foreach (XUiC_SkillCraftingInfoEntry xuiC_SkillCraftingInfoEntry in this.levelEntries)
			{
				xuiC_SkillCraftingInfoEntry.ListIndex = num - 1;
				xuiC_SkillCraftingInfoEntry.Data = null;
				xuiC_SkillCraftingInfoEntry.HiddenEntriesWithPaging = this.hiddenEntriesWithPaging;
				xuiC_SkillCraftingInfoEntry.MaxEntriesWithoutPaging = this.levelEntries.Count;
				xuiC_SkillCraftingInfoEntry.OnHover += this.Entry_OnHover;
				xuiC_SkillCraftingInfoEntry.OnPress += this.Entry_OnPress;
			}
			for (int i = 0; i < 14; i++)
			{
				XUiController childById = base.GetChildById(string.Format("itemIcon{0}", i + 1));
				childById.CustomData = i;
				childById.OnPress += this.Image_OnPress;
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

		// Token: 0x060000AD RID: 173 RVA: 0x00008364 File Offset: 0x00006564
		public void Image_OnPress(XUiController _sender, int _mouseButton)
		{
			int index = (int)_sender.CustomData;
			XUi xui = _sender.xui;
			bool flag = this.CurrentData == null || this.CurrentData.GetUnlockItemRecipes(index) == null;
			if (!flag)
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
					xuiC_RecipeList.SetRecipeDataByItems(this.CurrentData.GetUnlockItemRecipes(index));
				}
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00008460 File Offset: 0x00006660
		public void Entry_OnPress(XUiController _sender, int _mouseButton)
		{
			XUiC_SkillCraftingInfoEntry xuiC_SkillCraftingInfoEntry = _sender as XUiC_SkillCraftingInfoEntry;
			bool flag = xuiC_SkillCraftingInfoEntry == null;
			if (flag)
			{
				xuiC_SkillCraftingInfoEntry = (_sender.Parent as XUiC_SkillCraftingInfoEntry);
			}
			bool flag2 = xuiC_SkillCraftingInfoEntry != null;
			if (flag2)
			{
				bool flag3 = this.SelectedData != xuiC_SkillCraftingInfoEntry.Data && xuiC_SkillCraftingInfoEntry.Data != null;
				if (flag3)
				{
					this.SelectedEntry = xuiC_SkillCraftingInfoEntry;
					this.SelectedData = xuiC_SkillCraftingInfoEntry.Data;
				}
				else
				{
					this.SelectedEntry = null;
					this.SelectedData = null;
				}
			}
			else
			{
				this.SelectedEntry = null;
				this.SelectedData = null;
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000084F4 File Offset: 0x000066F4
		public void Entry_OnHover(XUiController _sender, bool _isOver)
		{
			XUiC_SkillCraftingInfoEntry xuiC_SkillCraftingInfoEntry = _sender as XUiC_SkillCraftingInfoEntry;
			bool flag = xuiC_SkillCraftingInfoEntry == null;
			if (flag)
			{
				xuiC_SkillCraftingInfoEntry = (_sender.Parent as XUiC_SkillCraftingInfoEntry);
			}
			bool flag2 = _isOver && xuiC_SkillCraftingInfoEntry != null;
			if (flag2)
			{
				this.HoveredData = xuiC_SkillCraftingInfoEntry.Data;
			}
			else
			{
				this.HoveredData = null;
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00008548 File Offset: 0x00006748
		public void SkillChanged()
		{
			XUiC_Paging xuiC_Paging = this.pager;
			if (xuiC_Paging != null)
			{
				xuiC_Paging.SetLastPageByElementsAndPageLength((this.CurrentSkill != null && this.CurrentSkill.ProgressionClass.MaxLevel > this.levelEntries.Count) ? (this.CurrentSkill.ProgressionClass.MaxLevel - 1) : 0, this.skillsPerPage);
			}
			XUiC_Paging xuiC_Paging2 = this.pager;
			if (xuiC_Paging2 != null)
			{
				xuiC_Paging2.Reset();
			}
			this.IsDirty = true;
			this.SelectedData = null;
			this.SelectedEntry = null;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000085D4 File Offset: 0x000067D4
		public void UpdateSkill()
		{
			bool flag = this.CurrentSkill != null && this.actionItemList != null;
			if (flag)
			{
				this.actionItemList.SetCraftingActionList(XUiC_ItemActionList.ItemActionListTypes.Skill, this);
			}
			XUiC_Paging xuiC_Paging = this.pager;
			int num = ((xuiC_Paging != null) ? xuiC_Paging.GetPage() : 0) * this.skillsPerPage;
			ProgressionClass progressionClass = (this.CurrentSkill != null) ? this.CurrentSkill.ProgressionClass : null;
			bool flag2 = progressionClass != null && progressionClass.DisplayDataList != null;
			if (flag2)
			{
				XUiC_SkillEntry entryForSkill = this.windowGroup.Controller.GetChildByType<XUiC_SkillListWindow>().GetEntryForSkill(this.CurrentSkill);
				foreach (XUiC_SkillCraftingInfoEntry xuiC_SkillCraftingInfoEntry in this.levelEntries)
				{
					xuiC_SkillCraftingInfoEntry.Data = ((progressionClass.DisplayDataList.Count > num) ? progressionClass.DisplayDataList[num] : null);
					xuiC_SkillCraftingInfoEntry.IsDirty = true;
					bool flag3 = entryForSkill != null;
					if (flag3)
					{
						xuiC_SkillCraftingInfoEntry.ViewComponent.NavLeftTarget = entryForSkill.ViewComponent;
					}
					num++;
				}
			}
			else
			{
				foreach (XUiC_SkillCraftingInfoEntry xuiC_SkillCraftingInfoEntry2 in this.levelEntries)
				{
					xuiC_SkillCraftingInfoEntry2.Data = null;
					xuiC_SkillCraftingInfoEntry2.IsDirty = true;
				}
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x0000875C File Offset: 0x0000695C
		public void Pager_OnPageChanged()
		{
			this.IsDirty = true;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00008768 File Offset: 0x00006968
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

		// Token: 0x060000B4 RID: 180 RVA: 0x000087B8 File Offset: 0x000069B8
		public override void OnClose()
		{
			base.OnClose();
			XUiEventManager.Instance.OnSkillExperienceAdded -= this.Current_OnSkillExperienceAdded;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000087DC File Offset: 0x000069DC
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

		// Token: 0x060000B6 RID: 182 RVA: 0x0000881C File Offset: 0x00006A1C
		public void Current_OnSkillExperienceAdded(ProgressionValue _changedSkill, int _newXp)
		{
			bool flag = this.CurrentSkill == _changedSkill;
			if (flag)
			{
				this.IsDirty = true;
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00008840 File Offset: 0x00006A40
		public override bool ParseAttribute(string _name, string _value, XUiController _parent)
		{
			bool flag = _name == "hidden_entries_with_paging";
			bool result;
			if (flag)
			{
				this.hiddenEntriesWithPaging = StringParsers.ParseSInt32(_value, 0, -1, NumberStyles.Integer);
				foreach (XUiC_SkillCraftingInfoEntry xuiC_SkillCraftingInfoEntry in this.levelEntries)
				{
					bool flag2 = xuiC_SkillCraftingInfoEntry != null;
					if (flag2)
					{
						xuiC_SkillCraftingInfoEntry.HiddenEntriesWithPaging = this.hiddenEntriesWithPaging;
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

		// Token: 0x060000B8 RID: 184 RVA: 0x000088F4 File Offset: 0x00006AF4
		public override bool GetBindingValueInternal(ref string _value, string _bindingName)
		{
			EntityPlayerLocal entityPlayer = base.xui.playerUI.entityPlayer;
			uint num = global::<PrivateImplementationDetails>.ComputeStringHash(_bindingName);
			if (num <= 1283949528U)
			{
				if (num <= 464048759U)
				{
					if (num != 443815844U)
					{
						if (num == 464048759U)
						{
							if (_bindingName == "alwaysfalse")
							{
								_value = "false";
								return true;
							}
						}
					}
					else if (_bindingName == "skillLevel")
					{
						_value = ((this.CurrentSkill != null) ? this.skillLevelFormatter.Format(this.CurrentSkill.GetCalculatedLevel(entityPlayer)) : "0");
						return true;
					}
				}
				else if (num != 1275709072U)
				{
					if (num == 1283949528U)
					{
						if (_bindingName == "currentlevel")
						{
							_value = Localization.Get("xuiSkillLevel", false);
							return true;
						}
					}
				}
				else if (_bindingName == "maxSkillLevel")
				{
					_value = ((this.CurrentSkill != null) ? this.maxSkillLevelFormatter.Format((float)ProgressionClass.GetCalculatedMaxLevel(entityPlayer, this.CurrentSkill)) : "0");
					return true;
				}
			}
			else if (num <= 3268933568U)
			{
				if (num != 2606420134U)
				{
					if (num == 3268933568U)
					{
						if (_bindingName == "showPaging")
						{
							_value = "false";
							return true;
						}
					}
				}
				else if (_bindingName == "groupdescription")
				{
					_value = ((this.CurrentSkill != null) ? Localization.Get(this.CurrentSkill.ProgressionClass.DescKey, false) : "");
					return true;
				}
			}
			else if (num != 3504806855U)
			{
				if (num != 4010384093U)
				{
					if (num == 4294521801U)
					{
						if (_bindingName == "detailsdescription")
						{
							_value = "";
							return true;
						}
					}
				}
				else if (_bindingName == "groupicon")
				{
					_value = ((this.CurrentSkill != null) ? this.CurrentSkill.ProgressionClass.Icon : "ui_game_symbol_skills");
					return true;
				}
			}
			else if (_bindingName == "groupname")
			{
				_value = ((this.CurrentSkill != null) ? Localization.Get(this.CurrentSkill.ProgressionClass.NameKey, false) : "Skill Info");
				return true;
			}
			bool flag = _bindingName.StartsWith("unlock_icon_atlas");
			bool result;
			if (flag)
			{
				bool flag2 = this.CurrentData != null;
				if (flag2)
				{
					int index = StringParsers.ParseSInt32(_bindingName.Replace("unlock_icon_atlas", ""), 0, -1, NumberStyles.Integer) - 1;
					_value = this.CurrentData.GetUnlockItemIconAtlas(entityPlayer, index);
				}
				else
				{
					_value = "ItemIconAtlas";
				}
				result = true;
			}
			else
			{
				bool flag3 = _bindingName.StartsWith("unlock_icon_locked");
				if (flag3)
				{
					bool flag4 = this.CurrentData != null;
					if (flag4)
					{
						int index2 = StringParsers.ParseSInt32(_bindingName.Replace("unlock_icon_locked", ""), 0, -1, NumberStyles.Integer) - 1;
						_value = this.CurrentData.GetUnlockItemLocked(entityPlayer, index2).ToString();
					}
					else
					{
						_value = "false";
					}
					result = true;
				}
				else
				{
					bool flag5 = _bindingName.StartsWith("unlock_icon_tooltip");
					if (flag5)
					{
						bool flag6 = this.CurrentData != null;
						if (flag6)
						{
							int index3 = StringParsers.ParseSInt32(_bindingName.Replace("unlock_icon_tooltip", ""), 0, -1, NumberStyles.Integer) - 1;
							_value = this.CurrentData.GetUnlockItemName(index3);
						}
						else
						{
							_value = "";
						}
						result = true;
					}
					else
					{
						bool flag7 = _bindingName.StartsWith("unlock_icon");
						if (flag7)
						{
							bool flag8 = this.CurrentData != null;
							if (flag8)
							{
								int index4 = StringParsers.ParseSInt32(_bindingName.Replace("unlock_icon", ""), 0, -1, NumberStyles.Integer) - 1;
								_value = this.CurrentData.GetUnlockItemIconName(index4);
							}
							else
							{
								_value = "";
							}
							result = true;
						}
						else
						{
							result = base.GetBindingValueInternal(ref _value, _bindingName);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0400006D RID: 109
		public XUiC_ItemActionList actionItemList;

		// Token: 0x0400006E RID: 110
		public int hiddenEntriesWithPaging = 1;

		// Token: 0x0400006F RID: 111
		public readonly List<XUiC_SkillCraftingInfoEntry> levelEntries = new List<XUiC_SkillCraftingInfoEntry>();

		// Token: 0x04000070 RID: 112
		public XUiC_Paging pager;

		// Token: 0x04000071 RID: 113
		public XUiC_SkillCraftingInfoEntry selectedEntry;

		// Token: 0x04000072 RID: 114
		public int skillsPerPage;

		// Token: 0x04000073 RID: 115
		public ProgressionClass.DisplayData hoveredLevel;

		// Token: 0x04000074 RID: 116
		public ProgressionClass.DisplayData selectedLevel;

		// Token: 0x04000075 RID: 117
		public readonly CachedStringFormatterFloat skillLevelFormatter = new CachedStringFormatterFloat(null);

		// Token: 0x04000076 RID: 118
		public readonly CachedStringFormatterFloat maxSkillLevelFormatter = new CachedStringFormatterFloat(null);

		// Token: 0x04000077 RID: 119
		public readonly CachedStringFormatter<string, float> attributeSetValueFormatter = new CachedStringFormatter<string, float>((string _s, float _f) => _s + ": " + _f.ToCultureInvariantString("0.#"));
	}
}
