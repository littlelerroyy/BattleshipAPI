using Microsoft.AspNetCore.Mvc;
using BattleshipAPI.Models;

namespace BattleshipAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        [Route("/[controller]/[action]")]
        [HttpGet]
        public IActionResult SetupGame([FromServices] GameSession GameSession, string SizeX, string SizeY)
        {
            //Make sure the sizing that comes back is a positive number
            //Make Sure the sizing is greater than 3
            try
            {
                GameSession.GridSizeX = uint.Parse(SizeX);
                GameSession.GridSizeY = uint.Parse(SizeY);
                if (GameSession.GridSizeX < 3 || GameSession.GridSizeY < 3)
                {
                    throw new Exception();
                }
            }
            catch
            {
                return BadRequest(new { Error = "Please Enter Positive Numbers Only that are 3 or greater." });
            }

            //Create new Player and Ships
            GameSession.Player1 = new Player();
            GameSession.Player1.Ships = new List<Ship>();
            GameSession.Player1.LocationsStriked = new List<Location>();

            return Ok();
        }

        [Route("/[controller]/[action]")]
        [HttpGet]
        public IActionResult AddSmallShip([FromServices]GameSession GameSession, uint PosX, uint PosY)
        {
            var Player = GameSession.Player1;

            //If Overlapping Return Error
            if (!Player.LocationisFree(PosX, PosY)) {
                return BadRequest(new { Error = "Position Overlapping Another Ship" });
            }

            Player.Ships.Add(new SmallShip(PosX, PosY));

            return Ok();
        }

        [Route("/[controller]/[action]")]
        [HttpGet]
        public IActionResult StrikePosition([FromServices] GameSession GameSession, uint PosX, uint PosY) 
        {
            // Strike Out of Bounds? Return Error
            if (PosX > GameSession.GridSizeX || PosY > GameSession.GridSizeY)
            {
                return BadRequest(new { Error = "Position Out Of Bounds" });
            }

            var Player = GameSession.Player1;

            var ShipThatGotStriken = Player.StrikePlayer(PosX,PosY);
            if (ShipThatGotStriken != null)
            {
                return Ok(new { Result = "HIT" });
            }
            return Ok(new { Result = "Missed" });

        }
    }
}