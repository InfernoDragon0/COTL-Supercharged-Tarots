using COTL_API.CustomTarotCard;
using HarmonyLib;
using Lamb.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperchargedTarots.Patches
{
    [HarmonyPatch]
    internal class PerfectCastPatch
    {
        [HarmonyPatch(typeof(UITarotChoiceOverlayController), nameof(UITarotChoiceOverlayController.Show))]
        [HarmonyPrefix]
        public static bool UITarotChoiceOverlayController_Show(UITarotChoiceOverlayController __instance,
                TarotCards.TarotCard card1, TarotCards.TarotCard card2, bool instant)
        {

            Plugin.Log.LogInfo("Removing " + card1.CardType.ToString() + " and " + card2.CardType.ToString());
            
            __instance._card1 = GetCard();
            __instance._card2 = GetCard(false);
            __instance._uiCard1.Play(__instance._card1);
            __instance._uiCard2.Play(__instance._card2);

            __instance.OnTarotCardSelected += card =>
            {
                Plugin.Log.LogInfo("keeping " + card.CardType.ToString());
            };

            __instance.Show(instant);
            return false;
        }
        private static TarotCards.TarotCard GetCard(bool customFirst = true)
        {

            TarotCards.TarotCard card = null;
            bool alreadyTaken = false;
            if (customFirst)
            {
                TarotCards.Card cardData = CustomTarotCardManager.CustomTarotCardList.Keys.ElementAt(UnityEngine.Random.Range(0, CustomTarotCardManager.CustomTarotCardList.Count));

                card = new TarotCards.TarotCard(cardData, 0);
            }

            if (DataManager.Instance.PlayerRunTrinketsContains(card, PlayerFarming.Instance))
            {
                Plugin.Log.LogInfo("custom already taken");
                alreadyTaken = true;
            }

            if (PlayerFarming.playersCount > 1)
            {
                if (DataManager.Instance.PlayerRunTrinketsContains(card, PlayerFarming.players[1]))
                {
                    Plugin.Log.LogInfo("custom already taken by other player");
                    alreadyTaken = true;
                }
            }

            if (alreadyTaken || card == null) //if already have the custom card, then draw a vanilla card
            {
                if (!DataManager.Instance.FirstTarot && TarotCards.DrawRandomCard(PlayerFarming.Instance) != null)
                {
                    DataManager.Instance.FirstTarot = true;
                    card = new TarotCards.TarotCard(TarotCards.Card.Lovers1, 0);
                }
                else
                    card = TarotCards.DrawRandomCard(PlayerFarming.Instance);
            }

            /*if (card != null && Plugin.shouldAddCardConfig.Value) {
                DataManager.Instance.PlayerRunTrinkets.Add(card);
                Plugin.Log.LogInfo("Adding (another) " + card.CardType.ToString());
            }*/

            return card;
        }

        [HarmonyPatch(typeof(PlayerFarming), nameof(PlayerFarming.CorrectProjectileChargeRelease))]
        [HarmonyPrefix]
        public static bool PlayerFarming_CorrectProjectileChargeRelease(ref bool __result)
        {
            if (TrinketManager.HasTrinket(Plugin.perfectCurseX))
            {
                Plugin.Log.LogInfo("Performing perfect cast");
                __result = true;
                return false;
            }

            return true;
        }

        
    }
}
