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


        
    }
}
