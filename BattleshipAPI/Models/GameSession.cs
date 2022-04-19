namespace BattleshipAPI.Models
{
    public class GameSession
    {
        public uint GridSizeX { get; set; }
        public uint GridSizeY { get; set; }

        public Player Player1 { get; set; }

        public Player Player2 { get; set; }

        public bool ValidateAndApplyGridSize(uint _GridSizeX, uint _GridSizeY)
        {

            if (GridSizeX < 3 || GridSizeY < 3)
            {
                return false;
            }

            this.GridSizeX = _GridSizeX;
            this.GridSizeY = _GridSizeY;
            return true;
        }
    }
}
