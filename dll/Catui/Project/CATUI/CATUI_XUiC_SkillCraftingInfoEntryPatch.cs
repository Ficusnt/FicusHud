using System;
using HarmonyLib;

// Token: 0x02000015 RID: 21
[HarmonyPatch]
public class XUiC_SkillCraftingInfoEntryPatch
{
	// Token: 0x06000038 RID: 56 RVA: 0x000058D8 File Offset: 0x00003AD8
	[HarmonyPrefix]
	[HarmonyPatch(typeof(XUiC_SkillCraftingInfoEntry), "GetBindingValueInternal")]
	public static bool Prefix(string _bindingName, ref string _value, ref bool __result, XUiC_SkillCraftingInfoEntry __instance)
	{
		bool flag = __instance.data != null;
		EntityPlayerLocal entityPlayer = __instance.xui.playerUI.entityPlayer;
		ProgressionClass.DisplayData data = __instance.data;
		bool result;
		if (!(_bindingName == "showlock"))
		{
			if (!(_bindingName == "iconatlas"))
			{
				if (!(_bindingName == "CATUI_SkillLevel"))
				{
					if (!(_bindingName == "CATUI_ItemUnlockLevel"))
					{
						if (!(_bindingName == "CATUI_QualityNextLevelFill"))
						{
							result = true;
						}
						else
						{
							_value = "0";
							bool flag2 = flag;
							if (flag2)
							{
								float num = (float)entityPlayer.Progression.GetProgressionValue(data.Owner.Name).Level;
								float num2 = (float)data.GetNextPoints(entityPlayer.Progression.GetProgressionValue(data.Owner.Name).Level);
								bool flag3 = num2 > 0f;
								if (flag3)
								{
									float num3 = num / num2;
									_value = ((num3 < 0.01f) ? "0" : num3.ToString("F3"));
								}
							}
							__result = true;
							result = false;
						}
					}
					else
					{
						_value = "";
						bool flag4 = flag;
						if (flag4)
						{
							ProgressionClass.DisplayData.UnlockData unlockData = data.GetUnlockData(0);
							bool flag5 = unlockData != null;
							if (flag5)
							{
								_value = data.QualityStarts[unlockData.UnlockTier].ToString();
							}
						}
						__result = true;
						result = false;
					}
				}
				else
				{
					_value = "";
					bool flag6 = flag;
					if (flag6)
					{
						_value = entityPlayer.Progression.GetProgressionValue(data.Owner.Name).Level.ToString();
					}
					__result = true;
					result = false;
				}
			}
			else
			{
				_value = "ItemIconAtlas";
				bool flag7 = flag;
				if (flag7)
				{
					_value = data.GetUnlockItemIconAtlas(entityPlayer, 0).ToString();
				}
				__result = true;
				result = false;
			}
		}
		else
		{
			_value = "false";
			bool flag8 = flag;
			if (flag8)
			{
				_value = data.GetUnlockItemLocked(entityPlayer, 0).ToString();
			}
			__result = true;
			result = false;
		}
		return result;
	}
}
