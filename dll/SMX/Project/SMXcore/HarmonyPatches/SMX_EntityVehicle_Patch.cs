using System;
using HarmonyLib;

namespace SMXcore.HarmonyPatches
{
	// Token: 0x02000028 RID: 40
	[HarmonyPatch(typeof(EntityVehicle))]
	public class EntityVehicle_Patch
	{
		// Token: 0x06000130 RID: 304 RVA: 0x0000C3E8 File Offset: 0x0000A5E8
		[HarmonyPrefix]
		[HarmonyPatch("getStorageSize")]
		public static bool getStorageSize(EntityVehicle __instance, ref Vector2i __result)
		{
			bool flag = __instance.storageModCount < 1;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				ItemValue[] modifications = __instance.vehicle.itemValue.Modifications;
				bool flag2 = modifications == null;
				if (flag2)
				{
					result = true;
				}
				else
				{
					__result = LootContainer.GetLootContainer(__instance.GetLootList(), true).size;
					ItemValue[] array = modifications;
					int i = 0;
					while (i < array.Length)
					{
						ItemValue itemValue = array[i];
						if (itemValue == null)
						{
							goto IL_88;
						}
						ItemClassModifier itemClassModifier = itemValue.ItemClass as ItemClassModifier;
						if (itemClassModifier == null)
						{
							goto IL_88;
						}
						bool flag3 = itemClassModifier.ItemTags.Test_AnySet(EntityVehicle.StorageModifierTags);
						IL_89:
						bool flag4 = flag3;
						if (flag4)
						{
							int num = 1;
							itemClassModifier.Properties.ParseInt("containerRowIncrease", ref num);
							__result.y += num;
						}
						i++;
						continue;
						IL_88:
						flag3 = false;
						goto IL_89;
					}
					result = false;
				}
			}
			return result;
		}
	}
}
