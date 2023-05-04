using COTL_API.CustomTarotCard;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperchargedTarots.Tarots
{
    public class Tarot_PerfectCurseX : CustomTarotCard
    {
        public override string InternalName => "TAROT_PERFECT_CURSE_X";

        public override string Skin => "Trinkets/AmmoEfficient";

        public override string LocalisedName(int upgradeIndex)
        {
            return "<color=\"red\">Curse Perfected</color>";
        }

        public override string LocalisedLore()
        {
            return "<color=\"yellow\">[Supercharged Series]</color>";
        }

        public override string LocalisedDescription(int upgradeIndex)
        {
            return "Always Perfect Curse Casting.";
        }

    }
}
