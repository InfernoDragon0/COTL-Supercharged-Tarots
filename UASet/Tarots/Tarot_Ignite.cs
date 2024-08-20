using COTL_API.CustomTarotCard;
using COTL_API.Helpers;
using System.IO;
using UnityEngine;

namespace SuperchargedTarotsUA.Tarots
{
    internal class Tarot_Ignite : CustomTarotCard
    {
        public override string InternalName => "TAROT_IGNITE_X";

        public override Sprite CustomSprite => TextureHelper.CreateSpriteFromPath(Path.Combine(Plugin.PluginPath, "Assets/tarot_ignite.png"));
        public override Sprite CustomBackSprite => TextureHelper.CreateSpriteFromPath(Path.Combine(Plugin.PluginPath, "Assets/cardback.png"));
        public override string LocalisedName(int upgradeIndex)
        {
            return "<color=\"red\">Ignite</color>";
        }

        public override string LocalisedLore()
        {
            return "<color=\"yellow\">[Supercharged Tarots: Unholy Alliance]</color>";
        }

        public override string LocalisedDescription(int upgradeIndex)
        {
            return "Both players explode when pressing Attack at the same time.";
        }

    }
}
