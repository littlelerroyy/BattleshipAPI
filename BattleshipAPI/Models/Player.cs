namespace BattleshipAPI.Models
{
    public class Player
    {
        public string? Name { get; set; }
        public List<Ship>? Ships { get; set; }
        public List<Location>? LocationsStriked { get; set; }

        public bool LocationisFree(uint PosX, uint PosY)
        {
            return !Ships.Any(Ship => Ship.Locations.Any(Location => Location.xAxis == PosX && Location.yAxis == PosY));
        }

        public bool ListofLocationsIsFree(List<Location> LocationList)
        {
            foreach (var Location in LocationList) 
            {
                if (!LocationisFree(Location.xAxis, Location.yAxis)) 
                {
                    return false;
                }
            }

            return true;
        }

        public Ship? StrikePlayer(uint PosX, uint PosY)
        {

            //If a ship has been found apply the strike on the ships location.
            var Ship = this.Ships.Where(Ship => Ship.Locations.Any(Location => Location.xAxis == PosX && Location.yAxis == PosY)).FirstOrDefault();

            if (Ship != null)
            {
                Ship.AddHitMarkerToShipLocation(PosX, PosY);
            }

            return Ship;
        }
    }
}
