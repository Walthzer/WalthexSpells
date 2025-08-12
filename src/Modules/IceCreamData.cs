using BlackMagicAPI.Modules.Spells;
using UnityEngine;

namespace WalthexSpells.Modules
{
    internal class IceCreamData : SpellData
    {
        public override string Name => "Biden Blast";

        public override string[] SubNames => ["Creamy", "Ice Cream"];

        public override float Cooldown => 15;

        public override Color GlowColor => Color.blue;

        public override bool DebugForceSpawn => true; //DEBUG: REMOVE BEFORE RELEASE
    }
}
