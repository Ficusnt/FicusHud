using System;
using HarmonyLib;

// Token: 0x02000005 RID: 5
[HarmonyPatch]
public class XUiC_CompassWindowPatch
{
	// Token: 0x06000008 RID: 8 RVA: 0x00002248 File Offset: 0x00000448
	[HarmonyPrefix]
	[HarmonyPatch(typeof(XUiC_CompassWindow), "GetBindingValueInternal")]
	public static bool GetBindingValueInternalPrefix(ref string value, string bindingName, ref bool __result, XUiC_CompassWindow __instance)
	{
		EntityPlayerLocal localPlayer = __instance.localPlayer;
		bool flag = localPlayer != null;
		BiomeDefinition.BiomeType? biomeType = null;
		WeatherManager.BiomeWeather biomeWeather = null;
		int num = 0;
		int num2 = 0;
		bool flag2 = flag;
		if (flag2)
		{
			num2 = WeatherManager.worldTime;
			BiomeDefinition biomeStandingOn = localPlayer.biomeStandingOn;
			bool flag3 = biomeStandingOn != null;
			if (flag3)
			{
				biomeType = new BiomeDefinition.BiomeType?(biomeStandingOn.m_BiomeType);
				biomeWeather = WeatherManager.Instance.FindBiomeWeather(biomeType.Value);
			}
			WeatherManager.BiomeWeather currentWeather = WeatherManager.currentWeather;
			int? num3;
			if (currentWeather == null)
			{
				num3 = null;
			}
			else
			{
				BiomeDefinition biomeDefinition = currentWeather.biomeDefinition;
				if (biomeDefinition == null)
				{
					num3 = null;
				}
				else
				{
					BiomeDefinition.WeatherGroup currentWeatherGroup = biomeDefinition.currentWeatherGroup;
					num3 = ((currentWeatherGroup != null) ? new int?(currentWeatherGroup.stormLevel) : null);
				}
			}
			int? num4 = num3;
			num = num4.GetValueOrDefault();
		}
		uint num5 = global::<PrivateImplementationDetails>.ComputeStringHash(bindingName);
		if (num5 <= 1505776159U)
		{
			if (num5 <= 1011318122U)
			{
				if (num5 != 272646402U)
				{
					if (num5 != 532664481U)
					{
						if (num5 == 1011318122U)
						{
							if (bindingName == "CATUI_stormStartWorldTime")
							{
								value = "0";
								bool flag4 = flag && biomeWeather != null;
								if (flag4)
								{
									value = biomeWeather.stormWorldTime.ToString();
								}
								__result = true;
								return false;
							}
						}
					}
					else if (bindingName == "CATUI_isDaytime")
					{
						value = "true";
						bool flag5 = flag;
						if (flag5)
						{
							value = GameManager.Instance.World.IsDaytime().ToString();
						}
						__result = true;
						return false;
					}
				}
				else if (bindingName == "CATUI_stormDurationTime")
				{
					value = "0";
					bool flag6 = flag && biomeWeather != null;
					if (flag6)
					{
						value = biomeWeather.stormDuration.ToString();
					}
					__result = true;
					return false;
				}
			}
			else if (num5 != 1264086689U)
			{
				if (num5 != 1274868425U)
				{
					if (num5 == 1505776159U)
					{
						if (bindingName == "CATUI_airDropFrequency")
						{
							value = "3";
							bool flag7 = flag;
							if (flag7)
							{
								value = ((float)GameStats.GetInt(EnumGameStats.AirDropFrequency) / 24f).ToString("F1");
							}
							__result = true;
							return false;
						}
					}
				}
				else if (bindingName == "CATUI_currentWeather")
				{
					value = "None";
					bool flag8 = flag && biomeType != null && biomeWeather != null && biomeWeather.biomeDefinition != null;
					if (flag8)
					{
						value = biomeWeather.biomeDefinition.weatherSpectrum.ToString();
					}
					__result = true;
					return false;
				}
			}
			else if (bindingName == "CATUI_stormEndWorldTime")
			{
				value = "0";
				bool flag9 = flag && biomeWeather != null;
				if (flag9)
				{
					value = (biomeWeather.stormWorldTime + biomeWeather.stormDuration).ToString();
				}
				__result = true;
				return false;
			}
		}
		else if (num5 <= 2320696923U)
		{
			if (num5 != 1511327158U)
			{
				if (num5 != 2098834831U)
				{
					if (num5 == 2320696923U)
					{
						if (bindingName == "CATUI_worldTime")
						{
							value = "0";
							bool flag10 = flag;
							if (flag10)
							{
								value = num2.ToString();
							}
							__result = true;
							return false;
						}
					}
				}
				else if (bindingName == "CATUI_stormLevel")
				{
					value = num.ToString();
					__result = true;
					return false;
				}
			}
			else if (bindingName == "CATUI_stormFill")
			{
				value = "0.000";
				bool flag11 = flag && biomeWeather != null && num > 0;
				if (flag11)
				{
					int num6 = biomeWeather.stormWorldTime + biomeWeather.stormDuration - num2;
					value = ((double)num6 / (double)biomeWeather.stormDuration).ToString("F3");
				}
				__result = true;
				return false;
			}
		}
		else if (num5 <= 3810308736U)
		{
			if (num5 != 3464867242U)
			{
				if (num5 == 3810308736U)
				{
					if (bindingName == "CATUI_stormRemainingTime")
					{
						value = "0";
						bool flag12 = flag && biomeWeather != null && num > 0;
						if (flag12)
						{
							value = (biomeWeather.stormWorldTime + biomeWeather.stormDuration - num2).ToString();
						}
						__result = true;
						return false;
					}
				}
			}
			else if (bindingName == "CATUI_nextBloodMoonDay")
			{
				value = "7";
				bool flag13 = flag;
				if (flag13)
				{
					value = GameStats.GetInt(EnumGameStats.BloodMoonDay).ToString();
				}
				__result = true;
				return false;
			}
		}
		else if (num5 != 4177585074U)
		{
			if (num5 == 4271403276U)
			{
				if (bindingName == "CATUI_stormName")
				{
					value = "";
					bool flag14 = flag && biomeType != null && num > 0;
					if (flag14)
					{
						bool flag15 = biomeType.Value == BiomeDefinition.BiomeType.burnt_forest;
						if (flag15)
						{
							value = "Burnt";
						}
						else
						{
							bool flag16 = biomeType.Value == BiomeDefinition.BiomeType.Desert || biomeType.Value == BiomeDefinition.BiomeType.Snow || biomeType.Value == BiomeDefinition.BiomeType.Wasteland;
							if (flag16)
							{
								value = biomeType.Value.ToString();
							}
						}
					}
					__result = true;
					return false;
				}
			}
		}
		else if (bindingName == "CATUI_stormDurationTimeReal")
		{
			value = "0";
			bool flag17 = flag && biomeWeather != null && num > 0;
			if (flag17)
			{
				int num7 = biomeWeather.stormWorldTime + biomeWeather.stormDuration - num2;
				int num8 = num7 / GameStats.GetInt(EnumGameStats.TimeOfDayIncPerSec);
				value = XUiM_PlayerBuffs.ConvertToTimeString((float)num8);
			}
			__result = true;
			return false;
		}
		return true;
	}
}
