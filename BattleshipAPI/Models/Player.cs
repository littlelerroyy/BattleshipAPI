namespace BattleshipAPI.Models
{
    public class Player
    {
        public string? Name { get; set; }
        public List<Ship>? Ships { get; set; }
        public List<Location>? LocationsStriked { get; set; }
    }
}
