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
        public IActionResult SetupGame([FromServices] GameSession GameSession, uint SizeX, uint SizeY)
        {

            if (!GameSession.ValidateAndApplyGridSize(SizeX, SizeY))
            {
                return BadRequest(new { Error = "Please enter positive numbers only that are 3 or greater." });
            }

            //Create new Player and Ships
            GameSession.Player1 = new Player()
            {
                Name = "CPU",
                Ships = new List<Ship>(),
                LocationsStriked = new List<Location>()
            };

            return Ok();
        }

        [Route("/[controller]/[action]")]
        [HttpGet]
        public IActionResult AddSmallShip([FromServices] GameSession GameSession, uint PosX, uint PosY)
        {
            var Player = GameSession.Player1;

            //If Overlapping Return Error
            if (!Player.LocationisFree(PosX, PosY))
            {
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
            if (!GameSession.CheckCoordinatesAreInBounds(PosX,PosY))
            {
                return BadRequest(new { Error = "Position Out Of Bounds" });
            }

            var Player = GameSession.Player1;

            var ShipThatGotStricken = Player.StrikePlayer(PosX, PosY);

            if (ShipThatGotStricken == null)
            {
                return Ok(new { Result = "Missed" });
            }

            ShipThatGotStricken.AddHitMarkerToShipLocation(PosX, PosY);

            return Ok(new { Result = ShipThatGotStricken.AnnounceHit() }); ;

        }
    }
}