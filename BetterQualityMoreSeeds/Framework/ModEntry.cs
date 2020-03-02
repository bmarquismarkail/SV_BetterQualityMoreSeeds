using BetterQualityMoreSeeds.Framework;
using Harmony;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Reflection;
using SObject = StardewValley.Object;

namespace BetterQualityMoreSeeds
{
    /// <summary>The mod entry point.</summary>
    public class ModEntry : Mod
    {

        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            //Harmony instance for patching main game code
            var harmony = HarmonyInstance.Create(this.ModManifest.UniqueID);


            TryToCheckAtPatch.Initialize(this.Monitor);
            SeedMakerMachinePatch.Initialize(this.Monitor);

            harmony.Patch(
                original: AccessTools.Method(typeof(Game1), nameof(Game1.tryToCheckAt)),
                prefix: new HarmonyMethod(typeof(TryToCheckAtPatch), nameof(TryToCheckAtPatch.TryToCheckAt_PreFix)),
                postfix: new HarmonyMethod(typeof(TryToCheckAtPatch), nameof(TryToCheckAtPatch.TryToCheckAt_PostFix))
                );

            Assembly assembly = typeof(Pathoschild.Stardew.Automate.IMachine).Assembly;
            Type SeedMakerMachine = assembly.GetType("Pathoschild.Stardew.Automate.Framework.Machines.Objects.SeedMakerMachine");
            harmony.Patch(
                original: AccessTools.Method(SeedMakerMachine, SeedMakerMachine.GetMethod("SetInput", System.Reflection.BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static).Name),
                prefix: new HarmonyMethod(typeof(SeedMakerMachinePatch), nameof(SeedMakerMachinePatch.SetInputPrefix))
                );
        }


        /*********
        ** Private methods
        *********/


    }
}
