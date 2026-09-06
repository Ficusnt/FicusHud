using System;
using System.Collections.Generic;
using System.Diagnostics;
using GUI_2;

namespace SMXcore
{
	// Token: 0x02000017 RID: 23
	public class XUiC_SkillCategoryList : XUiController
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000070 RID: 112 RVA: 0x000065D0 File Offset: 0x000047D0
		// (set) Token: 0x06000071 RID: 113 RVA: 0x000065E8 File Offset: 0x000047E8
		public XUiC_SkillCategoryEntry CurrentCategory
		{
			get
			{
				return this.currentCategory;
			}
			set
			{
				bool flag = this.currentCategory != null;
				if (flag)
				{
					this.currentCategory.Selected = false;
				}
				this.currentCategory = value;
				bool flag2 = this.currentCategory != null;
				if (flag2)
				{
					this.currentCategory.Selected = true;
					this.currentIndex = this.categoryButtons.IndexOf(this.currentCategory);
				}
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000072 RID: 114 RVA: 0x0000664C File Offset: 0x0000484C
		public int MaxCategories
		{
			get
			{
				return this.categoryButtons.Count;
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000073 RID: 115 RVA: 0x0000665C File Offset: 0x0000485C
		// (remove) Token: 0x06000074 RID: 116 RVA: 0x00006694 File Offset: 0x00004894
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event XUiC_SkillCategoryList.XUiEvent_SkillCategoryChangedEventHandler CategoryChanged;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000075 RID: 117 RVA: 0x000066CC File Offset: 0x000048CC
		// (remove) Token: 0x06000076 RID: 118 RVA: 0x00006704 File Offset: 0x00004904
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event XUiC_SkillCategoryList.XUiEvent_SkillCategoryChangedEventHandler CategoryClickChanged;

		// Token: 0x06000077 RID: 119 RVA: 0x0000673C File Offset: 0x0000493C
		public override void Init()
		{
			base.Init();
			base.GetChildrenByType<XUiC_SkillCategoryEntry>(this.categoryButtons);
			for (int i = 0; i < this.categoryButtons.Count; i++)
			{
				this.categoryButtons[i].CategoryList = this;
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00006790 File Offset: 0x00004990
		public override void Update(float _dt)
		{
			base.Update(_dt);
			bool flag = this.AllowKeyPaging && base.xui.playerUI.windowManager.IsKeyShortcutsAllowed();
			if (flag)
			{
				PlayerActionsGUI guiactions = base.xui.playerUI.playerInput.GUIActions;
				bool wasReleased = guiactions.PageUp.WasReleased;
				if (wasReleased)
				{
					this.IncrementCategory(1);
				}
				else
				{
					bool wasReleased2 = guiactions.PageDown.WasReleased;
					if (wasReleased2)
					{
						this.IncrementCategory(-1);
					}
				}
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00006816 File Offset: 0x00004A16
		internal void HandleCategoryChanged()
		{
			XUiC_SkillCategoryList.XUiEvent_SkillCategoryChangedEventHandler categoryChanged = this.CategoryChanged;
			if (categoryChanged != null)
			{
				categoryChanged(this.CurrentCategory);
			}
			XUiC_SkillCategoryList.XUiEvent_SkillCategoryChangedEventHandler categoryClickChanged = this.CategoryClickChanged;
			if (categoryClickChanged != null)
			{
				categoryClickChanged(this.CurrentCategory);
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000684C File Offset: 0x00004A4C
		private XUiC_SkillCategoryEntry GetCategoryByType(ProgressionClass.DisplayTypes category, out int index)
		{
			index = 0;
			for (int i = 0; i < this.categoryButtons.Count; i++)
			{
				bool flag = this.categoryButtons[i].CategoryType == category;
				if (flag)
				{
					index = i;
					return this.categoryButtons[i];
				}
			}
			return null;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000068AC File Offset: 0x00004AAC
		public XUiC_SkillCategoryEntry GetCategoryByIndex(int _index)
		{
			bool flag = _index >= this.categoryButtons.Count;
			XUiC_SkillCategoryEntry result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = this.categoryButtons[_index];
			}
			return result;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000068E4 File Offset: 0x00004AE4
		public void SetCategoryToFirst()
		{
			this.CurrentCategory = this.categoryButtons[0];
			XUiC_SkillCategoryList.XUiEvent_SkillCategoryChangedEventHandler categoryChanged = this.CategoryChanged;
			if (categoryChanged != null)
			{
				categoryChanged(this.CurrentCategory);
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00006914 File Offset: 0x00004B14
		public void SetCategory(ProgressionClass.DisplayTypes category)
		{
			int num;
			XUiC_SkillCategoryEntry categoryByType = this.GetCategoryByType(category, out num);
			bool flag = categoryByType != null || this.AllowUnselect;
			if (flag)
			{
				this.CurrentCategory = categoryByType;
				XUiC_SkillCategoryList.XUiEvent_SkillCategoryChangedEventHandler categoryChanged = this.CategoryChanged;
				if (categoryChanged != null)
				{
					categoryChanged(this.CurrentCategory);
				}
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00006960 File Offset: 0x00004B60
		private void IncrementCategory(int _offset)
		{
			bool flag = _offset == 0;
			if (!flag)
			{
				int i = 0;
				int num = NGUIMath.RepeatIndex(this.currentIndex + _offset, this.categoryButtons.Count);
				XUiC_SkillCategoryEntry xuiC_SkillCategoryEntry = this.categoryButtons[num];
				while (i < this.categoryButtons.Count)
				{
					bool flag2 = xuiC_SkillCategoryEntry != null && !(xuiC_SkillCategoryEntry.SpriteName == "");
					if (flag2)
					{
						break;
					}
					num = NGUIMath.RepeatIndex((_offset > 0) ? (num + 1) : (num - 1), this.categoryButtons.Count);
					xuiC_SkillCategoryEntry = this.categoryButtons[num];
					i++;
				}
				bool flag3 = xuiC_SkillCategoryEntry != null;
				if (flag3)
				{
					this.CurrentCategory = xuiC_SkillCategoryEntry;
					this.HandleCategoryChanged();
				}
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00006A28 File Offset: 0x00004C28
		public override void OnOpen()
		{
			base.OnOpen();
			base.xui.calloutWindow.ClearCallouts(XUiC_GamepadCalloutWindow.CalloutType.MenuCategory);
			base.xui.calloutWindow.AddCallout(UIUtils.ButtonIcon.LeftTrigger, "igcoCategoryLeft", XUiC_GamepadCalloutWindow.CalloutType.MenuCategory);
			base.xui.calloutWindow.AddCallout(UIUtils.ButtonIcon.RightTrigger, "igcoCategoryRight", XUiC_GamepadCalloutWindow.CalloutType.MenuCategory);
			base.xui.calloutWindow.EnableCallouts(XUiC_GamepadCalloutWindow.CalloutType.MenuCategory, 0f);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00006A97 File Offset: 0x00004C97
		public override void OnClose()
		{
			base.OnClose();
			base.xui.calloutWindow.DisableCallouts(XUiC_GamepadCalloutWindow.CalloutType.MenuCategory);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00006AB4 File Offset: 0x00004CB4
		public override bool ParseAttribute(string _name, string _value, XUiController _parent)
		{
			bool result;
			if (!(_name == "allow_unselect"))
			{
				if (!(_name == "allow_key_paging"))
				{
					result = base.ParseAttribute(_name, _value, _parent);
				}
				else
				{
					this.AllowKeyPaging = StringParsers.ParseBool(_value, 0, -1, true);
					result = true;
				}
			}
			else
			{
				this.AllowUnselect = StringParsers.ParseBool(_value, 0, -1, true);
				result = true;
			}
			return result;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00006B18 File Offset: 0x00004D18
		public bool SetupSkillCategories()
		{
			for (int i = 0; i < this.categoryButtons.Count; i++)
			{
				XUiC_SkillCategoryEntry xuiC_SkillCategoryEntry = this.categoryButtons[i];
				xuiC_SkillCategoryEntry.ViewComponent.IsVisible = true;
				xuiC_SkillCategoryEntry.ViewComponent.IsNavigatable = true;
			}
			return true;
		}

		// Token: 0x04000051 RID: 81
		private readonly List<XUiC_SkillCategoryEntry> categoryButtons = new List<XUiC_SkillCategoryEntry>();

		// Token: 0x04000052 RID: 82
		private int currentIndex;

		// Token: 0x04000053 RID: 83
		private XUiC_SkillCategoryEntry currentCategory;

		// Token: 0x04000054 RID: 84
		public bool AllowUnselect;

		// Token: 0x04000055 RID: 85
		public bool AllowKeyPaging = true;

		// Token: 0x02000032 RID: 50
		// (Invoke) Token: 0x06000166 RID: 358
		public delegate void XUiEvent_SkillCategoryChangedEventHandler(XUiC_SkillCategoryEntry categoryEntry);
	}
}
