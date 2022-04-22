namespace BattleshipAPI.Models
{
    public class Location
    {
        public uint xAxis { get; set; }
        public uint yAxis { get; set; }
        public bool BeenHit { get; set; }

        public static bool IsPositionsConsolidated(List<Location> LocationList)
        {
            //Grab any of the list x and y axis. We need to see if they are all the same.
            uint LocationXUnit = LocationList[0].xAxis;
            uint LocationYUnit = LocationList[0].yAxis;

            //If all y axis is all the same its horizontal if x, vertical.
            bool IsHorizontal = LocationList.TrueForAll(x => x.yAxis == LocationYUnit);
            bool IsVertical = LocationList.TrueForAll(x => x.xAxis == LocationXUnit);

            //If They are neither being detected Horizontal or Vertical they must be scattered.
            if (!IsHorizontal && !IsVertical)
            {
                return false;
            }

            //Lets see if the units are scattered on the same axis. Lets simply check if the two values are next to eachother.
            LocationList.OrderBy(x => x.xAxis);

            if (IsHorizontal)
            {
                LocationList.OrderBy(x => x.xAxis);

                for (int i = 1; i < LocationList.Count; i++)
                {
                    if (LocationList[i - 1].xAxis != LocationList[i].xAxis - 1)
                    {
                        return false;
                    }
                }
            }

            if (IsVertical)
            {
                LocationList.OrderBy(x => x.yAxis);

                for (int i = 1; i < LocationList.Count; i++)
                {
                    if (LocationList[i - 1].yAxis != LocationList[i].yAxis - 1)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }

}
