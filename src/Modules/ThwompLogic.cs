using BlackMagicAPI.Managers;
using BlackMagicAPI.Modules.Spells;
using System.Collections;
using UnityEngine;

namespace WalthexSpells.Modules;

internal class ThwompLogic : SpellLogic
{
    public static GameObject ThwompPrefab;

    public static void Init()
    {
        ThwompPrefab = WSPlugin.SpellAssets.LoadAsset<GameObject>("Thwomp");
        if (ThwompPrefab == null)
        {
            WSPlugin.Logger.LogError("Failed to load GameObject from SpellAssets: Thwomp");
            return;
        }
        //Register spells with BlackMagicAPI
        BlackMagicManager.RegisterSpell(WSPlugin.Instance, typeof(ThwompData), typeof(ThwompLogic));
    }
    public override void CastSpell(GameObject playerObj, PageController page, Vector3 spawnPos, Vector3 viewDirectionVector, int castingLevel)
    {
        //ThwompPrefab reference is required:
        if (ThwompPrefab == null)
        {
            WSPlugin.Logger.LogError("ThwompLogic.thwompPrefab is null, casting Thwompus Decendus failed");
            return;
        }

        GameObject cone = Instantiate(WSPlugin.SpellAssets.LoadAsset<GameObject>("iceCream"), spawnPos, Quaternion.identity);
        GameObject Thwomp =  Instantiate(ThwompPrefab, spawnPos + (Vector3.up * 100), Quaternion.identity);
        WSPlugin.Logger.LogDebug("Thwomp instance is:");
        WSPlugin.Logger.LogDebug(Thwomp);
    }
}