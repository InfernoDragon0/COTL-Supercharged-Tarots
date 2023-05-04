using COTL_API.CustomTarotCard;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperchargedTarots.Tarots
{
    internal class Tarot_HeartsX : CustomTarotCard
    {
        public override string InternalName => "TAROT_HEARTS_X";

        public override string Skin => "Trinkets/TheLovers2";

        public override string LocalisedName(int upgradeIndex)
        {
            return "<color=\"red\">Ace of Hearts</color>";
        }

        public override string LocalisedLore()
        {
            return "<color=\"yellow\">[Supercharged Series]</color>";
        }

        public override string LocalisedDescription(int upgradeIndex)
        {
            return "Grants you 10 Black Hearts";
        }

        public override void ApplyInstantEffects(TarotCards.TarotCard card)
        {
            PlayerFarming.Instance.GetComponent<HealthPlayer>().BlackHearts += 20f;
            var position2 = PlayerFarming.Instance.CameraBone.transform.position;
            BiomeConstants.Instance.EmitHeartPickUpVFX(position2, 0f, "red", "burst_big");
        }
    }
}
