using System;
using HarmonyLib;
using UnityEngine;

// Token: 0x02000009 RID: 9
[HarmonyPatch]
public class XUiC_HUDStatBarPatch
{
	// Token: 0x06000018 RID: 24 RVA: 0x00003154 File Offset: 0x00001354
	[HarmonyPrefix]
	[HarmonyPatch(typeof(XUiC_HUDStatBar), "GetBindingValueInternal")]
	public static bool GetBindingValueInternalPrefix(string _bindingName, ref string _value, ref bool __result, XUiC_HUDStatBar __instance)
	{
		uint num = global::<PrivateImplementationDetails>.ComputeStringHash(_bindingName);
		if (num <= 2003922529U)
		{
			if (num <= 1346710206U)
			{
				if (num <= 762844670U)
				{
					if (num <= 348259075U)
					{
						if (num != 166666849U)
						{
							if (num == 348259075U)
							{
								if (_bindingName == "CATUI_playerPing")
								{
									_value = "-1";
									bool flag = __instance.localPlayer != null;
									if (flag)
									{
										int pingToServer = __instance.localPlayer.pingToServer;
										bool flag2 = pingToServer > 0;
										if (flag2)
										{
											_value = ((pingToServer > 1000) ? ">1000" : pingToServer.ToString());
										}
										__instance.IsDirty = true;
									}
									__result = true;
									return false;
								}
							}
						}
						else if (_bindingName == "CATUI_playerActiveItemName")
						{
							_value = "";
							bool flag3 = __instance.localPlayer != null;
							if (flag3)
							{
								EntityPlayer localPlayer = __instance.localPlayer;
								Inventory inventory = localPlayer.inventory;
								ItemValue itemValue = inventory.GetItem(__instance.currentSlotIndex).itemValue;
								ItemClass itemClass = itemValue.ItemClass;
								bool flag4 = itemClass != null;
								if (flag4)
								{
									_value = itemClass.GetLocalizedItemName();
								}
								__instance.IsDirty = true;
							}
							__result = true;
							return false;
						}
					}
					else if (num != 617109906U)
					{
						if (num != 647687287U)
						{
							if (num == 762844670U)
							{
								if (_bindingName == "CATUI_playerZombieKills")
								{
									_value = "1";
									bool flag5 = __instance.localPlayer != null;
									if (flag5)
									{
										_value = XUiM_Player.GetZombieKills(__instance.localPlayer).ToString();
										__instance.IsDirty = true;
									}
									__result = true;
									return false;
								}
							}
						}
						else if (_bindingName == "CATUI_playerActiveItemIcon")
						{
							_value = "1";
							bool flag6 = __instance.localPlayer != null;
							if (flag6)
							{
								EntityPlayer localPlayer2 = __instance.localPlayer;
								Inventory inventory2 = localPlayer2.inventory;
								ItemValue itemValue2 = inventory2.GetItem(__instance.currentSlotIndex).itemValue;
								ItemClass itemClass2 = itemValue2.ItemClass;
								bool flag7 = itemClass2 != null;
								if (flag7)
								{
									_value = itemClass2.GetIconName();
								}
								__instance.IsDirty = true;
							}
							__result = true;
							return false;
						}
					}
					else if (_bindingName == "CATUI_playerSkillPointsAvailable")
					{
						_value = "1";
						bool flag8 = __instance.localPlayer != null;
						if (flag8)
						{
							_value = __instance.localPlayer.Progression.SkillPoints.ToString();
							__instance.IsDirty = true;
						}
						__result = true;
						return false;
					}
				}
				else if (num <= 1155550015U)
				{
					if (num != 798894980U)
					{
						if (num != 1068250970U)
						{
							if (num == 1155550015U)
							{
								if (_bindingName == "CATUI_playerTraderStage")
								{
									_value = "1";
									bool flag9 = __instance.localPlayer != null;
									if (flag9)
									{
										_value = __instance.localPlayer.QuestJournal.GetCurrentFactionTier(1, 0, false).ToString();
										__instance.IsDirty = true;
									}
									__result = true;
									return false;
								}
							}
						}
						else if (_bindingName == "CATUI_VehicleIsTurbo")
						{
							_value = "false";
							bool flag10 = __instance.vehicle != null;
							if (flag10)
							{
								_value = __instance.vehicle.GetVehicle().IsTurbo.ToString();
								__instance.IsDirty = true;
							}
							__result = true;
							return false;
						}
					}
					else if (_bindingName == "CATUI_VehicleMaxSpeedNotTurbo")
					{
						_value = "0";
						bool flag11 = __instance.vehicle != null;
						if (flag11)
						{
							_value = __instance.vehicle.GetVehicle().VelocityMaxForward.ToString();
							__instance.IsDirty = true;
						}
						__result = true;
						return false;
					}
				}
				else if (num != 1262841543U)
				{
					if (num != 1344161667U)
					{
						if (num == 1346710206U)
						{
							if (_bindingName == "CATUI_AmmoMax")
							{
								_value = "";
								bool flag12 = __instance.localPlayer != null;
								if (flag12)
								{
									ItemActionAttack attackAction = __instance.attackAction;
									int currentAmmoCount = __instance.currentAmmoCount;
									int currentSlotIndex = __instance.currentSlotIndex;
									EntityPlayerLocal localPlayer3 = __instance.localPlayer;
									bool flag13 = attackAction != null && attackAction.IsEditingTool();
									if (flag13)
									{
										ItemActionData itemActionDataInSlot = localPlayer3.inventory.GetItemActionDataInSlot(currentSlotIndex, 1);
										_value = attackAction.GetStat(itemActionDataInSlot);
									}
									else
									{
										_value = currentAmmoCount.ToString();
									}
									__instance.IsDirty = true;
								}
								__result = true;
								return false;
							}
						}
					}
					else if (_bindingName == "CATUI_outsidetemp")
					{
						_value = "";
						bool flag14 = __instance.localPlayer != null;
						if (flag14)
						{
							_value = XUiM_Player.GetOutsideTemp(__instance.localPlayer).ToString();
							__instance.IsDirty = true;
						}
						__result = true;
						return false;
					}
				}
				else if (_bindingName == "CATUI_playerPingVisible")
				{
					_value = "false";
					bool flag15 = __instance.localPlayer != null;
					if (flag15)
					{
						int pingToServer2 = __instance.localPlayer.pingToServer;
						bool flag16 = pingToServer2 > 0;
						if (flag16)
						{
							_value = "true";
						}
						__instance.IsDirty = true;
					}
					__result = true;
					return false;
				}
			}
			else if (num <= 1603546277U)
			{
				if (num <= 1452231538U)
				{
					if (num != 1399194945U)
					{
						if (num == 1452231538U)
						{
							if (_bindingName == "CATUI_VehicleIsLight")
							{
								_value = "false";
								bool flag17 = __instance.vehicle != null;
								if (flag17)
								{
									_value = __instance.vehicle.IsHeadlightOn.ToString();
									__instance.IsDirty = true;
								}
								__result = true;
								return false;
							}
						}
					}
					else if (_bindingName == "CATUI_VehicleInventorySlotCount")
					{
						_value = "false";
						bool flag18 = __instance.vehicle != null && __instance.vehicle.GetVehicle().HasStorage();
						if (flag18)
						{
							_value = __instance.vehicle.bag.GetSlots().Length.ToString();
							__instance.IsDirty = true;
						}
						__result = true;
						return false;
					}
				}
				else if (num != 1500362419U)
				{
					if (num != 1552186764U)
					{
						if (num == 1603546277U)
						{
							if (_bindingName == "CATUI_playerTraderStageProgressCurrent")
							{
								_value = "1";
								bool flag19 = __instance.localPlayer != null;
								if (flag19)
								{
									_value = __instance.localPlayer.QuestJournal.GetQuestFactionPoints(1).ToString();
									__instance.IsDirty = true;
								}
								__result = true;
								return false;
							}
						}
					}
					else if (_bindingName == "CATUI_VehicleInventoryItemCount")
					{
						_value = "false";
						bool flag20 = __instance.vehicle != null && __instance.vehicle.GetVehicle().HasStorage();
						if (flag20)
						{
							_value = __instance.vehicle.bag.GetUsedSlotCount().ToString();
							__instance.IsDirty = true;
						}
						__result = true;
						return false;
					}
				}
				else if (_bindingName == "CATUI_playerRunSpeed")
				{
					_value = "110";
					bool flag21 = __instance.localPlayer != null;
					if (flag21)
					{
						float num2 = EffectManager.GetValue(PassiveEffects.RunSpeed, null, 0f, __instance.localPlayer, null, default(FastTags<TagGroup.Global>), true, true, true, true, true, 1, true, false) * 100f;
						_value = ((int)num2).ToString();
						__instance.IsDirty = true;
					}
					__result = true;
					return false;
				}
			}
			else if (num <= 1746683367U)
			{
				if (num != 1640954300U)
				{
					if (num != 1726730345U)
					{
						if (num == 1746683367U)
						{
							if (_bindingName == "CATUI_playerArmorRating")
							{
								_value = "1";
								bool flag22 = __instance.localPlayer != null;
								if (flag22)
								{
									_value = XUiC_HUDStatBarPatch.playerArmorRatingFormatter.Format((int)EffectManager.GetValue(PassiveEffects.PhysicalDamageResist, null, 0f, __instance.localPlayer, null, default(FastTags<TagGroup.Global>), true, true, true, true, true, 1, true, false)).ToString();
									__instance.IsDirty = true;
								}
								__result = true;
								return false;
							}
						}
					}
					else if (_bindingName == "CATUI_VehicleIsBrake")
					{
						_value = "false";
						bool flag23 = __instance.vehicle != null;
						if (flag23)
						{
							_value = __instance.vehicle.GetVehicle().CurrentIsBreak.ToString();
							__instance.IsDirty = true;
						}
						__result = true;
						return false;
					}
				}
				else if (_bindingName == "CATUI_playerName")
				{
					_value = " ";
					bool flag24 = __instance.localPlayer != null;
					if (flag24)
					{
						_value = __instance.localPlayer.PlayerDisplayName;
						__instance.IsDirty = true;
					}
					__result = true;
					return false;
				}
			}
			else if (num != 1753079282U)
			{
				if (num != 1887654857U)
				{
					if (num == 2003922529U)
					{
						if (_bindingName == "CATUI_playerActiveItemUseTimesMax")
						{
							_value = "1";
							bool flag25 = __instance.localPlayer != null;
							if (flag25)
							{
								EntityPlayer localPlayer4 = __instance.localPlayer;
								Inventory inventory3 = localPlayer4.inventory;
								ItemStack item = inventory3.GetItem(__instance.currentSlotIndex);
								bool flag26 = item.IsEmpty();
								if (flag26)
								{
									_value = "0";
								}
								else
								{
									bool flag27 = item.itemValue.MaxUseTimes == 0;
									if (flag27)
									{
										_value = "1";
									}
									else
									{
										_value = item.itemValue.MaxUseTimes.ToString("F0");
									}
								}
								__instance.IsDirty = true;
							}
							__result = true;
							return false;
						}
					}
				}
				else if (_bindingName == "CATUI_playerMoveSpeed")
				{
					_value = "100";
					bool flag28 = __instance.localPlayer != null;
					if (flag28)
					{
						float num3 = EffectManager.GetValue(PassiveEffects.Mobility, null, 0f, __instance.localPlayer, null, XUiM_Player.GetPlayer().generalTags, true, true, true, true, true, 1, true, true) * 100f;
						_value = ((int)num3).ToString();
						__instance.IsDirty = true;
					}
					__result = true;
					return false;
				}
			}
			else if (_bindingName == "CATUI_coretempcolor")
			{
				_value = "255,255,255";
				bool flag29 = __instance.localPlayer != null;
				if (flag29)
				{
					float customVar = __instance.localPlayer.Buffs.GetCustomVar("_coretemp");
					if (!true)
					{
					}
					string text;
					if (customVar >= 85f)
					{
						if (customVar >= 100f)
						{
							text = "255,0,0";
						}
						else
						{
							text = "255,128,0";
						}
					}
					else
					{
						if (customVar > 32f)
						{
							if (customVar <= 50f)
							{
								text = "0,255,255";
								goto IL_BDC;
							}
						}
						else if (customVar <= 32f)
						{
							text = "0,153,255";
							goto IL_BDC;
						}
						text = "255,255,255";
					}
					IL_BDC:
					if (!true)
					{
					}
					_value = text;
					__instance.IsDirty = true;
				}
				__result = true;
				return false;
			}
		}
		else if (num <= 3237907554U)
		{
			if (num <= 2162929218U)
			{
				if (num <= 2084742803U)
				{
					if (num != 2038401893U)
					{
						if (num == 2084742803U)
						{
							if (_bindingName == "CATUI_coretemp")
							{
								_value = "";
								bool flag30 = __instance.localPlayer != null;
								if (flag30)
								{
									_value = XUiM_Player.GetCoreTemp(__instance.localPlayer).ToString();
									__instance.IsDirty = true;
								}
								__result = true;
								return false;
							}
						}
					}
					else if (_bindingName == "CATUI_playerBarteringBuying")
					{
						_value = "1";
						bool flag31 = __instance.localPlayer != null;
						if (flag31)
						{
							float num4 = EffectManager.GetValue(PassiveEffects.BarteringBuying, null, 0f, __instance.localPlayer, null, default(FastTags<TagGroup.Global>), true, true, true, true, true, 1, true, false) * 100f;
							_value = ((int)num4).ToString();
							__instance.IsDirty = true;
						}
						__result = true;
						return false;
					}
				}
				else if (num != 2086059729U)
				{
					if (num != 2104680860U)
					{
						if (num == 2162929218U)
						{
							if (_bindingName == "CATUI_playerCurrentLife")
							{
								_value = "1";
								bool flag32 = __instance.localPlayer != null;
								if (flag32)
								{
									_value = XUiM_Player.GetCurrentLife(__instance.localPlayer).ToString();
									__instance.IsDirty = true;
								}
								__result = true;
								return false;
							}
						}
					}
					else if (_bindingName == "CATUI_VehicleCanTurbo")
					{
						_value = "false";
						bool flag33 = __instance.vehicle != null;
						if (flag33)
						{
							_value = __instance.vehicle.GetVehicle().CanTurbo.ToString();
							__instance.IsDirty = true;
						}
						__result = true;
						return false;
					}
				}
				else if (_bindingName == "CATUI_playerLootStage")
				{
					_value = "1";
					bool flag34 = __instance.localPlayer != null;
					if (flag34)
					{
						_value = __instance.localPlayer.GetLootStage(0f, 0f).ToString();
						__instance.IsDirty = true;
					}
					__result = true;
					return false;
				}
			}
			else if (num <= 2617568818U)
			{
				if (num != 2207580130U)
				{
					if (num != 2435467382U)
					{
						if (num == 2617568818U)
						{
							if (_bindingName == "CATUI_playerTraveled")
							{
								_value = "1";
								bool flag35 = __instance.localPlayer != null;
								if (flag35)
								{
									_value = XUiM_Player.GetKMTraveled(__instance.localPlayer).ToString();
									__instance.IsDirty = true;
								}
								__result = true;
								return false;
							}
						}
					}
					else if (_bindingName == "CATUI_VehicleHasLight")
					{
						_value = "false";
						bool flag36 = __instance.vehicle != null;
						if (flag36)
						{
							_value = __instance.vehicle.HasHeadlight().ToString();
							__instance.IsDirty = true;
						}
						__result = true;
						return false;
					}
				}
				else if (_bindingName == "CATUI_playerActiveItemUseTimesResidue")
				{
					_value = "1";
					bool flag37 = __instance.localPlayer != null;
					if (flag37)
					{
						EntityPlayer localPlayer5 = __instance.localPlayer;
						Inventory inventory4 = localPlayer5.inventory;
						ItemStack item2 = inventory4.GetItem(__instance.currentSlotIndex);
						bool flag38 = item2.IsEmpty();
						if (flag38)
						{
							_value = "0";
						}
						else
						{
							bool flag39 = item2.itemValue.MaxUseTimes == 0;
							if (flag39)
							{
								_value = "1";
							}
							else
							{
								_value = ((float)item2.itemValue.MaxUseTimes - item2.itemValue.UseTimes).ToString("F0");
							}
						}
						__instance.IsDirty = true;
					}
					__result = true;
					return false;
				}
			}
			else if (num != 2637688580U)
			{
				if (num != 2652431681U)
				{
					if (num == 3237907554U)
					{
						if (_bindingName == "CATUI_outsidetempcolor")
						{
							_value = "255,255,255";
							bool flag40 = __instance.localPlayer != null;
							if (flag40)
							{
								float customVar2 = __instance.localPlayer.Buffs.GetCustomVar("_outsidetemp");
								if (!true)
								{
								}
								string text;
								if (customVar2 >= 85f)
								{
									if (customVar2 >= 100f)
									{
										text = "255,0,0";
									}
									else
									{
										text = "255,128,0";
									}
								}
								else
								{
									if (customVar2 > 32f)
									{
										if (customVar2 <= 50f)
										{
											text = "0,255,255";
											goto IL_CCE;
										}
									}
									else if (customVar2 <= 32f)
									{
										text = "0,153,255";
										goto IL_CCE;
									}
									text = "255,255,255";
								}
								IL_CCE:
								if (!true)
								{
								}
								_value = text;
								__instance.IsDirty = true;
							}
							__result = true;
							return false;
						}
					}
				}
				else if (_bindingName == "CATUI_playerMoveSpeedLevel")
				{
					_value = "4";
					bool flag41 = __instance.localPlayer != null;
					if (flag41)
					{
						float num5 = EffectManager.GetValue(PassiveEffects.Mobility, null, 0f, __instance.localPlayer, null, XUiM_Player.GetPlayer().generalTags, true, true, true, true, true, 1, true, true) * 100f;
						if (!true)
						{
						}
						string text;
						if (num5 < 90f)
						{
							if (num5 < 70f)
							{
								if (num5 >= 0f)
								{
									if (num5 >= 50f)
									{
										text = "1";
										goto IL_F6E;
									}
									text = "0";
									goto IL_F6E;
								}
							}
							else
							{
								if (num5 >= 80f)
								{
									text = "3";
									goto IL_F6E;
								}
								text = "2";
								goto IL_F6E;
							}
						}
						else if (num5 < 110f)
						{
							if (num5 >= 100f)
							{
								text = "4";
								goto IL_F6E;
							}
						}
						else if (num5 < 120f)
						{
							text = "5";
							goto IL_F6E;
						}
						text = "6";
						IL_F6E:
						if (!true)
						{
						}
						_value = text;
						__instance.IsDirty = true;
					}
					__result = true;
					return false;
				}
			}
			else if (_bindingName == "CATUI_playerEntityPenetrationCount")
			{
				_value = "1";
				bool flag42 = __instance.localPlayer != null;
				if (flag42)
				{
					_value = XUiC_HUDStatBarPatch.playerEntityPenetrationCountFormatter.Format(EffectManager.GetValue(PassiveEffects.EntityPenetrationCount, null, 0f, __instance.localPlayer, null, default(FastTags<TagGroup.Global>), true, true, true, true, true, 1, true, false));
					__instance.IsDirty = true;
				}
				__result = true;
				return false;
			}
		}
		else if (num <= 3735263411U)
		{
			if (num <= 3628885355U)
			{
				if (num != 3257479383U)
				{
					if (num != 3600752262U)
					{
						if (num == 3628885355U)
						{
							if (_bindingName == "CATUI_VehicleCurrentSpeedFill")
							{
								_value = "0";
								bool flag43 = __instance.vehicle != null;
								if (flag43)
								{
									Vehicle vehicle = __instance.vehicle.GetVehicle();
									float velocityMaxTurboForward = vehicle.VelocityMaxTurboForward;
									bool flag44 = vehicle.HasEnginePart();
									float effectVelocityMaxPer = vehicle.EffectVelocityMaxPer;
									float num6 = flag44 ? (velocityMaxTurboForward * effectVelocityMaxPer) : velocityMaxTurboForward;
									float num7 = Mathf.Abs(vehicle.CurrentForwardVelocity + 0.001f);
									float num8 = num7 / num6;
									_value = ((num8 < 0.01f) ? "0" : num8.ToString("F3"));
									__instance.IsDirty = true;
								}
								__result = true;
								return false;
							}
						}
					}
					else if (_bindingName == "CATUI_VehicleCurrentSpeed")
					{
						_value = "0";
						bool flag45 = __instance.vehicle != null;
						if (flag45)
						{
							float num9 = Mathf.Abs(__instance.vehicle.GetVehicle().CurrentForwardVelocity + 0.001f);
							_value = ((num9 < 0.01f) ? "0" : num9.ToString("F2"));
							__instance.IsDirty = true;
						}
						__result = true;
						return false;
					}
				}
				else if (_bindingName == "CATUI_VehicleIcon")
				{
					_value = "";
					bool flag46 = __instance.vehicle != null;
					if (flag46)
					{
						_value = __instance.vehicle.GetMapIcon();
						__instance.IsDirty = true;
					}
					__result = true;
					return false;
				}
			}
			else if (num != 3641703879U)
			{
				if (num != 3721183767U)
				{
					if (num == 3735263411U)
					{
						if (_bindingName == "CATUI_playerBarteringSelling")
						{
							_value = "1";
							bool flag47 = __instance.localPlayer != null;
							if (flag47)
							{
								float num10 = EffectManager.GetValue(PassiveEffects.BarteringSelling, null, 0f, __instance.localPlayer, null, default(FastTags<TagGroup.Global>), true, true, true, true, true, 1, true, false) * 100f;
								_value = ((int)num10).ToString();
								__instance.IsDirty = true;
							}
							__result = true;
							return false;
						}
					}
				}
				else if (_bindingName == "CATUI_VehicleCurrentSpeedKPH")
				{
					_value = "0";
					bool flag48 = __instance.vehicle != null;
					if (flag48)
					{
						float num11 = Mathf.Abs(__instance.vehicle.GetVehicle().CurrentForwardVelocity + 0.001f);
						_value = ((num11 < 0.01f) ? "0" : (num11 * 3.6f).ToString("F1"));
						__instance.IsDirty = true;
					}
					__result = true;
					return false;
				}
			}
			else if (_bindingName == "CATUI_VehicleMaxSpeed")
			{
				_value = "0";
				bool flag49 = __instance.vehicle != null;
				if (flag49)
				{
					float velocityMaxTurboForward2 = __instance.vehicle.GetVehicle().VelocityMaxTurboForward;
					bool flag50 = __instance.vehicle.GetVehicle().HasEnginePart();
					float effectVelocityMaxPer2 = __instance.vehicle.GetVehicle().EffectVelocityMaxPer;
					_value = (flag50 ? (velocityMaxTurboForward2 * effectVelocityMaxPer2) : velocityMaxTurboForward2).ToString("0.00");
					__instance.IsDirty = true;
				}
				__result = true;
				return false;
			}
		}
		else if (num <= 4038360423U)
		{
			if (num != 3797543836U)
			{
				if (num != 3908194434U)
				{
					if (num == 4038360423U)
					{
						if (_bindingName == "CATUI_VehicleHasHorn")
						{
							_value = "false";
							bool flag51 = __instance.vehicle != null;
							if (flag51)
							{
								_value = __instance.vehicle.GetVehicle().HasHorn().ToString();
								__instance.IsDirty = true;
							}
							__result = true;
							return false;
						}
					}
				}
				else if (_bindingName == "CATUI_playerPingColor")
				{
					_value = "0,0,0";
					bool flag52 = __instance.localPlayer != null;
					if (flag52)
					{
						int pingToServer3 = __instance.localPlayer.pingToServer;
						bool flag53 = pingToServer3 > 0;
						if (flag53)
						{
							bool flag54 = pingToServer3 <= 150;
							if (flag54)
							{
								_value = "67, 207, 124";
							}
							else
							{
								bool flag55 = pingToServer3 <= 500;
								if (flag55)
								{
									_value = "255, 195, 0";
								}
								else
								{
									_value = "255, 0, 0";
								}
							}
						}
						__instance.IsDirty = true;
					}
					__result = true;
					return false;
				}
			}
			else if (_bindingName == "CATUI_playerActiveItemDurabilityColor")
			{
				_value = "255,255,255";
				bool flag56 = __instance.localPlayer != null;
				if (flag56)
				{
					EntityPlayer localPlayer6 = __instance.localPlayer;
					Inventory inventory5 = localPlayer6.inventory;
					ItemValue itemValue3 = inventory5.GetItem(__instance.currentSlotIndex).itemValue;
					bool flag57 = itemValue3 != null;
					if (flag57)
					{
						Color32 v = QualityInfo.GetQualityColor((int)itemValue3.Quality);
						_value = XUiC_HUDStatBarPatch.rgbaColorFormatter.Format(v);
					}
					__instance.IsDirty = true;
				}
				__result = true;
				return false;
			}
		}
		else if (num != 4127094052U)
		{
			if (num != 4160249550U)
			{
				if (num == 4256825341U)
				{
					if (_bindingName == "CATUI_playerGameStage")
					{
						_value = "1";
						bool flag58 = __instance.localPlayer != null;
						if (flag58)
						{
							_value = __instance.localPlayer.gameStage.ToString();
							__instance.IsDirty = true;
						}
						__result = true;
						return false;
					}
				}
			}
			else if (_bindingName == "CATUI_playerTraderStageProgressMax")
			{
				_value = "10";
				bool flag59 = __instance.localPlayer != null;
				if (flag59)
				{
					int currentFactionTier = __instance.localPlayer.QuestJournal.GetCurrentFactionTier(1, 0, false);
					_value = __instance.localPlayer.QuestJournal.GetQuestFactionMax(1, currentFactionTier).ToString();
					__instance.IsDirty = true;
				}
				__result = true;
				return false;
			}
		}
		else if (_bindingName == "CATUI_playerArmorLevel")
		{
			_value = "1";
			bool flag60 = __instance.localPlayer != null;
			if (flag60)
			{
				string s = XUiC_HUDStatBarPatch.playerArmorRatingFormatter.Format((int)EffectManager.GetValue(PassiveEffects.PhysicalDamageResist, null, 0f, __instance.localPlayer, null, default(FastTags<TagGroup.Global>), true, true, true, true, true, 1, true, false));
				int num12 = int.Parse(s);
				if (!true)
				{
				}
				string text;
				if (num12 < 40)
				{
					if (num12 > 0)
					{
						if (num12 >= 20)
						{
							text = "2";
							goto IL_8E4;
						}
						text = "1";
						goto IL_8E4;
					}
					else if (num12 == 0)
					{
						text = "0";
						goto IL_8E4;
					}
				}
				else if (num12 < 80)
				{
					if (num12 >= 60)
					{
						text = "4";
						goto IL_8E4;
					}
					text = "3";
					goto IL_8E4;
				}
				else if (num12 < 100)
				{
					text = "5";
					goto IL_8E4;
				}
				text = "6";
				IL_8E4:
				if (!true)
				{
				}
				_value = text;
				__instance.IsDirty = true;
			}
			__result = true;
			return false;
		}
		return true;
	}

