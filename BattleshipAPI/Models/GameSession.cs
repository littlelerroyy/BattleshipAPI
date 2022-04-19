namespace BattleshipAPI.Models
{
    public class GameSession
    {
        public uint GridSizeX { get; set; }
        public uint GridSizeY { get; set; }

        public Player Player1 { get; set; }

        public Player Player2 { get; set; }

        public bool ValidateAndApplyGridSize(uint PosX, uint PosY) 
        {
            if (PosX < 3 || PosY < 3) 
            { 
            return false;
            }
            this.GridSizeX = PosX;
            this.GridSizeY = PosY;
            return true;
        }
    }
}
