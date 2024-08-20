using COTL_API.CustomTarotCard;
using HarmonyLib;
using Lamb.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UIElements;
using static MenuAdController;

namespace SuperchargedTarotsUA.Patches
{
    [HarmonyPatch]
    internal class PerfectCastPatch
    {
        [HarmonyPatch(typeof(UITarotChoiceOverlayController), nameof(UITarotChoiceOverlayController.Show))]
        [HarmonyPrefix]
        public static bool UITarotChoiceOverlayController_Show(UITarotChoiceOverlayController __instance,
                TarotCards.TarotCard card1, TarotCards.TarotCard card2, bool instant)
        {
            if (BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("InfernoDragon0.cotl.SuperchargedTarots"))
            {
                //random 0 to 1
                if (UnityEngine.Random.value > 0.5f)
                {
                    Plugin.Log.LogInfo("Let SuperchargedTarots decide the cards.");
                    return true;
                }
                Plugin.Log.LogInfo("We decide our own cards.");
            }
            
            __instance._card1 = GetCard(); //new TarotCards.TarotCard(Plugin.bloodForBlood, 0);
            __instance._card2 = GetCard(false);
            __instance._uiCard1.Play(__instance._card1);
            __instance._uiCard2.Play(__instance._card2);

            __instance.OnTarotCardSelected += card =>
            {
                /*DataManager.Instance.PlayerRunTrinkets.Remove(card == __instance._card1 ? __instance._card2 : __instance._card1);*/
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
                TarotCards.Card cardData = Plugin.soloCards[UnityEngine.Random.Range(0, Plugin.soloCards.Count)];

                if (CoopManager.CoopActive || Plugin.shouldAddCardConfig.Value)
                {
                    //get coop cards, 60% chance
                    if (UnityEngine.Random.value > 0.4f)
                    {
                        Plugin.Log.LogInfo("Giving Supercharged UA CO OP Card");
                        cardData = Plugin.coopCards[UnityEngine.Random.Range(0, Plugin.coopCards.Count)];
                    }
                    else
                    {
                        Plugin.Log.LogInfo("Giving Supercharged UA SOLO Card");
                    }
                    
                }

                card = new TarotCards.TarotCard(cardData, 0);
                Plugin.Log.LogInfo("Giving Supercharged UA Card " + cardData);
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
                card = TarotCards.DrawRandomCard(PlayerFarming.Instance);
            }

            /*if (card != null && Plugin.shouldAddCardConfig.Value) {
                DataManager.Instance.PlayerRunTrinketsAdd(card);
                Plugin.Log.LogInfo("Adding (another) " + card.CardType.ToString());
            }*/
            Plugin.Log.LogInfo("Giving Supercharged UA Card second " + card.CardType);
            return card;
        }

        /** Blood for Blood: Whenever you kill an enemy, gain 1 Spirit Heart **/
        [HarmonyPatch(typeof(Health), nameof(Health.HandleDamageModifierBuff))]
        [HarmonyPrefix]
        public static bool Health_HandleDamageModifierBuff(Health __instance)
        {
            if (TrinketManager.HasTrinket(Plugin.bloodForBlood))
            {
                PlayerFarming.Instance.health.TotalSpiritHearts++;
                /*PlayerFarming.Instance.health.SpiritHearts++;*/
                Plugin.Log.LogInfo("Blood for Blood: Gained 1 Empty Spirit Heart");
            }

            if (TrinketManager.HasTrinket(Plugin.duality))
            {
                Plugin.Log.LogInfo("Heal both players");
                PlayerFarming.Instance.health.Heal(1);
                
                if (PlayerFarming.playersCount > 1)
                {
                    PlayerFarming.players[1].health.Heal(1);
                }
            }

            return true;
        }

        /** Death Contract: Prefix Check Health **/
        [HarmonyPatch(typeof(HealthPlayer), nameof(HealthPlayer.DealDamage))]
        [HarmonyPrefix]
        public static bool HealthPlayer_DealDamage_Prefix(HealthPlayer __instance, out float __state)
        {
            __state = __instance.HP;
            if (TrinketManager.HasTrinket(Plugin.deathContract))
            {
                Plugin.Log.LogInfo("Death Contract: Pre Damage HP " + __state);
            }
            return true;


        }