	// Token: 0x06000019 RID: 25 RVA: 0x00004A68 File Offset: 0x00002C68
	[HarmonyPrefix]
	[HarmonyPatch(typeof(XUiC_HUDStatBar), "Update")]
	public static void Prefix(XUiC_HUDStatBar __instance)
	{
		bool flag = __instance.vehicle != null;
		if (flag)
		{
			__instance.RefreshBindings(true);
		}
	}

	// Token: 0x04000007 RID: 7
	[PublicizedFrom(EAccessModifier.Private)]
	public static CachedStringFormatterXuiRgbaColor rgbaColorFormatter = new CachedStringFormatterXuiRgbaColor();

	// Token: 0x04000008 RID: 8
	[PublicizedFrom(EAccessModifier.Private)]
	public static CachedStringFormatterInt playerStatCurrentHealthMaxFormatter = new CachedStringFormatterInt();

	// Token: 0x04000009 RID: 9
	[PublicizedFrom(EAccessModifier.Private)]
	public static CachedStringFormatterInt playerStatCurrentStaminaMaxFormatter = new CachedStringFormatterInt();

	// Token: 0x0400000A RID: 10
	[PublicizedFrom(EAccessModifier.Private)]
	public static CachedStringFormatterFloat playerEntityPenetrationCountFormatter = new CachedStringFormatterFloat(null);

	// Token: 0x0400000B RID: 11
	[PublicizedFrom(EAccessModifier.Private)]
	public static CachedStringFormatterInt playerArmorRatingFormatter = new CachedStringFormatterInt();

	// Token: 0x0400000C RID: 12
	[PublicizedFrom(EAccessModifier.Private)]
	public static CachedStringFormatterFloat playerRunSpeedFormatter = new CachedStringFormatterFloat(null);

	// Token: 0x0400000D RID: 13
	[PublicizedFrom(EAccessModifier.Private)]
	public static CachedStringFormatterInt playerCurrencyAmountFormatter = new CachedStringFormatterInt();
}
