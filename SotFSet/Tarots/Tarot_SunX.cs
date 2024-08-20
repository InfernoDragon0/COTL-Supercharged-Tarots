using COTL_API.CustomTarotCard;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperchargedTarots.Tarots
{
    internal class Tarot_SunX : CustomTarotCard
    {
        public override string InternalName => "TAROT_SUN_X";

        public override string Skin => "Trinkets/Sun";

        public override string LocalisedName(int upgradeIndex)
        {
            return "<color=\"red\">The Solunar Eclipse</color>";
        }

        public override string LocalisedLore()
        {
            return "<color=\"yellow\">[Supercharged Series]</color>";
        }

        public override string LocalisedDescription(int upgradeIndex)
        {
            return Plugin.sunConfig.Value + "x Damage during the day, " + Plugin.moonConfig.Value + "x Damage during the night.";
        }
        
        public override float GetWeaponDamageMultiplerIncrease(TarotCards.TarotCard card)
        {
            return !TimeManager.IsDay ? Plugin.moonConfig.Value : Plugin.sunConfig.Value;
        }
    }
}
