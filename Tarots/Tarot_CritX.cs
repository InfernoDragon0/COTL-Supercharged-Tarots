using COTL_API.CustomTarotCard;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperchargedTarots.Tarots
{
    internal class Tarot_CritX : CustomTarotCard
    {
        public override string InternalName => "TAROT_CRIT_X";

        public override string Skin => "Trinkets/EyeOfWeakness";

        public override string LocalisedName(int upgradeIndex)
        {
            return "<color=\"red\">Deadeye</color>";
        }

        public override string LocalisedLore()
        {
            return "<color=\"yellow\">[Supercharged Series]</color>";
        }

        public override string LocalisedDescription(int upgradeIndex)
        {
            return "Always Crit.";
        }

        public override float GetWeaponCritChanceIncrease(TarotCards.TarotCard card)
        {
            return 1f;
        }

    }
}
