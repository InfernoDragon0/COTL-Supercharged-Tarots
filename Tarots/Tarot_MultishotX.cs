using System;
using COTL_API.CustomTarotCard;

namespace SuperchargedTarots.Tarots
{
    internal class Tarot_MultishotX : CustomTarotCard
    {
        public override string InternalName => "TAROT_MULTISHOT_X";

        public override string Skin => "Trinkets/EyeOfWeakness";

        public override string LocalisedName(int upgradeIndex)
        {
            return "<color=\"red\">Multishot</color>";
        }

        public override string LocalisedLore()
        {
            return "<color=\"yellow\">[Supercharged Series]</color>";
        }

        public override string LocalisedDescription(int upgradeIndex)
        {
            return "Blunderbuss attacks will shoot in all 4 directions.";
        }
    }
}
