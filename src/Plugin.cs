using BepInEx;
using BepInEx.Logging;
using BlackMagicAPI.Managers;
using HarmonyLib;
using UnityEngine;
using WalthexSpells.Modules;

//Rename this to match the name of your mod, This needs to match the RootNamespace in the `.csproj` so edit that as well.
// e.g. <RootNamespace>WalthexSpells</RootNamespace>
namespace WalthexSpells;

// Ensure that BepInEx only loads your mod DLL into Mage Arena
[BepInProcess("MageArena")]
//This creates a dependency onto the ModSync mod, but only for BepInEX! Thunderstore only reads dependencies from the `manifest.json `
//Do not forget to include any dependecies there as well.
[BepInDependency("com.magearena.modsync", BepInDependency.DependencyFlags.HardDependency)]
[BepInPlugin(MyGUID, PluginName, VersionString)]
//Rename this to match the name of your mod
public class WSPlugin : BaseUnityPlugin
{
    //Boilerplate but specific
    internal static WSPlugin Instance { get; private set; }
    private const string MyGUID = "com.walthzer.walthexspells.dev";
    internal const string PluginName = "WalthexSpells";
    private const string VersionString = "1.0.0";
    //Boilerplate
    private static Harmony? Harmony;
    internal static new ManualLogSource Logger;
    public static string modsync = "all";

    //Prefabs to be filled by patches. Should use Reflections but can't be bothered
    public static GameObject FrostBolt;

    private void Awake()
    {
        // Plugin startup logic
        Logger = base.Logger;
        Instance = this;
        Harmony = new(MyGUID);
        Harmony.PatchAll();

        //Register spells with BlackMagicAPI
        BlackMagicManager.RegisterSpell(Instance, typeof(IceCreamData), typeof(IceCreamLogic));     //Biden Blast
        BlackMagicManager.RegisterSpell(Instance, typeof(ThwompData), typeof(ThwompLogic));         //Thwompus Decendus

        //Last line of Plugin Logic, to indicate success!
        Logger.LogInfo($"Plugin {MyGUID} v{VersionString} is loaded!");
    }
}
