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
        public static void SmallShipHasNotBeenDestroyed()
        {
            var GameSession = GameSessionMock1();

            //Strike next to a ship
            Assert.AreEqual(GameSession.Player1.StrikePlayer(1, 2), null);

            //Ship should not be destroyed
            Assert.AreEqual(GameSession.Player1.Ships[0].Destroyed, false);

        }

        [Test]
        public static void MediumShipHasBeenDestroyed()
        {
            var GameSession = GameSessionMock1();

            //Strike the ship until its destroyed
            Assert.AreEqual(GameSession.Player1.StrikePlayer(5, 5), GameSession.Player1.Ships[2]);
            Assert.AreEqual(GameSession.Player1.StrikePlayer(5, 4), GameSession.Player1.Ships[2]);

            //Ship should be destroyed
            Assert.AreEqual(GameSession.Player1.Ships[2].Destroyed, true);

        }

        [Test]
        public static void MediumShipHasNotBeenDestroyed()
        {
            var GameSession = GameSessionMock1();

            //Only strike the ship once
            Assert.AreEqual(GameSession.Player1.StrikePlayer(5, 5), GameSession.Player1.Ships[2]);

            //Ship should not be destroyed
            Assert.AreEqual(GameSession.Player1.Ships[2].Destroyed, false);

        }

        [Test]
        public static void LargeShipHasBeenDestroyed()
        {
            var GameSession = GameSessionMock1();

            //Strike the ship until its destroyed
            Assert.AreEqual(GameSession.Player1.StrikePlayer(3, 1), GameSession.Player1.Ships[3]);
            Assert.AreEqual(GameSession.Player1.StrikePlayer(3, 2), GameSession.Player1.Ships[3]);
            Assert.AreEqual(GameSession.Player1.StrikePlayer(3, 3), GameSession.Player1.Ships[3]);

            //Ship should be destroyed
            Assert.AreEqual(GameSession.Player1.Ships[3].Destroyed, true);

        }

        [Test]
        public static void LargeShipHasNotBeenDestroyed()
        {
            var GameSession = GameSessionMock1();

            //Strike the ship only twice
            Assert.AreEqual(GameSession.Player1.StrikePlayer(3, 1), GameSession.Player1.Ships[3]);
            Assert.AreEqual(GameSession.Player1.StrikePlayer(3, 3), GameSession.Player1.Ships[3]);

            //Ship should not be destroyed
            Assert.AreEqual(GameSession.Player1.Ships[3].Destroyed, false);

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
                        new MediumShip (5,5,5,4),
                        new LargeShip (3,1,3,2,3,3)
                    }
                }
            };
            return GameSession;
        }
    }
}
