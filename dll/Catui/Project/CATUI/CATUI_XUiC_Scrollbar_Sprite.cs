using System;

namespace Views
{
	// Token: 0x02000024 RID: 36
	public class XUiC_Scrollbar_Sprite : XUiV_Sprite
	{
		// Token: 0x0600006C RID: 108 RVA: 0x00008372 File Offset: 0x00006572
		public XUiC_Scrollbar_Sprite(string _id) : base(_id)
		{
		}

		// Token: 0x0600006D RID: 109 RVA: 0x0000837D File Offset: 0x0000657D
		public override void UpdateData()
		{
			this.color.a = this.sprite.alpha;
			base.UpdateData();
			this.sprite.depth = this.depth;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000083B0 File Offset: 0x000065B0
		public override void RefreshBoxCollider()
		{
			bool flag = this.sprite != null && !this.sprite.autoResizeBoxCollider;
			if (flag)
			{
				base.RefreshBoxCollider();
			}
		}

		// Token: 0x04000019 RID: 25
		private const string TAG = "ScrollBar Sprite";
	}
}
