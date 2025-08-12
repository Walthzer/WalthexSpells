using BlackMagicAPI.Modules.Spells;
using UnityEngine;

namespace WalthexSpells.Modules
{
    internal class ThwompData : SpellData
    {
        public override string Name => "Tump Decendus";

        public override string[] SubNames => ["flatten enemy", "Thwomp Decendus", "Tomp Decendus", "Thwompus decend", "Thwomp decend", "Tomp decend"];

        public override float Cooldown => 20;

        public override Color GlowColor => Color.gray;

        public override bool DebugForceSpawn => true; //DEBUG: REMOVE BEFORE RELEASE
    }
}
