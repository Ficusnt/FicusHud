using System;
using Quartz;
using SMXcore.HarmonyPatches;

namespace SMXcore
{
	// Token: 0x02000010 RID: 16
	public class XUiC_MapWaypointListEntry : XUiC_MapWaypointListEntry
	{
		// Token: 0x06000049 RID: 73 RVA: 0x00005C89 File Offset: 0x00003E89
		public override void Init()
		{
			base.Init();
			XUiC_MapWaypointListEntry_Patch.PatchUpdateSelectedMethod();
		}
	}
}
