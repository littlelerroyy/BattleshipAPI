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

        [Test]
        public static void SuccessFullShipStrike()
        {
            var GameSession = GameSessionMock1();

            Assert.AreEqual(GameSession.Player1.StrikePlayer(1, 1), GameSession.Player1.Ships[0]);
            Assert.AreEqual(GameSession.Player1.StrikePlayer(2, 3), GameSession.Player1.Ships[1]);

        }

        [Test]
        public static void UnSuccessFullShipStrike()
        {
            var GameSession = GameSessionMock1();

            Assert.AreEqual(GameSession.Player1.StrikePlayer(0, 1), null);
            Assert.AreEqual(GameSession.Player1.StrikePlayer(2, 5), null);

        }

        [Test]
        public static void SmallShipHasBeenDestroyed()
        {
            var GameSession = GameSessionMock1();
            
            //Strike the ship first
            Assert.AreEqual(GameSession.Player1.StrikePlayer(2, 3), GameSession.Player1.Ships[1]);
            
            //Ship should be destroyed
            Assert.AreEqual(GameSession.Player1.Ships[1].Destroyed,true);

        }

        [Test]
        public static void CoordinatesAreInBounds() 
        {
            var GameSession = GameSessionMock1();
            Assert.AreEqual(GameSession.CheckCoordinatesAreInBounds(2, 3), true);
        }

        [Test]
        public static void CoordinatesAreOutBounds()
        {
            var GameSession = GameSessionMock1();
            Assert.AreEqual(GameSession.CheckCoordinatesAreInBounds(7, 7), false);
        }

        [Test]
        public static void GridSizeIsTooSmall()
        {
            var GameSession = new GameSession();
            Assert.AreEqual(GameSession.ValidateAndApplyGridSize(2, 2), false);
        }

        [Test]
        public static void GridSizeIsCorrect()
        {
            var GameSession = new GameSession();
            Assert.AreEqual(GameSession.ValidateAndApplyGridSize(4, 4), true);
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
