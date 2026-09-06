using System;
using System.Collections.Generic;
using System.Xml.Linq;
using HarmonyLib;
using UnityEngine;

// Token: 0x0200001E RID: 30
[HarmonyPatch(typeof(XUiFromXml))]
public class XUiFromXmlReversePatch
{
	// Token: 0x0600004D RID: 77 RVA: 0x00007B91 File Offset: 0x00005D91
	[HarmonyReversePatch(0)]
	[HarmonyPatch("parseControlParams")]
	public static void parseControlParams(XElement _node, XUiController _parent, Dictionary<string, object> _controlParams)
	{
		throw new NotImplementedException("Error Reverse Patching XUiFromXML method: parseControlParams");
	}

	// Token: 0x0600004E RID: 78 RVA: 0x00007B9E File Offset: 0x00005D9E
	[HarmonyReversePatch(0)]
	[HarmonyPatch("setController")]
	public static void setController(XElement _node, XUiView _viewComponent, XUiController _parent)
	{
		throw new NotImplementedException("Error Reverse Patching XUiFromXML method: setController");
	}

	// Token: 0x0600004F RID: 79 RVA: 0x00007BAB File Offset: 0x00005DAB
	[HarmonyReversePatch(0)]
	[HarmonyPatch("parseAttributes")]
	public static void parseAttributes(XElement _node, XUiView _viewComponent, XUiController _parent, Dictionary<string, object> _controlParams = null)
	{
		throw new NotImplementedException("Error Reverse Patching XUiFromXML method: parseAttributes");
	}

	// Token: 0x06000050 RID: 80 RVA: 0x00007BB8 File Offset: 0x00005DB8
	[HarmonyReversePatch(0)]
	[HarmonyPatch("parseViewComponents")]
	public static XUiView parseViewComponents(XElement _node, XUiWindowGroup _windowGroup, XUiController _parent = null, string nodeNameOverride = "", Dictionary<string, object> _controlParams = null)
	{
		throw new NotImplementedException("Error Reverse Patching XUiFromXML method: parseViewComponents");
	}

	// Token: 0x06000051 RID: 81 RVA: 0x00007BC5 File Offset: 0x00005DC5
	[HarmonyReversePatch(0)]
	[HarmonyPatch("logForNode")]
	public static void logForNode(LogType _level, XElement _node, string _message)
	{
		throw new NotImplementedException("Error Reverse Patching XUiFromXML method: logForNode");
	}

	// Token: 0x04000014 RID: 20
	private const string TAG = "Error Reverse Patching XUiFromXML method: ";
}