        /** Reinforcement: Whenever you take damage, gain 1 Spirit Heart **/
        /** Warding Bond: When near each other, negate damage taken at a 70% chance **/
        [HarmonyPatch(typeof(HealthPlayer), nameof(HealthPlayer.DealDamage))]
        [HarmonyPostfix]
        public static void HealthPlayer_DealDamage(HealthPlayer __instance, ref bool __result, float __state)
        {
            if (TrinketManager.HasTrinket(Plugin.reinforcement))
            {
                if (__result)
                {
                    if (PlayerFarming.Instance.playerRelic != null)
                    {
                        Plugin.Log.LogInfo("Has Relic");
                        var sequence = EquipmentManager.GetRelicData(RelicType.SpawnCombatFollower).VFXData.PlayNewSequence(PlayerFarming.Instance.transform, new Transform[1]
                        {
                          PlayerFarming.Instance.transform
                        }, onlyImpact: true);
                        sequence.OnImpact += new Action<VFXObject, int>(PlayerFarming.Instance.playerRelic.SpawnFriendlyEnemy);
                    }
                    else
                    {
                        Plugin.Log.LogInfo("No Relic");
                    }

                    /*Plugin.Log.LogInfo("Reinforcement: Spawn a combat follower");
                    var follower = FollowerManager.GetRandomNonLockedFollower();
                    Plugin.Log.LogInfo("Reinforcement: Spawn a combat follower 2");
                    FollowerManager.SpawnedFollower spawnedFollower = FollowerManager.SpawnCopyFollower(FollowerManager.CombatFollowerPrefab, follower.Info._info, PlayerFarming.Instance.transform.position, PlayerFarming.Instance.transform.parent, PlayerFarming.Location);
                    Plugin.Log.LogInfo("Reinforcement: Spawn a combat follower 3");
                    Health friendlyEnemyHealth = spawnedFollower.Follower.GetComponent<Health>();
                    Plugin.Log.LogInfo("Reinforcement: Spawn a combat follower 4");
                    friendlyEnemyHealth.enabled = false;
                    friendlyEnemyHealth.team = Health.Team.PlayerTeam;
                    friendlyEnemyHealth.HP = friendlyEnemyHealth.totalHP = 15f;
                    friendlyEnemyHealth.ImmuneToPlayer = true;
                    friendlyEnemyHealth.ImmuneToTraps = false;
                    friendlyEnemyHealth.transform.position = PlayerFarming.Instance.transform.position;
                    friendlyEnemyHealth.gameObject.SetActive(false);
                    friendlyEnemyHealth.enabled = true;
                    Plugin.Log.LogInfo("Reinforcement: Spawn a combat follower 5");

                    EnemySwordsman component = friendlyEnemyHealth.GetComponent<EnemySwordsman>();
                    Plugin.Log.LogInfo("Reinforcement: Spawn a combat follower 6");
                    component.SeperateObject = true;
                    component.gameObject.SetActive(true);
                    component.Damage = 2f;
                    component.health.team = Health.Team.PlayerTeam;
                    component.FollowPlayer = true;
                    component.enabled = true;
                    component.VisionRange = int.MaxValue;
                    Plugin.Log.LogInfo("Reinforcement: Spawn a combat follower 7");*/
                }
            }

            if (TrinketManager.HasTrinket(Plugin.transference))
            {
                if (__result)
                {
                    Plugin.Log.LogInfo("Other player explodes.");
                    if (PlayerFarming.playersCount > 1)
                    {
                        if (PlayerFarming.players[0].health == __instance)
                        {
                            Explosion.CreateExplosion(PlayerFarming.players[1].transform.position, Health.Team.PlayerTeam, PlayerFarming.players[1].health, 12f, 4f);
                        }
                        else
                        {
                            Explosion.CreateExplosion(PlayerFarming.players[0].transform.position, Health.Team.PlayerTeam, PlayerFarming.players[0].health, 12f, 4f);
                        }
                    }
                    else
                    {
                        if (Plugin.shouldAddCardConfig.Value)
                        {
                            Explosion.CreateExplosion(PlayerFarming.players[0].transform.position, Health.Team.PlayerTeam, PlayerFarming.players[0].health, 4f, 2f);
                        }
                        Plugin.Log.LogInfo("Transference: Not multiplayer");
                    }
                }
            }

           

            if (TrinketManager.HasTrinket(Plugin.deathContract))
            {
                if (__result)
                {
                    if (__instance.HP < __state)
                    {
                        if (PlayerFarming.playersCount > 1)
                        {
                            if (PlayerFarming.players[0].health == __instance)
                            {
                                PlayerFarming.players[1].health.BlueHearts++;
                                Plugin.Log.LogInfo("Death Contract: Give P2 Health");
                            }
                            else
                            {
                                PlayerFarming.players[0].health.BlueHearts++;
                                Plugin.Log.LogInfo("Death Contract: Give P1 Health");
                            }
                        }
                        else
                        {
                            if (Plugin.shouldAddCardConfig.Value)
                            {
                                PlayerFarming.players[0].health.BlueHearts++;
                            }
                            Plugin.Log.LogInfo("Death Contract: Not multiplayer");
                        }
                        Plugin.Log.LogInfo("Death Contract: Post Damage HP Loss " + __state);
                    }
                    else
                    {
                        Plugin.Log.LogInfo("Death Contract: No HP Loss " + __state);
                    }
                }
            }

        }
        [HarmonyPatch(typeof(TrinketManager), nameof(TrinketManager.CanNegateDamage))]
        [HarmonyPrefix]
        public static bool TrinketManager_CanNegateDamage(TrinketManager __instance, ref bool __result)
        {
            if (TrinketManager.HasTrinket(Plugin.wardingBond))
            {
                __result = true;
                Plugin.Log.LogInfo("Warding Bond: Negate Damage possible");
                return false;
            }
            return true;
        }


