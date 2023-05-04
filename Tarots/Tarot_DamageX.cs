using COTL_API.CustomTarotCard;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperchargedTarots.Tarots
{
    internal class Tarot_DamageX : CustomTarotCard
    {
        public override string InternalName => "TAROT_DAMAGE_X";

        public override string Skin => "Trinkets/IncreasedDamage";

        public override string LocalisedName(int upgradeIndex)
        {
            return "<color=\"red\">Ruinous Strike</color>";
        }

        public override string LocalisedLore()
        {
            return "<color=\"yellow\">[Supercharged Series]</color>";
        }

        public override string LocalisedDescription(int upgradeIndex)
        {
            return "100% more Damage.";
        }

        public override float GetWeaponDamageMultiplerIncrease(TarotCards.TarotCard card)
        {
            return 1f;
        }

    }
}
