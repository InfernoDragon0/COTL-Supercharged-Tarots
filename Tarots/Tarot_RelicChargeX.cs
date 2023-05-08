using COTL_API.CustomTarotCard;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperchargedTarots.Tarots
{
    internal class Tarot_RelicChargeX : CustomTarotCard
    {
        public override string InternalName => "TAROT_RELICCHARGE_X";

        public override string Skin => "Trinkets/DecreaseRelicCharge";

        public override string LocalisedName(int upgradeIndex)
        {
            return "<color=\"red\">Relic Overdrive</color>";
        }

        public override string LocalisedLore()
        {
            return "<color=\"yellow\">[Supercharged Series]</color>";
        }

        public override string LocalisedDescription(int upgradeIndex)
        {
            return Plugin.relicChargeConfig.Value + "x Relic Charge Speed.";
        }

        public override float GetRelicChargeMultiplier(TarotCards.TarotCard card)
        {
            return Plugin.relicChargeConfig.Value;
        }

    }
}
