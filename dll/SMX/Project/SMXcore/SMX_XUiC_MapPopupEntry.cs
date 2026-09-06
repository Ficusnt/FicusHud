using System;
using UnityEngine;

namespace SMXcore
{
	// Token: 0x02000012 RID: 18
	public class XUiC_MapPopupEntry : XUiController
	{
		// Token: 0x0600004D RID: 77 RVA: 0x00005CBC File Offset: 0x00003EBC
		public override void OnHovered(bool _isOver)
		{
			XUiV_Sprite xuiV_Sprite = base.GetChildById("background").ViewComponent as XUiV_Sprite;
			bool flag = xuiV_Sprite != null;
			if (flag)
			{
				xuiV_Sprite.Color = (_isOver ? new Color32(96, 96, 96, byte.MaxValue) : new Color32(175, 30, 25, byte.MaxValue));
				xuiV_Sprite.SpriteName = (_isOver ? "smxui_button_background" : "smxui_button_background");
			}
			base.OnHovered(_isOver);
		}
	}
}
