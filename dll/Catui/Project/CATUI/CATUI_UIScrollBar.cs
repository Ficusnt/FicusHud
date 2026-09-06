using System;
using UnityEngine;

namespace Views
{
	// Token: 0x02000022 RID: 34
	public class UIScrollBar : UIScrollBar
	{
		// Token: 0x0600005E RID: 94 RVA: 0x00007F44 File Offset: 0x00006144
		public void setBackgroundWidget(UIWidget background)
		{
			bool flag = base.backgroundWidget != background;
			if (flag)
			{
				base.backgroundWidget = background;
				bool flag2 = !background.GetComponent<Collider>();
				if (!flag2)
				{
					UIEventListener uieventListener = UIEventListener.Get(background.gameObject);
					UIEventListener uieventListener2 = uieventListener;
					uieventListener2.onPress = (UIEventListener.BoolDelegate)Delegate.Combine(uieventListener2.onPress, new UIEventListener.BoolDelegate(this.OnPressBackground));
					UIEventListener uieventListener3 = uieventListener;
					uieventListener3.onDrag = (UIEventListener.VectorDelegate)Delegate.Combine(uieventListener3.onDrag, new UIEventListener.VectorDelegate(this.OnDragBackground));
					background.autoResizeBoxCollider = true;
				}
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00007FD8 File Offset: 0x000061D8
		public void setForegroundWidget(UIWidget foreground)
		{
			bool flag = base.foregroundWidget != foreground;
			if (flag)
			{
				base.foregroundWidget = foreground;
				bool flag2 = !foreground.GetComponent<Collider>();
				if (!flag2)
				{
					UIEventListener uieventListener = UIEventListener.Get(foreground.gameObject);
					UIEventListener uieventListener2 = uieventListener;
					uieventListener2.onPress = (UIEventListener.BoolDelegate)Delegate.Combine(uieventListener2.onPress, new UIEventListener.BoolDelegate(this.OnPressForeground));
					UIEventListener uieventListener3 = uieventListener;
					uieventListener3.onDrag = (UIEventListener.VectorDelegate)Delegate.Combine(uieventListener3.onDrag, new UIEventListener.VectorDelegate(this.OnDragForeground));
					foreground.autoResizeBoxCollider = true;
				}
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000806C File Offset: 0x0000626C
		protected new void OnPressBackground(GameObject go, bool isPressed)
		{
			bool flag = UICamera.currentScheme != UICamera.ControlScheme.Controller;
			if (flag)
			{
				this.mCam = UICamera.currentCamera;
				base.value = base.ScreenToValue(UICamera.lastEventPosition);
				bool flag2 = !isPressed && this.onDragFinished != null;
				if (flag2)
				{
					this.onDragFinished();
				}
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000080CC File Offset: 0x000062CC
		protected new void OnDragBackground(GameObject go, Vector2 delta)
		{
			bool flag = UICamera.currentScheme != UICamera.ControlScheme.Controller;
			if (flag)
			{
				this.mCam = UICamera.currentCamera;
				base.value = base.ScreenToValue(UICamera.lastEventPosition);
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00008108 File Offset: 0x00006308
		protected new void OnPressForeground(GameObject go, bool isPressed)
		{
			bool flag = UICamera.currentScheme != UICamera.ControlScheme.Controller;
			if (flag)
			{
				this.mCam = UICamera.currentCamera;
				if (isPressed)
				{
					this.mOffset = ((this.mFG == null) ? 0f : (base.value - base.ScreenToValue(UICamera.lastEventPosition)));
				}
				else
				{
					bool flag2 = this.onDragFinished != null;
					if (flag2)
					{
						this.onDragFinished();
					}
				}
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00008184 File Offset: 0x00006384
		protected new void OnDragForeground(GameObject go, Vector2 delta)
		{
			bool flag = UICamera.currentScheme != UICamera.ControlScheme.Controller;
			if (flag)
			{
				this.mCam = UICamera.currentCamera;
				base.value = this.mOffset + base.ScreenToValue(UICamera.lastEventPosition);
			}
		}

		// Token: 0x04000016 RID: 22
		private const string TAG = "XUi_UIScrollBar";
	}
}
