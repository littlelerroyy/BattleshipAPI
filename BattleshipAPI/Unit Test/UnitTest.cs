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

            Assert.AreEqual(true, GameSession.Player1.LocationisFree(1, 3));
        }

        [Test]
        public static void VerifyLocationIsNotFree()
        {
            var GameSession = GameSessionMock1();

            Assert.AreEqual(false, GameSession.Player1.LocationisFree(1, 1));
        }

        [Test]
        public static void SuccessFullShipStrike()
        {
            var GameSession = GameSessionMock1();

            Assert.AreEqual(GameSession.Player1.Ships[0], GameSession.Player1.StrikePlayer(1, 1));
            Assert.AreEqual(GameSession.Player1.Ships[1], GameSession.Player1.StrikePlayer(2, 3));

        }

        [Test]
        public static void UnSuccessFullShipStrike()
        {
            var GameSession = GameSessionMock1();

            Assert.AreEqual(null, GameSession.Player1.StrikePlayer(0, 1));
            Assert.AreEqual(null, GameSession.Player1.StrikePlayer(2, 5));

        }

        [Test]
        public static void SmallShipHasBeenDestroyed()
        {
            var GameSession = GameSessionMock1();
            
            //Strike the ship first
            Assert.AreEqual(GameSession.Player1.Ships[1], GameSession.Player1.StrikePlayer(2, 3));
            
            //Ship should be destroyed
            Assert.AreEqual(true, GameSession.Player1.Ships[1].Destroyed);

        }

        [Test]
        public static void SmallShipHasNotBeenDestroyed()
        {
            var GameSession = GameSessionMock1();

            //Strike next to a ship
            Assert.AreEqual(null, GameSession.Player1.StrikePlayer(1, 2));

            //Ship should not be destroyed
            Assert.AreEqual(false, GameSession.Player1.Ships[0].Destroyed);

        }

        [Test]
        public static void MediumShipHasBeenDestroyed()
        {
            var GameSession = GameSessionMock1();

            //Strike the ship until its destroyed
            Assert.AreEqual(GameSession.Player1.Ships[2], GameSession.Player1.StrikePlayer(5, 5));
            Assert.AreEqual(GameSession.Player1.Ships[2], GameSession.Player1.StrikePlayer(5, 4));

            //Ship should be destroyed
            Assert.AreEqual(true, GameSession.Player1.Ships[2].Destroyed);

        }

        [Test]
        public static void MediumShipHasNotBeenDestroyed()
        {
            var GameSession = GameSessionMock1();

            //Only strike the ship once
            Assert.AreEqual(GameSession.Player1.Ships[2], GameSession.Player1.StrikePlayer(5, 5));

            //Ship should not be destroyed
            Assert.AreEqual(false, GameSession.Player1.Ships[2].Destroyed);

        }

        [Test]
        public static void LargeShipHasBeenDestroyed()
        {
            var GameSession = GameSessionMock1();

            //Strike the ship until its destroyed
            Assert.AreEqual(GameSession.Player1.Ships[3], GameSession.Player1.StrikePlayer(3, 1));
            Assert.AreEqual(GameSession.Player1.Ships[3], GameSession.Player1.StrikePlayer(3, 2));
            Assert.AreEqual(GameSession.Player1.Ships[3], GameSession.Player1.StrikePlayer(3, 3));

            //Ship should be destroyed
            Assert.AreEqual(true, GameSession.Player1.Ships[3].Destroyed);

        }

        [Test]
        public static void LargeShipHasNotBeenDestroyed()
        {
            var GameSession = GameSessionMock1();

            //Strike the ship only twice
            Assert.AreEqual(GameSession.Player1.Ships[3], GameSession.Player1.StrikePlayer(3, 1));
            Assert.AreEqual(GameSession.Player1.Ships[3], GameSession.Player1.StrikePlayer(3, 3));

            //Ship should not be destroyed
            Assert.AreEqual(false, GameSession.Player1.Ships[3].Destroyed);

        }

        [Test]
        public static void CoordinatesAreInBounds() 
        {
            var GameSession = GameSessionMock1();
            Assert.AreEqual(true, GameSession.CheckCoordinatesAreInBounds(2, 3));
        }

        [Test]
        public static void CoordinatesAreOutBounds()
        {
            var GameSession = GameSessionMock1();
            Assert.AreEqual(false, GameSession.CheckCoordinatesAreInBounds(7, 7));
        }

        [Test]
        public static void GridSizeIsTooSmall()
        {
            var GameSession = new GameSession();
            Assert.AreEqual(false,GameSession.ValidateAndApplyGridSize(2, 2));
        }

        [Test]
        public static void GridSizeIsCorrect()
        {
            var GameSession = new GameSession();
            Assert.AreEqual(true, GameSession.ValidateAndApplyGridSize(4, 4));
        }

        [Test]
        public static void ShipLocationsIsConsolidated() 
        {
            var GameSession = GameSessionMock2();
            Assert.AreEqual(true, Location.IsPositionsConsolidated(GameSession.Player1.Ships[1].Locations));
            Assert.AreEqual(true, Location.IsPositionsConsolidated(GameSession.Player1.Ships[4].Locations));
        }

        public static void ShipLocationsIsNotConsolidated()
        {
            var GameSession = GameSessionMock2();
            
            Assert.AreEqual(false, Location.IsPositionsConsolidated(GameSession.Player1.Ships[0].Locations));
            Assert.AreEqual(false, Location.IsPositionsConsolidated(GameSession.Player1.Ships[2].Locations));
            Assert.AreEqual(false, Location.IsPositionsConsolidated(GameSession.Player1.Ships[3].Locations));
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

        private static GameSession GameSessionMock2()
        {
            var GameSession = new GameSession()
            {
                GridSizeX = 5,
                GridSizeY = 5,
                Player1 = new Player()
                {
                    Name = "CPU",
                    Ships = new List<Ship>() {

                        new LargeShip (2,1,3,2,3,3),
                        new LargeShip (3,1,3,2,3,3),
                        new LargeShip (3,1,3,2,3,4),
                        new LargeShip (1,2,3,2,5,2),
                        new LargeShip (1,2,1,3,1,4),
                    }
                }
            };
            return GameSession;
        }
    }
}
