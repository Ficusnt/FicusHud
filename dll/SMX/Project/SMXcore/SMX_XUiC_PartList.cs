using System;
using SMXcore.HarmonyPatches;

namespace SMXcore
{
	// Token: 0x0200000D RID: 13
	public class XUiC_PartList : XUiC_PartList
	{
		// Token: 0x0600003C RID: 60 RVA: 0x000058BB File Offset: 0x00003ABB
		public override void Init()
		{
			base.Init();
			XUiC_PartList_Patch.PatchSetMainItemMethod();
			XUiC_PartList_Patch.PatchSetSlotsMethod();
			XUiC_PartList_Patch.PatchSetSlotMethod();
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000058D8 File Offset: 0x00003AD8
		public new void SetSlot(ItemValue part, int index)
		{
			XUiC_ItemStack xuiC_ItemStack = this.itemControllers[index];
			xuiC_ItemStack.ViewComponent.IsVisible = true;
			bool flag = part != null && !part.IsEmpty();
			if (flag)
			{
				ItemStack itemStack = new ItemStack(part.Clone(), 1);
				xuiC_ItemStack.ItemStack = itemStack;
				xuiC_ItemStack.GreyedOut = false;
			}
			else
			{
				xuiC_ItemStack.ItemStack = ItemStack.Empty.Clone();
				xuiC_ItemStack.GreyedOut = false;
			}
			this.itemControllers[index].ViewComponent.EventOnPress = false;
			this.itemControllers[index].ViewComponent.EventOnHover = false;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00005974 File Offset: 0x00003B74
		public new void SetSlots(ItemValue[] parts, int startIndex = 0)
		{
			bool flag = startIndex == 0;
			if (flag)
			{
				bool flag2 = this.HasCosemticMods();
				if (flag2)
				{
					this.SetSlot(this.mainItem.itemValue.CosmeticMods[0], 0);
				}
				else
				{
					this.itemControllers[0].ItemStack = ItemStack.Empty.Clone();
					this.itemControllers[0].ViewComponent.IsVisible = false;
				}
				startIndex = 1;
			}
			for (int i = 0; i < this.itemControllers.Length - startIndex; i++)
			{
				int num = i + startIndex;
				XUiC_ItemStack xuiC_ItemStack = this.itemControllers[num];
				bool flag3 = parts.Length > i;
				if (flag3)
				{
					xuiC_ItemStack.ViewComponent.IsVisible = true;
					bool flag4 = parts[i] != null && !parts[i].IsEmpty();
					if (flag4)
					{
						ItemStack itemStack = new ItemStack(parts[i].Clone(), 1);
						xuiC_ItemStack.ItemStack = itemStack;
						xuiC_ItemStack.GreyedOut = false;
						xuiC_ItemStack.ViewComponent.IsVisible = true;
					}
					else
					{
						xuiC_ItemStack.ItemStack = ItemStack.Empty.Clone();
						xuiC_ItemStack.GreyedOut = false;
					}
				}
				else
				{
					xuiC_ItemStack.ItemStack = ItemStack.Empty.Clone();
					xuiC_ItemStack.GreyedOut = false;
					xuiC_ItemStack.ViewComponent.IsVisible = false;
				}
				xuiC_ItemStack.ViewComponent.EventOnPress = false;
				xuiC_ItemStack.ViewComponent.EventOnHover = false;
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00005AE4 File Offset: 0x00003CE4
		public new void SetMainItem(ItemStack itemStack)
		{
			this.mainItem = itemStack;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00005AF0 File Offset: 0x00003CF0
		public bool HasCosemticMods()
		{
			return this.mainItem.itemValue.CosmeticMods != null && this.mainItem.itemValue.CosmeticMods.Length != 0 && this.mainItem.itemValue.CosmeticMods[0] != null;
		}

		// Token: 0x04000046 RID: 70
		public ItemStack mainItem;
	}
}
