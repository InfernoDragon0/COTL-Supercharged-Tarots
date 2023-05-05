using COTL_API.CustomTarotCard;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperchargedTarots.Tarots
{
    internal class Tarot_SpeedRunX : CustomTarotCard
    {
        public override string InternalName => "TAROT_SPEEDRUN_X";

        public override string Skin => "Trinkets/MovementSpeed";

        public override string LocalisedName(int upgradeIndex)
        {
            return "<color=\"red\">Speedrunner</color>";
        }

        public override string LocalisedLore()
        {
            return "<color=\"yellow\">[Supercharged Series]</color>";
        }

        public override string LocalisedDescription(int upgradeIndex)
        {
            return "4x Movement Speed.";
        }

        public override float GetMovementSpeedMultiplier(TarotCards.TarotCard card)
        {
            return 4f;
        }

    }
}
