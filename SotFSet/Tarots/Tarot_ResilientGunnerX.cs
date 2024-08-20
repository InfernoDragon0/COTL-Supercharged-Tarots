using System;
using COTL_API.CustomTarotCard;

namespace SuperchargedTarots.Tarots
{
    internal class Tarot_ResilientGunnerX : CustomTarotCard
    {
        public override string InternalName => "TAROT_RESILIENTGUNNER_X";

        public override string Skin => "Trinkets/EyeOfWeakness";

        public override string LocalisedName(int upgradeIndex)
        {
            return "<color=\"red\">Resilient Gunner</color>";
        }

        public override string LocalisedLore()
        {
            return "<color=\"yellow\">[Supercharged Series]</color>";
        }

        public override string LocalisedDescription(int upgradeIndex)
        {
            return "No Recoil when using Blunderbuss.";
        }
    }
}