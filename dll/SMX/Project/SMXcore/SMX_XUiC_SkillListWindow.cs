using System;
using GUI_2;

namespace SMXcore
{
	// Token: 0x02000023 RID: 35
	public class XUiC_SkillListWindow : XUiController
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000102 RID: 258 RVA: 0x0000ABF3 File Offset: 0x00008DF3
		// (set) Token: 0x06000103 RID: 259 RVA: 0x0000ABFB File Offset: 0x00008DFB
		public ProgressionClass.DisplayTypes CategoryType { get; set; }

		// Token: 0x06000104 RID: 260 RVA: 0x0000AC04 File Offset: 0x00008E04
		public override void Init()
		{
			base.Init();
			this.totalItems = Localization.Get("lblTotalItems", false);
			this.pointsAvailable = Localization.Get("xuiPointsAvailable", false);
			this.skillsTitle = Localization.Get("xuiSkills", false);
			this.booksTitle = Localization.Get("lblCategoryBooks", false);
			this.craftingTitle = Localization.Get("xuiCrafting", false);
			this.skillList = base.GetChildByType<XUiC_SkillList>();
			this.bookList = base.GetChildByType<XUiC_BookList>();
			this.craftingSkillList = base.GetChildByType<XUiC_CraftingSkillList>();
			XUiController childByType = base.GetChildByType<XUiC_SkillCategoryList>();
			bool flag = childByType != null;
			if (flag)
			{
				this.categoryList = (XUiC_SkillCategoryList)childByType;
				this.categoryList.CategoryChanged += this.CategoryList_CategoryChanged;
			}
			this.skillList.SkillListWindow = this;
			this.bookList.SkillListWindow = this;
			this.craftingSkillList.SkillListWindow = this;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x0000ACF0 File Offset: 0x00008EF0
		public override void OnOpen()
		{
			base.OnOpen();
			base.xui.calloutWindow.ClearCallouts(XUiC_GamepadCalloutWindow.CalloutType.MenuShortcuts);
			base.xui.calloutWindow.AddCallout(UIUtils.ButtonIcon.FaceButtonNorth, "igcoSpendPoints", XUiC_GamepadCalloutWindow.CalloutType.MenuShortcuts);
			base.xui.calloutWindow.EnableCallouts(XUiC_GamepadCalloutWindow.CalloutType.MenuShortcuts, 0f);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000AD46 File Offset: 0x00008F46
		public override void OnClose()
		{
			base.OnClose();
			base.xui.calloutWindow.DisableCallouts(XUiC_GamepadCalloutWindow.CalloutType.MenuShortcuts);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x0000AD62 File Offset: 0x00008F62
		private void CategoryList_CategoryChanged(XUiC_SkillCategoryEntry _categoryEntry)
		{
			this.CategoryType = _categoryEntry.CategoryType;
			this.categoryName = _categoryEntry.CategoryDisplayName;
			this.categoryIcon = _categoryEntry.SpriteName;
			base.RefreshBindings(false);
			this.SelectFirstEntry();
		}

		// Token: 0x06000108 RID: 264 RVA: 0x0000AD9C File Offset: 0x00008F9C
		public void SetSelectedByUnlockData(RecipeUnlockData unlockData)
		{
			switch (unlockData.UnlockType)
			{
			case RecipeUnlockData.UnlockTypes.Perk:
			{
				bool isPerk = unlockData.Perk.IsPerk;
				if (isPerk)
				{
					this.categoryList.SetCategory(ProgressionClass.DisplayTypes.Standard);
				}
				break;
			}
			case RecipeUnlockData.UnlockTypes.Book:
			{
				bool isPerk2 = unlockData.Perk.IsPerk;
				if (isPerk2)
				{
					this.categoryList.SetCategory(ProgressionClass.DisplayTypes.Book);
				}
				break;
			}
			case RecipeUnlockData.UnlockTypes.Skill:
			{
				bool isCrafting = unlockData.Perk.IsCrafting;
				if (isCrafting)
				{
					this.categoryList.SetCategory(ProgressionClass.DisplayTypes.Crafting);
				}
				break;
			}
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000AE30 File Offset: 0x00009030
		public override bool GetBindingValueInternal(ref string value, string bindingName)
		{
			uint num = global::<PrivateImplementationDetails>.ComputeStringHash(bindingName);
			if (num <= 1741938684U)
			{
				if (num != 1050865026U)
				{
					if (num != 1237242696U)
					{
						if (num == 1741938684U)
						{
							if (bindingName == "categoryicon")
							{
								value = this.categoryIcon;
								return true;
							}
						}
					}
					else if (bindingName == "isbook")
					{
						value = ((this.CategoryType == ProgressionClass.DisplayTypes.Book) ? "true" : "false");
						return true;
					}
				}
				else if (bindingName == "isnormal")
				{
					value = ((this.CategoryType == ProgressionClass.DisplayTypes.Standard) ? "true" : "false");
					return true;
				}
			}
			else if (num <= 3877939383U)
			{
				if (num != 3822843618U)
				{
					if (num == 3877939383U)
					{
						if (bindingName == "totalskills")
						{
							value = "";
							bool flag = this.skillList != null;
							if (flag)
							{
								value = this.totalSkillsFormatter.Format(this.totalItems, this.skillList.GetActiveCount());
							}
							return true;
						}
					}
				}
				else if (bindingName == "titlename")
				{
					value = "";
					switch (this.CategoryType)
					{
					case ProgressionClass.DisplayTypes.Standard:
						value = this.skillsTitle;
						break;
					case ProgressionClass.DisplayTypes.Book:
						value = this.booksTitle;
						break;
					case ProgressionClass.DisplayTypes.Crafting:
						value = this.craftingTitle;
						break;
					}
					return true;
				}
			}
			else if (num != 3983894959U)
			{
				if (num == 4035727244U)
				{
					if (bindingName == "skillpointsavailable")
					{
						string v = this.pointsAvailable;
						EntityPlayerLocal entityPlayer = base.xui.playerUI.entityPlayer;
						bool flag2 = XUi.IsGameRunning() && entityPlayer != null;
						if (flag2)
						{
							value = this.skillPointsAvailableFormatter.Format(v, entityPlayer.Progression.SkillPoints);
						}
						return true;
					}
				}
			}
			else if (bindingName == "iscrafting")
			{
				value = ((this.CategoryType == ProgressionClass.DisplayTypes.Crafting) ? "true" : "false");
				return true;
			}
			return false;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0000B090 File Offset: 0x00009290
		public void SelectFirstEntry()
		{
			switch (this.CategoryType)
			{
			case ProgressionClass.DisplayTypes.Standard:
				this.skillList.SelectFirstEntry();
				break;
			case ProgressionClass.DisplayTypes.Book:
				this.bookList.SelectFirstEntry();
				break;
			case ProgressionClass.DisplayTypes.Crafting:
				this.craftingSkillList.SelectFirstEntry();
				break;
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000B0E8 File Offset: 0x000092E8
		public XUiC_SkillEntry GetEntryForSkill(ProgressionValue skill)
		{
			bool flag = skill == null;
			XUiC_SkillEntry result;
			if (flag)
			{
				result = null;
			}
			else
			{
				switch (skill.ProgressionClass.DisplayType)
				{
				case ProgressionClass.DisplayTypes.Standard:
					result = this.skillList.GetEntryForSkill(skill);
					break;
				case ProgressionClass.DisplayTypes.Book:
					result = this.bookList.GetEntryForSkill(skill);
					break;
				case ProgressionClass.DisplayTypes.Crafting:
					result = this.craftingSkillList.GetEntryForSkill(skill);
					break;
				default:
					result = null;
					break;
				}
			}
			return result;
		}

		// Token: 0x0400009C RID: 156
		private string totalItems = "";

		// Token: 0x0400009D RID: 157
		private string categoryName = "Intellect";

		// Token: 0x0400009E RID: 158
		private string categoryIcon = "";

		// Token: 0x0400009F RID: 159
		private string pointsAvailable;

		// Token: 0x040000A0 RID: 160
		private string skillsTitle = "";

		// Token: 0x040000A1 RID: 161
		private string booksTitle = "";

		// Token: 0x040000A2 RID: 162
		private string craftingTitle = "";

		// Token: 0x040000A3 RID: 163
		private XUiC_SkillCategoryList categoryList;

		// Token: 0x040000A4 RID: 164
		private XUiC_SkillList skillList;

		// Token: 0x040000A5 RID: 165
		private XUiC_BookList bookList;

		// Token: 0x040000A6 RID: 166
		private XUiC_CraftingSkillList craftingSkillList;

		// Token: 0x040000A7 RID: 167
		private readonly CachedStringFormatter<string, int> totalSkillsFormatter = new CachedStringFormatter<string, int>((string _s, int _i) => string.Format(_s, _i));

		// Token: 0x040000A8 RID: 168
		private readonly CachedStringFormatter<string, int> skillPointsAvailableFormatter = new CachedStringFormatter<string, int>((string _s, int _i) => string.Format("{0} {1}", _s, _i));
	}
}
