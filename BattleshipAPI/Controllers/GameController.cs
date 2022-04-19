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
        public IActionResult DeclareBoardSize([FromServices] GameSession GameSession, string SizeX, string SizeY)
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
            catch {                
                return BadRequest(new {Error="Please Enter Positive Numbers Only that are 3 or greater."});
            }
            return Ok();
        }
    }
}