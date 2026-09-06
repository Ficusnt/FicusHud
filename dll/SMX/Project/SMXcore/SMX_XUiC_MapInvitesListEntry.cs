using System;
using Quartz;
using SMXcore.HarmonyPatches;

namespace SMXcore
{
	// Token: 0x02000011 RID: 17
	public class XUiC_MapInvitesListEntry : XUiC_MapInvitesListEntry
	{
		// Token: 0x0600004B RID: 75 RVA: 0x00005CA2 File Offset: 0x00003EA2
		public override void Init()
		{
			base.Init();
			XUiC_MapInvitesListEntry_Patch.PatchUpdateSelectedMethod();
		}
	}
}
