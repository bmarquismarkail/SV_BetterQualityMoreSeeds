using Netcode;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Object = StardewValley.Object;

namespace BetterQualityMoreSeeds.Framework
{
    class MethodPatch
    {
        private static IMonitor Monitor;

        //Call Method from Entry Class
        public static void Initialize(IMonitor monitor)
        {
            Monitor = monitor;
        }

        public static bool PerformObjectDropInAction(Object __instance, Item dropInItem, bool probe, Farmer who, ref bool __result)
        {
            try
            {
                if (!__instance.name.Equals("Seed Maker")) return true;
                if (__instance.heldObject.Value != null)
                {
                    __result = false;
                    return false;
                }

                Object object1 = dropInItem as Object;
                if (object1 != null && object1.ParentSheetIndex == 433)
                {
                    __result =  false;
                    return false;
                }

                Dictionary<int, string> dictionary = Game1.temporaryContent.Load<Dictionary<int, string>>("Data\\Crops");
                bool flag = false;
                int parentSheetIndex = -1;
                foreach (KeyValuePair<int, string> keyValuePair in dictionary)
                {
                    if (Convert.ToInt32(keyValuePair.Value.Split('/')[3]) == object1.ParentSheetIndex)
                    {
                        flag = true;
                        parentSheetIndex = keyValuePair.Key;
                        break;
                    }
                }
                if (flag)
                {
                    Random random = new Random((int)Game1.stats.DaysPlayed + (int)Game1.uniqueIDForThisGame / 2 + (int)__instance.TileLocation.X + (int)__instance.TileLocation.Y * 77 + Game1.timeOfDay);
                    __instance.heldObject.Value = new Object(parentSheetIndex, random.Next(1, 4), false, -1, 0);
                    if (!probe)
                    {
                        if (random.NextDouble() < 0.005 && parentSheetIndex != 499)
                            __instance.heldObject.Value = new Object(499, 1, false, -1, 0);
                        else if (random.NextDouble() < 0.02)
                            __instance.heldObject.Value = new Object(770, random.Next(1, 5), false, -1, 0);
                        __instance.MinutesUntilReady = 20;
                        who.currentLocation.playSound("Ship", NetAudio.SoundContext.Default);
                        DelayedAction.playSoundAfterDelay("dirtyHit", 250, (GameLocation)null, -1);
                    }
                    __instance.heldObject.Value.Stack += (object1.Quality >=4? object1.Quality-1: object1.Quality);
                    __result = true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return true;
            }
        }
    }
}
