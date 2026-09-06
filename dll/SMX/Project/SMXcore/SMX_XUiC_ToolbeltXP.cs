using System;
using System.Globalization;
using UnityEngine;

namespace SMXcore
{
	// Token: 0x02000008 RID: 8
	public class XUiC_ToolbeltXP : XUiController
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00003E20 File Offset: 0x00002020
		public override void Update(float _dt)
		{
			base.Update(_dt);
			bool flag = (DateTime.Now - this.updateTime).TotalSeconds > 0.5;
			if (flag)
			{
				this.updateTime = DateTime.Now;
			}
			base.RefreshBindings(false);
			bool flag2 = this.CustomAttributes.ContainsKey("standard_xp_color");
			if (flag2)
			{
				this.standardXPColor = this.CustomAttributes["standard_xp_color"];
			}
			else
			{
				this.standardXPColor = "128,4,128";
			}
			bool flag3 = this.CustomAttributes.ContainsKey("updating_xp_color");
			if (flag3)
			{
				this.updatingXPColor = this.CustomAttributes["updating_xp_color"];
			}
			else
			{
				this.updatingXPColor = "128,4,128";
			}
			bool flag4 = this.CustomAttributes.ContainsKey("deficit_xp_color");
			if (flag4)
			{
				this.expDeficitColor = this.CustomAttributes["deficit_xp_color"];
			}
			else
			{
				this.expDeficitColor = "222,20,20";
			}
			bool flag5 = this.CustomAttributes.ContainsKey("xp_fill_speed");
			if (flag5)
			{
				this.xpFillSpeed = StringParsers.ParseFloat(this.CustomAttributes["xp_fill_speed"], 0, -1, NumberStyles.Any);
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00003F60 File Offset: 0x00002160
		public override void OnOpen()
		{
			base.OnOpen();
			bool flag = this.localPlayer == null;
			if (flag)
			{
				this.localPlayer = base.xui.playerUI.entityPlayer;
			}
			this.currentValue = (this.lastValue = XUiM_Player.GetLevelPercent(this.localPlayer));
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00003FB8 File Offset: 0x000021B8
		public override bool GetBindingValueInternal(ref string value, string bindingName)
		{
			bool flag = bindingName != null;
			if (flag)
			{
				bool flag2 = bindingName == "xp";
				if (flag2)
				{
					bool flag3 = this.localPlayer != null;
					if (flag3)
					{
						bool flag4 = this.localPlayer.Progression.ExpDeficit > 0;
						if (flag4)
						{
							float v = Math.Max(this.lastDeficitValue, 0f) * 1.01f;
							value = this.bindingXp.Format(v);
							this.currentValue = (float)this.localPlayer.Progression.ExpDeficit / (float)this.localPlayer.Progression.GetExpForNextLevel();
							bool flag5 = this.currentValue != this.lastDeficitValue;
							if (flag5)
							{
								this.lastDeficitValue = Mathf.Lerp(this.lastDeficitValue, this.currentValue, Time.deltaTime * this.xpFillSpeed);
								bool flag6 = Mathf.Abs(this.currentValue - this.lastDeficitValue) < 0.005f;
								if (flag6)
								{
									this.lastDeficitValue = this.currentValue;
								}
							}
						}
						else
						{
							float v2 = Math.Max(this.lastValue, 0f) * 1.01f;
							value = this.bindingXp.Format(v2);
							this.currentValue = XUiM_Player.GetLevelPercent(this.localPlayer);
							bool flag7 = this.currentValue != this.lastValue;
							if (flag7)
							{
								this.lastValue = Mathf.Lerp(this.lastValue, this.currentValue, Time.deltaTime * this.xpFillSpeed);
								bool flag8 = Mathf.Abs(this.currentValue - this.lastValue) < 0.005f;
								if (flag8)
								{
									this.lastValue = this.currentValue;
								}
							}
						}
					}
					return true;
				}
				bool flag9 = bindingName == "xpcolor";
				if (flag9)
				{
					bool flag10 = this.localPlayer != null;
					if (flag10)
					{
						bool flag11 = this.localPlayer.Progression.ExpDeficit > 0;
						if (flag11)
						{
							value = this.expDeficitColor;
						}
						else
						{
							value = ((this.currentValue == this.lastValue) ? this.standardXPColor : this.updatingXPColor);
						}
					}
					else
					{
						value = "";
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000026 RID: 38
		private EntityPlayer localPlayer;

		// Token: 0x04000027 RID: 39
		private DateTime updateTime;

		// Token: 0x04000028 RID: 40
		private float lastValue;

		// Token: 0x04000029 RID: 41
		private float currentValue;

		// Token: 0x0400002A RID: 42
		private float lastDeficitValue;

		// Token: 0x0400002B RID: 43
		private string standardXPColor = "";

		// Token: 0x0400002C RID: 44
		private string updatingXPColor = "";

		// Token: 0x0400002D RID: 45
		private string expDeficitColor = "";

		// Token: 0x0400002E RID: 46
		private float xpFillSpeed = 2.5f;

		// Token: 0x0400002F RID: 47
		private CachedStringFormatter<float> bindingXp = new CachedStringFormatter<float>((float _f) => _f.ToCultureInvariantString());
	}
}
