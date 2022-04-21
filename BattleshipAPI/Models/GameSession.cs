namespace BattleshipAPI.Models
{
    public class GameSession
    {
        public uint GridSizeX { get; set; }
        public uint GridSizeY { get; set; }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        private const int GridMinX = 3;
        private const int GridMinY = 3;

        public bool ValidateAndApplyGridSize(uint _GridSizeX, uint _GridSizeY)
        {
            if (_GridSizeX < GridMinX || _GridSizeY < GridMinY)
            {
                return false;
            }

            this.GridSizeX = _GridSizeX;
            this.GridSizeY = _GridSizeY;

            return true;
        }

        public bool CheckCoordinatesAreInBounds(uint PosX, uint PosY)
        {
            if (PosX > GridSizeX || PosY > GridSizeY)
            { 
                return false;
            }
            return true;
        }
    }
}
