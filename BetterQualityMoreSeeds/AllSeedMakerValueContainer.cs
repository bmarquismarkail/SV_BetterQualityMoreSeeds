namespace SB_BQMS
{
    public class AllSeedMakerValueContainer
    {
        public StardewValley.Object droppedObject;
        public StardewValley.GameLocation location;
        public bool hasBeenChecked = false;

        public AllSeedMakerValueContainer(StardewValley.Object firstObject, StardewValley.GameLocation whereat, bool isChecked)
        {
            droppedObject = firstObject;
            location = whereat;
            hasBeenChecked = isChecked;
        }
    }
}
