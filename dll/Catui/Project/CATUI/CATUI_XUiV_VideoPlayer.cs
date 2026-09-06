using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Video;

namespace Views
{
	// Token: 0x02000029 RID: 41
	public class XUiV_VideoPlayer : XUiView
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00009F70 File Offset: 0x00008170
		public VideoPlayer VideoPlayer
		{
			get
			{
				return this.videoPlayer;
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060000B9 RID: 185 RVA: 0x00009F88 File Offset: 0x00008188
		// (remove) Token: 0x060000BA RID: 186 RVA: 0x00009FC0 File Offset: 0x000081C0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event XUiV_VideoPlayer.OnVideoFinishedPlayingEvent OnVideoFinishedPlaying;

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00009FF8 File Offset: 0x000081F8
		// (set) Token: 0x060000BC RID: 188 RVA: 0x0000A010 File Offset: 0x00008210
		public string VideoPath
		{
			get
			{
				return this.videoPath;
			}
			set
			{
				bool flag = value != this.videoPath;
				if (flag)
				{
					this.videoPath = value;
					this.videoDirty = true;
					this.isDirty = true;
				}
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000BD RID: 189 RVA: 0x0000A048 File Offset: 0x00008248
		// (set) Token: 0x060000BE RID: 190 RVA: 0x0000A060 File Offset: 0x00008260
		public bool LoopVideo
		{
			get
			{
				return this.loopVideo;
			}
			set
			{
				bool flag = value != this.loopVideo;
				if (flag)
				{
					this.loopVideo = value;
					this.isDirty = true;
				}
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000BF RID: 191 RVA: 0x0000A090 File Offset: 0x00008290
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x0000A0A8 File Offset: 0x000082A8
		public bool RestartOnOpen
		{
			get
			{
				return this.restartOnOpen;
			}
			set
			{
				this.restartOnOpen = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x0000A0B4 File Offset: 0x000082B4
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x0000A0CC File Offset: 0x000082CC
		public bool AutoPlay
		{
			get
			{
				return this.autoplay;
			}
			set
			{
				this.autoplay = value;
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x0000A0D6 File Offset: 0x000082D6
		public XUiV_VideoPlayer(string _id) : base(_id)
		{
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0000A0F6 File Offset: 0x000082F6
		public override void CreateComponents(GameObject _go)
		{
			_go.AddComponent<UITexture>();
			_go.AddComponent<VideoPlayer>();
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0000A108 File Offset: 0x00008308
		public override void InitView()
		{
			base.InitView();
			this.uiTexture = this.uiTransform.GetComponent<UITexture>();
			this.videoPlayer = this.uiTransform.GetComponent<VideoPlayer>();
			this.videoPlayer.playOnAwake = false;
			this.videoPlayer.renderMode = VideoRenderMode.RenderTexture;
			this.videoPlayer.loopPointReached += this.VideoPlayer_loopPointReached;
			ModEvents.GameShutdown.RegisterHandler(delegate(ref ModEvents.SGameShutdownData data)
			{
				this.OnShutdown();
			});
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000A188 File Offset: 0x00008388
		public override void UpdateData()
		{
			bool flag = !this.isDirty;
			if (!flag)
			{
				this.uiTexture.SetDimensions(this.size.x, this.size.y);
				bool flag2 = this.renderTexture != null;
				if (flag2)
				{
					this.renderTexture.Release();
					this.renderTexture = new RenderTexture(this.size.x, this.size.y, 32);
					this.renderTexture.format = RenderTextureFormat.ARGB32;
					this.videoPlayer.targetTexture = this.renderTexture;
					this.uiTexture.mainTexture = this.renderTexture;
				}
				else
				{
					this.renderTexture = new RenderTexture(this.size.x, this.size.y, 32);
					this.renderTexture.format = RenderTextureFormat.ARGB32;
					this.videoPlayer.targetTexture = this.renderTexture;
					this.uiTexture.mainTexture = this.renderTexture;
				}
				this.videoPlayer.isLooping = this.loopVideo;
				bool flag3 = !this.initialized;
				if (flag3)
				{
					this.uiTexture.pivot = this.pivot;
					this.uiTexture.depth = this.depth;
					this.uiTransform.localScale = Vector3.one;
					this.uiTransform.localPosition = new Vector3((float)this.position.x, (float)this.position.y, 0f);
					bool flag4 = this.EventOnHover || this.EventOnPress || this.EventOnScroll || this.EventOnDrag;
					if (flag4)
					{
						BoxCollider collider = this.collider;
						collider.center = this.uiTexture.localCenter;
						collider.size = new Vector3(this.uiTexture.localSize.x * this.colliderScale, this.uiTexture.localSize.y * this.colliderScale, 0f);
					}
					this.initialized = true;
				}
				this.uiTexture.keepAspectRatio = this.keepAspectRatio;
				this.uiTexture.aspectRatio = this.aspectRatio;
				base.parseAnchors(this.uiTexture, true);
				base.UpdateData();
				bool flag5 = !string.IsNullOrEmpty(this.videoPath);
				if (flag5)
				{
					bool flag6 = this.videoDirty;
					if (flag6)
					{
						this.videoPlayer.url = this.videoPath;
						this.videoDirty = false;
					}
					bool flag7 = !this.videoPlayer.isPlaying;
					if (flag7)
					{
						this.videoPlayer.Play();
						bool flag8 = !this.autoplay;
						if (flag8)
						{
							this.videoPlayer.Pause();
						}
					}
				}
				else
				{
					this.Stop();
				}
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x0000A464 File Offset: 0x00008664
		public override void OnOpen()
		{
			base.OnOpen();
			bool flag = this.restartOnOpen;
			if (flag)
			{
				this.videoPlayer.frame = 0L;
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x0000A493 File Offset: 0x00008693
		public override void OnClose()
		{
			base.OnClose();
			this.Pause();
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000A4A4 File Offset: 0x000086A4
		public override bool ParseAttribute(string attribute, string value, XUiController parent)
		{
			bool result;
			if (!(attribute == "video"))
			{
				if (!(attribute == "restartonopen"))
				{
					if (!(attribute == "autoplay"))
					{
						if (!(attribute == "loop"))
						{
							result = base.ParseAttribute(attribute, value, parent);
						}
						else
						{
							this.LoopVideo = StringParsers.ParseBool(value, 0, -1, true);
							result = true;
						}
					}
					else
					{
						this.autoplay = StringParsers.ParseBool(value, 0, -1, true);
						result = true;
					}
				}
				else
				{
					this.restartOnOpen = StringParsers.ParseBool(value, 0, -1, true);
					result = true;
				}
			}
			else
			{
				string text = ModManager.PatchModPathString(value);
				this.VideoPath = ((text != null) ? text : value);
				result = true;
			}
			return result;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000A54C File Offset: 0x0000874C
		public override void Cleanup()
		{
			base.Cleanup();
			ModEvents.GameShutdown.UnregisterHandler(delegate(ref ModEvents.SGameShutdownData data)
			{
				this.OnShutdown();
			});
			this.Stop();
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000A574 File Offset: 0x00008774
		public void Play()
		{
			bool flag = !string.IsNullOrEmpty(this.videoPath) && !this.videoPlayer.isPlaying;
			if (flag)
			{
				this.videoPlayer.Play();
			}
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000A5B2 File Offset: 0x000087B2
		public void Pause()
		{
			this.renderTexture.Release();
			this.videoPlayer.Pause();
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0000A5D0 File Offset: 0x000087D0
		public void Stop()
		{
			bool flag = this.videoPlayer != null;
			if (flag)
			{
				this.renderTexture.Release();
				this.videoPlayer.Stop();
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0000A608 File Offset: 0x00008808
		private void OnShutdown()
		{
			this.videoPlayer.Pause();
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000A617 File Offset: 0x00008817
		private void VideoPlayer_loopPointReached(VideoPlayer source)
		{
			XUiV_VideoPlayer.OnVideoFinishedPlayingEvent onVideoFinishedPlaying = this.OnVideoFinishedPlaying;
			if (onVideoFinishedPlaying != null)
			{
				onVideoFinishedPlaying(this);
			}
		}

		// Token: 0x04000048 RID: 72
		private const string TAG = "XUiV_VideoPlayer";

		// Token: 0x04000049 RID: 73
		protected VideoPlayer videoPlayer;

		// Token: 0x0400004A RID: 74
		protected UITexture uiTexture;

		// Token: 0x0400004B RID: 75
		protected RenderTexture renderTexture;

		// Token: 0x0400004C RID: 76
		protected string videoPath;

		// Token: 0x0400004D RID: 77
		protected bool videoDirty;

		// Token: 0x0400004E RID: 78
		protected bool loopVideo = true;

		// Token: 0x0400004F RID: 79
		protected bool restartOnOpen = false;

		// Token: 0x04000050 RID: 80
		protected bool autoplay = true;

		// Token: 0x02000033 RID: 51
		// (Invoke) Token: 0x060000E9 RID: 233
		public delegate void OnVideoFinishedPlayingEvent(XUiV_VideoPlayer videoPlayer);
	}
}
