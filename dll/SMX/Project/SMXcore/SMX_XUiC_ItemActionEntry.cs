using System;

namespace SMXcore
{
	// Token: 0x0200000F RID: 15
	public class XUiC_ItemActionEntry : XUiC_ItemActionEntry
	{
		// Token: 0x06000044 RID: 68 RVA: 0x00005B90 File Offset: 0x00003D90
		public override void Init()
		{
			base.Init();
			this.background.Controller.OnPress += this.OnPress;
			this.background.Controller.OnHover += this.OnHover;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00005BE0 File Offset: 0x00003DE0
		public override void Update(float _dt)
		{
			this.spriteName = ((this.itemActionEntry != null) ? this.itemActionEntry.IconName : "");
			base.Update(_dt);
			bool flag = this.background.SpriteName != this.spriteName;
			if (flag)
			{
				this.background.SpriteName = this.spriteName;
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00005C44 File Offset: 0x00003E44
		private new void OnHover(XUiController sender, bool isOver)
		{
			XUiV_Sprite xuiV_Sprite = (XUiV_Sprite)sender.ViewComponent;
			xuiV_Sprite.SpriteName = this.spriteName;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00005C6B File Offset: 0x00003E6B
		private new void OnPress(XUiController sender, int mouseButton)
		{
			this.background.SpriteName = this.spriteName;
		}

		// Token: 0x04000047 RID: 71
		private string spriteName;
	}
}
