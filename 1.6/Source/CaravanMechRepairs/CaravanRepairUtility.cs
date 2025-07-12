using RimWorld.Planet;
using RimWorld;
using Verse;
using System.Collections.Generic;
using System.Linq;

namespace CaravanMechRepairs
{
    public static class CaravanRepairUtility
    {
        private static List<Pawn> MechsNeedingRepair = new List<Pawn>();

        public static void CheckRepair(Caravan caravan)
        {
            foreach (var pawn in caravan.pawns)
            {
                if (IsMechanitorValid(pawn))
                {
                    var repairIntervalTicks = (int)(1f / pawn.GetStatValue(StatDefOf.MechRepairSpeed) * 600f);

                    if (pawn.IsHashIntervalTick(repairIntervalTicks))
                        TryRepairToAnyMech(caravan);
                }
            }
        }

        private static bool IsMechanitorValid(Pawn pawn)
        {
            if (!pawn.RaceProps.Humanlike
                || !MechanitorUtility.IsMechanitor(pawn)
                || !pawn.IsColonist
                || pawn.WorkTypeIsDisabled(WorkTypeDefOf.Crafting)
                || pawn.DeadOrDowned
                || pawn.InMentalState)
                return false;

            return true;
        }

        public static void TryRepairToAnyMech(Caravan caravan)
        {
            FindMechsNeedingRepair(caravan, MechsNeedingRepair);

            if (!MechsNeedingRepair.Any())
                return;

            var mechanitor = FindMechanitor(caravan);
            var mechanoid = MechsNeedingRepair.First();

            if (mechanitor != null)
            {
                mechanoid.needs.energy.CurLevel -= mechanoid.GetStatValue(StatDefOf.MechEnergyLossPerHP);
                MechRepairUtility.RepairTick(mechanoid, 1);
                MechsNeedingRepair.Clear();
            }
        }

        private static void FindMechsNeedingRepair(Caravan caravan, List<Pawn> mechsNeedingRepair)
        {
            mechsNeedingRepair.Clear();

            foreach (var pawn in caravan.pawns)
            {
                if (pawn.IsColonyMech && MechRepairUtility.CanRepair(pawn))
                    mechsNeedingRepair.Add(pawn);
            }
        }

        private static Pawn FindMechanitor(Caravan caravan)
        {
            foreach (var pawn in caravan.pawns)
            {
                if (MechanitorUtility.IsMechanitor(pawn))
                    return pawn;
            }

            return null;
        }
    }
}
