using System;
using GearsAPI.Settings.Global;

// Token: 0x02000003 RID: 3
public class SMXSettings
{
	// Token: 0x06000006 RID: 6 RVA: 0x000020DC File Offset: 0x000002DC
	public static void SkipNewsScreen(IGlobalModSetting setting, string newValue)
	{
		bool shownNewsScreenOnce = newValue == "On";
		XUiC_MainMenu.shownNewsScreenOnce = shownNewsScreenOnce;
	}
}
