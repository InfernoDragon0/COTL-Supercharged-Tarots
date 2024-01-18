using System;
using COTL_API.CustomTarotCard;

namespace SuperchargedTarots.Tarots
{

    internal class Tarot_BlunderBusterX : CustomTarotCard
    {
        public override string InternalName => "TAROT_BLUNDERBUSTER_X";

        public override string Skin => "Trinkets/EyeOfWeakness";

        public override string LocalisedName(int upgradeIndex)
        {
            return "<color=\"red\">Blunder Buster</color>";
        }

        public override string LocalisedLore()
        {
            return "<color=\"yellow\">[Supercharged Series]</color>";
        }

        public override string LocalisedDescription(int upgradeIndex)
        {
            return "Infinite Ammo for Blunderbuss.";
        }

    }
}