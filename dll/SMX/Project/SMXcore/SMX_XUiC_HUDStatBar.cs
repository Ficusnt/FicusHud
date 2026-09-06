using System;
using UnityEngine;

namespace SMXcore
{
	// Token: 0x02000009 RID: 9
	public class XUiC_HUDStatBar : XUiController
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00004270 File Offset: 0x00002470
		// (set) Token: 0x06000021 RID: 33 RVA: 0x00004288 File Offset: 0x00002488
		public HUDStatGroups StatGroup
		{
			get
			{
				return this.statGroup;
			}
			set
			{
				this.statGroup = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00004294 File Offset: 0x00002494
		// (set) Token: 0x06000023 RID: 35 RVA: 0x000042AC File Offset: 0x000024AC
		public HUDStatTypes StatType
		{
			get
			{
				return this.statType;
			}
			set
			{
				this.statType = value;
				this.SetStatValues();
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000042BD File Offset: 0x000024BD
		// (set) Token: 0x06000025 RID: 37 RVA: 0x000042C5 File Offset: 0x000024C5
		public EntityPlayer LocalPlayer { get; internal set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000042CE File Offset: 0x000024CE
		// (set) Token: 0x06000027 RID: 39 RVA: 0x000042D6 File Offset: 0x000024D6
		public EntityVehicle Vehicle { get; internal set; }

		// Token: 0x06000028 RID: 40 RVA: 0x000042E0 File Offset: 0x000024E0
		public override void Init()
		{
			base.Init();
			this.IsDirty = true;
			XUiController childById = base.GetChildById("BarContent");
			bool flag = childById != null;
			if (flag)
			{
				this.barContent = (XUiV_Sprite)childById.ViewComponent;
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00004324 File Offset: 0x00002524
		public override void Update(float _dt)
		{
			base.Update(_dt);
			this.deltaTime = _dt;
			bool flag = this.LocalPlayer == null && XUi.IsGameRunning();
			if (flag)
			{
				this.LocalPlayer = base.xui.playerUI.entityPlayer;
			}
			bool flag2 = this.statGroup == HUDStatGroups.Vehicle && this.LocalPlayer != null;
			if (flag2)
			{
				bool flag3 = this.Vehicle == null && this.LocalPlayer.AttachedToEntity != null && this.LocalPlayer.AttachedToEntity is EntityVehicle;
				if (flag3)
				{
					this.Vehicle = (EntityVehicle)this.LocalPlayer.AttachedToEntity;
					this.IsDirty = true;
				}
				else
				{
					bool flag4 = this.Vehicle != null && this.LocalPlayer.AttachedToEntity == null;
					if (flag4)
					{
						this.Vehicle = null;
						this.IsDirty = true;
					}
				}
			}
			bool flag5 = this.statType == HUDStatTypes.Stealth && this.LocalPlayer.IsCrouching != this.wasCrouching;
			if (flag5)
			{
				this.wasCrouching = this.LocalPlayer.IsCrouching;
				base.RefreshBindings(true);
				this.IsDirty = true;
			}
			this.RefreshFill();
			bool flag6 = this.HasChanged() || this.IsDirty;
			if (flag6)
			{
				bool isDirty = this.IsDirty;
				if (isDirty)
				{
					this.IsDirty = false;
				}
				base.RefreshBindings(true);
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000044B0 File Offset: 0x000026B0
		public override void OnOpen()
		{
			base.OnOpen();
			this.IsDirty = true;
			base.RefreshBindings(true);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000044C9 File Offset: 0x000026C9
		public override void OnClose()
		{
			base.OnClose();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000044D4 File Offset: 0x000026D4
		public override bool GetBindingValueInternal(ref string value, string bindingName)
		{
			uint num = global::<PrivateImplementationDetails>.ComputeStringHash(bindingName);
			if (num <= 2758588565U)
			{
				if (num <= 1122103630U)
				{
					if (num != 243870623U)
					{
						if (num != 669092238U)
						{
							if (num == 1122103630U)
							{
								if (bindingName == "statcurrentwithmax")
								{
									value = this.GetCurrentStatWithMax();
									return true;
								}
							}
						}
						else if (bindingName == "statfill")
						{
							bool flag = this.LocalPlayer == null || (this.statGroup == HUDStatGroups.Vehicle && this.Vehicle == null);
							if (flag)
							{
								value = "0";
								return true;
							}
							float t = this.deltaTime * 3f;
							float b = 0f;
							switch (this.statType)
							{
							case HUDStatTypes.Health:
								b = this.LocalPlayer.Stats.Health.ValuePercentUI;
								break;
							case HUDStatTypes.Stamina:
								b = this.LocalPlayer.Stats.Stamina.ValuePercentUI;
								break;
							case HUDStatTypes.Water:
								b = this.LocalPlayer.Stats.Water.ValuePercentUI;
								break;
							case HUDStatTypes.Food:
								b = this.LocalPlayer.Stats.Food.ValuePercentUI;
								break;
							case HUDStatTypes.Stealth:
								b = this.LocalPlayer.Stealth.ValuePercentUI;
								break;
							case HUDStatTypes.VehicleHealth:
								b = this.Vehicle.GetVehicle().GetHealthPercent();
								break;
							case HUDStatTypes.VehicleFuel:
								b = this.Vehicle.GetVehicle().GetFuelPercent();
								break;
							case HUDStatTypes.VehicleBattery:
								b = this.Vehicle.GetVehicle().GetBatteryLevel();
								break;
							}
							float v = Math.Max(this.lastValue, 0f) * 1.01f;
							value = this.statfillFormatter.Format(v);
							this.lastValue = Mathf.Lerp(this.lastValue, b, t);
							return true;
						}
					}
					else if (bindingName == "statmax")
					{
						value = this.GetMaxStat();
						return true;
					}
				}
				else if (num != 1542587592U)
				{
					if (num != 1822678806U)
					{
						if (num == 2758588565U)
						{
							if (bindingName == "staticonatlas")
							{
								value = this.statAtlas;
								return true;
							}
						}
					}
					else if (bindingName == "staticon")
					{
						bool flag2 = this.statType == HUDStatTypes.VehicleHealth;
						if (flag2)
						{
							value = ((this.Vehicle != null) ? this.Vehicle.GetMapIcon() : "");
						}
						else
						{
							value = this.statIcon;
						}
						return true;
					}
				}
				else if (bindingName == "statcurrent")
				{
					value = this.GetCurrentStat();
					return true;
				}
			}
			else if (num <= 3799067675U)
			{
				if (num != 2825508620U)
				{
					if (num != 3150708601U)
					{
						if (num == 3799067675U)
						{
							if (bindingName == "statvisible")
							{
								value = this.IsStatVisible().ToString();
								return true;
							}
						}
					}
					else if (bindingName == "staticoncolor")
					{
						value = this.staticoncolorFormatter.Format(Color.white);
						return true;
					}
				}
				else if (bindingName == "statimage")
				{
					value = this.statImage;
					return true;
				}
			}
			else if (num != 3888153342U)
			{
				if (num != 3905392387U)
				{
					if (num == 3907838626U)
					{
						if (bindingName == "statmodifiedmax")
						{
							bool flag3 = this.LocalPlayer == null || (this.statGroup == HUDStatGroups.Vehicle && this.Vehicle == null);
							if (flag3)
							{
								value = "0";
								return true;
							}
							switch (this.statType)
							{
							case HUDStatTypes.Health:
								value = this.statmodifiedmaxFormatter.Format(this.LocalPlayer.Stats.Health.ModifiedMax, this.LocalPlayer.Stats.Health.Max);
								break;
							case HUDStatTypes.Stamina:
								value = this.statmodifiedmaxFormatter.Format(this.LocalPlayer.Stats.Stamina.ModifiedMax, this.LocalPlayer.Stats.Stamina.Max);
								break;
							case HUDStatTypes.Water:
								value = this.statmodifiedmaxFormatter.Format(this.LocalPlayer.Stats.Water.ModifiedMax, this.LocalPlayer.Stats.Water.Max);
								break;
							case HUDStatTypes.Food:
								value = this.statmodifiedmaxFormatter.Format(this.LocalPlayer.Stats.Food.ModifiedMax, this.LocalPlayer.Stats.Food.Max);
								break;
							}
							return true;
						}
					}
				}
				else if (bindingName == "stealthcolor")
				{
					Color32 v2 = Color32.Lerp(new Color32(72, 82, 0, byte.MaxValue), new Color32(187, 199, 0, byte.MaxValue), this.lastValue);
					value = this.stealthColorFormatter.Format(v2);
					return true;
				}
			}
			else if (bindingName == "statregenrate")
			{
				bool flag4 = this.LocalPlayer == null || (this.statGroup == HUDStatGroups.Vehicle && this.Vehicle == null);
				if (flag4)
				{
					value = "0";
					return true;
				}
				switch (this.statType)
				{
				case HUDStatTypes.Health:
					value = this.statregenrateFormatter.Format(this.LocalPlayer.Stats.Health.RegenerationAmountUI);
					break;
				case HUDStatTypes.Stamina:
					value = this.statregenrateFormatter.Format(this.LocalPlayer.Stats.Stamina.RegenerationAmountUI);
					break;
				case HUDStatTypes.Water:
					value = this.statregenrateFormatter.Format(this.LocalPlayer.Stats.Water.RegenerationAmountUI);
					break;
				case HUDStatTypes.Food:
					value = this.statregenrateFormatter.Format(this.LocalPlayer.Stats.Food.RegenerationAmountUI);
					break;
				}
				return true;
			}
			return base.GetBindingValueInternal(ref value, bindingName);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00004B90 File Offset: 0x00002D90
		public override bool ParseAttribute(string name, string value, XUiController _parent)
		{
			bool flag = base.ParseAttribute(name, value, _parent);
			bool flag2 = !flag;
			bool result;
			if (flag2)
			{
				bool flag3 = name != null && name == "stat_type";
				if (flag3)
				{
					this.StatType = EnumUtils.Parse<HUDStatTypes>(value, true);
					result = true;
				}
				else
				{
					result = false;
				}
			}
			else
			{
				result = flag;
			}
			return result;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00004BE4 File Offset: 0x00002DE4
		public bool HasChanged()
		{
			bool result = false;
			switch (this.statType)
			{
			case HUDStatTypes.Health:
				result = true;
				break;
			case HUDStatTypes.Stamina:
				result = true;
				break;
			case HUDStatTypes.Water:
				result = (this.oldValue != this.LocalPlayer.Stats.Water.ValuePercentUI);
				this.oldValue = this.LocalPlayer.Stats.Water.ValuePercentUI;
				break;
			case HUDStatTypes.Food:
				result = (this.oldValue != this.LocalPlayer.Stats.Food.ValuePercentUI);
				this.oldValue = this.LocalPlayer.Stats.Food.ValuePercentUI;
				break;
			case HUDStatTypes.Stealth:
				result = (this.oldValue != this.lastValue);
				this.oldValue = this.lastValue;
				break;
			case HUDStatTypes.VehicleHealth:
			{
				bool flag = this.Vehicle == null;
				if (flag)
				{
					return false;
				}
				int health = this.Vehicle.GetVehicle().GetHealth();
				result = (this.oldValue != (float)health);
				this.oldValue = (float)health;
				break;
			}
			case HUDStatTypes.VehicleFuel:
			{
				bool flag2 = this.Vehicle == null;
				if (flag2)
				{
					return false;
				}
				result = (this.oldValue != this.Vehicle.GetVehicle().GetFuelLevel());
				this.oldValue = this.Vehicle.GetVehicle().GetFuelLevel();
				break;
			}
			case HUDStatTypes.VehicleBattery:
			{
				bool flag3 = this.Vehicle == null;
				if (flag3)
				{
					return false;
				}
				result = (this.oldValue != this.Vehicle.GetVehicle().GetBatteryLevel());
				this.oldValue = this.Vehicle.GetVehicle().GetBatteryLevel();
				break;
			}
			}
			return result;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00004DCC File Offset: 0x00002FCC
		public void RefreshFill()
		{
			bool flag = this.barContent != null && !(this.LocalPlayer == null) && (this.statGroup != HUDStatGroups.Vehicle || !(this.Vehicle == null));
			if (flag)
			{
				float t = Time.deltaTime * 3f;
				float b = 0f;
				switch (this.statType)
				{
				case HUDStatTypes.Health:
					b = Mathf.Clamp01(this.LocalPlayer.Stats.Health.ValuePercentUI);
					break;
				case HUDStatTypes.Stamina:
					b = Mathf.Clamp01(this.LocalPlayer.Stats.Stamina.ValuePercentUI);
					break;
				case HUDStatTypes.Water:
					b = this.LocalPlayer.Stats.Water.ValuePercentUI;
					break;
				case HUDStatTypes.Food:
					b = this.LocalPlayer.Stats.Food.ValuePercentUI;
					break;
				case HUDStatTypes.Stealth:
					b = this.LocalPlayer.Stealth.ValuePercentUI;
					break;
				case HUDStatTypes.VehicleHealth:
					b = this.Vehicle.GetVehicle().GetHealthPercent();
					break;
				case HUDStatTypes.VehicleFuel:
					b = this.Vehicle.GetVehicle().GetFuelPercent();
					break;
				case HUDStatTypes.VehicleBattery:
					b = this.Vehicle.GetVehicle().GetBatteryLevel();
					break;
				}
				float fill = Math.Max(this.lastValue, 0f);
				this.lastValue = Mathf.Lerp(this.lastValue, b, t);
				this.barContent.Fill = fill;
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00004F50 File Offset: 0x00003150
		public void SetStatValues()
		{
			switch (this.statType)
			{
			case HUDStatTypes.Health:
				this.statImage = "ui_game_stat_bar_health";
				this.statIcon = "ui_game_symbol_add";
				this.statGroup = HUDStatGroups.Player;
				break;
			case HUDStatTypes.Stamina:
				this.statImage = "ui_game_stat_bar_stamina";
				this.statIcon = "ui_game_symbol_run";
				this.statGroup = HUDStatGroups.Player;
				break;
			case HUDStatTypes.Water:
				this.statImage = "ui_game_stat_bar_stamina";
				this.statIcon = "ui_game_symbol_water";
				this.statGroup = HUDStatGroups.Player;
				break;
			case HUDStatTypes.Food:
				this.statImage = "ui_game_stat_bar_health";
				this.statIcon = "ui_game_symbol_hunger";
				this.statGroup = HUDStatGroups.Player;
				break;
			case HUDStatTypes.Stealth:
				this.statImage = "ui_game_stat_bar_health";
				this.statIcon = "ui_game_symbol_stealth";
				this.statGroup = HUDStatGroups.Player;
				break;
			case HUDStatTypes.VehicleHealth:
				this.statImage = "ui_game_stat_bar_health";
				this.statIcon = "ui_game_symbol_minibike";
				this.statGroup = HUDStatGroups.Vehicle;
				break;
			case HUDStatTypes.VehicleFuel:
				this.statImage = "ui_game_stat_bar_stamina";
				this.statIcon = "ui_game_symbol_gas";
				this.statGroup = HUDStatGroups.Vehicle;
				break;
			case HUDStatTypes.VehicleBattery:
				this.statImage = "ui_game_popup";
				this.statIcon = "ui_game_symbol_battery";
				this.statGroup = HUDStatGroups.Vehicle;
				break;
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00005098 File Offset: 0x00003298
		private string GetCurrentStat()
		{
			string text = "";
			bool flag = this.LocalPlayer == null || (this.statGroup == HUDStatGroups.Vehicle && this.Vehicle == null);
			string result;
			if (flag)
			{
				result = text;
			}
			else
			{
				switch (this.statType)
				{
				case HUDStatTypes.Health:
					text = this.statcurrentFormatterInt.Format(this.LocalPlayer.Health);
					break;
				case HUDStatTypes.Stamina:
					text = this.statcurrentFormatterFloat.Format(this.LocalPlayer.Stamina);
					break;
				case HUDStatTypes.Water:
					text = this.statcurrentFormatterInt.Format((int)(this.LocalPlayer.Stats.Water.ValuePercentUI * 100f));
					break;
				case HUDStatTypes.Food:
					text = this.statcurrentFormatterInt.Format((int)(this.LocalPlayer.Stats.Food.ValuePercentUI * 100f));
					break;
				case HUDStatTypes.Stealth:
					text = this.statcurrentFormatterFloat.Format((float)((int)(this.LocalPlayer.Stealth.ValuePercentUI * 100f)));
					break;
				case HUDStatTypes.VehicleHealth:
					text = this.statcurrentFormatterInt.Format(this.Vehicle.GetVehicle().GetHealth());
					break;
				case HUDStatTypes.VehicleFuel:
					text = this.statcurrentFormatterFloat.Format(this.Vehicle.GetVehicle().GetFuelLevel());
					break;
				case HUDStatTypes.VehicleBattery:
					text = this.statcurrentFormatterFloat.Format(this.Vehicle.GetVehicle().GetBatteryLevel());
					break;
				}
				result = text;
			}
			return result;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00005234 File Offset: 0x00003434
		private string GetMaxStat()
		{
			string text = "";
			bool flag = this.LocalPlayer == null || (this.statGroup == HUDStatGroups.Vehicle && this.Vehicle == null);
			string result;
			if (flag)
			{
				result = text;
			}
			else
			{
				switch (this.statType)
				{
				case HUDStatTypes.Health:
					text = this.statcurrentFormatterInt.Format((int)this.LocalPlayer.Stats.Health.Max);
					break;
				case HUDStatTypes.Stamina:
					text = this.statcurrentFormatterInt.Format((int)this.LocalPlayer.Stats.Stamina.Max);
					break;
				case HUDStatTypes.Water:
					text = this.statcurrentFormatterInt.Format((int)this.LocalPlayer.Stats.Water.Max);
					break;
				case HUDStatTypes.Food:
					text = this.statcurrentFormatterInt.Format((int)this.LocalPlayer.Stats.Food.Max);
					break;
				case HUDStatTypes.VehicleHealth:
					text = this.statcurrentFormatterInt.Format(this.Vehicle.GetVehicle().GetMaxHealth());
					break;
				case HUDStatTypes.VehicleFuel:
					text = this.statcurrentFormatterInt.Format((int)this.Vehicle.GetVehicle().GetMaxFuelLevel());
					break;
				}
				result = text;
			}
			return result;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000538C File Offset: 0x0000358C
		private string GetCurrentStatWithMax()
		{
			string text = "";
			bool flag = this.LocalPlayer == null || (this.statGroup == HUDStatGroups.Vehicle && this.Vehicle == null);
			string result;
			if (flag)
			{
				result = text;
			}
			else
			{
				switch (this.statType)
				{
				case HUDStatTypes.Health:
					text = this.statcurrentWMaxFormatterAOfB.Format((int)this.LocalPlayer.Stats.Health.Value, (int)this.LocalPlayer.Stats.Health.Max);
					break;
				case HUDStatTypes.Stamina:
					text = this.statcurrentWMaxFormatterAOfB.Format((int)XUiM_Player.GetStamina(this.LocalPlayer), (int)this.LocalPlayer.Stats.Stamina.Max);
					break;
				case HUDStatTypes.Water:
					text = this.statcurrentWMaxFormatterOf100.Format((int)(this.LocalPlayer.Stats.Water.ValuePercentUI * 100f));
					break;
				case HUDStatTypes.Food:
					text = this.statcurrentWMaxFormatterOf100.Format((int)(this.LocalPlayer.Stats.Food.ValuePercentUI * 100f));
					break;
				case HUDStatTypes.Stealth:
					text = this.statcurrentWMaxFormatterOf100.Format((int)(this.LocalPlayer.Stealth.ValuePercentUI * 100f));
					break;
				case HUDStatTypes.VehicleHealth:
					text = this.statcurrentWMaxFormatterPercent.Format((int)(this.Vehicle.GetVehicle().GetHealthPercent() * 100f));
					break;
				case HUDStatTypes.VehicleFuel:
					text = this.statcurrentWMaxFormatterPercent.Format((int)(this.Vehicle.GetVehicle().GetFuelPercent() * 100f));
					break;
				case HUDStatTypes.VehicleBattery:
					text = this.statcurrentWMaxFormatterPercent.Format((int)(this.Vehicle.GetVehicle().GetBatteryLevel() * 100f));
					break;
				}
				result = text;
			}
			return result;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00005574 File Offset: 0x00003774
		private bool IsStatVisible()
		{
			bool flag = this.LocalPlayer == null;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = this.LocalPlayer.IsDead();
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = this.statGroup == HUDStatGroups.Vehicle;
					if (flag3)
					{
						bool flag4 = this.statType == HUDStatTypes.VehicleFuel;
						if (flag4)
						{
							result = (this.Vehicle != null && this.Vehicle.GetVehicle().HasEnginePart());
						}
						else
						{
							result = (this.Vehicle != null);
						}
					}
					else
					{
						bool flag5 = this.statType == HUDStatTypes.Stealth;
						if (flag5)
						{
							base.xui.BuffPopoutList.SetYOffset(this.LocalPlayer.Crouching ? 52 : 0);
							result = this.LocalPlayer.Crouching;
						}
						else
						{
							result = true;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x04000030 RID: 48
		private float lastValue;

		// Token: 0x04000031 RID: 49
		private HUDStatGroups statGroup;

		// Token: 0x04000032 RID: 50
		private HUDStatTypes statType;

		// Token: 0x04000033 RID: 51
		private string statImage = "";

		// Token: 0x04000034 RID: 52
		private string statIcon = "";

		// Token: 0x04000035 RID: 53
		private string statAtlas = "UIAtlas";

		// Token: 0x04000036 RID: 54
		private XUiV_Sprite barContent;

		// Token: 0x04000037 RID: 55
		private float deltaTime;

		// Token: 0x04000038 RID: 56
		private bool wasCrouching;

		// Token: 0x04000039 RID: 57
		private float oldValue;

		// Token: 0x0400003A RID: 58
		private readonly CachedStringFormatter<int> statcurrentFormatterInt = new CachedStringFormatter<int>((int _i) => _i.ToString());

		// Token: 0x0400003B RID: 59
		private readonly CachedStringFormatter<float> statcurrentFormatterFloat = new CachedStringFormatter<float>((float _i) => _i.ToCultureInvariantString());

		// Token: 0x0400003C RID: 60
		private readonly CachedStringFormatter<int, int> statcurrentWMaxFormatterAOfB = new CachedStringFormatter<int, int>((int _i, int _i1) => string.Format("{0}/{1}", _i, _i1));

		// Token: 0x0400003D RID: 61
		private readonly CachedStringFormatter<int> statcurrentWMaxFormatterOf100 = new CachedStringFormatter<int>((int _i) => _i.ToString() + "/100");

		// Token: 0x0400003E RID: 62
		private readonly CachedStringFormatter<int> statcurrentWMaxFormatterPercent = new CachedStringFormatter<int>((int _i) => _i.ToString() + "%");

		// Token: 0x0400003F RID: 63
		private readonly CachedStringFormatter<float, float> statmodifiedmaxFormatter = new CachedStringFormatter<float, float>((float _f1, float _f2) => (_f1 / _f2).ToCultureInvariantString());

		// Token: 0x04000040 RID: 64
		private readonly CachedStringFormatter<float> statregenrateFormatter = new CachedStringFormatter<float>((float _f) => ((_f >= 0f) ? "+" : "") + _f.ToCultureInvariantString("0.00"));

		// Token: 0x04000041 RID: 65
		private readonly CachedStringFormatter<float> statfillFormatter = new CachedStringFormatter<float>((float _i) => _i.ToCultureInvariantString());

		// Token: 0x04000042 RID: 66
		private readonly CachedStringFormatterXuiRgbaColor staticoncolorFormatter = new CachedStringFormatterXuiRgbaColor();

		// Token: 0x04000043 RID: 67
		private readonly CachedStringFormatterXuiRgbaColor stealthColorFormatter = new CachedStringFormatterXuiRgbaColor();
	}
}
