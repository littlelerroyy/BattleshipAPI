namespace BattleshipAPI.Models
{
    public class LargeShip : Ship
    {
        public LargeShip(uint _pX1, uint _pY1, uint _pX2, uint _pY2, uint _pX3, uint _pY3)
        {
            Locations = new List<Location>()
            {
                new Location { xAxis = _pX1, yAxis = _pY1},
                new Location { xAxis = _pX2, yAxis = _pY2},
                new Location { xAxis = _pX3, yAxis = _pY3}
            };
        }
    }
}
