using Microsoft.AspNetCore.Mvc;

namespace BattleshipAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        [Route("/[controller]/[action]")]
        [HttpGet]
        public IActionResult TestAction() 
        {
            return null;
        }
    }
}