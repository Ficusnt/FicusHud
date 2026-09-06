using System;
using UnityEngine;

namespace SMXcore
{
	// Token: 0x02000026 RID: 38
	public class XUiC_TraderItemEntry : XUiC_TraderItemEntry
	{
		// Token: 0x0600012A RID: 298 RVA: 0x0000C218 File Offset: 0x0000A418
		public override void Update(float _dt)
		{
			base.Update(_dt);
			bool isDirty = this.isDirty;
			if (isDirty)
			{
				this.isDirty = false;
				base.ViewComponent.IsVisible = base.ViewComponent.Enabled;
			}
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000C258 File Offset: 0x0000A458
		public override bool ParseAttribute(string name, string value, XUiController parent)
		{
			bool result;
			if (!(name == "selectedbackgroundsprite"))
			{
				if (!(name == "notselectedbackgroundsprite"))
				{
					if (!(name == "selectedbackgroundcolor"))
					{
						if (!(name == "notselectedbackgroundcolor"))
						{
							result = base.ParseAttribute(name, value, parent);
						}
						else
						{
							this.notSelectedBackgroundColor = StringParsers.ParseColor32(value);
							result = true;
						}
					}
					else
					{
						this.selectedBackgroundColor = StringParsers.ParseColor32(value);
						result = true;
					}
				}
				else
				{
					this.notSelectedSpriteName = value;
					result = true;
				}
			}
			else
			{
				this.selectedSpriteName = value;
				result = true;
			}
			return result;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0000C2E4 File Offset: 0x0000A4E4
		public override void SelectedChanged(bool isSelected)
		{
			bool flag = this.background != null;
			if (flag)
			{
				this.background.Color = (isSelected ? this.selectedBackgroundColor : this.notSelectedBackgroundColor);
				this.background.SpriteName = (isSelected ? this.selectedSpriteName : this.notSelectedSpriteName);
			}
		}

		// Token: 0x040000C6 RID: 198
		private string selectedSpriteName = "ui_game_select_row";

		// Token: 0x040000C7 RID: 199
		private string notSelectedSpriteName = "menu_empty";

		// Token: 0x040000C8 RID: 200
		private Color selectedBackgroundColor = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);

		// Token: 0x040000C9 RID: 201
		private Color notSelectedBackgroundColor = new Color32(64, 64, 64, byte.MaxValue);
	}
}
