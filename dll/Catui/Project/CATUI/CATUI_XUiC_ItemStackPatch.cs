using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using UnityEngine;

// Token: 0x0200000C RID: 12
[HarmonyPatch]
public class XUiC_ItemStackPatch
{
	// Token: 0x06000022 RID: 34 RVA: 0x00004E38 File Offset: 0x00003038
	[HarmonyPostfix]
	[HarmonyPatch(typeof(XUiC_ItemStack), "Init")]
	public static void InitPostfix(XUiC_ItemStack __instance)
	{
		bool flag = XUiC_ItemStackPatch._patchedInstances.Contains(__instance);
		if (!flag)
		{
			XUiC_ItemStackPatch._patchedInstances.Add(__instance);
			__instance.OnPress += delegate(XUiController _sender, int _mouseButton)
			{
				bool flag2 = !InputUtils.AltKeyPressed;
				if (!flag2)
				{
					bool flag3 = false;
					bool flag4 = _sender.CustomAttributes.ContainsKey("allow_clicklock");
					if (flag4)
					{
						flag3 = StringParsers.ParseBool(_sender.CustomAttributes["allow_clicklock"], 0, -1, true);
					}
					bool flag5 = !flag3;
					if (!flag5)
					{
						XUiC_BackpackWindow childByType = __instance.xui.GetChildByType<XUiC_BackpackWindow>();
						XUiC_LootWindow childByType2 = __instance.xui.GetChildByType<XUiC_LootWindow>();
						XUiC_VehicleContainer childByType3 = __instance.xui.GetChildByType<XUiC_VehicleContainer>();
						__instance.UserLockedSlot = !__instance.UserLockedSlot;
						__instance.RefreshBindings(false);
						bool flag6 = _sender.Parent.ToString() == "XUiC_Backpack";
						if (flag6)
						{
							childByType.UpdateLockedSlots(childByType.standardControls);
						}
						else
						{
							bool flag7 = _sender.Parent.ToString() == "XUiC_LootContainer" && childByType2.IsOpen;
							if (flag7)
							{
								childByType2.UpdateLockedSlots(childByType2.standardControls);
							}
							else
							{
								bool flag8 = _sender.Parent.ToString() == "XUiController" && childByType3.IsOpen;
								if (flag8)
								{
									childByType3.UpdateLockedSlots(childByType3.standardControls);
								}
							}
						}
						__instance.xui.PlayMenuClickSound();
					}
				}
			};
		}
	}

	// Token: 0x06000023 RID: 35 RVA: 0x00004E94 File Offset: 0x00003094
	[HarmonyPrefix]
	[HarmonyPatch(typeof(XUiC_ItemStack), "GetBindingValueInternal")]
	public static bool GetBindingValueInternalPrefix(string _bindingName, ref string _value, ref bool __result, XUiC_ItemStack __instance)
	{
		bool result;
		if (!(_bindingName == "CATUI_itemStackSlotIndex"))
		{
			if (!(_bindingName == "CATUI_itemStackModifications"))
			{
				result = true;
			}
			else
			{
				_value = "";
				object obj;
				if (__instance == null)
				{
					obj = null;
				}
				else
				{
					ItemStack itemStack = __instance.itemStack;
					obj = ((itemStack != null) ? itemStack.itemValue : null);
				}
				bool flag = obj == null;
				if (flag)
				{
					Debug.Log("<color=#00FF00>[CATUI] CATUI_itemStackModifications __instance?.itemStack?.itemValue == null </color>");
					__result = true;
					result = false;
				}
				else
				{
					string text;
					__instance.CustomAttributes.TryGetValue("mod_highlight", out text);
					string text2;
					__instance.CustomAttributes.TryGetValue("mod_default", out text2);
					if (text == null)
					{
						text = "[04FE85]▇[-] ";
					}
					if (text2 == null)
					{
						text2 = "▇ ";
					}
					ItemValue itemValue = __instance.itemStack.itemValue;
					ItemValue[] modifications = itemValue.Modifications;
					bool flag2 = itemValue.Quality <= 0 || modifications == null || modifications.Length == 0;
					if (flag2)
					{
						__result = true;
						result = false;
					}
					else
					{
						StringBuilder stringBuilder = new StringBuilder();
						for (int i = 0; i < modifications.Length; i++)
						{
							bool flag3 = modifications[i] == null;
							if (flag3)
							{
								stringBuilder.Append(text2);
							}
							else
							{
								ItemValue itemValue2 = modifications[i];
								ItemClass itemClass = (itemValue2 != null) ? itemValue2.ItemClass : null;
								bool flag4 = itemClass != null && !string.IsNullOrEmpty((itemClass != null) ? itemClass.GetItemName() : null);
								if (flag4)
								{
									stringBuilder.Append(text);
								}
								else
								{
									stringBuilder.Append(text2);
								}
							}
						}
						_value = stringBuilder.ToString();
						__result = true;
						result = false;
					}
				}
			}
		}
		else
		{
			_value = "0";
			bool flag5;
			if (__instance == null)
			{
				flag5 = false;
			}
			else
			{
				int slotNumber = __instance.SlotNumber;
				flag5 = true;
			}
			bool flag6 = flag5;
			if (flag6)
			{
				_value = (__instance.SlotNumber + 1).ToString();
			}
			__result = true;
			result = false;
		}
		return result;
	}

	// Token: 0x06000024 RID: 36 RVA: 0x00005054 File Offset: 0x00003254
	[HarmonyPostfix]
	[HarmonyPatch(typeof(XUiC_ItemStack), "AllowIconGrow", 1)]
	public static void AllowIconGrowPostfix(ref bool __result)
	{
		__result = false;
	}

	// Token: 0x0400000F RID: 15
	private const string ALLOW_CLICKLOCK_ATTR = "allow_clicklock";

	// Token: 0x04000010 RID: 16
	private static readonly HashSet<XUiC_ItemStack> _patchedInstances = new HashSet<XUiC_ItemStack>();

	// Token: 0x04000011 RID: 17
	private const string MODIFICATION_HIGHLIGHTED = "[04FE85]▇[-] ";

	// Token: 0x04000012 RID: 18
	private const string MODIFICATION_DEFAULT = "▇ ";
}
