using COTL_API.CustomTarotCard;
using COTL_API.Helpers;
using System.IO;
using UnityEngine;

namespace SuperchargedTarotsUA.Tarots
{
    internal class Tarot_WardingBond : CustomTarotCard
    {
        public override string InternalName => "TAROT_WARDINGBOND_X";

        public override Sprite CustomSprite => TextureHelper.CreateSpriteFromPath(Path.Combine(Plugin.PluginPath, "Assets/tarot_wardingbond.png"));
        public override Sprite CustomBackSprite => TextureHelper.CreateSpriteFromPath(Path.Combine(Plugin.PluginPath, "Assets/cardback.png"));
        public override string LocalisedName(int upgradeIndex)
        {
            return "<color=\"red\">Warding Bond</color>";
        }

        public override string LocalisedLore()
        {
            return "<color=\"yellow\">[Supercharged Tarots: Unholy Alliance]</color>";
        }

        public override string LocalisedDescription(int upgradeIndex)
        {
            return "While near each other, negate damage taken at a 70% chance.";
        }

    }
}
