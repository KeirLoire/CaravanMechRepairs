using HarmonyLib;
using RimWorld.Planet;

namespace CaravanMechRepairs.Patches
{
    [HarmonyPatch(typeof(CaravanTendUtility), nameof(CaravanTendUtility.CheckTend))]
    public static class CaravanTendUtilityPatch
    {
        [HarmonyPostfix]
        public static void Postfix(Caravan caravan)
        {
            CaravanRepairUtility.CheckRepair(caravan);
        }
        
    }
}
