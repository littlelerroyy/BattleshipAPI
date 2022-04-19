using BattleshipAPI.Models;
using NUnit.Framework;

namespace BattleshipAPI.Unit_Test
{
    public class UnitTest
    {
        [Test]
        public static void VerifyLocationIsFree()
        {
            var GameSession = GameSessionMock1();

            Assert.AreEqual(GameSession.Player1.LocationisFree(1, 3), true);
        }
        [Test]
        public static void VerifyLocationIsNotFree() 
        {
            var GameSession = GameSessionMock1();

            Assert.AreEqual(GameSession.Player1.LocationisFree(1, 1), false);
        }

        public static void SuccessFullShipStrike() 
        {
            var GameSession = GameSessionMock1();

            Assert.AreEqual(GameSession.Player1.StrikePlayer(1,1), GameSession.Player1.Ships[0]);
            Assert.AreEqual(GameSession.Player1.StrikePlayer(2, 3), GameSession.Player1.Ships[1]);

        }

        public static void UnSuccessFullShipStrike()
        {
            var GameSession = GameSessionMock1();

            Assert.AreEqual(GameSession.Player1.StrikePlayer(0, 1), null);
            Assert.AreEqual(GameSession.Player1.StrikePlayer(2, 5), null);

        }

        public static void ShipHasBeenDestroyed()
        {
            var GameSession = GameSessionMock1();

            Assert.AreEqual(GameSession.Player1.Ships[0], null);
            Assert.AreEqual(GameSession.Player1.StrikePlayer(2, 5), null);

        }


        private static GameSession GameSessionMock1()
        {
            var GameSession = new GameSession()
            {
                GridSizeX = 5,
                GridSizeY = 5,
                Player1 = new Player()
                {
                    Name = "CPU",
                    Ships = new List<Ship>() {

                        new SmallShip (1,1),
                        new SmallShip (2,3),
                    }
                }
            };
            return GameSession;
        }
    }
}
