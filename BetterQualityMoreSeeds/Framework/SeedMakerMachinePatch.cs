using Pathoschild.Stardew.Automate;
using StardewModdingAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BetterQualityMoreSeeds.Framework
{
    class SeedMakerMachinePatch
    {
        private static IMonitor Monitor;
        private static Type MachineType;

        //Call Method from Entry Class
        public static void Initialize(IMonitor monitor, Type type)
        {
            Monitor = monitor;
            MachineType = type;
        }

        internal static void SetInputPrefix(object __instance , IStorage input , out KeyValuePair<object, object> __state)
        {
            __state = new KeyValuePair<object, object>(null, null);

            IConsumable consumable;

            MethodInfo IsValidCrop = MachineType.GetMethod("IsValidCrop");
            Func<ITrackedStack, bool> newFunc = x => IsValidCrop.Invoke(__instance);
           input.TryGetIngredient(new Func<ITrackedStack, bool>(), 1, out consumable);
        }
    }
}
