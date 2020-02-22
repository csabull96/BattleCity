// <copyright file="LogicTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BattleCity.Logic.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BattleCity.Structure;
    using NUnit.Framework;

    /// <summary>
    /// Class for Logic tests.
    /// </summary>
    [TestFixture]
    public class LogicTests
    {
        private static readonly List<Player> PlayerList = new List<Player>()
        {
            new Player(10, 10),
            new Player(20, 20),
        };

        private static readonly List<Base> ElementsList = new List<Base>()
        {
            new Wall(0, 0, false),
            new PowerUp(0, 10),
        };

        private Logic logic;

        /// <summary>
        /// Sets up the required variables before testing.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.logic = new Logic();
        }

        /// <summary>
        /// Tests if EnemyAppearance correctly adds a new tank to the Elements list.
        /// </summary>
        [Test]
        public void EnemyAppearance_AddsNewTank_ToElementsList()
        {
            this.logic.EnemyAppearance(PlayerList, ElementsList);
            Assert.That(ElementsList[ElementsList.Count - 1] is Tank);
        }

        /// <summary>
        /// Tests if PowerUp correctly adds a new power up to the elements list.
        /// </summary>
        [Test]
        public void PowerUp_AddsNewPowerup_ToElementsList()
        {
            this.logic.PowerUp(ElementsList);
            Assert.That(ElementsList[ElementsList.Count - 1] is PowerUp);
        }

        /// <summary>
        /// Tests the IsAlive property.
        /// </summary>
        /// <param name="life">The life amount to test with.</param>
        /// <param name="isAlive">The expected return value.</param>
        [TestCase(5, true)]
        [TestCase(1, true)]
        [TestCase(0, false)]
        [TestCase(-3, false)]
        public void Tank_CorrectlyDetermines_WhetherIsAlive(int life, bool isAlive)
        {
            Tank testTank = new Tank(0, 0);
            testTank.Life = life;
            Assert.That(testTank.Alive == isAlive);
        }

        /// <summary>
        /// Tests if AddTank adds a new enemy tank when all spawn points are blocked.
        /// </summary>
        [Test]
        public void EnemyAppearance_DoesntAddTank_WhenAllSpotsAreBlocked()
        {
            ElementsList.Add(new Wall(0, 0, true));
            ElementsList.Add(new Wall(360, 0, true));
            ElementsList.Add(new Wall(720, 0, true));
            int initialCount = ElementsList.Count;
            this.logic.EnemyAppearance(PlayerList, ElementsList);
            Assert.That(initialCount == ElementsList.Count);
        }

        /// <summary>
        /// Tests tank movement.
        /// </summary>
        /// <param name="dx">movement on the x axis.</param>
        /// <param name="dy">movement on the y axis.</param>
        [TestCase(0, 0)]
        [TestCase(10, 0)]
        [TestCase(0, -10)]
        [TestCase(-10, 10)]
        public void TankMove_MovesTank_ByCorrectAmount(int dx, int dy)
        {
            Tank testTank = new Tank(100, 100);
            double initialX = testTank.Body.X;
            double initialY = testTank.Body.Y;
            testTank.Move(dx, dy);
            Assert.That(testTank.Body.X == initialX + dx && testTank.Body.Y == initialY + dy);
        }

        /// <summary>
        /// Tests if PlayerRespawn respawns non-player elements.
        /// </summary>
        [Test]
        public void PlayerRespawn_OnlyRespawnsPlayers()
        {
            Wall wall = new Wall(0, 0, false);
            Eagle eagle = new Eagle(0, 0);
            wall.Life = 0;
            eagle.Life = 0;
            wall.PlayerRespawn();
            eagle.PlayerRespawn();
            Assert.That(eagle.Life == 0 && wall.Life == 0);
        }

        /// <summary>
        /// Tests if PlayerRespawn correctly works with coordinates.
        /// </summary>
        /// <param name="x">The x coordinate where the player should respawn.</param>
        /// <param name="y">The y coordinate where the player should respawn.</param>
        [TestCase(0, 0)]
        [TestCase(10, 10)]
        public void PlayerRespawn_RespawnsAtCorrectCoordinates(int x, int y)
        {
            Player player = new Player(100, 100);
            player.Life = 2;
            player.Id = 0;
            player.PlayerRespawn(x, y);
            Assert.That(player.Body.X == x && player.Body.Y == y);
        }

        /// <summary>
        /// Tests if the Shot function correctly determines projectile collision.
        /// </summary>
        [Test]
        public void Bullet_Shot_CorrectlyDeterminesCollision()
        {
            Tank tank = new Tank(0, 0);
            Tank enemyTank = new Tank(100, 100);
            Bullet bullet = new Bullet(tank, Direction.Up, 4, 100, 100);
            Assert.That(bullet.Shot(enemyTank));
        }

        /// <summary>
        /// Determines if BulletsMove moves the bullets correctly on the Y axis.
        /// </summary>
        /// <param name="direction">The heading of the bullet.</param>
        /// <param name="change">The expected change of the bullet's position.</param>
        [TestCase(Direction.Up, -4)]
        [TestCase(Direction.Down, 4)]
        public void BulletsMovement_MovesBulletsCorrectlyOnYAxis(Direction direction, int change)
        {
            List<Bullet> bullets = new List<Bullet>();
            bullets.Add(new Bullet(new Tank(0, 0), direction, 4, 0, 0));
            this.logic.BulletsMovement(bullets);
            Assert.That(bullets[0].Body.Y == change);
        }

        /// <summary>
        /// Determines if BulletsMove moves the bullets correctly on the X axis.
        /// </summary>
        /// <param name="direction">The heading of the bullet.</param>
        /// <param name="change">The expected change of the bullet's position.</param>
        [TestCase(Direction.Left, -4)]
        [TestCase(Direction.Right, 4)]
        public void BulletsMovement_MovesBulletsCorrectlyOnXAxis(Direction direction, int change)
        {
            List<Bullet> bullets = new List<Bullet>();
            bullets.Add(new Bullet(new Tank(0, 0), direction, 4, 0, 0));
            this.logic.BulletsMovement(bullets);
            Assert.That(bullets[0].Body.X == change);
        }
    }
}
