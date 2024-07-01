using HarmonyLib;
using System.Reflection;
using Verse;

namespace CaravanMechRepairs
{
    public class CaravanMechRepairs : Mod
    {
        public CaravanMechRepairs(ModContentPack content) : base(content)
        {
            var harmony = new Harmony("keirloire.caravanmechrepairs");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}
