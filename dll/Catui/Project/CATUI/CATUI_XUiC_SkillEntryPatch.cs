using System;
using HarmonyLib;
using UnityEngine;

// Token: 0x02000017 RID: 23
[HarmonyPatch]
public class XUiC_SkillEntryPatch
{
	// Token: 0x0600003D RID: 61 RVA: 0x00005EB4 File Offset: 0x000040B4
	[HarmonyPrefix]
	[HarmonyPatch(typeof(XUiC_SkillEntry), "GetBindingValueInternal")]
	public static bool GetBindingValueInternalPrefix(string bindingName, ref string value, ref bool __result, XUiC_SkillEntry __instance)
	{
		uint num = global::<PrivateImplementationDetails>.ComputeStringHash(bindingName);
		if (num <= 1381226231U)
		{
			if (num <= 641276857U)
			{
				if (num != 332851489U)
				{
					if (num == 641276857U)
					{
						if (bindingName == "CATUI_SkillEntryIsBuffed")
						{
							value = "fasle";
							bool flag = __instance.currentSkill != null && !__instance.currentSkill.ProgressionClass.IsBookGroup && !__instance.currentSkill.ProgressionClass.IsCrafting;
							if (flag)
							{
								int num2 = __instance.currentSkill.CalculatedLevel(__instance.xui.playerUI.entityPlayer);
								int level = __instance.currentSkill.Level;
								bool flag2 = num2 > level;
								if (flag2)
								{
									value = "true";
								}
							}
							__result = true;
							return false;
						}
					}
				}
				else if (bindingName == "CATUI_GroupType")
				{
					value = "skill";
					bool flag3 = __instance.currentSkill != null;
					if (flag3)
					{
						ProgressionClass progressionClass = __instance.currentSkill.ProgressionClass;
						bool isCrafting = progressionClass.IsCrafting;
						if (isCrafting)
						{
							value = "craft";
						}
						else
						{
							bool isBookGroup = progressionClass.IsBookGroup;
							if (isBookGroup)
							{
								value = "book";
							}
						}
					}
					__result = true;
					return false;
				}
			}
			else if (num != 765459171U)
			{
				if (num != 1305717697U)
				{
					if (num == 1381226231U)
					{
						if (bindingName == "CATUI_GroupEntryLevel")
						{
							value = "0";
							bool flag4 = __instance.currentSkill != null;
							if (flag4)
							{
								bool isBookGroup2 = __instance.currentSkill.ProgressionClass.IsBookGroup;
								if (isBookGroup2)
								{
									int num3 = 0;
									int num4 = 0;
									for (int i = 0; i < __instance.currentSkill.ProgressionClass.Children.Count; i++)
									{
										num3++;
										bool flag5 = __instance.xui.playerUI.entityPlayer.Progression.GetProgressionValue(__instance.currentSkill.ProgressionClass.Children[i].Name).Level == 1;
										if (flag5)
										{
											num4++;
										}
									}
									value = Mathf.Min(num4, num3 - 1).ToString();
								}
								else
								{
									value = __instance.currentSkill.CalculatedLevel(__instance.xui.playerUI.entityPlayer).ToString();
								}
							}
							__result = true;
							return false;
						}
					}
				}
				else if (bindingName == "CATUI_SkillEntryIsNerfed")
				{
					value = "fasle";
					bool flag6 = __instance.currentSkill != null && !__instance.currentSkill.ProgressionClass.IsBookGroup && !__instance.currentSkill.ProgressionClass.IsCrafting;
					if (flag6)
					{
						int num5 = __instance.currentSkill.CalculatedLevel(__instance.xui.playerUI.entityPlayer);
						int level2 = __instance.currentSkill.Level;
						bool flag7 = num5 < level2;
						if (flag7)
						{
							value = "true";
						}
					}
					__result = true;
					return false;
				}
			}
			else if (bindingName == "rowstatecolor")
			{
				value = (__instance.IsSelected ? "160,160,160,255" : (__instance.IsHovered ? __instance.hoverColor : __instance.rowColor));
				__result = true;
				return false;
			}
		}
		else if (num <= 1592594526U)
		{
			if (num != 1423618815U)
			{
				if (num != 1533161145U)
				{
					if (num == 1592594526U)
					{
						if (bindingName == "CATUI_GroupEntryCount")
						{
							value = "0";
							bool flag8 = __instance.Skill != null && __instance.Skill.ProgressionClass.Parent != null;
							if (flag8)
							{
								int num6 = 0;
								foreach (ProgressionClass progressionClass2 in __instance.Skill.ProgressionClass.Parent.Children)
								{
									bool flag9 = !progressionClass2.IsSkill;
									if (flag9)
									{
										num6++;
									}
								}
								value = num6.ToString();
							}
							__result = true;
							return false;
						}
					}
				}
				else if (bindingName == "CATUI_SkillEntryDisabled")
				{
					value = ((__instance.currentSkill == null) ? "true" : ((__instance.currentSkill.CalculatedMaxLevel(__instance.xui.playerUI.entityPlayer) == 0) ? "true" : "false"));
					__result = true;
					return false;
				}
			}
			else if (bindingName == "CATUI_GroupEntryType")
			{
				value = "skill";
				bool flag10 = __instance.currentSkill != null;
				if (flag10)
				{
					ProgressionClass progressionClass3 = __instance.currentSkill.ProgressionClass;
					bool isPerk = progressionClass3.IsPerk;
					if (isPerk)
					{
						value = "perk";
					}
					else
					{
						bool isAttribute = progressionClass3.IsAttribute;
						if (isAttribute)
						{
							value = "attribute";
						}
					}
				}
				__result = true;
				return false;
			}
		}
		else if (num != 2726455007U)
		{
			if (num != 2743402980U)
			{
				if (num == 3219935438U)
				{
					if (bindingName == "CATUI_GroupEntryLevelFill")
					{
						value = "0";
						bool flag11 = __instance.currentSkill != null;
						if (flag11)
						{
							bool isBookGroup3 = __instance.currentSkill.ProgressionClass.IsBookGroup;
							if (isBookGroup3)
							{
								float num7 = 0f;
								float num8 = 0f;
								for (int j = 0; j < __instance.currentSkill.ProgressionClass.Children.Count; j++)
								{
									num7 += 1f;
									bool flag12 = __instance.xui.playerUI.entityPlayer.Progression.GetProgressionValue(__instance.currentSkill.ProgressionClass.Children[j].Name).Level == 1;
									if (flag12)
									{
										num8 += 1f;
									}
								}
								num8 = Mathf.Min(num8, num7 - 1f);
								float num9 = num8 / (num7 - 1f);
								value = ((num9 < 0.01f) ? "0" : num9.ToString("F2"));
							}
							else
							{
								float num10 = (float)__instance.currentSkill.CalculatedLevel(__instance.xui.playerUI.entityPlayer);
								float num11 = (float)__instance.currentSkill.ProgressionClass.MaxLevel;
								bool flag13 = num11 == 0f;
								if (flag13)
								{
									value = "1";
								}
								else
								{
									float num12 = num10 / num11;
									value = ((num12 < 0.01f) ? "0" : num12.ToString("F2"));
								}
							}
						}
						__result = true;
						return false;
					}
				}
			}
			else if (bindingName == "CATUI_GroupIcon")
			{
				value = "";
				bool flag14 = __instance.Skill != null && __instance.Skill.ProgressionClass.Parent != null;
				if (flag14)
				{
					value = __instance.Skill.ProgressionClass.Parent.Icon;
				}
				__result = true;
				return false;
			}
		}
		else if (bindingName == "CATUI_GroupEntryLevelMax")
		{
			value = "0";
			bool flag15 = __instance.currentSkill != null;
			if (flag15)
			{
				bool isBookGroup4 = __instance.currentSkill.ProgressionClass.IsBookGroup;
				if (isBookGroup4)
				{
					int num13 = 0;
					for (int k = 0; k < __instance.currentSkill.ProgressionClass.Children.Count; k++)
					{
						num13++;
					}
					value = (num13 - 1).ToString();
				}
				else
				{
					value = __instance.currentSkill.ProgressionClass.MaxLevel.ToString();
				}
			}
			__result = true;
			return false;
		}
		return true;
	}
}
