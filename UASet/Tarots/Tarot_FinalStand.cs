using COTL_API.CustomTarotCard;
using COTL_API.Helpers;
using System.IO;
using UnityEngine;

namespace SuperchargedTarotsUA.Tarots
{
    internal class Tarot_FinalStand : CustomTarotCard
    {
        public override string InternalName => "TAROT_FINALSTAND_X";

        public override Sprite CustomSprite => TextureHelper.CreateSpriteFromPath(Path.Combine(Plugin.PluginPath, "Assets/tarot_finalstand.png"));
        public override Sprite CustomBackSprite => TextureHelper.CreateSpriteFromPath(Path.Combine(Plugin.PluginPath, "Assets/cardback.png"));
        public override string LocalisedName(int upgradeIndex)
        {
            return "<color=\"red\">Final Stand</color>";
        }

        public override string LocalisedLore()
        {
            return "<color=\"yellow\">[Supercharged Tarots: Unholy Alliance]</color>";
        }

        public override string LocalisedDescription(int upgradeIndex)
        {
            return "For every dead follower you own, increase weapon damage by 10%.";
        }
        
        public override float GetWeaponDamageMultiplerIncrease(TarotCards.TarotCard card)
        {
            var deadFollowers = DataManager.instance.Followers_Dead.Count;
            Plugin.Log.LogInfo("Granting final stand bonus using " + deadFollowers + " dead followers.");

            return 1f + (deadFollowers * 0.1f);
        }
    }
}
