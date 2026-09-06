using System;

namespace SMXcore
{
	// Token: 0x0200000B RID: 11
	public class XUiC_ItemDronePartStack : XUiC_ItemDronePartStack
	{
		// Token: 0x06000038 RID: 56 RVA: 0x0000582B File Offset: 0x00003A2B
		public override void SelectedChanged(bool isSelected)
		{
			base.SetColor(isSelected ? this.selectColor : XUiC_BasePartStack.backgroundColor);
			((XUiV_Sprite)this.background.ViewComponent).SpriteName = (isSelected ? "smxlib_slot_frame_narrow" : "smxlib_slot_frame_narrow");
		}
	}
}
