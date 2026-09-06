using System;

namespace SMXcore
{
	// Token: 0x02000015 RID: 21
	public class XUiC_MapSubPopupList : XUiController
	{
		// Token: 0x0600005C RID: 92 RVA: 0x00006058 File Offset: 0x00004258
		public override void Init()
		{
			base.Init();
			for (int i = 0; i < this.children.Count; i++)
			{
				XUiController xuiController = this.children[i].Children[0];
				bool flag = xuiController is XUiC_MapSubPopupEntry;
				if (flag)
				{
					XUiC_MapSubPopupEntry xuiC_MapSubPopupEntry = (XUiC_MapSubPopupEntry)xuiController;
					xuiC_MapSubPopupEntry.SetIndex(i);
					xuiC_MapSubPopupEntry.SetSpriteName(XUiC_MapSubPopupList.sprites[i % XUiC_MapSubPopupList.sprites.Length]);
				}
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000060D8 File Offset: 0x000042D8
		internal void ResetList()
		{
			for (int i = 0; i < this.children.Count; i++)
			{
				XUiController xuiController = this.children[i].Children[0];
				bool flag = xuiController is XUiC_MapSubPopupEntry;
				if (flag)
				{
					((XUiC_MapSubPopupEntry)xuiController).Reset();
				}
			}
		}

		// Token: 0x0400004A RID: 74
		private static string[] sprites = new string[]
		{
			"ui_game_symbol_map_waypoint01",
			"ui_game_symbol_map_waypoint02",
			"ui_game_symbol_map_waypoint03",
			"ui_game_symbol_map_waypoint04",
			"ui_game_symbol_map_waypoint05",
			"ui_game_symbol_map_waypoint06",
			"ui_game_symbol_map_waypoint07",
			"ui_game_symbol_map_waypoint08",
			"ui_game_symbol_map_waypoint09",
			"ui_game_symbol_map_waypoint10",
			"ui_game_symbol_map_waypoint11",
			"ui_game_symbol_map_waypoint12",
			"ui_game_symbol_map_waypoint13",
			"ui_game_symbol_map_waypoint14",
			"ui_game_symbol_map_waypoint15",
			"ui_game_symbol_map_waypoint16",
			"ui_game_symbol_map_waypoint17",
			"ui_game_symbol_map_waypoint18",
			"ui_game_symbol_map_waypoint19",
			"ui_game_symbol_map_waypoint20",
			"ui_game_symbol_map_waypoint21",
			"ui_game_symbol_map_waypoint22",
			"ui_game_symbol_map_waypoint23",
			"ui_game_symbol_map_waypoint24",
			"ui_game_symbol_map_waypoint25",
			"ui_game_symbol_map_waypoint26",
			"ui_game_symbol_map_waypoint27",
			"ui_game_symbol_map_waypoint28",
			"ui_game_symbol_map_waypoint29",
			"ui_game_symbol_map_waypoint30",
			"ui_game_symbol_map_waypoint31",
			"ui_game_symbol_map_waypoint32",
			"ui_game_symbol_map_waypoint33",
			"ui_game_symbol_map_waypoint34",
			"ui_game_symbol_map_waypoint35",
			"ui_game_symbol_map_waypoint36",
			"ui_game_symbol_map_waypoint37",
			"ui_game_symbol_map_waypoint38",
			"ui_game_symbol_map_waypoint39",
			"ui_game_symbol_map_waypoint40",
			"ui_game_symbol_map_waypoint41",
			"ui_game_symbol_map_waypoint42",
			"ui_game_symbol_map_waypoint43",
			"ui_game_symbol_map_waypoint44",
			"ui_game_symbol_map_waypoint45",
			"ui_game_symbol_map_waypoint46",
			"ui_game_symbol_map_waypoint47",
			"ui_game_symbol_map_waypoint48"
		};
	}
}
