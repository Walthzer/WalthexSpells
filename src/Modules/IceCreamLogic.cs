using BlackMagicAPI.Managers;
using BlackMagicAPI.Modules.Spells;
using System.Collections;
using UnityEngine;

namespace WalthexSpells.Modules;

internal class IceCreamLogic : SpellLogic
{
    public static GameObject iceCreamPrefab;
    private static int coneSpreadAngle = 55;
    private static int baseConesCount = 7;
    private static float coneVelocity = 50f;
    private GameObject[] cones;

    public static void Init()
    {
        iceCreamPrefab = WSPlugin.SpellAssets.LoadAsset<GameObject>("iceCream");
        if (iceCreamPrefab == null)
        {
            WSPlugin.Logger.LogError("Failed to load GameObject from SpellAssets: iceCream");
            return;
        }
        //Register spells with BlackMagicAPI
        BlackMagicManager.RegisterSpell(WSPlugin.Instance, typeof(IceCreamData), typeof(IceCreamLogic));
    }
    public override void CastSpell(GameObject playerObj, PageController page, Vector3 spawnPos, Vector3 viewDirectionVector, int castingLevel)
    {
        //Frost bolt reference is required:
        if (WSPlugin.FrostBolt == null)
        {
            WSPlugin.Logger.LogError("WSPlugin.FrostBolt is null, casting BidenBlast failed");
            return;
        }

        Mesh iceCreamMesh = iceCreamPrefab.GetComponent<MeshFilter>().mesh;
        Material[] iceCreamMaterials = iceCreamPrefab.GetComponent<MeshRenderer>().GetMaterialArray();

        //Do Spell
        //Add extra projectiles for every castingLevel above 1
        // 7 projectiles for level 1
        // 9 projectiles for level 2
        // 11 projectiles for level 3
        int conesCount = baseConesCount + ((castingLevel-1) * 2);
        cones = new GameObject[conesCount];
        for (int i = 0; i < cones.Length; i++)
        {
            cones[i] = Instantiate(WSPlugin.FrostBolt, spawnPos, Quaternion.identity);
            GameObject icespike = cones[i].transform.Find("icespike").gameObject;
            icespike.GetComponent<MeshFilter>().mesh = iceCreamMesh;
            icespike.GetComponent<MeshRenderer>().SetMaterialArray(iceCreamMaterials);
            icespike.transform.rotation = icespike.transform.rotation * Quaternion.Euler(90, 0, 0);

            FrostBoltController controller = cones[i].GetComponent<FrostBoltController>();
            controller.muzzleVelocity = coneVelocity;
            controller.PlayerSetup(playerObj, ConeLaunchVector(viewDirectionVector, i, conesCount), castingLevel);
        }

    }
    
    private Vector3 ConeLaunchVector(Vector3 viewDirectionVector, int index, int conesCount)
    {
        //
        int vectorRotAngle = ((coneSpreadAngle / conesCount) * index) - (coneSpreadAngle / 2);
        return Quaternion.Euler(0, vectorRotAngle, 0) * viewDirectionVector;
    }
}