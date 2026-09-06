using System;
using UnityEngine;

namespace SMXcore
{
	// Token: 0x02000014 RID: 20
	public class XUiC_MapSubPopupEntry : XUiController
	{
		// Token: 0x06000053 RID: 83 RVA: 0x00005E85 File Offset: 0x00004085
		public override void Init()
		{
			base.Init();
			base.OnPress += this.onPressed;
			base.OnVisiblity += this.XUiC_MapSubPopupEntry_OnVisiblity;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00005EB5 File Offset: 0x000040B5
		private void XUiC_MapSubPopupEntry_OnVisiblity(XUiController _sender, bool _visible)
		{
			this.select(false);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00005EC0 File Offset: 0x000040C0
		public void SetIndex(int _idx)
		{
			this.index = _idx;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00005ECC File Offset: 0x000040CC
		public void SetSpriteName(string _s)
		{
			this.spriteName = _s;
			for (int i = 0; i < base.Parent.Children.Count; i++)
			{
				XUiView viewComponent = base.Parent.Children[i].ViewComponent;
				bool flag = viewComponent.ID.EqualsCaseInsensitive("icon");
				if (flag)
				{
					((XUiV_Sprite)viewComponent).SpriteName = _s;
				}
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00005F3C File Offset: 0x0000413C
		public override void OnHovered(bool _isOver)
		{
			this.select(_isOver);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00005F48 File Offset: 0x00004148
		private void onPressed(XUiController _sender, int _mouseButton)
		{
			this.select(true);
			((XUiC_MapArea)base.xui.GetWindow("mapArea").Controller).OnWaypointEntryChosen(this.spriteName);
			XUiC_MapEnterWaypoint childByType = base.xui.GetWindow("mapAreaEnterWaypointName").Controller.GetChildByType<XUiC_MapEnterWaypoint>();
			XUiV_Window window = base.xui.GetWindow("mapAreaSetWaypoint");
			Vector2i position = window.Position + new Vector2i(0, -window.Size.y);
			childByType.Show(position);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00005FD8 File Offset: 0x000041D8
		private void select(bool _bSelect)
		{
			XUiV_Sprite xuiV_Sprite = this.viewComponent as XUiV_Sprite;
			bool flag = xuiV_Sprite != null;
			if (flag)
			{
				xuiV_Sprite.Color = (_bSelect ? new Color32(96, 96, 96, byte.MaxValue) : new Color32(64, 64, 64, byte.MaxValue));
				xuiV_Sprite.SpriteName = (_bSelect ? "smxui_button_background" : "smxui_button_background");
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00006043 File Offset: 0x00004243
		public void Reset()
		{
			this.select(false);
		}

		// Token: 0x04000048 RID: 72
		private int index;

		// Token: 0x04000049 RID: 73
		private string spriteName;
	}
}
