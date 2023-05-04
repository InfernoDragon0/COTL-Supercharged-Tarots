using COTL_API.CustomTarotCard;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperchargedTarots.Tarots
{
    internal class Tarot_CurseDamageX : CustomTarotCard
    {
        public override string InternalName => "TAROT_CURSEDAMAGE_X";

        public override string Skin => "Trinkets/Potion";

        public override string LocalisedName(int upgradeIndex)
        {
            return "<color=\"red\">Supernova</color>";
        }

        public override string LocalisedLore()
        {
            return "<color=\"yellow\">[Supercharged Series]</color>";
        }

        public override string LocalisedDescription(int upgradeIndex)
        {
            return "300% more Curse Damage.";
        }

        public override float GetCurseDamageMultiplerIncrease(TarotCards.TarotCard card)
        {
            return 3f;
        }

    }
}
