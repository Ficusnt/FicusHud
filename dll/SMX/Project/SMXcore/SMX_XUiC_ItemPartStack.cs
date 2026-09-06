using System;

namespace SMXcore
{
	// Token: 0x0200000C RID: 12
	public class XUiC_ItemPartStack : XUiC_ItemPartStack
	{
		// Token: 0x0600003A RID: 58 RVA: 0x00005873 File Offset: 0x00003A73
		public override void SelectedChanged(bool isSelected)
		{
			base.SetColor(isSelected ? this.selectColor : XUiC_BasePartStack.backgroundColor);
			((XUiV_Sprite)this.background.ViewComponent).SpriteName = (isSelected ? "smxlib_slot_frame_narrow" : "smxlib_slot_frame_narrow");
		}
	}
}
