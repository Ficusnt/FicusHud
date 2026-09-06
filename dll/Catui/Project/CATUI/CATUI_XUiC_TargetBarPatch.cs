using System;
using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;

// Token: 0x0200001B RID: 27
[HarmonyPatch]
public class XUiC_TargetBarPatch
{
	// Token: 0x06000045 RID: 69 RVA: 0x00006D6C File Offset: 0x00004F6C
	[HarmonyPrefix]
	[HarmonyPatch(typeof(XUiC_TargetBar), "GetBindingValueInternal")]
	public static bool GetBindingValueInternalPrefix(string bindingName, ref string value, ref bool __result, XUiC_TargetBar __instance)
	{
		EntityAlive target = __instance.Target;
		uint num = global::<PrivateImplementationDetails>.ComputeStringHash(bindingName);
		if (num <= 2323832937U)
		{
			if (num <= 1728592160U)
			{
				if (num <= 1525317642U)
				{
					if (num != 118386821U)
					{
						if (num == 1525317642U)
						{
							if (bindingName == "CATUI_EntityBleedingTimer")
							{
								value = "";
								bool flag = target != null;
								if (flag)
								{
									BuffValue buff = target.Buffs.GetBuff("buffInjuryBleeding");
									bool flag2 = buff != null;
									if (flag2)
									{
										float customVar = target.Buffs.GetCustomVar(buff.BuffClass.DisplayValueCVar);
										bool flag3 = customVar > 0f;
										if (flag3)
										{
											value = customVar.ToString();
										}
									}
								}
								__result = true;
								return false;
							}
						}
					}
					else if (bindingName == "CATUI_EntityIsRadiatedRegenBlock")
					{
						value = "false";
						bool flag4 = target != null;
						if (flag4)
						{
							value = (target.Buffs.GetBuff("buffRadiatedRegenBlock") != null).ToString();
						}
						__result = true;
						return false;
					}
				}
				else if (num != 1720507064U)
				{
					if (num == 1728592160U)
					{
						if (bindingName == "CATUI_EntityTags")
						{
							value = "";
							bool flag5 = target != null;
							if (flag5)
							{
								value = EntityClass.list[target.entityClass].Tags.ToString();
							}
							__result = true;
							return false;
						}
					}
				}
				else if (bindingName == "CATUI_EntityIsShocked")
				{
					value = "false";
					bool flag6 = target != null;
					if (flag6)
					{
						value = (target.Buffs.GetBuff("buffShocked") != null).ToString();
					}
					__result = true;
					return false;
				}
			}
			else if (num <= 2229113812U)
			{
				if (num != 2191333330U)
				{
					if (num == 2229113812U)
					{
						if (bindingName == "CATUI_EntityBuffList")
						{
							value = "";
							bool flag7 = target != null;
							if (flag7)
							{
								string text = string.Empty;
								List<BuffValue> activeBuffs = target.Buffs.ActiveBuffs;
								for (int i = 0; i < activeBuffs.Count; i++)
								{
									text += activeBuffs[i].buffName;
									bool flag8 = i < activeBuffs.Count - 1;
									if (flag8)
									{
										text += ", ";
									}
								}
								value = text;
							}
							__result = true;
							return false;
						}
					}
				}
				else if (bindingName == "CATUI_EntityRadiatedRegenBlockTimer")
				{
					value = "0";
					bool flag9 = target != null;
					if (flag9)
					{
						BuffValue buff2 = target.Buffs.GetBuff("buffRadiatedRegenBlock");
						bool flag10 = buff2 != null;
						if (flag10)
						{
							value = (buff2.BuffClass.DurationMax - (float)Mathf.FloorToInt(buff2.DurationInSeconds)).ToString();
						}
					}
					__result = true;
					return false;
				}
			}
			else if (num != 2271160337U)
			{
				if (num == 2323832937U)
				{
					if (bindingName == "CATUI_EntityShockedTimer")
					{
						value = "";
						bool flag11 = target != null;
						if (flag11)
						{
							EntityClass entityClass = EntityClass.list[target.entityClass];
							bool flag12 = entityClass.Tags.Test_Bit(FastTags<TagGroup.Global>.GetBit("charged"));
							BuffValue buff3 = target.Buffs.GetBuff("buffShocked");
							bool flag13 = buff3 != null;
							if (flag13)
							{
								float num2 = (float)Mathf.CeilToInt((flag12 ? (buff3.BuffClass.DurationMax / 2f) : buff3.BuffClass.DurationMax) - buff3.DurationInSeconds);
								bool flag14 = num2 > 0f;
								if (flag14)
								{
									value = num2.ToString();
								}
							}
						}
						__result = true;
						return false;
					}
				}
			}
			else if (bindingName == "CATUI_EntityArmorRating")
			{
				value = "0";
				bool flag15 = target != null;
				if (flag15)
				{
					value = EffectManager.GetValue(PassiveEffects.PhysicalDamageResist, null, 0f, target, null, default(FastTags<TagGroup.Global>), true, true, true, true, true, 1, true, false).ToString("F0");
				}
				__result = true;
				return false;
			}
		}
		else if (num <= 2907735941U)
		{
			if (num <= 2592840685U)
			{
				if (num != 2411593524U)
				{
					if (num == 2592840685U)
					{
						if (bindingName == "CATUI_EntityIsBleeding")
						{
							value = "false";
							bool flag16 = target != null;
							if (flag16)
							{
								value = (target.Buffs.GetBuff("buffInjuryBleeding") != null).ToString();
							}
							__result = true;
							return false;
						}
					}
				}
				else if (bindingName == "CATUI_EntityIsCrippled")
				{
					value = "false";
					bool flag17 = target != null;
					if (flag17)
					{
						value = (target.Buffs.GetBuff("buffInjuryCrippled01") != null).ToString();
					}
					__result = true;
					return false;
				}
			}
			else if (num != 2806588556U)
			{
				if (num == 2907735941U)
				{
					if (bindingName == "CATUI_EntityOnFireTimer")
					{
						value = "";
						bool flag18 = target != null;
						if (flag18)
						{
							BuffValue buff4 = target.Buffs.GetBuff("buffIsOnFire");
							bool flag19 = buff4 != null;
							if (flag19)
							{
								float customVar2 = target.Buffs.GetCustomVar(buff4.BuffClass.DisplayValueCVar);
								bool flag20 = customVar2 > 0f;
								if (flag20)
								{
									value = Mathf.CeilToInt(customVar2).ToString();
								}
							}
						}
						__result = true;
						return false;
					}
				}
			}
			else if (bindingName == "CATUI_EntityIsRadiatedRegen")
			{
				value = "false";
				bool flag21 = target != null;
				if (flag21)
				{
					value = (target.Buffs.GetBuff("buffRadiatedRegen") != null).ToString();
				}
				__result = true;
				return false;
			}
		}
		else if (num <= 3639133744U)
		{
			if (num != 3289988813U)
			{
				if (num == 3639133744U)
				{
					if (bindingName == "CATUI_EntityIsSleeping")
					{
						value = "false";
						bool flag22 = target != null;
						if (flag22)
						{
							value = target.IsSleeping.ToString();
						}
						__result = true;
						return false;
					}
				}
			}
			else if (bindingName == "CATUI_EntityType")
			{
				value = "normal";
				bool flag23 = target != null;
				if (flag23)
				{
					EntityClass entityClass2 = EntityClass.list[target.entityClass];
					bool flag24 = entityClass2.Tags.Test_Bit(FastTags<TagGroup.Global>.GetBit("boss"));
					bool flag25 = entityClass2.Tags.Test_Bit(FastTags<TagGroup.Global>.GetBit("feral"));
					bool flag26 = entityClass2.Tags.Test_Bit(FastTags<TagGroup.Global>.GetBit("radiated"));
					bool flag27 = entityClass2.Tags.Test_Bit(FastTags<TagGroup.Global>.GetBit("charged"));
					bool flag28 = entityClass2.Tags.Test_Bit(FastTags<TagGroup.Global>.GetBit("infernal"));
					bool flag29 = entityClass2.entityClassName == "animalBear";
					bool flag30 = entityClass2.entityClassName == "animalZombieBear";
					bool flag31 = entityClass2.entityClassName == "animalDireWolf";
					bool flag32 = flag26;
					if (flag32)
					{
						value = "radiated";
					}
					else
					{
						bool flag33 = flag27;
						if (flag33)
						{
							value = "charged";
						}
						else
						{
							bool flag34 = flag28;
							if (flag34)
							{
								value = "infernal";
							}
							else
							{
								bool flag35 = flag25;
								if (flag35)
								{
									value = "feral";
								}
								else
								{
									bool flag36 = flag24 || flag29 || flag30 || flag31;
									if (flag36)
									{
										value = "boss";
									}
								}
							}
						}
					}
				}
				__result = true;
				return false;
			}
		}
		else if (num != 3955153924U)
		{
			if (num != 4223362725U)
			{
				if (num == 4290085291U)
				{
					if (bindingName == "CATUI_EntityBleedingCounter")
					{
						value = "false";
						bool flag37 = target != null;
						if (flag37)
						{
							BuffValue buff5 = target.Buffs.GetBuff("buffInjuryBleeding");
							bool flag38 = buff5 != null;
							if (flag38)
							{
								float customVar3 = target.Buffs.GetCustomVar("bleedCounter");
								bool flag39 = customVar3 > 0f;
								if (flag39)
								{
									value = customVar3.ToString();
								}
							}
						}
						__result = true;
						return false;
					}
				}
			}
			else if (bindingName == "CATUI_EntityBuffListTimer")
			{
				value = "";
				bool flag40 = target != null;
				if (flag40)
				{
					string text2 = string.Empty;
					List<BuffValue> activeBuffs2 = target.Buffs.ActiveBuffs;
					for (int j = 0; j < activeBuffs2.Count; j++)
					{
						string name = (activeBuffs2[j].BuffClass.DisplayValueCVar != null) ? activeBuffs2[j].BuffClass.DisplayValueCVar : "";
						text2 += target.Buffs.GetCustomVar(name).ToString();
						bool flag41 = j < activeBuffs2.Count - 1;
						if (flag41)
						{
							text2 += ", ";
						}
					}
					value = text2;
				}
				__result = true;
				return false;
			}
		}
		else if (bindingName == "CATUI_EntityIsOnFire")
		{
			value = "false";
			bool flag42 = target != null;
			if (flag42)
			{
				value = (target.Buffs.GetBuff("buffIsOnFire") != null).ToString();
			}
			__result = true;
			return false;
		}
		return true;
	}
}
