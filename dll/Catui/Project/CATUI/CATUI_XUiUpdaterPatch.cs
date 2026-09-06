using System;
using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;

// Token: 0x0200001F RID: 31
[HarmonyPatch]
public class XUiUpdaterPatch
{
	// Token: 0x06000053 RID: 83 RVA: 0x00007BDC File Offset: 0x00005DDC
	[HarmonyPostfix]
	[HarmonyPatch(typeof(XUiUpdater), "Update")]
	public static void UpdatePostfix()
	{
		bool flag = InputUtils.AltKeyPressed && Input.GetKeyDown(KeyCode.B);
		if (flag)
		{
			EntityPlayerLocal primaryPlayer = GameManager.Instance.World.GetPrimaryPlayer();
			bool flag2 = primaryPlayer == null;
			if (flag2)
			{
				return;
			}
			EntityDrone entityDrone = XUiUpdaterPatch.FindPlayerDrone(primaryPlayer);
			bool flag3 = entityDrone != null;
			if (flag3)
			{
				entityDrone.accessInventory(primaryPlayer);
			}
		}
		bool keyDown = Input.GetKeyDown(KeyCode.Mouse3);
		if (keyDown)
		{
			EntityPlayerLocal primaryPlayer2 = GameManager.Instance.World.GetPrimaryPlayer();
			bool flag4 = primaryPlayer2 == null;
			if (!flag4)
			{
				XUi xui = primaryPlayer2.playerUI.xui;
				bool flag5 = xui == null;
				if (!flag5)
				{
					XUiC_ContainerStandardControls xuiC_ContainerStandardControls = null;
					XUiC_BackpackWindow childByType = xui.GetChildByType<XUiC_BackpackWindow>();
					XUiC_LootWindow childByType2 = xui.GetChildByType<XUiC_LootWindow>();
					XUiC_VehicleContainer childByType3 = xui.GetChildByType<XUiC_VehicleContainer>();
					bool flag6 = childByType != null;
					if (flag6)
					{
						xuiC_ContainerStandardControls = childByType.GetChildByType<XUiC_ContainerStandardControls>();
					}
					bool flag7 = xuiC_ContainerStandardControls != null && xuiC_ContainerStandardControls != null && xuiC_ContainerStandardControls.MoveAllowed != null && (childByType2.IsOpen || childByType3.IsOpen);
					if (flag7)
					{
						xuiC_ContainerStandardControls.MoveFillAndSmart();
						xui.PlayMenuClickSound();
					}
				}
			}
		}
	}

	// Token: 0x06000054 RID: 84 RVA: 0x00007D0C File Offset: 0x00005F0C
	private static EntityDrone FindPlayerDrone(EntityPlayerLocal player)
	{
		List<Entity> list = GameManager.Instance.World.Entities.list;
		foreach (Entity entity in list)
		{
			EntityDrone entityDrone = entity as EntityDrone;
			bool flag = entityDrone != null;
			if (flag)
			{
				EntityAlive owner = entityDrone.Owner;
				int? num = (owner != null) ? new int?(owner.entityId) : null;
				int entityId = player.entityId;
				bool flag2 = num.GetValueOrDefault() == entityId & num != null;
				if (flag2)
				{
					return entityDrone;
				}
			}
		}
		return null;
	}
}
