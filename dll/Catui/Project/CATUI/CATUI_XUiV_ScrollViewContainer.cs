using System;
using System.Collections.Generic;
using UnityEngine;

namespace Views
{
	// Token: 0x02000028 RID: 40
	public class XUiV_ScrollViewContainer : XUiView
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000AD RID: 173 RVA: 0x000097D8 File Offset: 0x000079D8
		public XUiV_ScrollView ScrollView
		{
			get
			{
				return this.scrollView;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000AE RID: 174 RVA: 0x000097F0 File Offset: 0x000079F0
		public XUiV_ScrollBar ScrollBar
		{
			get
			{
				return this.scrollBar;
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00009808 File Offset: 0x00007A08
		public XUiV_ScrollViewContainer(string id) : base(id)
		{
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00009848 File Offset: 0x00007A48
		public override void InitView()
		{
			this.scrollViewId = this.id;
			this.id += "Container";
			this.scrollView = new XUiV_ScrollView(this.scrollViewId);
			this.scrollView.xui = base.xui;
			this.scrollView.Controller = new XUiController();
			this.scrollView.Controller.xui = base.xui;
			this.scrollView.SetDefaults(this.controller);
			this.scrollView.Controller.WindowGroup = this.controller.WindowGroup;
			this.scrollView.Container = this;
			this.SetScrollViewChildren();
			base.InitView();
			this.widget = this.uiTransform.GetComponent<UIWidget>();
			bool flag = this.scrollbarId != null;
			if (flag)
			{
				XUiController childById = this.controller.Parent.GetChildById(this.scrollbarId);
				this.scrollBar = (((childById != null) ? childById.ViewComponent : null) as XUiV_ScrollBar);
			}
			this.grid = this.FindGrid(this.controller);
			bool flag2 = this.grid != null;
			if (flag2)
			{
				this.grid.OnSizeChanged += this.OnGridSizeChanged;
			}
			this.UpdateData();
			this.initialized = true;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000099A0 File Offset: 0x00007BA0
		public override void CreateComponents(GameObject _go)
		{
			_go.AddComponent<UIWidget>();
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000099AC File Offset: 0x00007BAC
		public override void UpdateData()
		{
			base.UpdateData();
			bool isDirty = this.isDirty;
			if (isDirty)
			{
				bool flag = this.collider != null;
				if (flag)
				{
					float x = (float)this.size.x * 0.5f;
					float num = (float)this.size.y * 0.5f;
					this.collider.center = new Vector3(x, -num, 0f);
					this.collider.size = new Vector3((float)this.size.x * this.colliderScale, (float)this.size.y * this.colliderScale, 0f);
					this.collider.enabled = true;
				}
				this.scrollView.Position = Vector2i.zero;
				this.scrollView.Depth = this.depth + 1;
				this.scrollView.Pivot = this.pivot;
				this.scrollView.Size = this.size;
				this.scrollView.ClippingSoftness = this.clippingSoftness;
				this.scrollView.Clipping = this.clipping;
				this.scrollView.Movement = this.scrollDirection;
				this.scrollView.ResetPositionOnOpen = this.resetPositionOnOpen;
				this.scrollView.DragEffect = (this.overScroll ? UIScrollView.DragEffect.MomentumAndSpring : UIScrollView.DragEffect.Momentum);
				this.scrollView.ScrollWheelFactor = this.scrollWheelFactor;
				this.uiTransform.localScale = Vector3.one;
			}
			bool flag2 = !this.initialized;
			if (flag2)
			{
				bool flag3 = this.scrollBar != null && this.scrollBar.UiScrollBar != null;
				if (flag3)
				{
					this.scrollView.UiScrollBar = this.scrollBar.UiScrollBar;
					this.scrollBar.UiScrollBar.fillDirection = ((this.scrollDirection == UIScrollView.Movement.Vertical) ? UIProgressBar.FillDirection.TopToBottom : UIProgressBar.FillDirection.LeftToRight);
				}
				this.widget.depth = this.depth;
				this.widget.width = this.size.x;
				this.widget.height = this.size.y;
				UIEventListener uieventListener = UIEventListener.Get(this.uiTransform.gameObject);
				UIEventListener uieventListener2 = uieventListener;
				uieventListener2.onScroll = (UIEventListener.FloatDelegate)Delegate.Combine(uieventListener2.onScroll, new UIEventListener.FloatDelegate(this.scrollView.OnScroll));
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00009C20 File Offset: 0x00007E20
		public override bool ParseAttribute(string attribute, string value, XUiController parent)
		{
			bool flag = attribute != null;
			bool result;
			if (flag)
			{
				uint num = global::<PrivateImplementationDetails>.ComputeStringHash(attribute);
				if (num <= 2324115316U)
				{
					if (num != 808990465U)
					{
						if (num != 954579279U)
						{
							if (num == 2324115316U)
							{
								if (attribute == "scroll_speed")
								{
									float.TryParse(value, out this.scrollWheelFactor);
									this.isDirty = true;
									return true;
								}
							}
						}
						else if (attribute == "clipping")
						{
							this.clipping = EnumUtils.Parse<UIDrawCall.Clipping>(value, false);
							this.isDirty = true;
							return true;
						}
					}
					else if (attribute == "over_scroll")
					{
						this.overScroll = StringParsers.ParseBool(value, 0, -1, true);
						this.isDirty = true;
						return true;
					}
				}
				else if (num <= 3585523833U)
				{
					if (num != 2728752196U)
					{
						if (num == 3585523833U)
						{
							if (attribute == "scrollbar")
							{
								this.scrollbarId = value;
								return true;
							}
						}
					}
					else if (attribute == "clippingsoftness")
					{
						this.clippingSoftness = StringParsers.ParseVector2(value);
						this.isDirty = true;
						return true;
					}
				}
				else if (num != 3746155054U)
				{
					if (num == 4279776162U)
					{
						if (attribute == "reset_position")
						{
							this.resetPositionOnOpen = StringParsers.ParseBool(value, 0, -1, true);
							return true;
						}
					}
				}
				else if (attribute == "scroll_direction")
				{
					this.scrollDirection = EnumUtils.Parse<UIScrollView.Movement>(value, false);
					this.isDirty = true;
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

		// Token: 0x060000B4 RID: 180 RVA: 0x00009DF2 File Offset: 0x00007FF2
		internal void OnScrollViewScrolled(GameObject _go, float _delta)
		{
			this.controller.Scrolled(_delta);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00009E04 File Offset: 0x00008004
		private void SetScrollViewChildren()
		{
			List<XUiController> children = this.controller.Children;
			foreach (XUiController xuiController in children)
			{
				xuiController.Parent = this.scrollView.Controller;
				this.scrollView.Controller.AddChild(xuiController);
			}
			children.Clear();
			this.scrollView.Controller.Parent = this.controller;
			this.controller.AddChild(this.scrollView.Controller);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00009EB8 File Offset: 0x000080B8
		private XUiV_Grid FindGrid(XUiController controller)
		{
			Queue<XUiController> queue = new Queue<XUiController>();
			queue.Enqueue(controller);
			XUiV_Grid xuiV_Grid = null;
			while (queue.Count > 0)
			{
				XUiController xuiController = queue.Dequeue();
				xuiV_Grid = (xuiController.ViewComponent as XUiV_Grid);
				bool flag = xuiV_Grid != null;
				if (flag)
				{
					return xuiV_Grid;
				}
				foreach (XUiController item in xuiController.Children)
				{
					queue.Enqueue(item);
				}
			}
			return xuiV_Grid;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00009F60 File Offset: 0x00008160
		private void OnGridSizeChanged(Vector2Int cells, Vector2 size)
		{
			this.scrollView.ForceResetPosition();
		}

		// Token: 0x0400003B RID: 59
		private const string TAG = "ScrollViewContainer";

		// Token: 0x0400003C RID: 60
		protected XUiV_ScrollView scrollView;

		// Token: 0x0400003D RID: 61
		protected XUiV_ScrollBar scrollBar;

		// Token: 0x0400003E RID: 62
		protected XUiV_Grid grid;

		// Token: 0x0400003F RID: 63
		protected UIWidget widget;

		// Token: 0x04000040 RID: 64
		private UIScrollView.Movement scrollDirection = UIScrollView.Movement.Vertical;

		// Token: 0x04000041 RID: 65
		private Vector2 clippingSoftness = Vector2.zero;

		// Token: 0x04000042 RID: 66
		private UIDrawCall.Clipping clipping = UIDrawCall.Clipping.SoftClip;

		// Token: 0x04000043 RID: 67
		private string scrollbarId;

		// Token: 0x04000044 RID: 68
		private string scrollViewId;

		// Token: 0x04000045 RID: 69
		private bool resetPositionOnOpen = false;

		// Token: 0x04000046 RID: 70
		private bool overScroll = false;

		// Token: 0x04000047 RID: 71
		private float scrollWheelFactor = 2.5f;
	}
}
