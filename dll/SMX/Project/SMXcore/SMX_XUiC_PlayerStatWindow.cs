using System;
using System.Globalization;
using UnityEngine;

namespace SMXcore
{
	// Token: 0x02000006 RID: 6
	public class XUiC_PlayerStatWindow : XUiController
	{
		// Token: 0x06000014 RID: 20 RVA: 0x000027AE File Offset: 0x000009AE
		public override void Init()
		{
			base.Init();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000027B8 File Offset: 0x000009B8
		public override bool GetBindingValueInternal(ref string value, string bindingName)
		{
			uint num = global::<PrivateImplementationDetails>.ComputeStringHash(bindingName);
			if (num <= 2219475343U)
			{
				if (num <= 885900949U)
				{
					if (num <= 126665037U)
					{
						if (num <= 25234755U)
						{
							if (num != 8937094U)
							{
								if (num == 25234755U)
								{
									if (bindingName == "playercarrycapacity")
									{
										bool flag = XUi.IsGameRunning() && this.player != null;
										if (flag)
										{
											value = ((float)MathUtils.Min(this.player.bag.MaxItemCount, this.player.bag.SlotCount)).ToString();
										}
										return true;
									}
								}
							}
							else if (bindingName == "playerhealth")
							{
								value = ((this.player != null) ? this.playerHealthFormatter.Format((int)XUiM_Player.GetHealth(this.player)) : "");
								return true;
							}
						}
						else if (num != 75296162U)
						{
							if (num != 83123014U)
							{
								if (num == 126665037U)
								{
									if (bindingName == "playerfoodfill")
									{
										value = ((this.player != null) ? this.playerFoodFillFormatter.Format(XUiM_Player.GetFoodPercent(this.player)) : "");
										return true;
									}
								}
							}
							else if (bindingName == "playerlevel")
							{
								value = ((this.player != null) ? this.playerLevelFormatter.Format(XUiM_Player.GetLevel(this.player)) : "");
								return true;
							}
						}
						else if (bindingName == "playerfoodtitle")
						{
							value = Localization.Get("xuiFood", false);
							return true;
						}
					}
					else if (num <= 696376978U)
					{
						if (num != 234495987U)
						{
							if (num != 304163417U)
							{
								if (num == 696376978U)
								{
									if (bindingName == "playerlootstagetitle")
									{
										value = Localization.Get("xuiLootstage", false);
										return true;
									}
								}
							}
							else if (bindingName == "playercoretemp")
							{
								value = ((this.player != null) ? XUiM_Player.GetCoreTemp(this.player) : "");
								return true;
							}
						}
						else if (bindingName == "playerdeathstitle")
						{
							value = Localization.Get("xuiDeaths", false);
							return true;
						}
					}
					else if (num != 772965355U)
					{
						if (num != 782575427U)
						{
							if (num == 885900949U)
							{
								if (bindingName == "playertravelledtitle")
								{
									value = Localization.Get("xuiKMTravelled", false);
									return true;
								}
							}
						}
						else if (bindingName == "playerdeaths")
						{
							value = ((this.player != null) ? this.playerDeathsFormatter.Format(XUiM_Player.GetDeaths(this.player)) : "");
							return true;
						}
					}
					else if (bindingName == "playerbagfill")
					{
						bool flag2 = XUi.IsGameRunning() && this.player != null;
						if (flag2)
						{
							value = Mathf.Clamp01((float)this.player.bag.GetUsedSlotCount() / (float)this.player.bag.SlotCount).ToCultureInvariantString();
						}
						return true;
					}
				}
				else if (num <= 1564253156U)
				{
					if (num <= 1467771888U)
					{
						if (num != 939501123U)
						{
							if (num != 965025103U)
							{
								if (num == 1467771888U)
								{
									if (bindingName == "playerleveltitle")
									{
										value = Localization.Get("xuiLevel", false);
										return true;
									}
								}
							}
							else if (bindingName == "playerwater")
							{
								value = ((this.player != null) ? this.playerWaterFormatter.Format(XUiM_Player.GetWater(this.player)) : "");
								return true;
							}
						}
						else if (bindingName == "playerwellnesstitle")
						{
							value = Localization.Get("xuiWellness", false);
							return true;
						}
					}
					else if (num != 1477941828U)
					{
						if (num != 1541848814U)
						{
							if (num == 1564253156U)
							{
								if (bindingName == "time")
								{
									value = "";
									bool flag3 = XUi.IsGameRunning() && base.xui.playerUI.entityPlayer != null;
									if (flag3)
									{
										value = this.timeFormatter.Format(GameManager.Instance.World.worldTime);
									}
									return true;
								}
							}
						}
						else if (bindingName == "playerhungerdeficiency")
						{
							bool flag4 = XUi.IsGameRunning() && this.player != null;
							if (flag4)
							{
								int num2 = StringParsers.ParseSInt32(this.playerFoodMaxFormatter.Format(XUiM_Player.GetFoodMax(this.player)), 0, -1, NumberStyles.Integer);
								int num3 = StringParsers.ParseSInt32(this.playerFoodFormatter.Format(XUiM_Player.GetFood(this.player)), 0, -1, NumberStyles.Integer);
								value = (num2 - num3).ToString();
							}
							return true;
						}
					}
					else if (bindingName == "playerlongestlife")
					{
						value = ((this.player != null) ? XUiM_Player.GetLongestLife(this.player) : "");
						return true;
					}
				}
				else if (num <= 2023588471U)
				{
					if (num != 1811778199U)
					{
						if (num != 1905779113U)
						{
							if (num == 2023588471U)
							{
								if (bindingName == "playerzombiekillstitle")
								{
									value = Localization.Get("xuiZombieKills", false);
									return true;
								}
							}
						}
						else if (bindingName == "playerthirstdeficiency")
						{
							bool flag5 = XUi.IsGameRunning() && this.player != null;
							if (flag5)
							{
								int num4 = StringParsers.ParseSInt32(this.playerWaterMaxFormatter.Format(XUiM_Player.GetWaterMax(this.player)), 0, -1, NumberStyles.Integer);
								int num5 = StringParsers.ParseSInt32(this.playerWaterFormatter.Format(XUiM_Player.GetWater(this.player)), 0, -1, NumberStyles.Integer);
								value = (num4 - num5).ToString();
							}
							return true;
						}
					}
					else if (bindingName == "playeritemscraftedtitle")
					{
						value = Localization.Get("xuiItemsCrafted", false);
						return true;
					}
				}
				else if (num != 2128080849U)
				{
					if (num != 2186126559U)
					{
						if (num == 2219475343U)
						{
							if (bindingName == "playermaxstamina")
							{
								value = ((this.player != null) ? this.playerMaxStaminaFormatter.Format((int)XUiM_Player.GetMaxStamina(this.player)) : "");
								return true;
							}
						}
					}
					else if (bindingName == "playeritemscrafted")
					{
						value = ((this.player != null) ? this.playerItemsCraftedFormatter.Format(XUiM_Player.GetItemsCrafted(this.player)) : "");
						return true;
					}
				}
				else if (bindingName == "playerbagfreeslots")
				{
					bool flag6 = XUi.IsGameRunning() && this.player != null;
					if (flag6)
					{
						int slotCount = this.player.bag.SlotCount;
						int usedSlotCount = this.player.bag.GetUsedSlotCount();
						value = (slotCount - usedSlotCount).ToString();
					}
					return true;
				}
			}
			else if (num <= 3537464933U)
			{
				if (num <= 3042900123U)
				{
					if (num <= 2532548756U)
					{
						if (num != 2395478116U)
						{
							if (num == 2532548756U)
							{
								if (bindingName == "playerfood")
								{
									value = ((this.player != null) ? this.playerFoodFormatter.Format(XUiM_Player.GetFood(this.player)) : "");
									return true;
								}
							}
						}
						else if (bindingName == "playerlootstage")
						{
							value = ((this.player != null) ? this.player.GetHighestPartyLootStage(0f, 0f).ToString() : "");
							return true;
						}
					}
					else if (num != 2863867014U)
					{
						if (num != 2974192615U)
						{
							if (num == 3042900123U)
							{
								if (bindingName == "playerpvpkills")
								{
									value = ((this.player != null) ? this.playerPvpKillsFormatter.Format(XUiM_Player.GetPlayerKills(this.player)) : "");
									return true;
								}
							}
						}
						else if (bindingName == "playerwatertitle")
						{
							value = Localization.Get("xuiWater", false);
							return true;
						}
					}
					else if (bindingName == "hasskillpoint")
					{
						bool flag7 = XUi.IsGameRunning() && this.player != null;
						if (flag7)
						{
							bool flag8 = this.player.Progression.SkillPoints > 0;
							if (flag8)
							{
								value = "true";
							}
							else
							{
								value = "false";
							}
						}
						return true;
					}
				}
				else if (num <= 3275992332U)
				{
					if (num != 3249756066U)
					{
						if (num != 3270262403U)
						{
							if (num == 3275992332U)
							{
								if (bindingName == "playermaxhealth")
								{
									value = ((this.player != null) ? this.playerMaxHealthFormatter.Format((int)XUiM_Player.GetMaxHealth(this.player)) : "");
									return true;
								}
							}
						}
						else if (bindingName == "npcportrait")
						{
							bool flag9 = base.xui.Dialog.Respondent != null;
							if (flag9)
							{
								value = this.NPC.NPCInfo.Portrait;
							}
							return true;
						}
					}
					else if (bindingName == "playerfoodmax")
					{
						value = ((this.player != null) ? this.playerFoodMaxFormatter.Format(XUiM_Player.GetFoodMax(this.player)) : "");
						return true;
					}
				}
				else if (num != 3422893579U)
				{
					if (num != 3484390642U)
					{
						if (num == 3537464933U)
						{
							if (bindingName == "playerstamina")
							{
								value = ((this.player != null) ? this.playerStaminaFormatter.Format((int)XUiM_Player.GetStamina(this.player)) : "");
								return true;
							}
						}
					}
					else if (bindingName == "playerlongestlifetitle")
					{
						value = Localization.Get("xuiLongestLife", false);
						return true;
					}
				}
				else if (bindingName == "playerlevelfill")
				{
					value = ((this.player != null) ? this.playerLevelFillFormatter.Format(XUiM_Player.GetLevelPercent(this.player)) : "");
					return true;
				}
			}
			else if (num <= 4025935093U)
			{
				if (num <= 3830391293U)
				{
					if (num != 3705263762U)
					{
						if (num != 3753559114U)
						{
							if (num == 3830391293U)
							{
								if (bindingName == "day")
								{
									value = "";
									bool flag10 = XUi.IsGameRunning() && base.xui.playerUI.entityPlayer != null;
									if (flag10)
									{
										value = this.dayFormatter.Format(GameManager.Instance.World.worldTime);
									}
									return true;
								}
							}
						}
						else if (bindingName == "playerbagfillcolor")
						{
							bool flag11 = XUi.IsGameRunning() && this.player != null;
							if (flag11)
							{
								float num6 = (float)MathUtils.Min(this.player.bag.MaxItemCount, this.player.bag.SlotCount);
								float num7 = (float)this.player.bag.GetUsedSlotCount() / num6;
								value = "43,124,18,255";
								bool flag12 = (double)num7 > 0.75;
								if (flag12)
								{
									value = "255,255,0,255";
								}
								bool flag13 = (double)num7 > 0.9;
								if (flag13)
								{
									value = "255,144,24,255";
								}
								bool flag14 = num7 > 1f;
								if (flag14)
								{
									value = "175,30,25,255";
								}
							}
							return true;
						}
					}
					else if (bindingName == "playerxptonextlevel")
					{
						value = ((this.player != null) ? this.playerXpToNextLevelFormatter.Format(XUiM_Player.GetXPToNextLevel(this.player) + this.player.Progression.ExpDeficit) : "");
						return true;
					}
				}
				else if (num != 3887827771U)
				{
					if (num != 3931175545U)
					{
						if (num == 4025935093U)
						{
							if (bindingName == "playercoretemptitle")
							{
								value = Localization.Get("xuiFeelsLike", false);
								return true;
							}
						}
					}
					else if (bindingName == "playertravelled")
					{
						value = ((this.player != null) ? XUiM_Player.GetKMTraveled(this.player) : "");
						return true;
					}
				}
				else if (bindingName == "playerpvpkillstitle")
				{
					value = Localization.Get("xuiPlayerKills", false);
					return true;
				}
			}
			else if (num <= 4077864767U)
			{
				if (num != 4035727244U)
				{
					if (num != 4046338599U)
					{
						if (num == 4077864767U)
						{
							if (bindingName == "playerzombiekills")
							{
								value = ((this.player != null) ? this.playerZombieKillsFormatter.Format(XUiM_Player.GetZombieKills(this.player)) : "");
								return true;
							}
						}
					}
					else if (bindingName == "playerbagsize")
					{
						value = ((this.player != null) ? this.player.bag.SlotCount.ToString() : "");
						return true;
					}
				}
				else if (bindingName == "skillpointsavailable")
				{
					string v = this.pointsAvailable;
					EntityPlayerLocal entityPlayer = base.xui.playerUI.entityPlayer;
					bool flag15 = XUi.IsGameRunning() && entityPlayer != null;
					if (flag15)
					{
						value = this.skillPointsAvailableFormatter.Format(v, entityPlayer.Progression.SkillPoints);
					}
					return true;
				}
			}
			else if (num != 4080145910U)
			{
				if (num != 4103208176U)
				{
					if (num == 4107995367U)
					{
						if (bindingName == "playerwatermax")
						{
							value = ((this.player != null) ? this.playerWaterMaxFormatter.Format(XUiM_Player.GetWaterMax(this.player)) : "");
							return true;
						}
					}
				}
				else if (bindingName == "playerbagusedslots")
				{
					bool flag16 = XUi.IsGameRunning() && this.player != null;
					if (flag16)
					{
						value = this.player.bag.GetUsedSlotCount().ToString();
					}
					return true;
				}
			}
			else if (bindingName == "playerwaterfill")
			{
				value = ((this.player != null) ? this.playerWaterFillFormatter.Format(XUiM_Player.GetWaterPercent(this.player)) : "");
				return true;
			}
			return base.GetBindingValueInternal(ref value, bindingName);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000038DC File Offset: 0x00001ADC
		public override void Update(float _dt)
		{
			bool flag = this.viewComponent.IsVisible && Time.time > this.updateTime;
			if (flag)
			{
				this.updateTime = Time.time + 0.25f;
				base.RefreshBindings(this.IsDirty);
				bool isDirty = this.IsDirty;
				if (isDirty)
				{
					this.IsDirty = false;
				}
			}
			base.Update(_dt);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00003946 File Offset: 0x00001B46
		public override void OnOpen()
		{
			base.OnOpen();
			this.IsDirty = true;
			this.player = base.xui.playerUI.entityPlayer;
			this.NPC = base.xui.Dialog.Respondent;
		}

		// Token: 0x0400000B RID: 11
		private EntityPlayer player;

		// Token: 0x0400000C RID: 12
		private EntityNPC NPC;

		// Token: 0x0400000D RID: 13
		private EntityVehicle vehicle;

		// Token: 0x0400000E RID: 14
		private readonly CachedStringFormatter<int> playerDeathsFormatter = new CachedStringFormatter<int>((int _i) => _i.ToString());

		// Token: 0x0400000F RID: 15
		private readonly CachedStringFormatter<float> playerFoodFormatter = new CachedStringFormatter<float>((float _i) => _i.ToCultureInvariantString("0"));

		// Token: 0x04000010 RID: 16
		private readonly CachedStringFormatter<float> playerFoodFillFormatter = new CachedStringFormatter<float>((float _i) => _i.ToCultureInvariantString());

		// Token: 0x04000011 RID: 17
		private readonly CachedStringFormatter<int> playerItemsCraftedFormatter = new CachedStringFormatter<int>((int _i) => _i.ToString());

		// Token: 0x04000012 RID: 18
		private readonly CachedStringFormatter<int> playerLevelFormatter = new CachedStringFormatter<int>((int _i) => _i.ToString());

		// Token: 0x04000013 RID: 19
		private readonly CachedStringFormatter<float> playerLevelFillFormatter = new CachedStringFormatter<float>((float _i) => _i.ToCultureInvariantString());

		// Token: 0x04000014 RID: 20
		private readonly CachedStringFormatter<int> playerPvpKillsFormatter = new CachedStringFormatter<int>((int _i) => _i.ToString());

		// Token: 0x04000015 RID: 21
		private readonly CachedStringFormatter<float> playerWaterFormatter = new CachedStringFormatter<float>((float _i) => _i.ToCultureInvariantString("0"));

		// Token: 0x04000016 RID: 22
		private readonly CachedStringFormatter<float> playerWaterFillFormatter = new CachedStringFormatter<float>((float _i) => _i.ToCultureInvariantString());

		// Token: 0x04000017 RID: 23
		private readonly CachedStringFormatter<int> playerZombieKillsFormatter = new CachedStringFormatter<int>((int _i) => _i.ToString());

		// Token: 0x04000018 RID: 24
		private readonly CachedStringFormatter<int> playerWaterMaxFormatter = new CachedStringFormatter<int>((int _i) => _i.ToString());

		// Token: 0x04000019 RID: 25
		private readonly CachedStringFormatter<int> playerFoodMaxFormatter = new CachedStringFormatter<int>((int _i) => _i.ToString());

		// Token: 0x0400001A RID: 26
		private readonly CachedStringFormatter<int> playerHealthFormatter = new CachedStringFormatter<int>((int _i) => _i.ToString());

		// Token: 0x0400001B RID: 27
		private readonly CachedStringFormatter<int> playerMaxHealthFormatter = new CachedStringFormatter<int>((int _i) => _i.ToString());

		// Token: 0x0400001C RID: 28
		private readonly CachedStringFormatter<int> playerStaminaFormatter = new CachedStringFormatter<int>((int _i) => _i.ToString());

		// Token: 0x0400001D RID: 29
		private readonly CachedStringFormatter<int> playerMaxStaminaFormatter = new CachedStringFormatter<int>((int _i) => _i.ToString());

		// Token: 0x0400001E RID: 30
		private readonly CachedStringFormatter<int> playerXpToNextLevelFormatter = new CachedStringFormatter<int>((int _i) => _i.ToString());

		// Token: 0x0400001F RID: 31
		private readonly CachedStringFormatter<int> playerCarryCapacityFormatter = new CachedStringFormatter<int>((int _i) => _i.ToString());

		// Token: 0x04000020 RID: 32
		private readonly CachedStringFormatter<int> playerBagSizeFormatter = new CachedStringFormatter<int>((int _i) => _i.ToString());

		// Token: 0x04000021 RID: 33
		private readonly CachedStringFormatter<ulong> dayFormatter = new CachedStringFormatter<ulong>((ulong _worldTime) => ValueDisplayFormatters.WorldTime(_worldTime, "{0}"));

		// Token: 0x04000022 RID: 34
		private readonly CachedStringFormatter<ulong> timeFormatter = new CachedStringFormatter<ulong>((ulong _worldTime) => ValueDisplayFormatters.WorldTime(_worldTime, "{1:00}:{2:00}"));

		// Token: 0x04000023 RID: 35
		private float updateTime;

		// Token: 0x04000024 RID: 36
		private string pointsAvailable;

		// Token: 0x04000025 RID: 37
		private readonly CachedStringFormatter<string, int> skillPointsAvailableFormatter = new CachedStringFormatter<string, int>((string _s, int _i) => string.Format("{1}", _s, _i));
	}
}
