using COTL_API.CustomTarotCard;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperchargedTarots.Tarots
{
    internal class Tarot_AmmoX : CustomTarotCard
    {
        public override string InternalName => "TAROT_AMMO_X";

        public override string Skin => "Trinkets/AmmoEfficient";

        public override string LocalisedName(int upgradeIndex)
        {
            return "<color=\"red\">Shadow Quiver</color>";
        }

        public override string LocalisedLore()
        {
            return "<color=\"yellow\">[Supercharged Series]</color>";
        }

        public override string LocalisedDescription(int upgradeIndex)
        {
            return "Near Infinite Curse Casting.";
        }

        public override float GetAmmoEfficiency(TarotCards.TarotCard card)
        {
            return 666f;
        }

    }
}
