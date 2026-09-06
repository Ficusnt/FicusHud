using System;

namespace SMXcore
{
	// Token: 0x02000013 RID: 19
	public class XUiC_MapPopupList : XUiController
	{
		// Token: 0x0600004F RID: 79 RVA: 0x00005D48 File Offset: 0x00003F48
		public override void Init()
		{
			base.Init();
			this.children[0].OnPress += this.onPressEntry1;
			this.children[1].OnPress += this.onPressEntry2;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00005D99 File Offset: 0x00003F99
		private void onPressEntry1(XUiController _sender, int _mouseButton)
		{
			((XUiC_MapArea)base.xui.GetWindow("mapArea").Controller).OnSetWaypoint();
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00005DBC File Offset: 0x00003FBC
		private void onPressEntry2(XUiController _sender, int _mouseButton)
		{
			bool flag = _sender is XUiC_MapPopupEntry;
			if (flag)
			{
				XUiV_Window window = base.xui.GetWindow("mapAreaSetWaypoint");
				XUiV_Window window2 = base.xui.GetWindow("mapAreaChooseWaypoint");
				int num = 46;
				window2.Position = window.Position + new Vector2i(199, -num);
				bool flag2 = window2.Position.y < 0;
				if (flag2)
				{
					window2.Position = new Vector2i(window2.Position.x, window2.Position.y + window2.Size.y);
				}
				window2.IsVisible = true;
				window2.Controller.GetChildByType<XUiC_MapSubPopupList>().ResetList();
			}
		}
	}
}
