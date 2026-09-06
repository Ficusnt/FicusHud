using System;
using UnityEngine;
using XMLData.Parsers;

namespace SMXcore
{
	// Token: 0x02000005 RID: 5
	public class XUiC_BloodMoon : XUiController
	{
		// Token: 0x0600000B RID: 11 RVA: 0x000021F0 File Offset: 0x000003F0
		public override void Init()
		{
			base.Init();
			this.sprite = (this.viewComponent as XUiV_Sprite);
			Color color = this.sprite.Color;
			color.a = 0f;
			this.sprite.Color = color;
			this.bloodMoonWarningEnum = EnumParser.Parse<EnumGameStats>("BloodMoonWarning");
			this.bloodMoonDayEnum = EnumParser.Parse<EnumGameStats>("BloodMoonDay");
			this.dayLightLengthEnum = EnumParser.Parse<EnumGameStats>("DayLightLength");
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000226C File Offset: 0x0000046C
		public override void Update(float _dt)
		{
			base.Update(_dt);
			bool flag = XUi.IsGameRunning();
			if (flag)
			{
				int @int = GameStats.GetInt(this.bloodMoonWarningEnum);
				bool flag2 = @int == -1;
				if (!flag2)
				{
					ValueTuple<int, int, int> valueTuple = GameUtils.WorldTimeToElements(GameManager.Instance.World.worldTime);
					int item = valueTuple.Item1;
					int item2 = valueTuple.Item2;
					int item3 = valueTuple.Item3;
					ValueTuple<int, int> valueTuple2 = GameUtils.CalcDuskDawnHours(GameStats.GetInt(this.dayLightLengthEnum));
					int item4 = valueTuple2.Item1;
					int item5 = valueTuple2.Item2;
					bool flag3 = !this.isInit;
					if (flag3)
					{
						ulong num = this.HourMinuteTimeToLong((long)item4, 0L);
						this.bloodMoonTime = num - this.bloodMoonTime;
						this.fullMoonTime = num - this.fullMoonTime;
						this.isInit = true;
					}
					int int2 = GameStats.GetInt(this.bloodMoonDayEnum);
					bool flag4 = item != int2 && item != int2 + 1;
					if (flag4)
					{
						bool flag5 = this.sprite.Color.a != 0f;
						if (flag5)
						{
							Color color = this.sprite.Color;
							color.a = 0f;
							this.sprite.Color = color;
						}
					}
					else
					{
						bool flag6 = item == int2;
						if (flag6)
						{
							ulong num2 = this.HourMinuteTimeToLong((long)@int, 0L);
							bool flag7 = num2 > this.fullMoonTime;
							if (flag7)
							{
								this.fullMoonTime = num2;
							}
							ulong num3 = this.WorldTimeToHourMinute(GameManager.Instance.World.worldTime);
							bool flag8 = num2 <= num3 && num3 < this.fullMoonTime;
							if (flag8)
							{
								Color color2 = this.moonColorTint;
								float progressBetweenTime = this.GetProgressBetweenTime(num3, num2, this.fullMoonTime);
								color2.a = Mathf.Lerp(0f, 1f, progressBetweenTime);
								this.sprite.Color = color2;
							}
							bool flag9 = this.fullMoonTime <= num3 && num3 <= this.bloodMoonTime;
							if (flag9)
							{
								float progressBetweenTime2 = this.GetProgressBetweenTime(num3, this.fullMoonTime, this.bloodMoonTime);
								Color color3 = Color.Lerp(this.moonColorTint, this.bloodMoonColorTint, progressBetweenTime2);
								this.sprite.Color = color3;
							}
							bool flag10 = this.bloodMoonTime < num3 && this.sprite.Color != this.bloodMoonColorTint;
							if (flag10)
							{
								this.sprite.Color = this.bloodMoonColorTint;
							}
						}
						bool flag11 = item == int2 + 1;
						if (flag11)
						{
							ulong num4 = this.WorldTimeToHourMinute(GameManager.Instance.World.worldTime);
							ulong num5 = this.HourMinuteTimeToLong((long)(item5 - 1), 50L);
							ulong num6 = this.HourMinuteTimeToLong((long)item5, 0L);
							bool flag12 = num4 < num5 && this.sprite.Color != this.bloodMoonColorTint;
							if (flag12)
							{
								this.sprite.Color = this.bloodMoonColorTint;
							}
							bool flag13 = num5 <= num4 && num4 <= num6;
							if (flag13)
							{
								Color b = this.moonColorTint;
								b.a = 0f;
								float progressBetweenTime3 = this.GetProgressBetweenTime(num4, num5, num6);
								Color color4 = Color.Lerp(this.bloodMoonColorTint, b, progressBetweenTime3);
								this.sprite.Color = color4;
							}
							bool flag14 = num6 < num4 && this.sprite.Color.a != 0f;
							if (flag14)
							{
								Color color5 = this.sprite.Color;
								color5.a = 0f;
								this.sprite.Color = color5;
							}
						}
					}
				}
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002618 File Offset: 0x00000818
		public override bool ParseAttribute(string name, string value, XUiController parent)
		{
			bool result;
			if (!(name == "fullmoontime"))
			{
				if (!(name == "bloodmoontime"))
				{
					if (!(name == "bloodmooncolortint"))
					{
						if (!(name == "mooncolortint"))
						{
							result = base.ParseAttribute(name, value, parent);
						}
						else
						{
							this.moonColorTint = StringParsers.ParseColor32(value);
							result = true;
						}
					}
					else
					{
						this.bloodMoonColorTint = StringParsers.ParseColor32(value);
						result = true;
					}
				}
				else
				{
					this.bloodMoonTime = this.ParseTime(value);
					result = true;
				}
			}
			else
			{
				this.fullMoonTime = this.ParseTime(value);
				result = true;
			}
			return result;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000026B0 File Offset: 0x000008B0
		private float GetProgressBetweenTime(ulong currentTime, ulong minTime, ulong maxTime)
		{
			currentTime = this.ClampUlong(currentTime, minTime, maxTime);
			return (currentTime - minTime) / (maxTime - minTime);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000026DC File Offset: 0x000008DC
		private ulong ClampUlong(ulong value, ulong min, ulong max)
		{
			bool flag = value >= min;
			ulong result;
			if (flag)
			{
				bool flag2 = value <= max;
				if (flag2)
				{
					result = value;
				}
				else
				{
					result = max;
				}
			}
			else
			{
				result = min;
			}
			return result;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002710 File Offset: 0x00000910
		private ulong HourMinuteTimeToLong(long hours, long minutes)
		{
			return (ulong)(hours * 1000L + minutes * 1000L / 60L);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002738 File Offset: 0x00000938
		private ulong WorldTimeToHourMinute(ulong worldTime)
		{
			return worldTime % 24000UL;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002754 File Offset: 0x00000954
		private ulong ParseTime(string time)
		{
			string[] array = time.Split(new char[]
			{
				':'
			});
			bool flag = array.Length == 2;
			ulong result;
			if (flag)
			{
				result = this.HourMinuteTimeToLong(long.Parse(array[0]), long.Parse(array[1]));
			}
			else
			{
				result = 0UL;
			}
			return result;
		}

		// Token: 0x04000002 RID: 2
		private XUiV_Sprite sprite;

		// Token: 0x04000003 RID: 3
		private ulong fullMoonTime;

		// Token: 0x04000004 RID: 4
		private ulong bloodMoonTime;

		// Token: 0x04000005 RID: 5
		private Color bloodMoonColorTint;

		// Token: 0x04000006 RID: 6
		private Color moonColorTint;

		// Token: 0x04000007 RID: 7
		private EnumGameStats bloodMoonWarningEnum;

		// Token: 0x04000008 RID: 8
		private EnumGameStats bloodMoonDayEnum;

		// Token: 0x04000009 RID: 9
		private EnumGameStats dayLightLengthEnum;

		// Token: 0x0400000A RID: 10
		private bool isInit = false;
	}
}
