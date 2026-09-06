using System;
using UnityEngine;

namespace SMXcore
{
	// Token: 0x02000025 RID: 37
	public class XUiC_HUDActiveItem : XUiController
	{
		// Token: 0x06000117 RID: 279 RVA: 0x0000B5F7 File Offset: 0x000097F7
		public override void Init()
		{
			base.Init();
			this.IsDirty = true;
			this.itemValue = ItemValue.None.Clone();
		}

		// Token: 0x06000118 RID: 280 RVA: 0x0000B618 File Offset: 0x00009818
		public override void Update(float _dt)
		{
			base.Update(_dt);
			bool flag = this.localPlayer == null && XUi.IsGameRunning();
			if (flag)
			{
				this.localPlayer = base.xui.playerUI.entityPlayer;
				this.IsDirty = true;
			}
			bool flag2 = this.currentSlotIndex != base.xui.PlayerInventory.Toolbelt.GetFocusedItemIdx();
			if (flag2)
			{
				this.currentSlotIndex = base.xui.PlayerInventory.Toolbelt.GetFocusedItemIdx();
				this.IsDirty = true;
			}
			bool flag3 = this.IsDirty || this.HasChanged();
			if (flag3)
			{
				this.SetupActiveItemEntry();
				this.updateActiveItemAmmo();
				base.RefreshBindings(true);
				this.IsDirty = false;
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x0000B6E4 File Offset: 0x000098E4
		public override void OnOpen()
		{
			base.OnOpen();
			base.xui.PlayerInventory.OnBackpackItemsChanged += this.PlayerInventory_OnBackpackItemsChanged;
			base.xui.PlayerInventory.OnToolbeltItemsChanged += this.PlayerInventory_OnToolbeltItemsChanged;
			this.IsDirty = true;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000B73C File Offset: 0x0000993C
		public override void OnClose()
		{
			base.OnClose();
			base.xui.PlayerInventory.OnBackpackItemsChanged -= this.PlayerInventory_OnBackpackItemsChanged;
			base.xui.PlayerInventory.OnToolbeltItemsChanged -= this.PlayerInventory_OnToolbeltItemsChanged;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x0000B78C File Offset: 0x0000998C
		public override bool GetBindingValueInternal(ref string value, string bindingName)
		{
			uint num = global::<PrivateImplementationDetails>.ComputeStringHash(bindingName);
			if (num <= 2512966967U)
			{
				if (num <= 708774830U)
				{
					if (num != 63978615U)
					{
						if (num != 443196151U)
						{
							if (num == 708774830U)
							{
								if (bindingName == "loadedammo")
								{
									value = this.GetLoadedAmmo();
									return true;
								}
							}
						}
						else if (bindingName == "istool")
						{
							value = this.IsToolHeld().ToString();
							return true;
						}
					}
					else if (bindingName == "entitydamage")
					{
						value = this.entityDamage;
						return true;
					}
				}
				else if (num != 1822678806U)
				{
					if (num != 2378736196U)
					{
						if (num == 2512966967U)
						{
							if (bindingName == "totalammo")
							{
								value = this.GetTotalAmmo();
								return true;
							}
						}
					}
					else if (bindingName == "elevation")
					{
						value = "";
						bool flag = XUi.IsGameRunning() && this.localPlayer != null;
						if (flag)
						{
							int v = Mathf.RoundToInt(this.localPlayer.GetPosition().y - WeatherManager.SeaLevel());
							value = this.levelFormatter.Format(v);
						}
						return true;
					}
				}
				else if (bindingName == "staticon")
				{
					value = ((this.displayItemClass != null) ? this.displayItemClass.GetIconName() : "");
					return true;
				}
			}
			else if (num <= 3150708601U)
			{
				if (num != 2758588565U)
				{
					if (num != 3085591727U)
					{
						if (num == 3150708601U)
						{
							if (bindingName == "staticoncolor")
							{
								Color32 v2 = (this.displayItemClass != null) ? this.displayItemClass.GetIconTint(null) : Color.white;
								value = this.staticoncolorFormatter.Format(v2);
								return true;
							}
						}
					}
					else if (bindingName == "blockdamage")
					{
						value = this.blockDamage;
						return true;
					}
				}
				else if (bindingName == "staticonatlas")
				{
					value = this.statAtlas;
					return true;
				}
			}
			else if (num != 3782832427U)
			{
				if (num != 3799067675U)
				{
					if (num == 4264423029U)
					{
						if (bindingName == "isgun")
						{
							value = (this.heldItemClass != null && this.heldItemClass.IsGun()).ToString();
							return true;
						}
					}
				}
				else if (bindingName == "statvisible")
				{
					value = this.IsStatVisible().ToString();
					return true;
				}
			}
			else if (bindingName == "ismelee")
			{
				value = this.IsMeleeHeld().ToString();
				return true;
			}
			return false;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0000BAD0 File Offset: 0x00009CD0
		private bool IsStatVisible()
		{
			bool flag = this.localPlayer == null;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = this.localPlayer.IsDead();
				result = (!flag2 && this.heldItemClass != null);
			}
			return result;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000BB14 File Offset: 0x00009D14
		private string GetLoadedAmmo()
		{
			string text = "";
			bool flag = this.localPlayer == null;
			string result;
			if (flag)
			{
				result = text;
			}
			else
			{
				bool flag2 = this.itemAction is ItemActionTextureBlock;
				if (flag2)
				{
					text = this.currentPaintAmmoFormatter.Format(this.currentAmmoCount);
				}
				else
				{
					text = this.statcurrentFormatterInt.Format(this.localPlayer.inventory.holdingItemItemValue.Meta);
				}
				result = text;
			}
			return result;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000BB90 File Offset: 0x00009D90
		private string GetTotalAmmo()
		{
			string text = "";
			bool flag = this.localPlayer == null;
			string result;
			if (flag)
			{
				result = text;
			}
			else
			{
				text = this.statcurrentFormatterInt.Format(this.currentAmmoCount);
				result = text;
			}
			return result;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000BBD0 File Offset: 0x00009DD0
		private bool IsToolHeld()
		{
			return this.heldItemClass != null && this.heldItemClass.HasAnyTags(FastTags<TagGroup.Global>.Parse("tool")) && !this.heldItemClass.IsGun();
		}

		// Token: 0x06000120 RID: 288 RVA: 0x0000BC14 File Offset: 0x00009E14
		private bool IsMeleeHeld()
		{
			return this.heldItemClass != null && this.heldItemClass.IsDynamicMelee() && !this.IsToolHeld();
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000BC48 File Offset: 0x00009E48
		private void SetupActiveItemEntry()
		{
			this.heldItemClass = null;
			this.displayItemClass = null;
			this.itemAction = null;
			this.entityDamage = string.Empty;
			this.blockDamage = string.Empty;
			bool flag = this.localPlayer == null || this.localPlayer.inventory.GetItemInSlot(this.currentSlotIndex) == null;
			if (flag)
			{
				this.itemValue = ItemValue.None.Clone();
			}
			else
			{
				this.itemValue = this.localPlayer.inventory.GetItem(this.currentSlotIndex).itemValue;
				bool flag2 = this.itemValue.ItemClass != null;
				if (flag2)
				{
					this.heldItemClass = this.itemValue.ItemClass;
					bool flag3 = this.itemValue.ItemClass.IsGun();
					if (flag3)
					{
						ItemActionAttack itemActionAttack = this.itemValue.ItemClass.Actions[0] as ItemActionAttack;
						bool flag4 = itemActionAttack == null || itemActionAttack is ItemActionMelee || (itemActionAttack.InfiniteAmmo && !itemActionAttack.ForceShowAmmo) || (int)EffectManager.GetValue(PassiveEffects.MagazineSize, this.localPlayer.inventory.holdingItemItemValue, 0f, this.localPlayer, null, default(FastTags<TagGroup.Global>), true, true, true, true, true, 1, true, false) <= 0;
						if (flag4)
						{
							this.currentAmmoCount = 0;
						}
						else
						{
							bool flag5 = itemActionAttack.MagazineItemNames != null && itemActionAttack.MagazineItemNames.Length != 0;
							if (flag5)
							{
								this.lastAmmoName = itemActionAttack.MagazineItemNames[(int)this.itemValue.SelectedAmmoTypeIndex];
								this.itemValue = ItemClass.GetItem(this.lastAmmoName, false);
								this.displayItemClass = ItemClass.GetItemClass(this.lastAmmoName, false);
							}
							this.itemAction = itemActionAttack;
						}
					}
					else
					{
						bool flag6 = this.itemValue.ItemClass.IsDynamicMelee() || this.itemValue.ItemClass.HasAnyTags(FastTags<TagGroup.Global>.Parse("tool"));
						if (flag6)
						{
							bool flag7 = this.itemValue.ItemClass.GetIconName() == "missingIcon";
							if (!flag7)
							{
								this.itemAction = this.itemValue.ItemClass.Actions[0];
								this.displayItemClass = this.itemValue.ItemClass;
								this.entityDamage = this.GetEntityDamage();
								this.blockDamage = this.GetBlockDamage();
							}
						}
					}
				}
				else
				{
					this.currentAmmoCount = 0;
				}
			}
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000BEB8 File Offset: 0x0000A0B8
		private void updateActiveItemAmmo()
		{
			bool flag = this.heldItemClass != null && this.heldItemClass.IsGun() && this.itemValue.type != 0;
			if (flag)
			{
				this.currentAmmoCount = this.localPlayer.inventory.GetItemCount(this.itemValue, false, -1, -1, true);
				this.currentAmmoCount += this.localPlayer.bag.GetItemCount(this.itemValue, -1, -1, true);
				this.IsDirty = true;
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x0000BF40 File Offset: 0x0000A140
		private string GetEntityDamage()
		{
			bool flag = this.localPlayer != null && this.itemValue != null;
			string result;
			if (flag)
			{
				result = EffectManager.GetValue(XUiC_HUDActiveItem.peEntityDamage, this.itemValue, 0f, this.localPlayer, null, default(FastTags<TagGroup.Global>), true, true, true, true, true, 1, true, false).ToString("0.#");
			}
			else
			{
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0000BFB4 File Offset: 0x0000A1B4
		private string GetBlockDamage()
		{
			bool flag = this.localPlayer != null && this.itemValue != null;
			string result;
			if (flag)
			{
				result = EffectManager.GetValue(XUiC_HUDActiveItem.peBlockDamage, this.itemValue, 0f, this.localPlayer, null, default(FastTags<TagGroup.Global>), true, true, true, true, true, 1, true, false).ToString("0.#");
			}
			else
			{
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000C028 File Offset: 0x0000A228
		private bool HasChanged()
		{
			bool result = false;
			bool flag = this.localPlayer.inventory.holdingItemItemValue.ItemClass.Actions[0] is ItemActionRanged;
			if (flag)
			{
				result = (this.oldValue != (float)this.localPlayer.inventory.holdingItemItemValue.Meta);
				this.oldValue = (float)this.localPlayer.inventory.holdingItemItemValue.Meta;
			}
			else
			{
				bool flag2 = this.IsToolHeld();
				if (flag2)
				{
					float num = (float)Mathf.RoundToInt(this.localPlayer.GetPosition().y - WeatherManager.SeaLevel());
					result = (this.oldValue != num);
					this.oldValue = num;
				}
				else
				{
					bool flag3 = this.IsMeleeHeld();
					if (flag3)
					{
						result = (this.entityDamage != this.GetEntityDamage() || this.blockDamage != this.GetBlockDamage());
					}
				}
			}
			return result;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0000C11E File Offset: 0x0000A31E
		private void PlayerInventory_OnToolbeltItemsChanged()
		{
			this.IsDirty = true;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x0000C128 File Offset: 0x0000A328
		private void PlayerInventory_OnBackpackItemsChanged()
		{
			this.IsDirty = true;
		}

		// Token: 0x040000B3 RID: 179
		private string statAtlas = "ItemIconAtlas";

		// Token: 0x040000B4 RID: 180
		private string lastAmmoName = "";

		// Token: 0x040000B5 RID: 181
		private int currentAmmoCount;

		// Token: 0x040000B6 RID: 182
		private ItemValue itemValue;

		// Token: 0x040000B7 RID: 183
		private ItemClass displayItemClass;

		// Token: 0x040000B8 RID: 184
		private ItemAction itemAction;

		// Token: 0x040000B9 RID: 185
		private ItemClass heldItemClass;

		// Token: 0x040000BA RID: 186
		private float oldValue;

		// Token: 0x040000BB RID: 187
		private int currentSlotIndex = -1;

		// Token: 0x040000BC RID: 188
		private string entityDamage;

		// Token: 0x040000BD RID: 189
		private string blockDamage;

		// Token: 0x040000BE RID: 190
		private static PassiveEffects peBlockDamage = (PassiveEffects)Enum.Parse(typeof(PassiveEffects), "BlockDamage");

		// Token: 0x040000BF RID: 191
		private static PassiveEffects peEntityDamage = (PassiveEffects)Enum.Parse(typeof(PassiveEffects), "EntityDamage");

		// Token: 0x040000C0 RID: 192
		private EntityPlayer localPlayer;

		// Token: 0x040000C1 RID: 193
		private readonly CachedStringFormatter<int> statcurrentFormatterInt = new CachedStringFormatterInt();

		// Token: 0x040000C2 RID: 194
		private readonly CachedStringFormatter<int> currentPaintAmmoFormatter = new CachedStringFormatterInt();

		// Token: 0x040000C3 RID: 195
		private readonly CachedStringFormatter<int, int> statcurrentWMaxFormatterAOfB = new CachedStringFormatter<int, int>((int _i, int _i1) => string.Format("{0}/{1}", _i, _i1));

		// Token: 0x040000C4 RID: 196
		private readonly CachedStringFormatterXuiRgbaColor staticoncolorFormatter = new CachedStringFormatterXuiRgbaColor();

		// Token: 0x040000C5 RID: 197
		private readonly CachedStringFormatter<int> levelFormatter = new CachedStringFormatter<int>((int _i) => _i.ToString("+0;-#"));
	}
}
