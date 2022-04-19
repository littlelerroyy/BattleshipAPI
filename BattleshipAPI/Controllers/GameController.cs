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
        public IActionResult TestAction([FromServices] GameSession GameSession)
        {
            GameSession.GridSizeX = 2;
            GameSession.GridSizeY = 2;
            return null;
        }
    }
}