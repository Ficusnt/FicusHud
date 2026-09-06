using System;

// Token: 0x02000003 RID: 3
internal class XUiC_CATUI_Hover : XUiController
{
	// Token: 0x06000002 RID: 2 RVA: 0x00002059 File Offset: 0x00000259
	public override void Init()
	{
		base.Init();
		this.CATUI_EL_hover_bg = (XUiV_Sprite)base.GetChildById("CATUI_EL_hover_bg").ViewComponent;
	}

	// Token: 0x06000003 RID: 3 RVA: 0x00002080 File Offset: 0x00000280
	public override bool ParseAttribute(string _name, string _value, XUiController _parent)
	{
		bool result;
		if (!(_name == "default_hover_color"))
		{
			if (!(_name == "hover_color"))
			{
				result = base.ParseAttribute(_name, _value, _parent);
			}
			else
			{
				this.hover_color = _value;
				result = true;
			}
		}
		else
		{
			this.default_hover_color = _value;
			result = true;
		}
		return result;
	}

	// Token: 0x06000004 RID: 4 RVA: 0x000020D4 File Offset: 0x000002D4
	public override void OnHovered(bool _isOver)
	{
		base.OnHovered(_isOver);
		bool flag = this.CATUI_EL_hover_bg != null;
		if (flag)
		{
			if (_isOver)
			{
				this.CATUI_EL_hover_bg.Color = StringParsers.ParseColor32(this.hover_color);
			}
			else
			{
				this.CATUI_EL_hover_bg.Color = StringParsers.ParseColor32(this.default_hover_color);
			}
		}
	}

	// Token: 0x04000001 RID: 1
	private XUiV_Sprite CATUI_EL_hover_bg;

	// Token: 0x04000002 RID: 2
	private string default_hover_color;

	// Token: 0x04000003 RID: 3
	private string hover_color;
}
