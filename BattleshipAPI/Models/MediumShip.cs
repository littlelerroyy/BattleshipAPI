namespace BattleshipAPI.Models
{
    public class MediumShip : Ship
    {
        public MediumShip(uint _pX1, uint _pY1, uint _pX2, uint _pY2) 
        {
            Locations = new List<Location>()
            {
                new Location { xAxis = _pX1, yAxis = _pY1},
                new Location { xAxis = _pX2, yAxis = _pY2}
            };
        }
    }
}