            [HarmonyPatch(typeof(Health), nameof(Health.ChanceToNegateDamage))]
        [HarmonyPrefix]
        public static bool Health_ChanceToNegateDamage(Health __instance, ref bool __result)
        {
            if (TrinketManager.HasTrinket(Plugin.wardingBond))
            {
                var chance = UnityEngine.Random.value > 0.3f;
                if (PlayerFarming.playersCount > 1)
                {
                    var check = (double)Vector3.Distance(PlayerFarming.players[0].transform.position, PlayerFarming.players[1].transform.position) >= 3.0;
                    if (check)
                    {
                        Plugin.Log.LogInfo("Warding Bond: Not near each other or single player - " + __result);
                    }
                    else if (chance)
                    {
                        __result = true;
                        Plugin.Log.LogInfo("Warding Bond: Near each other, negate");
                    }
                    else
                    {
                        Plugin.Log.LogInfo("Warding Bond: no negate");
                    }
                }
                else
                {
                    if (chance && Plugin.shouldAddCardConfig.Value)
                    {
                        __result = true;
                        Plugin.Log.LogInfo("Warding Bond: Negate Damage Solo");

                    }

                }
                return false;
            }
            return true;
        }


        /** Duality: Whenever you deal damage, heal the other player for half a heart **/
       /* [HarmonyPatch(typeof(Health), nameof(Health.DealDamage))]*/
        [HarmonyPatch(typeof(PlayerWeapon), nameof(PlayerWeapon.DoAttackRoutine), MethodType.Enumerator)]
        [HarmonyPostfix]
        public static void PlayerWeapon_DoAttackRoutine(PlayerWeapon __instance/*, float Damage*/)
        {

            if (!InputManager.Gameplay.GetAttackButtonDown() && !InputManager.Gameplay.GetHeavyAttackButtonDown()) return;

            if (TrinketManager.HasTrinket(Plugin.staticElectricity))
            {
                var check = PlayerFarming.playersCount <= 1 || (double)Vector3.Distance(PlayerFarming.players[0].transform.position, PlayerFarming.players[1].transform.position) >= 3.0;

                if (check)
                {
                    if (PlayerFarming.playersCount <= 1 && Plugin.shouldAddCardConfig.Value)
                    {
                        new LightningStrikeAbility(1).Play(PlayerFarming.players[0].gameObject, Health.Team.Team2, 12, PlayerFarming.players[0], true);
                    }
                    Plugin.Log.LogInfo("Static Electricity: Not near each other or single player");
                }
                else
                {
                    Plugin.Log.LogInfo("Static Electricity: creating lightning strikes");
                    new LightningStrikeAbility(1).Play(PlayerFarming.players[0].gameObject, Health.Team.Team2, 12, PlayerFarming.players[0], true);
                    new LightningStrikeAbility(1).Play(PlayerFarming.players[1].gameObject, Health.Team.Team2, 12, PlayerFarming.players[1], true);
                }
            }

            if (TrinketManager.HasTrinket(Plugin.ignite))
            {
                /*if (Damage > 0.0 && __instance.team == Health.Team.Team2)
                {*/
                if (PlayerFarming.playersCount > 1)
                {
                    if (Mathf.Abs(PlayerFarming.players[0].playerWeapon.TimeOfAttack - PlayerFarming.players[1].playerWeapon.TimeOfAttack) < 0.20)
                    {
                        Explosion.CreateExplosion(PlayerFarming.players[0].transform.position, Health.Team.PlayerTeam, PlayerFarming.players[0].health, 3f);
                        Explosion.CreateExplosion(PlayerFarming.players[1].transform.position, Health.Team.PlayerTeam, PlayerFarming.players[1].health, 3f);
                        Plugin.Log.LogInfo("Ignite: creating explosion");
                    }
                        
                }
                else
                {
                    if (Plugin.shouldAddCardConfig.Value)
                    {
                        Explosion.CreateExplosion(PlayerFarming.players[0].transform.position, Health.Team.PlayerTeam, PlayerFarming.players[0].health, 3f);
                    }

                    Plugin.Log.LogInfo("Ignite: not multiplayer");
                }
                /*}*/
            }
        }


    }
}
