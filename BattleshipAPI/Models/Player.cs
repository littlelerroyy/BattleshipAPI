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
    }
}
