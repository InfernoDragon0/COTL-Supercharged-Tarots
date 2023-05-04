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


        private void Awake()
        {
            Plugin.Log = base.Logger;

            PluginPath = Path.GetDirectoryName(Info.Location);

            CustomTarotCardManager.Add(new Tarot_AmmoX());
            CustomTarotCardManager.Add(new Tarot_CritX());
            CustomTarotCardManager.Add(new Tarot_CurseDamageX());
            CustomTarotCardManager.Add(new Tarot_DamageX());
            CustomTarotCardManager.Add(new Tarot_HeartsX());
            CustomTarotCardManager.Add(new Tarot_SpeedX());
            CustomTarotCardManager.Add(new Tarot_SunX());
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