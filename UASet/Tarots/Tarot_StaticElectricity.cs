using COTL_API.CustomTarotCard;
using COTL_API.Helpers;
using System.IO;
using UnityEngine;

namespace SuperchargedTarotsUA.Tarots
{
    internal class Tarot_StaticElectricity : CustomTarotCard
    {
        public override string InternalName => "TAROT_STATICELECTRICITY_X";

        public override Sprite CustomSprite => TextureHelper.CreateSpriteFromPath(Path.Combine(Plugin.PluginPath, "Assets/tarot_staticelectricity.png"));
        public override Sprite CustomBackSprite => TextureHelper.CreateSpriteFromPath(Path.Combine(Plugin.PluginPath, "Assets/cardback.png"));
        public override string LocalisedName(int upgradeIndex)
        {
            return "<color=\"red\">Static Electricity</color>";
        }

        public override string LocalisedLore()
        {
            return "<color=\"yellow\">[Supercharged Tarots: Unholy Alliance]</color>";
        }

        public override string LocalisedDescription(int upgradeIndex)
        {
            return "While near each other, pressing Attack generate lightning strikes.";
        }

    }
}
