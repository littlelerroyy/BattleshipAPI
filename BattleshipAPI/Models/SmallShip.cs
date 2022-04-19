namespace BattleshipAPI.Models
{
    public class SmallShip: Ship
    {
        public SmallShip(uint _pX1, uint _pY1) 
        {
            Locations = new List<Location>()
            {
                new Location { xAxis = _pX1, yAxis = _pY1}                
            };
        }
    }
}
