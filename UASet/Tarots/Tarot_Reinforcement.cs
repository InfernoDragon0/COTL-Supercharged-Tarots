using COTL_API.CustomTarotCard;
using COTL_API.Helpers;
using System.IO;
using UnityEngine;

namespace SuperchargedTarotsUA.Tarots
{
    internal class Tarot_Reinforcement : CustomTarotCard
    {
        public override string InternalName => "TAROT_REINFORCEMENT_X";

        public override Sprite CustomSprite => TextureHelper.CreateSpriteFromPath(Path.Combine(Plugin.PluginPath, "Assets/tarot_reinforcement.png"));
        public override Sprite CustomBackSprite => TextureHelper.CreateSpriteFromPath(Path.Combine(Plugin.PluginPath, "Assets/cardback.png"));
        public override string LocalisedName(int upgradeIndex)
        {
            return "<color=\"red\">Reinforcement</color>";
        }

        public override string LocalisedLore()
        {
            return "<color=\"yellow\">[Supercharged Tarots: Unholy Alliance]</color>";
        }

        public override string LocalisedDescription(int upgradeIndex)
        {
            return "You are immune to traps. Each time you take damage, summon a combat follower to fight for you.";
        }

        public override void ApplyInstantEffects(TarotCards.TarotCard card)
        {
            //become immune to traps
            PlayerFarming.Instance.health.ImmuneToTraps = true;
        }

    }
}
