namespace BattleshipAPI.Models
{
    public class GameSession
    {
        public uint GridSizeX { get; set; }
        public uint GridSizeY { get; set; }

        public Player Player1 { get; set; }

        public Player Player2 { get; set; }

    }
}
