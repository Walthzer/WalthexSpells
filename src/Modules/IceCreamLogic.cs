using BlackMagicAPI.Modules.Spells;
using System.Collections;
using UnityEngine;

namespace WalthexSpells.Modules;

internal class IceCreamLogic : SpellLogic
{
    private static int coneSpreadAngle = 45;
    private static int baseConesCount = 5;
    private GameObject[] cones;

    public override void CastSpell(GameObject playerObj, PageController page, Vector3 spawnPos, Vector3 viewDirectionVector, int castingLevel)
    {
        //Frost bolt reference is required:
        if (WSPlugin.FrostBolt == null)
        {
            WSPlugin.Logger.LogError("WSPlugin.FrostBolt is null, casting BidenBlast failed");
            return;
        }

        //Do Spell
        //Add extra projectiles for every castingLevel above 1
        // 5 projectiles for level 1
        // 7 projectiles for level 2
        // 9 projectiles for level 3
        int conesCount = baseConesCount + ((castingLevel-1) * 2);
        cones = new GameObject[conesCount];
        for (int i = 0; i < cones.Length; i++)
        {
            cones[i] = Instantiate(WSPlugin.FrostBolt, spawnPos, Quaternion.identity);
            FrostBoltController controller = cones[i].GetComponent<FrostBoltController>();
            controller.muzzleVelocity = 40f;
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