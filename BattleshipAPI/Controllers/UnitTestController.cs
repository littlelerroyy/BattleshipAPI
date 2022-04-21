using Microsoft.AspNetCore.Mvc;
using BattleshipAPI.Unit_Test;

namespace BattleshipAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UnitTestController : ControllerBase
    {
        [Route("/[controller]/[action]")]
        [HttpGet]
        public IActionResult RunUnitTests()
        {
            
            UnitTest.VerifyLocationIsFree();
            UnitTest.VerifyLocationIsNotFree();
            UnitTest.SuccessFullShipStrike();
            UnitTest.UnSuccessFullShipStrike();
            UnitTest.CoordinatesAreInBounds();
            UnitTest.CoordinatesAreOutBounds();
            UnitTest.GridSizeIsTooSmall();
            UnitTest.GridSizeIsCorrect();
            UnitTest.SmallShipHasBeenDestroyed();
            UnitTest.SmallShipHasNotBeenDestroyed();
            UnitTest.MediumShipHasBeenDestroyed();
            UnitTest.MediumShipHasNotBeenDestroyed();
            UnitTest.LargeShipHasBeenDestroyed();
            UnitTest.LargeShipHasNotBeenDestroyed();

            return Ok();
        }
    }
}
