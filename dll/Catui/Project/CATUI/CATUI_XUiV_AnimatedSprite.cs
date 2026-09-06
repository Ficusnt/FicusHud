using System;
using HarmonyLib;
using UnityEngine;

namespace Views
{
	// Token: 0x02000025 RID: 37
	public class XUiV_AnimatedSprite : XUiV_Sprite
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600006F RID: 111 RVA: 0x000083EC File Offset: 0x000065EC
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00008404 File Offset: 0x00006604
		public string SpriteNamePrefix
		{
			get
			{
				return this.prefix;
			}
			set
			{
				bool flag = this.prefix != value;
				if (flag)
				{
					this.prefix = value;
					this.isDirty = true;
					this.resetAnimation = true;
				}
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000071 RID: 113 RVA: 0x0000843C File Offset: 0x0000663C
		// (set) Token: 0x06000072 RID: 114 RVA: 0x00008454 File Offset: 0x00006654
		public bool Loop
		{
			get
			{
				return this.loop;
			}
			set
			{
				bool flag = this.loop != value;
				if (flag)
				{
					this.loop = value;
					this.isDirty = true;
					this.resetAnimation = true;
				}
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000073 RID: 115 RVA: 0x0000848C File Offset: 0x0000668C
		// (set) Token: 0x06000074 RID: 116 RVA: 0x000084A4 File Offset: 0x000066A4
		public int FrameRate
		{
			get
			{
				return this.frameRate;
			}
			set
			{
				bool flag = this.frameRate != value;
				if (flag)
				{
					this.frameRate = value;
					this.isDirty = true;
				}
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000084D2 File Offset: 0x000066D2
		public XUiV_AnimatedSprite(string id) : base(id)
		{
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000084F3 File Offset: 0x000066F3
		public override void CreateComponents(GameObject go)
		{
			base.CreateComponents(go);
			go.AddComponent<UISpriteAnimation>();
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00008508 File Offset: 0x00006708
		public override void UpdateData()
		{
			bool flag = this.animation == null && !this.initialized;
			if (flag)
			{
				this.animation = this.uiTransform.GetComponent<UISpriteAnimation>();
				Traverse.Create(this.animation).Field("mSnap").SetValue(false);
			}
			bool flag2 = !string.IsNullOrEmpty(this.sprite.spriteName);
			if (flag2)
			{
				this.spriteName = this.sprite.spriteName;
			}
			base.UpdateData();
			this.animation.namePrefix = this.prefix;
			this.animation.framesPerSecond = this.frameRate;
			this.animation.loop = this.loop;
			bool flag3 = this.resetAnimation;
			if (flag3)
			{
				this.animation.ResetToBeginning();
				this.animation.Play();
				this.resetAnimation = false;
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000085FC File Offset: 0x000067FC
		public override bool ParseAttribute(string attribute, string value, XUiController parent)
		{
			bool flag = attribute != null;
			bool result;
			if (flag)
			{
				if (!(attribute == "spriteprefix"))
				{
					if (!(attribute == "loop"))
					{
						if (!(attribute == "framerate"))
						{
							result = base.ParseAttribute(attribute, value, parent);
						}
						else
						{
							this.FrameRate = int.Parse(value);
							result = true;
						}
					}
					else
					{
						this.Loop = StringParsers.ParseBool(value, 0, -1, true);
						result = true;
					}
				}
				else
				{
					this.SpriteNamePrefix = value;
					result = true;
				}
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00008682 File Offset: 0x00006882
		public void PlayAnimation()
		{
			this.animation.Play();
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00008691 File Offset: 0x00006891
		public void PauseAnimation()
		{
			this.animation.Pause();
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000086A0 File Offset: 0x000068A0
		public void ResetAnimation()
		{
			this.animation.ResetToBeginning();
		}

		// Token: 0x0400001A RID: 26
		private const string TAG = "AnimatedSprite";

		// Token: 0x0400001B RID: 27
		protected UISpriteAnimation animation;

		// Token: 0x0400001C RID: 28
		protected string prefix;

		// Token: 0x0400001D RID: 29
		protected bool loop = true;

		// Token: 0x0400001E RID: 30
		protected int frameRate = 30;

		// Token: 0x0400001F RID: 31
		private bool resetAnimation = false;
	}
}
