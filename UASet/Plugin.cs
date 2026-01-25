using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.IO;
using COTL_API.CustomTarotCard;
using SuperchargedTarotsUA.Tarots;
using BepInEx.Configuration;
using COTL_API.CustomSkins;
using COTL_API.Helpers;
using System.Collections.Generic;

namespace SuperchargedTarotsUA
{
    [BepInPlugin(PluginGuid, PluginName, PluginVer)]
    [BepInDependency("io.github.xhayper.COTL_API")]
    [BepInDependency("InfernoDragon0.cotl.SuperchargedTarots", BepInDependency.DependencyFlags.SoftDependency)]
    [HarmonyPatch]
    public class Plugin : BaseUnityPlugin
    {
        public const string PluginGuid = "InfernoDragon0.cotl.SuperchargedTarotsUA";
        public const string PluginName = "SuperchargedTarotsUA";
        public const string PluginVer = "1.0.1";

        internal static ManualLogSource Log;
        internal readonly static Harmony Harmony = new(PluginGuid);

        internal static string PluginPath;
       
        public static TarotCards.Card secondWind;
        public static TarotCards.Card finalStand;
        public static TarotCards.Card reinforcement;
        public static TarotCards.Card bloodForBlood;

        public static TarotCards.Card staticElectricity;
        public static TarotCards.Card duality;
        public static TarotCards.Card transference;
        public static TarotCards.Card wardingBond;
        public static TarotCards.Card deathContract;
        public static TarotCards.Card ignite;


        //Config for Tarots
        internal static ConfigEntry<bool> tarotSecondWindConfig;
        internal static ConfigEntry<bool> tarotFinalStandConfig;
        internal static ConfigEntry<bool> tarotReinforcementConfig;
        internal static ConfigEntry<bool> tarotBloodForBloodConfig;

        internal static ConfigEntry<bool> tarotStaticElectricityConfig;
        internal static ConfigEntry<bool> tarotDualityConfig;
        internal static ConfigEntry<bool> tarotTransferenceConfig;
        internal static ConfigEntry<bool> tarotWardingBondConfig;
        internal static ConfigEntry<bool> tarotDeathContractConfig;
        internal static ConfigEntry<bool> tarotIgniteConfig;

        internal static ConfigEntry<bool> shouldAddCardConfig;

        public static List<TarotCards.Card> coopCards = [
        ];

        public static List<TarotCards.Card> soloCards = [
        ];
        

