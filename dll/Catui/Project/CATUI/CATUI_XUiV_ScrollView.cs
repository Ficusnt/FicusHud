using System;
using System.Collections.Generic;
using UnityEngine;

namespace Views
{
	// Token: 0x02000027 RID: 39
	public class XUiV_ScrollView : XUiView
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00008F60 File Offset: 0x00007160
		public UIScrollView UiScrollView
		{
			get
			{
				return this.uiScrollView;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00008F78 File Offset: 0x00007178
		// (set) Token: 0x06000092 RID: 146 RVA: 0x00008F90 File Offset: 0x00007190
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
					this.isDirty = true;
				}
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00008FC0 File Offset: 0x000071C0
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00008FD8 File Offset: 0x000071D8
		public XUiV_ScrollViewContainer Container
		{
			get
			{
				return this.container;
			}
			internal set
			{
				this.container = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00008FE4 File Offset: 0x000071E4
		// (set) Token: 0x06000096 RID: 150 RVA: 0x00008FFC File Offset: 0x000071FC
		public UIDrawCall.Clipping Clipping
		{
			get
			{
				return this.clipping;
			}
			set
			{
				bool flag = this.clipping != value;
				if (flag)
				{
					this.clipping = value;
					this.isDirty = true;
				}
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000097 RID: 151 RVA: 0x0000902C File Offset: 0x0000722C
		// (set) Token: 0x06000098 RID: 152 RVA: 0x00009044 File Offset: 0x00007244
		public UIScrollView.Movement Movement
		{
			get
			{
				return this.movement;
			}
			set
			{
				bool flag = this.movement != value;
				if (flag)
				{
					this.movement = value;
					this.isDirty = true;
				}
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00009074 File Offset: 0x00007274
		// (set) Token: 0x0600009A RID: 154 RVA: 0x0000908C File Offset: 0x0000728C
		public Vector2 ClippingSoftness
		{
			get
			{
				return this.clippingSoftness;
			}
			set
			{
				bool flag = this.clippingSoftness != value;
				if (flag)
				{
					this.clippingSoftness = value;
					this.isDirty = true;
				}
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600009B RID: 155 RVA: 0x000090BC File Offset: 0x000072BC
		// (set) Token: 0x0600009C RID: 156 RVA: 0x000090D4 File Offset: 0x000072D4
		public UIScrollView.DragEffect DragEffect
		{
			get
			{
				return this.dragEffect;
			}
			set
			{
				bool flag = this.dragEffect != value;
				if (flag)
				{
					this.dragEffect = value;
					this.isDirty = true;
				}
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00009104 File Offset: 0x00007304
		// (set) Token: 0x0600009E RID: 158 RVA: 0x0000911C File Offset: 0x0000731C
		public float ScrollWheelFactor
		{
			get
			{
				return this.scrollWheelFactor;
			}
			set
			{
				bool flag = this.scrollWheelFactor != value;
				if (flag)
				{
					this.scrollWheelFactor = value;
					this.isDirty = true;
				}
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600009F RID: 159 RVA: 0x0000914A File Offset: 0x0000734A
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x00009152 File Offset: 0x00007352
		public bool ResetPositionOnOpen
		{
			get
			{
				return this.resetPositionOnOpen;
			}
			set
			{
				this.resetPositionOnOpen = value;
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000915B File Offset: 0x0000735B
		public XUiV_ScrollView(string _id) : base(_id)
		{
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00009198 File Offset: 0x00007398
		public override void InitView()
		{
			this.EventOnScroll = true;
			base.InitView();
			this.uiScrollView = this.uiTransform.GetComponent<UIScrollView>();
			this.UpdateData();
			this.controller.xui.OnBuilt += delegate()
			{
				this.AddOnScrollListeners(this.controller);
				this.SetScrollbar();
			};
			this.initialized = true;
			this.collider.enabled = false;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00009200 File Offset: 0x00007400
		public override void UpdateData()
		{
			bool isDirty = this.isDirty;
			if (isDirty)
			{
				this.uiScrollView.panel.depth = this.depth;
				this.uiScrollView.panel.softBorderPadding = true;
				this.uiScrollView.contentPivot = this.pivot;
				this.uiScrollView.dragEffect = this.dragEffect;
				bool flag = this.clipping > UIDrawCall.Clipping.None;
				if (flag)
				{
					bool flag2 = this.clippingCenter == new Vector2(-10000f, -10000f);
					if (flag2)
					{
						this.clippingCenter = new Vector2((float)(this.size.x / 2), (float)(-(float)this.size.y / 2));
					}
					bool flag3 = this.clippingSize == new Vector2(-10000f, -10000f);
					if (flag3)
					{
						this.clippingSize = new Vector2((float)this.size.x, (float)this.size.y);
					}
					this.UpdateClipping();
				}
			}
			bool flag4 = !this.initialized;
			if (flag4)
			{
				this.uiTransform.localScale = Vector3.one;
				this.uiTransform.localPosition = new Vector3((float)this.position.x, (float)this.position.y, 0f);
				this.uiScrollView.scrollWheelFactor = this.scrollWheelFactor;
				this.uiScrollView.movement = this.movement;
				this.uiScrollView.disableDragIfFits = true;
			}
			bool flag5 = this.opened;
			if (flag5)
			{
				this.opened = false;
				this.ResetPosition();
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000093A5 File Offset: 0x000075A5
		public override void CreateComponents(GameObject go)
		{
			go.AddComponent<UIPanel>();
			go.AddComponent<UIScrollView>();
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000093B6 File Offset: 0x000075B6
		public override void OnOpen()
		{
			base.OnOpen();
			this.opened = true;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000093C7 File Offset: 0x000075C7
		public new void OnScroll(GameObject go, float delta)
		{
			this.uiScrollView.Scroll(delta);
			this.controller.Scrolled(delta);
			this.container.OnScrollViewScrolled(go, delta);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000093F4 File Offset: 0x000075F4
		public void ResetPosition()
		{
			bool flag = this.resetPositionOnOpen && (this.uiScrollView.shouldMoveVertically || this.uiScrollView.shouldMoveHorizontally);
			if (flag)
			{
				this.uiScrollView.ResetPosition();
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000943A File Offset: 0x0000763A
		public void ForceResetPosition()
		{
			this.uiScrollView.ResetPosition();
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000944C File Offset: 0x0000764C
		private void SetScrollbar()
		{
			bool flag = this.uiScrollView == null || this.uiScrollBar == null;
			if (!flag)
			{
				EventDelegate.Add(this.uiScrollBar.onChange, new EventDelegate.Callback(this.uiScrollView.OnScrollBar));
				this.uiScrollBar.BroadcastMessage("CacheDefaultColor", SendMessageOptions.DontRequireReceiver);
				bool flag2 = this.movement == UIScrollView.Movement.Vertical;
				if (flag2)
				{
					this.uiScrollView.verticalScrollBar = this.uiScrollBar;
					this.uiScrollBar.alpha = ((this.uiScrollView.showScrollBars == UIScrollView.ShowCondition.Always || this.uiScrollView.shouldMoveVertically) ? 1f : 0f);
					this.uiScrollView.horizontalScrollBar = null;
				}
				bool flag3 = this.movement == UIScrollView.Movement.Horizontal;
				if (flag3)
				{
					this.uiScrollView.horizontalScrollBar = this.uiScrollBar;
					this.uiScrollBar.alpha = ((this.uiScrollView.showScrollBars == UIScrollView.ShowCondition.Always || this.uiScrollView.shouldMoveHorizontally) ? 1f : 0f);
					this.uiScrollView.verticalScrollBar = null;
				}
				bool flag4 = this.uiScrollBar.backgroundWidget != null;
				if (flag4)
				{
					this.uiScrollBar.backgroundWidget.autoResizeBoxCollider = true;
				}
				bool flag5 = EventDelegate.IsValid(this.uiScrollBar.onChange);
				if (flag5)
				{
					EventDelegate.Execute(this.uiScrollBar.onChange);
				}
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000095C0 File Offset: 0x000077C0
		private void AddOnScrollListeners(XUiController controller)
		{
			List<XUiController> children = controller.Children;
			foreach (XUiController xuiController in children)
			{
				XUiView viewComponent = xuiController.ViewComponent;
				bool flag = viewComponent != null && viewComponent.HasEvent && !viewComponent.EventOnScroll;
				if (flag)
				{
					UIEventListener uieventListener = UIEventListener.Get(viewComponent.UiTransform.gameObject);
					UIEventListener uieventListener2 = uieventListener;
					uieventListener2.onScroll = (UIEventListener.FloatDelegate)Delegate.Combine(uieventListener2.onScroll, new UIEventListener.FloatDelegate(this.OnScroll));
				}
				this.AddOnScrollListeners(xuiController);
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00009678 File Offset: 0x00007878
		private void UpdateClipping()
		{
			bool flag = this.clipping > UIDrawCall.Clipping.None;
			if (flag)
			{
				bool flag2 = this.uiScrollView.panel.clipping != this.clipping;
				if (flag2)
				{
					this.uiScrollView.panel.clipping = this.clipping;
				}
				bool flag3 = this.uiScrollView.panel.clipSoftness != this.clippingSoftness;
				if (flag3)
				{
					this.uiScrollView.panel.clipSoftness = this.clippingSoftness;
				}
				bool flag4 = this.clippingSize.x < 0f;
				if (flag4)
				{
					this.clippingSize.x = 0f;
				}
				bool flag5 = this.clippingSize.y < 0f;
				if (flag5)
				{
					this.clippingSize.y = 0f;
				}
				Vector4 vector = new Vector4(this.clippingCenter.x, this.clippingCenter.y, this.clippingSize.x, this.clippingSize.y);
				bool flag6 = this.uiScrollView.panel.baseClipRegion != vector;
				if (flag6)
				{
					this.uiScrollView.panel.baseClipRegion = vector;
				}
			}
		}

		// Token: 0x0400002E RID: 46
		private const string TAG = "ScrollView";

		// Token: 0x0400002F RID: 47
		protected UIScrollView uiScrollView;

		// Token: 0x04000030 RID: 48
		protected UIScrollBar uiScrollBar;

		// Token: 0x04000031 RID: 49
		protected XUiV_ScrollViewContainer container;

		// Token: 0x04000032 RID: 50
		private bool opened;

		// Token: 0x04000033 RID: 51
		private UIScrollView.Movement movement;

		// Token: 0x04000034 RID: 52
		private UIScrollView.DragEffect dragEffect;

		// Token: 0x04000035 RID: 53
		private UIDrawCall.Clipping clipping = UIDrawCall.Clipping.SoftClip;

		// Token: 0x04000036 RID: 54
		private bool resetPositionOnOpen;

		// Token: 0x04000037 RID: 55
		private Vector2 clippingSize = new Vector2(-10000f, -10000f);

		// Token: 0x04000038 RID: 56
		private Vector2 clippingCenter = new Vector2(-10000f, -10000f);

		// Token: 0x04000039 RID: 57
		private Vector2 clippingSoftness;

		// Token: 0x0400003A RID: 58
		private float scrollWheelFactor;
	}
}
