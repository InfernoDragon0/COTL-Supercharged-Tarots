using COTL_API.CustomTarotCard;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperchargedTarots.Tarots
{
    internal class Tarot_SpeedX : CustomTarotCard
    {
        public override string InternalName => "TAROT_SPEED_X";

        public override string Skin => "Trinkets/AttackRate";

        public override string LocalisedName(int upgradeIndex)
        {
            return "<color=\"red\">Sonic Surge</color>";
        }

        public override string LocalisedLore()
        {
            return "<color=\"yellow\">[Supercharged Series]</color>";
        }

        public override string LocalisedDescription(int upgradeIndex)
        {
            return "5x Attack Speed.";
        }

        public override float GetAttackRateMultiplier(TarotCards.TarotCard card)
        {
            return 5f;
        }

    }
}
