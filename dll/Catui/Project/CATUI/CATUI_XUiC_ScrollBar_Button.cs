using System;
using Audio;
using UnityEngine;

namespace Views
{
	// Token: 0x02000023 RID: 35
	public class XUiC_ScrollBar_Button : XUiV_Button
	{
		// Token: 0x06000065 RID: 101 RVA: 0x000081D0 File Offset: 0x000063D0
		public XUiC_ScrollBar_Button(string _id) : base(_id)
		{
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000081DC File Offset: 0x000063DC
		public override void InitView()
		{
			base.InitView();
			UIEventListener uieventListener = UIEventListener.Get(this.uiTransform.gameObject);
			UIEventListener uieventListener2 = uieventListener;
			uieventListener2.onPress = (UIEventListener.BoolDelegate)Delegate.Combine(uieventListener2.onPress, new UIEventListener.BoolDelegate(this.OnPress));
			this.EventOnPress = (this.xuiSound == null);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00008236 File Offset: 0x00006436
		public override void UpdateData()
		{
			this.currentColor.a = this.sprite.alpha;
			base.UpdateData();
			this.sprite.depth = this.depth;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00008268 File Offset: 0x00006468
		public override void RefreshBoxCollider()
		{
			bool flag = this.sprite != null && !this.sprite.autoResizeBoxCollider;
			if (flag)
			{
				base.RefreshBoxCollider();
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000082A4 File Offset: 0x000064A4
		public override bool ParseAttribute(string attribute, string value, XUiController parent)
		{
			bool flag = attribute != null;
			bool result;
			if (flag)
			{
				if (!(attribute == "sound_play_on_press_down"))
				{
					result = base.ParseAttribute(attribute, value, parent);
				}
				else
				{
					base.xui.LoadData<AudioClip>(value, delegate(AudioClip audioClip)
					{
						this.xuiSound = audioClip;
					});
					result = true;
				}
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000082FC File Offset: 0x000064FC
		private new void OnPress(GameObject go, bool pressed)
		{
			bool flag = this.enabled && pressed;
			if (flag)
			{
				bool flag2 = this.xuiSound != null && this.xuiSound != null && UICamera.currentTouchID == -1;
				if (flag2)
				{
					Manager.PlayXUiSound(this.xuiSound, this.soundVolume);
				}
				this.controller.Pressed(UICamera.currentTouchID);
			}
		}

		// Token: 0x04000017 RID: 23
		private const string TAG = "ScrollBar Button";

		// Token: 0x04000018 RID: 24
		private new AudioClip xuiSound;
	}
}
