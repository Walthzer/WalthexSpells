using BepInEx;
using BepInEx.Logging;
using BlackMagicAPI.Helpers;
using BlackMagicAPI.Managers;
using HarmonyLib;
using System.Reflection;
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
[BepInDependency("com.d1gq.black.magic.api", BepInDependency.DependencyFlags.HardDependency)]
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

    public static AssetBundle SpellAssets;

    //Prefabs to be filled by patches. Should use Reflections but can't be bothered
    public static GameObject FrostBolt;

    private void Awake()
    {
        // Plugin startup logic
        Logger = base.Logger;
        Instance = this;
        Harmony = new(MyGUID);
        Harmony.PatchAll();

        SpellAssets = Assembly.GetExecutingAssembly().LoadAssetBundleFromResources("WalthexSpells.Resources.spells");
        if (SpellAssets == null)
        {
            Logger.LogError("Failed to load assetBundle from resources: spells");
            return;
        }

        //Init spells and Register with BlackMagicAPI 
        IceCreamLogic.Init(); //Biden Blast
        ThwompLogic.Init(); //Thwompus Decendus       

        //Last line of Plugin Logic, to indicate success!
        Logger.LogInfo($"Plugin {MyGUID} v{VersionString} is loaded!");
    }
}
