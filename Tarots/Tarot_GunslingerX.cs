using System;
using COTL_API.CustomTarotCard;

namespace SuperchargedTarots.Tarots
{
    internal class Tarot_GunslingerX : CustomTarotCard
    {
        public override string InternalName => "TAROT_GUNSLINGER_X";

        public override string Skin => "Trinkets/EyeOfWeakness";

        public override string LocalisedName(int upgradeIndex)
        {
            return "<color=\"red\">The Gunslinger</color>";
        }

        public override string LocalisedLore()
        {
            return "<color=\"yellow\">[Supercharged Series]</color>";
        }

        public override string LocalisedDescription(int upgradeIndex)
        {
            return "All weapon choices are now Blunderbuss only.";
        }
    }
}