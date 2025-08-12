using BepInEx.Logging;
using HarmonyLib;
using System;

namespace WalthexSpells.Patches.Controllers;

//Using a patch on the MageBook to obtain references to spell prefabs.
[HarmonyPatch(typeof(MageBookController))]
internal class MageBookControllerPatch
{
    [HarmonyPatch(nameof(MageBookController.Awake))]
    [HarmonyPrefix]
    private static void Awake_PostFix(MageBookController __instance)
    {
        if (WSPlugin.FrostBolt == null)
        {
            WSPlugin.FrostBolt = __instance.frostBolt;
        }
    }
}