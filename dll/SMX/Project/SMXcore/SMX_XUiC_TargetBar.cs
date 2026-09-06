using System;

namespace SMXcore
{
	// Token: 0x02000007 RID: 7
	public class XUiC_TargetBar : XUiC_TargetBar
	{
		// Token: 0x06000019 RID: 25 RVA: 0x00003D34 File Offset: 0x00001F34
		public override void Update(float _dt)
		{
			this.viewComponent.IsVisible = ((!(base.xui.playerUI.entityPlayer.AttachedToEntity != null) || !(base.xui.playerUI.entityPlayer.AttachedToEntity is EntityVehicle)) && !base.xui.playerUI.entityPlayer.IsDead());
			base.Update(_dt);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00003DAC File Offset: 0x00001FAC
		public override bool GetBindingValueInternal(ref string value, string bindingName)
		{
			bool result;
			if (!(bindingName == "statmax"))
			{
				result = base.GetBindingValueInternal(ref value, bindingName);
			}
			else
			{
				value = ((base.Target != null) ? this.statcurrentFormatterInt.Format((int)base.Target.Stats.Health.Max) : "");
				result = true;
			}
			return result;
		}
	}
}