        private void Awake()
        {

            //TrinketManager.AddTrinket(new TarotCards.TarotCard(SuperchargedTarotsUA.Plugin.reinforcement, 0), PlayerFarming.Instance);
            //TrinketManager.AddTrinket(new TarotCards.TarotCard(SuperchargedTarotsUA.Plugin.secondWind, 0), PlayerFarming.Instance);
            //TrinketManager.AddTrinket(new TarotCards.TarotCard(SuperchargedTarotsUA.Plugin.finalStand, 0), PlayerFarming.Instance);
            //TrinketManager.AddTrinket(new TarotCards.TarotCard(SuperchargedTarotsUA.Plugin.bloodForBlood, 0), PlayerFarming.Instance);

            //TrinketManager.AddTrinket(new TarotCards.TarotCard(SuperchargedTarotsUA.Plugin.staticElectricity, 0), PlayerFarming.Instance);
            //TrinketManager.AddTrinket(new TarotCards.TarotCard(SuperchargedTarotsUA.Plugin.duality, 0), PlayerFarming.Instance);
            //TrinketManager.AddTrinket(new TarotCards.TarotCard(SuperchargedTarotsUA.Plugin.transference, 0), PlayerFarming.Instance);
            //TrinketManager.AddTrinket(new TarotCards.TarotCard(SuperchargedTarotsUA.Plugin.wardingBond, 0), PlayerFarming.Instance);
            //TrinketManager.AddTrinket(new TarotCards.TarotCard(SuperchargedTarotsUA.Plugin.deathContract, 0), PlayerFarming.Instance);
            //TrinketManager.AddTrinket(new TarotCards.TarotCard(SuperchargedTarotsUA.Plugin.ignite, 0), PlayerFarming.Instance);


            Plugin.Log = base.Logger;

            PluginPath = Path.GetDirectoryName(Info.Location);

            //SETUP: Config
            tarotSecondWindConfig = Config.Bind("SuperchargedTarotsUA", "Second Wind", true, "Enable this card?");
            tarotFinalStandConfig = Config.Bind("SuperchargedTarotsUA", "Final Stand", true, "Enable this card?");
            tarotReinforcementConfig = Config.Bind("SuperchargedTarotsUA", "Reinforcement", true, "Enable this card?");
            tarotBloodForBloodConfig = Config.Bind("SuperchargedTarotsUA", "Blood For Blood", true, "Enable this card?");

            tarotStaticElectricityConfig = Config.Bind("SuperchargedTarotsUA", "Static Electricity", true, "Enable this card?");
            tarotDualityConfig = Config.Bind("SuperchargedTarotsUA", "Duality", true, "Enable this card?");
            tarotTransferenceConfig = Config.Bind("SupercargedTarotsUA", "Transference", true, "Enable this card?");
            tarotWardingBondConfig = Config.Bind("SuperchargedTarotsUA", "Warding Bond", true, "Enable this card?");
            tarotDeathContractConfig = Config.Bind("SuperchargedTarotsUA", "Death Contract", true, "Enable this card?");
            tarotIgniteConfig = Config.Bind("SuperchargedTarotsUA", "Ignite", true, "Enable this card?");

            shouldAddCardConfig = Config.Bind("SuperchargedTarotsUA", "EnableCoopSoloEffects", false, "Set to true if you would like effects of coop cards to apply in solo mode.");

            //SETUP: Add Solo Cards
            if (tarotSecondWindConfig.Value == true)
            {
                secondWind = CustomTarotCardManager.Add(new Tarot_SecondWind());
            }

            if (tarotFinalStandConfig.Value == true)
            {
                finalStand = CustomTarotCardManager.Add(new Tarot_FinalStand());
            }

            if (tarotReinforcementConfig.Value == true)
            {
                reinforcement = CustomTarotCardManager.Add(new Tarot_Reinforcement());
            }
            
            if (tarotBloodForBloodConfig.Value == true)
            {
                bloodForBlood = CustomTarotCardManager.Add(new Tarot_BloodForBlood());
            }

            //SETUP: Add Coop Cards
            if (tarotStaticElectricityConfig.Value == true)
            {
                staticElectricity = CustomTarotCardManager.Add(new Tarot_StaticElectricity());
            }

            if (tarotDualityConfig.Value == true)
            {
                duality = CustomTarotCardManager.Add(new Tarot_Duality());
            }

            if (tarotTransferenceConfig.Value == true)
            {
                transference = CustomTarotCardManager.Add(new Tarot_Transference());
            }

            if (tarotWardingBondConfig.Value == true)
            {
                wardingBond = CustomTarotCardManager.Add(new Tarot_WardingBond());
            }

            if (tarotDeathContractConfig.Value == true)
            {
                deathContract = CustomTarotCardManager.Add(new Tarot_DeathContract());
            }

            if (tarotIgniteConfig.Value == true)
            {
                ignite = CustomTarotCardManager.Add(new Tarot_Ignite());
            }
            
            //SETUP: Add to card list
            coopCards.Add(staticElectricity);
            coopCards.Add(duality);
            coopCards.Add(transference);
            coopCards.Add(wardingBond);
            coopCards.Add(deathContract);
            coopCards.Add(ignite);

            soloCards.Add(secondWind);
            soloCards.Add(finalStand);
            soloCards.Add(reinforcement);
            soloCards.Add(bloodForBlood);

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