using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.IO;
using COTL_API.CustomTarotCard;
using SuperchargedTarots.Tarots;

namespace SuperchargedTarots
{
    [BepInPlugin(PluginGuid, PluginName, PluginVer)]
    [BepInDependency("io.github.xhayper.COTL_API")]
    [HarmonyPatch]
    public class Plugin : BaseUnityPlugin
    {
        public const string PluginGuid = "InfernoDragon0.cotl.SuperchargedTarots";
        public const string PluginName = "SuperchargedTarots";
        public const string PluginVer = "1.0.0";

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

        private void Awake()
        {
            Plugin.Log = base.Logger;

            PluginPath = Path.GetDirectoryName(Info.Location);

            ammoX = CustomTarotCardManager.Add(new Tarot_AmmoX());
            critX = CustomTarotCardManager.Add(new Tarot_CritX());
            curseDamageX = CustomTarotCardManager.Add(new Tarot_CurseDamageX());
            damageX = CustomTarotCardManager.Add(new Tarot_DamageX());
            heartsX = CustomTarotCardManager.Add(new Tarot_HeartsX());
            speedX = CustomTarotCardManager.Add(new Tarot_SpeedX());
            speedRunX = CustomTarotCardManager.Add(new Tarot_SpeedRunX());
            sunX = CustomTarotCardManager.Add(new Tarot_SunX());
            perfectCurseX = CustomTarotCardManager.Add(new Tarot_PerfectCurseX());
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