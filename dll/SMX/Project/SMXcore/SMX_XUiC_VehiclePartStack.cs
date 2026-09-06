using System;

namespace SMXcore
{
	// Token: 0x0200000E RID: 14
	public class XUiC_VehiclePartStack : XUiC_VehiclePartStack
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00005B48 File Offset: 0x00003D48
		public override void SelectedChanged(bool isSelected)
		{
			base.SetColor(isSelected ? this.selectColor : XUiC_BasePartStack.backgroundColor);
			((XUiV_Sprite)this.background.ViewComponent).SpriteName = (isSelected ? "smxlib_slot_frame_narrow" : "smxlib_slot_frame_narrow");
		}
	}
}
