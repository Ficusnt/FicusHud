using System;
using GUI_2;

namespace SMXcore
{
	// Token: 0x02000024 RID: 36
	public class XUiC_SkillWindowGroup : XUiController
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600010D RID: 269 RVA: 0x0000B204 File Offset: 0x00009404
		// (set) Token: 0x0600010E RID: 270 RVA: 0x0000B21C File Offset: 0x0000941C
		public ProgressionValue CurrentSkill
		{
			get
			{
				return this.currentSkill;
			}
			set
			{
				this.currentSkill = value;
				this.IsDirty = true;
			}
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0000B230 File Offset: 0x00009430
		public override void Init()
		{
			base.Init();
			this.skillList = base.GetChildByType<XUiC_SkillList>();
			this.skillListWindow = base.GetChildByType<XUiC_SkillListWindow>();
			this.skillAttributeInfoWindow = base.GetChildByType<XUiC_SkillAttributeInfoWindow>();
			this.skillPerkInfoWindow = base.GetChildByType<XUiC_SkillPerkInfoWindow>();
			this.skillBookInfoWindow = base.GetChildByType<XUiC_SkillBookInfoWindow>();
			this.skillCraftingInfoWindow = base.GetChildByType<XUiC_SkillCraftingInfoWindow>();
			this.skillEntries = base.GetChildrenByType<XUiC_SkillEntry>(null);
			for (int i = 0; i < this.skillEntries.Length; i++)
			{
				this.skillEntries[i].OnPress += this.XUiC_SkillEntry_OnPress;
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000B2CD File Offset: 0x000094CD
		private void CategoryList_CategoryChanged(XUiC_SkillCategoryEntry categoryEntry)
		{
			this.IsDirty = true;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000B2D7 File Offset: 0x000094D7
		private void CategoryList_CategoryClickChanged(XUiC_SkillCategoryEntry categoryEntry)
		{
			this.IsDirty = true;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000B2E1 File Offset: 0x000094E1
		private void XUiC_SkillEntry_OnPress(XUiController _sender, int _mouseButton)
		{
			this.IsDirty = true;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000B2EB File Offset: 0x000094EB
		public override void OnClose()
		{
			base.OnClose();
			base.xui.playerUI.windowManager.CloseIfOpen("windowpaging");
			base.xui.calloutWindow.DisableCallouts(XUiC_GamepadCalloutWindow.CalloutType.Menu);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000B324 File Offset: 0x00009524
		public override void Update(float _dt)
		{
			base.Update(_dt);
			bool isDirty = this.IsDirty;
			if (isDirty)
			{
				this.currentSkill = base.xui.selectedSkill;
				this.skillAttributeInfoWindow.SkillChanged();
				this.skillPerkInfoWindow.SkillChanged();
				this.skillBookInfoWindow.SkillChanged();
				this.skillCraftingInfoWindow.SkillChanged();
				bool flag = this.skillListWindow.CategoryType == ProgressionClass.DisplayTypes.Book;
				if (flag)
				{
					this.skillBookInfoWindow.ViewComponent.IsVisible = true;
				}
				else
				{
					bool flag2 = this.skillListWindow.CategoryType == ProgressionClass.DisplayTypes.Crafting;
					if (flag2)
					{
						this.skillCraftingInfoWindow.ViewComponent.IsVisible = true;
					}
					else
					{
						bool flag3 = base.xui.selectedSkill != null;
						if (flag3)
						{
							bool isAttribute = base.xui.selectedSkill.ProgressionClass.IsAttribute;
							if (isAttribute)
							{
								this.skillAttributeInfoWindow.ViewComponent.IsVisible = true;
							}
							else
							{
								this.skillPerkInfoWindow.ViewComponent.IsVisible = true;
							}
						}
					}
				}
				this.IsDirty = false;
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0000B440 File Offset: 0x00009640
		public override void OnOpen()
		{
			base.OnOpen();
			bool flag = this.categoryList == null;
			if (flag)
			{
				XUiC_SkillCategoryList childByType = base.GetChildByType<XUiC_SkillCategoryList>();
				bool flag2 = childByType != null;
				if (flag2)
				{
					this.categoryList = childByType;
					this.categoryList.SetupSkillCategories();
					this.categoryList.CategoryChanged += this.CategoryList_CategoryChanged;
					this.categoryList.CategoryClickChanged += this.CategoryList_CategoryClickChanged;
				}
			}
			base.xui.playerUI.windowManager.OpenIfNotOpen("windowpaging", false, false, true);
			base.xui.calloutWindow.ClearCallouts(XUiC_GamepadCalloutWindow.CalloutType.Menu);
			base.xui.calloutWindow.AddCallout(UIUtils.ButtonIcon.FaceButtonSouth, "igcoSelect", XUiC_GamepadCalloutWindow.CalloutType.Menu);
			base.xui.calloutWindow.AddCallout(UIUtils.ButtonIcon.FaceButtonEast, "igcoExit", XUiC_GamepadCalloutWindow.CalloutType.Menu);
			base.xui.calloutWindow.EnableCallouts(XUiC_GamepadCalloutWindow.CalloutType.Menu, 0f);
			XUiC_WindowSelector childByType2 = base.xui.FindWindowGroupByName("windowpaging").GetChildByType<XUiC_WindowSelector>();
			bool flag3 = childByType2 != null;
			if (flag3)
			{
				childByType2.SetSelected("skills");
			}
			this.IsDirty = true;
			bool flag4 = this.categoryList.CurrentCategory == null;
			if (flag4)
			{
				this.categoryList.SetCategoryToFirst();
			}
			this.skillListWindow.CategoryType = this.categoryList.CurrentCategory.CategoryType;
			this.skillList.RefreshSkillList();
			bool flag5 = base.xui.selectedSkill == null;
			if (flag5)
			{
				this.skillList.SelectFirstEntry();
			}
			else
			{
				this.skillList.SelectedEntry.SelectCursorElement(true, false);
			}
			this.IsDirty = true;
		}

		// Token: 0x040000AA RID: 170
		public XUiC_SkillEntry[] skillEntries;

		// Token: 0x040000AB RID: 171
		public XUiC_SkillList skillList;

		// Token: 0x040000AC RID: 172
		public XUiC_SkillCategoryList categoryList;

		// Token: 0x040000AD RID: 173
		public XUiC_SkillListWindow skillListWindow;

		// Token: 0x040000AE RID: 174
		public XUiC_SkillAttributeInfoWindow skillAttributeInfoWindow;

		// Token: 0x040000AF RID: 175
		public XUiC_SkillPerkInfoWindow skillPerkInfoWindow;

		// Token: 0x040000B0 RID: 176
		public XUiC_SkillBookInfoWindow skillBookInfoWindow;

		// Token: 0x040000B1 RID: 177
		public XUiC_SkillCraftingInfoWindow skillCraftingInfoWindow;

		// Token: 0x040000B2 RID: 178
		public ProgressionValue currentSkill;
	}
}
