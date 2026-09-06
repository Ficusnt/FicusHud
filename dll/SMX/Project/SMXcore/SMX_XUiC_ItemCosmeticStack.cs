using System;

namespace SMXcore
{
	// Token: 0x0200000A RID: 10
	public class XUiC_ItemCosmeticStack : XUiC_ItemCosmeticStack
	{
		// Token: 0x06000036 RID: 54 RVA: 0x000057E3 File Offset: 0x000039E3
		public override void SelectedChanged(bool isSelected)
		{
			base.SetColor(isSelected ? this.selectColor : XUiC_BasePartStack.backgroundColor);
			((XUiV_Sprite)this.background.ViewComponent).SpriteName = (isSelected ? "smxlib_slot_frame_narrow" : "smxlib_slot_frame_narrow");
		}
	}
}
