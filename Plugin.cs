using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.IO;
using COTL_API.CustomTarotCard;
using SuperchargedTarots.Tarots;
using BepInEx.Configuration;

namespace SuperchargedTarots
{
    [BepInPlugin(PluginGuid, PluginName, PluginVer)]
    [BepInDependency("io.github.xhayper.COTL_API")]
    [HarmonyPatch]
    public class Plugin : BaseUnityPlugin
    {
        public const string PluginGuid = "InfernoDragon0.cotl.SuperchargedTarots";
        public const string PluginName = "SuperchargedTarots";
        public const string PluginVer = "1.0.1";

        internal static ManualLogSource Log;
        internal readonly static Harmony Harmony = new(PluginGuid);

        internal static string PluginPath;
        public static TarotCards.Card perfectCurseX;
        public static TarotCards.Card speedX;
        public static TarotCards.Card speedRunX;
        public static TarotCards.Card heartsX;
        public static TarotCards.Card damageX;
        public static TarotCards.Card curseDamageX;
        public static TarotCards.Card sunX;
        public static TarotCards.Card critX;
        public static TarotCards.Card ammoX;
        public static TarotCards.Card relicChargeX;

        //Config for Tarots
        internal static ConfigEntry<float> ammoConfig;
        internal static ConfigEntry<float> curseDamageConfig;
        internal static ConfigEntry<float> damageConfig;
        internal static ConfigEntry<float> heartsConfig;
        internal static ConfigEntry<float> relicChargeConfig;
        internal static ConfigEntry<float> speedConfig;
        internal static ConfigEntry<float> speedRunConfig;
        internal static ConfigEntry<float> sunConfig;
        internal static ConfigEntry<float> moonConfig;
        

        private void Awake()
        {
            Plugin.Log = base.Logger;

            PluginPath = Path.GetDirectoryName(Info.Location);

            //SETUP: Config
            ammoConfig = Config.Bind("SuperchargedTarots", "Ammo", 66f, "Fervor Use Discount (higher the lesser, but dont go over 100)");
            curseDamageConfig = Config.Bind("SuperchargedTarots", "CurseDamage", 3f, "Curse Damage Multiplier");
            damageConfig = Config.Bind("SuperchargedTarots", "Damage", 1f, "Damage Multiplier");
            heartsConfig = Config.Bind("SuperchargedTarots", "Hearts", 20f, "Black Hearts, 2 per full heart");
            relicChargeConfig = Config.Bind("SuperchargedTarots", "RelicCharge", 30f, "Relic Charge Multiplier");
            speedConfig = Config.Bind("SuperchargedTarots", "AttackSpeed", 5f, "Attack Speed Multiplier");
            speedRunConfig = Config.Bind("SuperchargedTarots", "SpeedRun", 3f, "Move Speed Multiplier (should not go over 10, or you will move too fast)");
            sunConfig = Config.Bind("SuperchargedTarots", "Sun", 2f, "Sun Damage Multiplier");
            moonConfig = Config.Bind("SuperchargedTarots", "Moon", 3f, "Moon Damage Multiplier");

            //SETUP: Add Cards
            ammoX = CustomTarotCardManager.Add(new Tarot_AmmoX());
            critX = CustomTarotCardManager.Add(new Tarot_CritX());
            curseDamageX = CustomTarotCardManager.Add(new Tarot_CurseDamageX());
            damageX = CustomTarotCardManager.Add(new Tarot_DamageX());
            heartsX = CustomTarotCardManager.Add(new Tarot_HeartsX());
            speedX = CustomTarotCardManager.Add(new Tarot_SpeedX());
            speedRunX = CustomTarotCardManager.Add(new Tarot_SpeedRunX());
            sunX = CustomTarotCardManager.Add(new Tarot_SunX());
            perfectCurseX = CustomTarotCardManager.Add(new Tarot_PerfectCurseX());
            relicChargeX = CustomTarotCardManager.Add(new Tarot_RelicChargeX());
        }

        private void OnEnable()
        {
            Harmony.PatchAll();
            Logger.LogInfo($"Loaded {PluginName}!");
        }

        private void OnDisable()
        {
            Harmony.UnpatchSelf();
            Logger.LogInfo($"Unloaded {PluginName}!");
        }
    }
}