using System;
using Quartz;

namespace SMXcore
{
	// Token: 0x02000016 RID: 22
	public class XUiC_SkillCategoryEntry : XUiController
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00006300 File Offset: 0x00004500
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00006308 File Offset: 0x00004508
		public XUiC_SkillCategoryList CategoryList { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00006314 File Offset: 0x00004514
		// (set) Token: 0x06000063 RID: 99 RVA: 0x0000632C File Offset: 0x0000452C
		public ProgressionClass.DisplayTypes CategoryType
		{
			get
			{
				return this.categoryType;
			}
			set
			{
				Logging.Inform("Category Type = " + value.ToString());
				this.categoryType = value;
				this.IsDirty = true;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000064 RID: 100 RVA: 0x0000635C File Offset: 0x0000455C
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00006374 File Offset: 0x00004574
		public string CategoryDisplayName
		{
			get
			{
				return this.categoryDisplayName;
			}
			set
			{
				this.categoryDisplayName = value;
				this.IsDirty = true;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00006388 File Offset: 0x00004588
		// (set) Token: 0x06000067 RID: 103 RVA: 0x000063A0 File Offset: 0x000045A0
		public string SpriteName
		{
			get
			{
				return this.spriteName;
			}
			set
			{
				this.spriteName = value;
				this.IsDirty = true;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000068 RID: 104 RVA: 0x000063B4 File Offset: 0x000045B4
		// (set) Token: 0x06000069 RID: 105 RVA: 0x000063CC File Offset: 0x000045CC
		public new bool Selected
		{
			get
			{
				return this.selected;
			}
			set
			{
				this.selected = value;
				this.button.Selected = this.selected;
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000063E8 File Offset: 0x000045E8
		public override void Init()
		{
			base.Init();
			this.button = (base.ViewComponent as XUiV_Button);
			base.OnPress += this.XUiC_CategoryEntry_OnPress;
			this.IsDirty = true;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00006420 File Offset: 0x00004620
		private void XUiC_CategoryEntry_OnPress(XUiController _sender, int _mouseButton)
		{
			bool flag = this.CategoryList.CurrentCategory == this && this.CategoryList.AllowUnselect;
			if (flag)
			{
				this.CategoryList.CurrentCategory = null;
			}
			else
			{
				this.CategoryList.CurrentCategory = this;
			}
			this.CategoryList.HandleCategoryChanged();
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000647C File Offset: 0x0000467C
		public override void Update(float _dt)
		{
			base.Update(_dt);
			bool isDirty = this.IsDirty;
			if (isDirty)
			{
				base.ViewComponent.IsNavigatable = true;
				base.RefreshBindings(false);
				this.IsDirty = false;
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000064BC File Offset: 0x000046BC
		public override bool GetBindingValueInternal(ref string value, string bindingName)
		{
			bool result;
			if (!(bindingName == "categoryicon"))
			{
				if (!(bindingName == "categorydisplayname"))
				{
					result = false;
				}
				else
				{
					value = this.categoryDisplayName;
					result = true;
				}
			}
			else
			{
				value = this.spriteName;
				result = true;
			}
			return result;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00006508 File Offset: 0x00004708
		public override bool ParseAttribute(string name, string value, XUiController parent)
		{
			bool result;
			if (!(name == "categoryname"))
			{
				if (!(name == "spritename"))
				{
					if (!(name == "displayname_key"))
					{
						result = base.ParseAttribute(name, value, parent);
					}
					else
					{
						bool flag = !string.IsNullOrEmpty(value);
						if (flag)
						{
							this.CategoryDisplayName = Localization.Get(value, false);
						}
						result = true;
					}
				}
				else
				{
					bool flag2 = !string.IsNullOrEmpty(value);
					if (flag2)
					{
						this.SpriteName = value;
					}
					result = true;
				}
			}
			else
			{
				bool flag3 = !string.IsNullOrEmpty(value);
				if (flag3)
				{
					this.CategoryType = EnumUtils.Parse<ProgressionClass.DisplayTypes>(value, true);
				}
				result = true;
			}
			return result;
		}

		// Token: 0x0400004B RID: 75
		private ProgressionClass.DisplayTypes categoryType;

		// Token: 0x0400004C RID: 76
		private string categoryDisplayName = "";

		// Token: 0x0400004D RID: 77
		private string spriteName = "";

		// Token: 0x0400004E RID: 78
		private bool selected;

		// Token: 0x0400004F RID: 79
		private XUiV_Button button;
	}
}
