using System.Reflection;
using HarmonyLib;
using UnityEngine;

/// <summary>
/// FicusHUD — IModApi entry point.
/// Bootstraps Harmony and logs load confirmation.
/// </summary>
public class FicusHUD_ModAPI : IModApi
{
    public void InitMod(Mod _modInstance)
    {
        Assembly asm = Assembly.GetExecutingAssembly();
        new Harmony(asm.GetName().Name).PatchAll(asm);
        Debug.Log("<color=#A8D8A8>[FicusHUD] Loaded.</color>");
    }
}