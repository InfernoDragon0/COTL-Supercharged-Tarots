using COTL_API.CustomTarotCard;
using HarmonyLib;
using Lamb.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SuperchargedTarots.Patches
{
    [HarmonyPatch]
    internal class AmmoPatch
    {
        [HarmonyPatch(typeof(PlayerSpells), nameof(PlayerSpells.AmmoCost), MethodType.Getter)]
        [HarmonyPrefix]
        public static bool PlayerSpells_AmmoCost(PlayerSpells __instance, ref int __result)
        {
            if (TrinketManager.HasTrinket(Plugin.ammoX))
            {
                __result = 1;
                return false;
            }
            return true;
        }

        [HarmonyPatch(typeof(BlunderAmmo), nameof(BlunderAmmo.UseAmmo))]
        [HarmonyPrefix]
        public static bool BlunderAmmo_UseAmmo(BlunderAmmo __instance, ref bool __result)
        {
            if (TrinketManager.HasTrinket(Plugin.blunderBusterX))
            {
                __result = true;
                return false;
            }
            return true;
        }

        [HarmonyPatch(typeof(Interaction_WeaponSelectionPodium), nameof(Interaction_WeaponSelectionPodium.SetWeapon))]
        [HarmonyPrefix]
        public static bool Interaction_WeaponSelectionPodium_SetWeapon(Interaction_WeaponSelectionPodium __instance)
        {
            if (TrinketManager.HasTrinket(Plugin.gunslingerX))
            {
                __instance.TypeOfWeapon = EquipmentType.Blunderbuss;
                __instance.PrevEquipment = __instance.TypeOfWeapon;
                __instance.WeaponLevel = DataManager.Instance.CurrentRunWeaponLevel + 1;
                return false;
            }
            return true;
        }

        [HarmonyPatch(typeof(PlayerWeapon), nameof(PlayerWeapon.DoBlunderbussAttack))]
        [HarmonyPostfix]
        public static void PlayerWeapon_DoBlunderbussAttack(PlayerWeapon __instance, Health.AttackTypes attackType, ref bool __result)
        {
            if (TrinketManager.HasTrinket(Plugin.multishotX)) {
                Plugin.Log.LogInfo("shooting 4 directions");
                //the first shot will be forward, then for loop 3 times for facing angle 90 180 270
                for (int i = 0; i < 3; i++) {
                    float adjustedFacingAngle = __instance.state.facingAngle + ((i + 1) * 90);

                    Swipe component = UnityEngine.Object.Instantiate<Swipe>(__instance.blunderAmmo.swipe).GetComponent<Swipe>();
                    Vector3 Position = __instance.transform.position + new Vector3(__instance.CurrentWeapon.WeaponData.Combos[__instance.CurrentCombo].RangeRadius * __instance.CurrentWeapon.RangeMultiplier * Mathf.Cos(__instance.state.facingAngle * ((float) Math.PI / 180f)), __instance.CurrentWeapon.WeaponData.Combos[__instance.CurrentCombo].RangeRadius * __instance.CurrentWeapon.RangeMultiplier * Mathf.Sin(__instance.state.facingAngle * ((float) Math.PI / 180f)), -0.5f);
                    Health.AttackFlags AttackFlags = 0;
                    __instance.additionalBlunderbussFlags = AttackFlags;
                    component.destroyAfterDuration = true;
                    component.Init(Position, adjustedFacingAngle, (Health.Team) ((__instance.overrideTeam != null) ? (int)__instance.overrideTeam : (int) __instance.health.team), __instance.health, new Action<Health, Health.AttackTypes>(__instance.BlunderBussEnemyCallBack), __instance.CurrentWeapon.WeaponData.Combos[__instance.CurrentCombo].RangeRadius * __instance.CurrentWeapon.RangeMultiplier, 0.0f, __instance.GetCritChance(), attackType, AttackFlags);
                    component.gameObject.transform.localScale = Vector3.one;
                }
                
            }

            if (TrinketManager.HasTrinket(Plugin.gunslingerX))
            {
                __result = false;
            }
        }
    }
}
