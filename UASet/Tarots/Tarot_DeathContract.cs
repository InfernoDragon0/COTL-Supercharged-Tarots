using COTL_API.CustomTarotCard;
using COTL_API.Helpers;
using System.IO;
using UnityEngine;

namespace SuperchargedTarotsUA.Tarots
{
    internal class Tarot_DeathContract : CustomTarotCard
    {
        public override string InternalName => "TAROT_DEATHCONTRACT_X";

        public override Sprite CustomSprite => TextureHelper.CreateSpriteFromPath(Path.Combine(Plugin.PluginPath, "Assets/tarot_deathcontract.png"));

        public override Sprite CustomBackSprite => TextureHelper.CreateSpriteFromPath(Path.Combine(Plugin.PluginPath, "Assets/cardback.png"));

        public override string LocalisedName(int upgradeIndex)
        {
            return "<color=\"red\">Death Contract</color>";
        }

        public override string LocalisedLore()
        {
            return "<color=\"yellow\">[Supercharged Tarots: Unholy Alliance]</color>";
        }

        public override string LocalisedDescription(int upgradeIndex)
        {
            return "Taking damage to red hearts transfers damage taken as blue hearts to the other player.";
        }

    }
}
