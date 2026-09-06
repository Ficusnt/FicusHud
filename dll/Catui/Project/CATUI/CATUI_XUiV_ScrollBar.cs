using System;
using UnityEngine;

namespace Views
{
	// Token: 0x02000026 RID: 38
	public class XUiV_ScrollBar : XUiView
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600007C RID: 124 RVA: 0x000086AF File Offset: 0x000068AF
		public bool HasXMLChildren
		{
			get
			{
				return !string.IsNullOrEmpty(this.foregroundViewId) || !string.IsNullOrEmpty(this.backgroundViewId);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600007D RID: 125 RVA: 0x000086CF File Offset: 0x000068CF
		private bool hasBackgroundSprite
		{
			get
			{
				return !this.backgroundSpriteName.Equals(XUi.BlankTexture) || !string.IsNullOrEmpty(this.backgroundViewId);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600007E RID: 126 RVA: 0x000086F4 File Offset: 0x000068F4
		// (set) Token: 0x0600007F RID: 127 RVA: 0x0000870C File Offset: 0x0000690C
		public UIScrollBar UiScrollBar
		{
			get
			{
				return this.uiScrollBar;
			}
			set
			{
				bool flag = this.uiScrollBar != value;
				if (flag)
				{
					this.uiScrollBar = value;
				}
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00008734 File Offset: 0x00006934
		// (set) Token: 0x06000081 RID: 129 RVA: 0x0000874C File Offset: 0x0000694C
		public Color BackgroundSpriteColor
		{
			get
			{
				return this.backgroundSpriteColor;
			}
			set
			{
				bool flag = this.backgroundSpriteColor.r != value.r || this.backgroundSpriteColor.g != value.g || this.backgroundSpriteColor.b != value.b || this.backgroundSpriteColor.a != value.a;
				if (flag)
				{
					this.backgroundSpriteColor = value;
					this.isDirty = true;
				}
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000082 RID: 130 RVA: 0x000087C0 File Offset: 0x000069C0
		// (set) Token: 0x06000083 RID: 131 RVA: 0x000087D8 File Offset: 0x000069D8
		public Color ForegroundSpriteColor
		{
			get
			{
				return this.foregroundSpriteColor;
			}
			set
			{
				bool flag = this.foregroundSpriteColor.r != value.r || this.foregroundSpriteColor.g != value.g || this.foregroundSpriteColor.b != value.b || this.foregroundSpriteColor.a != value.a;
				if (flag)
				{
					this.foregroundSpriteColor = value;
					this.isDirty = true;
				}
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000084 RID: 132 RVA: 0x0000884C File Offset: 0x00006A4C
		// (set) Token: 0x06000085 RID: 133 RVA: 0x00008864 File Offset: 0x00006A64
		public string BackgroundSpriteName
		{
			get
			{
				return this.backgroundSpriteName;
			}
			set
			{
				bool flag = this.backgroundSpriteName != value;
				if (flag)
				{
					bool flag2 = !string.IsNullOrEmpty(value);
					if (flag2)
					{
						this.backgroundSpriteName = value;
					}
					else
					{
						this.backgroundSpriteName = XUi.BlankTexture;
					}
					this.isDirty = true;
				}
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000086 RID: 134 RVA: 0x000088B0 File Offset: 0x00006AB0
		// (set) Token: 0x06000087 RID: 135 RVA: 0x000088C8 File Offset: 0x00006AC8
		public string ForegroundSpriteName
		{
			get
			{
				return this.foregroundSpriteName;
			}
			set
			{
				bool flag = this.foregroundSpriteName != value;
				if (flag)
				{
					this.foregroundSpriteName = ((!string.IsNullOrEmpty(value)) ? value : XUi.BlankTexture);
					this.isDirty = true;
				}
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00008905 File Offset: 0x00006B05
		public XUiV_ScrollBar(string _id) : base(_id)
		{
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00008938 File Offset: 0x00006B38
		public override void InitView()
		{
			bool flag = !string.IsNullOrEmpty(this.foregroundViewId);
			if (flag)
			{
				XUiController childById = this.controller.GetChildById(this.foregroundViewId);
				this.foregroundView = ((childById != null) ? childById.ViewComponent : null);
			}
			bool flag2 = !string.IsNullOrEmpty(this.backgroundViewId);
			if (flag2)
			{
				XUiController childById2 = this.controller.GetChildById(this.backgroundViewId);
				this.backgroundView = ((childById2 != null) ? childById2.ViewComponent : null);
			}
			bool flag3 = this.foregroundView == null;
			if (flag3)
			{
				XUiV_Sprite xuiV_Sprite = new XUiC_Scrollbar_Sprite(this.id + "_foreground");
				xuiV_Sprite.xui = base.xui;
				xuiV_Sprite.Controller = new XUiController(this.controller);
				xuiV_Sprite.Controller.xui = base.xui;
				xuiV_Sprite.Controller.WindowGroup = this.controller.WindowGroup;
				xuiV_Sprite.SetDefaults(this.controller);
				xuiV_Sprite.UIAtlas = "UIAtlas";
				xuiV_Sprite.Position = new Vector2i(0, 0);
				xuiV_Sprite.SpriteName = this.foregroundSpriteName;
				xuiV_Sprite.Color = this.foregroundSpriteColor;
				xuiV_Sprite.ForegroundLayer = true;
				xuiV_Sprite.Pivot = this.pivot;
				xuiV_Sprite.Type = UIBasicSprite.Type.Sliced;
				this.foregroundView = xuiV_Sprite;
			}
			bool flag4 = this.backgroundView == null;
			if (flag4)
			{
				XUiV_Sprite xuiV_Sprite2 = new XUiC_Scrollbar_Sprite(this.id + "_background");
				xuiV_Sprite2.xui = base.xui;
				xuiV_Sprite2.Controller = new XUiController(this.controller);
				xuiV_Sprite2.Controller.xui = base.xui;
				xuiV_Sprite2.Controller.WindowGroup = this.controller.WindowGroup;
				xuiV_Sprite2.SetDefaults(this.controller);
				xuiV_Sprite2.UIAtlas = "UIAtlas";
				xuiV_Sprite2.Position = new Vector2i(0, 0);
				xuiV_Sprite2.SpriteName = this.backgroundSpriteName;
				xuiV_Sprite2.Color = this.backgroundSpriteColor;
				xuiV_Sprite2.ForegroundLayer = true;
				xuiV_Sprite2.Pivot = this.pivot;
				xuiV_Sprite2.Type = UIBasicSprite.Type.Sliced;
				this.backgroundView = xuiV_Sprite2;
			}
			base.InitView();
			this.uiScrollBar = this.uiTransform.GetComponent<UIScrollBar>();
			this.panel = this.uiTransform.GetComponent<UIPanel>();
			this.controller.xui.OnBuilt += this.OnBuild;
			this.UpdateData();
			this.initialized = true;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00008BC2 File Offset: 0x00006DC2
		public override void Update(float _dt)
		{
			base.Update(_dt);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00008BD0 File Offset: 0x00006DD0
		public override void UpdateData()
		{
			base.UpdateData();
			bool isDirty = this.isDirty;
			if (isDirty)
			{
				this.panel.depth = this.depth;
				bool flag = this.foregroundCollider != null;
				if (flag)
				{
					this.foregroundCollider.enabled = true;
				}
				bool flag2 = this.backgroundCollider != null;
				if (flag2)
				{
					this.uiScrollBar.backgroundWidget.enabled = this.hasBackgroundSprite;
					this.backgroundCollider.enabled = this.hasBackgroundSprite;
				}
			}
			bool flag3 = !this.initialized;
			if (flag3)
			{
				this.uiTransform.localScale = Vector3.one;
				this.uiTransform.localPosition = new Vector3((float)this.position.x, (float)this.position.y, 0f);
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00008CAC File Offset: 0x00006EAC
		public override bool ParseAttribute(string attribute, string value, XUiController parent)
		{
			bool flag = attribute != null;
			bool result;
			if (flag)
			{
				uint num = global::<PrivateImplementationDetails>.ComputeStringHash(attribute);
				if (num <= 605851524U)
				{
					if (num != 192242078U)
					{
						if (num != 555731337U)
						{
							if (num == 605851524U)
							{
								if (attribute == "backgroundsprite")
								{
									this.BackgroundSpriteName = value;
									return true;
								}
							}
						}
						else if (attribute == "foregroundsprite")
						{
							this.ForegroundSpriteName = value;
							return true;
						}
					}
					else if (attribute == "backgroundname")
					{
						this.backgroundViewId = value;
						return true;
					}
				}
				else if (num <= 1141591383U)
				{
					if (num != 727013168U)
					{
						if (num == 1141591383U)
						{
							if (attribute == "foregroundcolor")
							{
								this.ForegroundSpriteColor = StringParsers.ParseColor32(value);
								return true;
							}
						}
					}
					else if (attribute == "backgroundcolor")
					{
						this.BackgroundSpriteColor = StringParsers.ParseColor32(value);
						return true;
					}
				}
				else if (num != 1944027971U)
				{
					if (num == 2157316278U)
					{
						if (attribute == "padding")
						{
							this.foregroundPadding = int.Parse(value);
							return true;
						}
					}
				}
				else if (attribute == "foregroundname")
				{
					this.foregroundViewId = value;
					return true;
				}
				result = base.ParseAttribute(attribute, value, parent);
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00008E26 File Offset: 0x00007026
		public override void CreateComponents(GameObject _go)
		{
			_go.AddComponent<UIScrollBar>();
			_go.AddComponent<UIPanel>();
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00008E38 File Offset: 0x00007038
		private void OnBuild()
		{
			this.uiScrollBar.setForegroundWidget(this.getSprite(this.foregroundView));
			this.uiScrollBar.setBackgroundWidget(this.getSprite(this.backgroundView));
			this.foregroundCollider = this.foregroundView.UiTransform.GetComponent<Collider>();
			this.backgroundCollider = this.backgroundView.UiTransform.GetComponent<Collider>();
			this.foregroundView.Size = new Vector2i(this.size.x - this.foregroundPadding, this.size.y - this.foregroundPadding);
			this.foregroundView.Depth = 2;
			this.backgroundView.Size = new Vector2i(this.size.x, this.size.y);
			this.backgroundView.Depth = 1;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00008F18 File Offset: 0x00007118
		private UISprite getSprite(XUiView view)
		{
			XUiV_Sprite xuiV_Sprite = view as XUiV_Sprite;
			bool flag = xuiV_Sprite != null;
			UISprite result;
			if (flag)
			{
				result = xuiV_Sprite.Sprite;
			}
			else
			{
				XUiV_Button xuiV_Button = view as XUiV_Button;
				bool flag2 = xuiV_Button != null;
				if (flag2)
				{
					result = xuiV_Button.Sprite;
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x04000020 RID: 32
		private const string TAG = "ScrollBar";

		// Token: 0x04000021 RID: 33
		protected UIScrollBar uiScrollBar;

		// Token: 0x04000022 RID: 34
		protected UIPanel panel;

		// Token: 0x04000023 RID: 35
		private Collider foregroundCollider;

		// Token: 0x04000024 RID: 36
		private Collider backgroundCollider;

		// Token: 0x04000025 RID: 37
		protected XUiView foregroundView;

		// Token: 0x04000026 RID: 38
		protected XUiView backgroundView;

		// Token: 0x04000027 RID: 39
		private string foregroundViewId;

		// Token: 0x04000028 RID: 40
		private string backgroundViewId;

		// Token: 0x04000029 RID: 41
		private string backgroundSpriteName = XUi.BlankTexture;

		// Token: 0x0400002A RID: 42
		private Color backgroundSpriteColor = Color.white;

		// Token: 0x0400002B RID: 43
		private string foregroundSpriteName;

		// Token: 0x0400002C RID: 44
		private Color foregroundSpriteColor = Color.white;

		// Token: 0x0400002D RID: 45
		private int foregroundPadding = 5;
	}
}
