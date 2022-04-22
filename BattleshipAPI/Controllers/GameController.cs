using Microsoft.AspNetCore.Mvc;
using BattleshipAPI.Models;
using BattleshipAPI.ViewModels;

namespace BattleshipAPI.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        [Route("/[controller]/[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(SuccessModel), 200)]
        [ProducesResponseType(typeof(ErrorModel), 400)]
        public IActionResult SetupGame([FromServices] GameSession GameSession, uint SizeX, uint SizeY)
        {

            //Create new Error Model & find errors
            var ErrorModel = new ErrorModel { Errors = new List<String>() };

            if (!GameSession.ValidateAndApplyGridSize(SizeX, SizeY))
            {
                ErrorModel.Errors.Add("Please enter positive numbers only that are 3 or greater.");
            }

            //Return the Errors if any
            if (ErrorModel.Errors.Count != 0)
            {
                return BadRequest(ErrorModel);
            }

            //Create new Player and Ships
            GameSession.Player1 = new Player()
            {
                Name = "CPU",
                Ships = new List<Ship>(),
                LocationsStriked = new List<Location>()
            };

            return Ok(new SuccessModel { Result = "Smiley Face" });
        }

        [Route("/[controller]/[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(SuccessModel), 200)]
        [ProducesResponseType(typeof(ErrorModel), 400)]
        public IActionResult AddSmallShip([FromServices] GameSession GameSession, uint PosX, uint PosY)
        {
            //Applying to Player 1 "CPU" only for this task
            var Player = GameSession.Player1;

            //Create new Error Model & find errors
            var ErrorModel = new ErrorModel { Errors = new List<String>() };

            if (!Player.LocationisFree(PosX, PosY))
            {
                ErrorModel.Errors.Add("Cannot spawn a new ship over an existing ship");
            }

            if (!GameSession.CheckCoordinatesAreInBounds(PosX, PosY))
            {
                ErrorModel.Errors.Add("Coordinates you have selected are out of bounds");
            }

            //Return the Errors if any
            if (ErrorModel.Errors.Count != 0)
            {
                return BadRequest(ErrorModel);
            }

            //Add new ship
            Player.Ships.Add(new SmallShip(PosX, PosY));

            return Ok(new SuccessModel { Result = "Smiley Face" });
        }


        [Route("/[controller]/[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(SuccessModel), 200)]
        [ProducesResponseType(typeof(ErrorModel), 400)]
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

            //Create new Error Model & find errors
            var ErrorModel = new ErrorModel { Errors = new List<String>() };

            if (!Player.ListofLocationsIsFree(LocationList))
            {
                ErrorModel.Errors.Add("Cannot spawn a new ship over an existing ship");
            }
            if (!GameSession.CheckCoordinateListAreInBounds(LocationList))
            {
                ErrorModel.Errors.Add("Coordinates you have selected are out of bounds");
            }
            if (!Location.IsPositionsConsolidated(LocationList))
            {
                ErrorModel.Errors.Add("One side of the ships axis needs to be identical X or Y, while the other axis needs values to be positioned next to each other");
            }

            //Return the Errors if any
            if (ErrorModel.Errors.Count != 0)
            {
                return BadRequest(ErrorModel);
            }

            //Add new ship
            Player.Ships.Add(new MediumShip(PosX1, PosY1, PosX2, PosY2));

            return Ok(new SuccessModel { Result = "Smiley Face" });
        }

        [Route("/[controller]/[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(SuccessModel), 200)]
        [ProducesResponseType(typeof(ErrorModel), 400)]
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

            //Create new Error Model & find errors
            var ErrorModel = new ErrorModel { Errors = new List<String>() };

            if (!Player.ListofLocationsIsFree(LocationList))
            {
                ErrorModel.Errors.Add("Cannot spawn a new ship over an existing ship");
            }
            if (!GameSession.CheckCoordinateListAreInBounds(LocationList))
            {
                ErrorModel.Errors.Add("Coordinates you have selected are out of bounds");
            }
            if (!Location.IsPositionsConsolidated(LocationList))
            {
                ErrorModel.Errors.Add("One side of the ships axis needs to be identical X or Y, while the other axis needs values to be positioned next to each other");
            }

            //Return the Errors if any
            if (ErrorModel.Errors.Count != 0)
            {
                return BadRequest(ErrorModel);
            }

            //Add new ship
            Player.Ships.Add(new LargeShip(PosX1, PosY1, PosX2, PosY2, PosX3, PosY3));

            return Ok(new SuccessModel { Result = "Smiley Face" });
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