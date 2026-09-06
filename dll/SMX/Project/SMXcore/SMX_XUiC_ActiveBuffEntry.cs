using System;
using UnityEngine;

namespace SMXcore
{
	// Token: 0x02000004 RID: 4
	public class XUiC_ActiveBuffEntry : XUiC_ActiveBuffEntry
	{
		// Token: 0x06000008 RID: 8 RVA: 0x00002108 File Offset: 0x00000308
		public override bool GetBindingValueInternal(ref string value, string bindingName)
		{
			bool result;
			if (!(bindingName == "hasbuff"))
			{
				result = base.GetBindingValueInternal(ref value, bindingName);
			}
			else
			{
				value = ((base.Notification != null && base.Notification.Buff != null) ? "true" : "false");
				result = true;
			}
			return result;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000215C File Offset: 0x0000035C
		public override void SelectedChanged(bool isSelected)
		{
			if (isSelected)
			{
				base.InfoWindow.SetBuffInfo(this);
			}
			bool flag = this.background != null;
			if (flag)
			{
				this.background.Color = (isSelected ? new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue) : new Color32(96, 96, 96, byte.MaxValue));
				this.background.SpriteName = (isSelected ? "smxlib_slot_frame_narrow" : "smxlib_slot_frame_narrow");
			}
		}
	}
}
