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
            //Applying to Player 1 "CPU" only for this task
            var Player = GameSession.Player1;

            //If Overlapping Return Error
            if (!Player.LocationisFree(PosX, PosY) || !GameSession.CheckCoordinatesAreInBounds(PosX, PosY))
            {
                return BadRequest(new { Error = "Position Overlapping Another Ship or you are trying to spawn them out of the grid" });
            }

            //Add new ship
            Player.Ships.Add(new SmallShip(PosX, PosY));

            return Ok();
        }

        [Route("/[controller]/[action]")]
        [HttpGet]
        public IActionResult AddMediumShip([FromServices] GameSession GameSession, uint PosX1, uint PosY1, uint PosX2, uint PosY2)
        {
            //Applying to Player 1 "CPU" only for this task
            var Player = GameSession.Player1;

            //Lets create a new positions list and check them.
            var LocationList = new List<Location>()
            {
                new Location{xAxis =  PosX1, yAxis=PosY1},
                new Location{xAxis =  PosX2, yAxis=PosY2},
            };

            //If Overlapping Return Error
            if (!Player.ListofLocationsIsFree(LocationList) || !GameSession.CheckCoordinateListAreInBounds(LocationList))
            {
                return BadRequest(new { Error = "Position Overlapping Another Ship or you are trying to spawn them out of the grid" });
            }

            //Add new ship
            Player.Ships.Add(new MediumShip(PosX1, PosY1, PosX2, PosY2));

            return Ok();
        }

        [Route("/[controller]/[action]")]
        [HttpGet]
        public IActionResult AddLargeShip([FromServices] GameSession GameSession, uint PosX1, uint PosY1, uint PosX2, uint PosY2, uint PosX3, uint PosY3)
        {

            //Applying to Player 1 "CPU" only for this task
            var Player = GameSession.Player1;

            //Lets create a new positions list and check them.
            var LocationList = new List<Location>()
            {
                new Location{xAxis =  PosX1, yAxis=PosY1},
                new Location{xAxis =  PosX2, yAxis=PosY2},
                new Location{xAxis =  PosX3, yAxis=PosY3},
            };

            //If Overlapping Return Error
            if (!Player.ListofLocationsIsFree(LocationList) || !GameSession.CheckCoordinateListAreInBounds(LocationList))
            {
                return BadRequest(new { Error = "Position Overlapping Another Ship or you are trying to spawn them out of the grid" });
            }

            //Add new ship
            Player.Ships.Add(new LargeShip(PosX1, PosY1, PosX2, PosY2, PosX3, PosY3));

            return Ok();
        }

        [Route("/[controller]/[action]")]
        [HttpGet]
        public IActionResult StrikePosition([FromServices] GameSession GameSession, uint PosX, uint PosY)
        {
            // Strike Out of Bounds? Return Error
            if (!GameSession.CheckCoordinatesAreInBounds(PosX, PosY))
            {
                return BadRequest(new { Error = "Position Out Of Bounds" });
            }

            //Applying to Player 1 "CPU" only for this task
            var Player = GameSession.Player1;
            var ShipThatGotStricken = Player.StrikePlayer(PosX, PosY);

            if (ShipThatGotStricken == null)
            {
                return Ok(new { Result = "Missed" });
            }

            return Ok(new { Result = ShipThatGotStricken.AnnounceHit() });
        }
    }
}