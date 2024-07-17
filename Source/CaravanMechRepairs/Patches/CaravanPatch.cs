﻿using HarmonyLib;
using RimWorld.Planet;
using System.Reflection;
using Verse;

namespace CaravanMechRepairs.Patches
{
    [HarmonyPatch]
    public static class CaravanPatch
    {
        public static MethodBase TargetMethod()
        {
            var type = AccessTools.TypeByName("RimWorld.Planet.Caravan");

            return AccessTools.Method(type, "Tick");
        }

        public static void Postfix(ref Caravan __instance)
        {
            if (ModsConfig.BiotechActive)
                CaravanRepairUtility.CheckRepair(__instance);
        }
    }
}