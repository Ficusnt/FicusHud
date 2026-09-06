using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using HarmonyLib;
using UnityEngine;
using Views;

// Token: 0x0200001D RID: 29
[HarmonyPatch(typeof(XUiFromXml))]
public class XUiFromXmlPatch
{
	// Token: 0x06000049 RID: 73 RVA: 0x00007824 File Offset: 0x00005A24
	[HarmonyPrefix]
	[HarmonyPatch("parseViewComponents")]
	public static bool parseByElementName(ref XUiView __result, XElement _node, XUiController _parent, XUiWindowGroup _windowGroup, string nodeNameOverride = "", Dictionary<string, object> _controlParams = null)
	{
		string localName = _node.Name.LocalName;
		string id = localName;
		bool flag = nodeNameOverride == "" && _node.HasAttribute("name");
		if (flag)
		{
			id = _node.GetAttribute("name");
		}
		else
		{
			bool flag2 = nodeNameOverride != "";
			if (flag2)
			{
				id = nodeNameOverride;
			}
		}
		bool flag3 = _controlParams != null;
		if (flag3)
		{
			XUiFromXmlReversePatch.parseControlParams(_node, _parent, _controlParams);
		}
		XUiView xuiView = null;
		string text = localName;
		string a = text;
		if (!(a == "CATUI_animatedsprite"))
		{
			if (!(a == "CATUI_videoplayer"))
			{
				if (!(a == "CATUI_scrollview"))
				{
					if (a == "CATUI_scrollbar")
					{
						xuiView = new XUiV_ScrollBar(id);
						xuiView.xui = _windowGroup.xui;
						XUiFromXmlReversePatch.setController(_node, xuiView, _parent);
						XUiFromXmlReversePatch.parseAttributes(_node, xuiView, _parent, _controlParams);
						xuiView.Controller.WindowGroup = _windowGroup;
						XUiFromXmlPatch.createScrollBarViewComponents(_node, xuiView as XUiV_ScrollBar, _windowGroup, _controlParams);
						__result = xuiView;
						return false;
					}
				}
				else
				{
					xuiView = new XUiV_ScrollViewContainer(id);
				}
			}
			else
			{
				xuiView = new XUiV_VideoPlayer(id);
			}
		}
		else
		{
			xuiView = new XUiV_AnimatedSprite(id);
		}
		bool flag4 = xuiView != null;
		bool result;
		if (flag4)
		{
			xuiView.xui = _windowGroup.xui;
			XUiFromXmlReversePatch.setController(_node, xuiView, _parent);
			XUiFromXmlReversePatch.parseAttributes(_node, xuiView, _parent, _controlParams);
			xuiView.Controller.WindowGroup = _windowGroup;
			foreach (XElement node in _node.Elements())
			{
				XUiFromXmlReversePatch.parseViewComponents(node, _windowGroup, xuiView.Controller, "", _controlParams);
			}
			__result = xuiView;
			result = false;
		}
		else
		{
			result = true;
		}
		return result;
	}

	// Token: 0x0600004A RID: 74 RVA: 0x000079FC File Offset: 0x00005BFC
	private static void createScrollBarViewComponents(XElement _node, XUiV_ScrollBar view, XUiWindowGroup _windowGroup, Dictionary<string, object> _controlParams = null)
	{
		bool flag = !view.HasXMLChildren;
		if (!flag)
		{
			int num = _node.Elements().Count<XElement>();
			bool flag2 = num > 2;
			if (flag2)
			{
				XUiFromXmlReversePatch.logForNode(LogType.Log, _node, "[XUi] XUiFromXml::parseByElementName: Invalid scrollbar child count. Must have zero to two child element.");
			}
			else
			{
				foreach (XElement node in _node.Elements())
				{
					XUiFromXmlPatch.ParseScrollBarViewComponents(node, view.Controller, _windowGroup, "", _controlParams);
				}
			}
		}
	}

	// Token: 0x0600004B RID: 75 RVA: 0x00007A94 File Offset: 0x00005C94
	private static void ParseScrollBarViewComponents(XElement node, XUiController parent, XUiWindowGroup windowGroup, string nodeNameOverride = "", Dictionary<string, object> _controlParams = null)
	{
		string localName = node.Name.LocalName;
		string id = localName;
		bool flag = nodeNameOverride == "" && node.HasAttribute("name");
		if (flag)
		{
			id = node.GetAttribute("name");
		}
		else
		{
			bool flag2 = nodeNameOverride != "";
			if (flag2)
			{
				id = nodeNameOverride;
			}
		}
		bool flag3 = _controlParams != null;
		if (flag3)
		{
			XUiFromXmlReversePatch.parseControlParams(node, parent, _controlParams);
		}
		XUiView xuiView = null;
		string text = localName;
		string a = text;
		if (!(a == "sprite"))
		{
			if (a == "button")
			{
				xuiView = new XUiC_ScrollBar_Button(id);
			}
		}
		else
		{
			xuiView = new XUiC_Scrollbar_Sprite(id);
		}
		bool flag4 = xuiView != null;
		if (flag4)
		{
			xuiView.xui = windowGroup.xui;
			XUiFromXmlReversePatch.setController(node, xuiView, parent);
			XUiFromXmlReversePatch.parseAttributes(node, xuiView, parent, _controlParams);
			xuiView.Controller.WindowGroup = windowGroup;
		}
	}

	// Token: 0x04000013 RID: 19
	private const string TAG = "XUiFromXmlPatch";
}
