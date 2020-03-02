using Harmony;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using System;
using System.Collections.Generic;
using SObject = StardewValley.Object;

namespace BetterQualityMoreSeeds
{
    /// <summary>The mod entry point.</summary>
    public class ModEntry : Mod
    {
        //SerializableDictionary<GameLocation, List<SObject>> allChests;

        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {

            var harmony = HarmonyInstance.Create(this.ModManifest.UniqueID);

            harmony.Patch(
                original: AccessTools.Method(typeof(SObject), nameof(StardewValley.Object.performObjectDropInAction)),
                postfix: new HarmonyMethod(typeof(Framework.MethodPatch), nameof(Framework.MethodPatch.PerformObjectDropInAction))
                );

        }

        /*********
        ** Private methods
        *********/


    }
}
