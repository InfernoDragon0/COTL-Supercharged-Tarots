using COTL_API.CustomTarotCard;
using COTL_API.Helpers;
using System.IO;
using UnityEngine;

namespace SuperchargedTarotsUA.Tarots
{
    internal class Tarot_SecondWind : CustomTarotCard
    {
        public override string InternalName => "TAROT_SECONDWIND_X";

        /*public override string Skin => "Trinkets/DecreaseRelicCharge";*/

        public override Sprite CustomSprite => TextureHelper.CreateSpriteFromPath(Path.Combine(Plugin.PluginPath, "Assets/tarot_secondwind.png"));
        public override Sprite CustomBackSprite => TextureHelper.CreateSpriteFromPath(Path.Combine(Plugin.PluginPath, "Assets/cardback.png"));

        public override string LocalisedName(int upgradeIndex)
        {
            return "<color=\"red\">Second Wind</color>";
        }

        public override string LocalisedLore()
        {
            return "<color=\"yellow\">[Supercharged Tarots: Unholy Alliance]</color>";
        }

        public override string LocalisedDescription(int upgradeIndex)
        {
            return "Gain 2 Blue Hearts for each heart you currently have.";
        }
        public override void ApplyInstantEffects(TarotCards.TarotCard card)
        {
            //get all current heart types
            var spirithearts = PlayerFarming.Instance.GetComponent<HealthPlayer>().TotalSpiritHearts;
            var blackhearts = PlayerFarming.Instance.GetComponent<HealthPlayer>().BlackHearts;
            var bluehearts = PlayerFarming.Instance.GetComponent<HealthPlayer>().BlueHearts;
            var redhearts = PlayerFarming.Instance.GetComponent<HealthPlayer>().totalHP;

            //add all of them then x2
            var finalhearts = (spirithearts + blackhearts + bluehearts + redhearts) * 2;

            PlayerFarming.Instance.GetComponent<HealthPlayer>().BlueHearts += finalhearts;
            var position2 = PlayerFarming.Instance.CameraBone.transform.position;
            BiomeConstants.Instance.EmitHeartPickUpVFX(position2, 0f, "red", "burst_big");
        }

    }
}
