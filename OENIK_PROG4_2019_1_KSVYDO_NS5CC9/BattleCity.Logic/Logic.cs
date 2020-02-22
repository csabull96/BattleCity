// <copyright file="Logic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BattleCity.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Media;
    using BattleCity.Repository;
    using BattleCity.Structure;
    using static System.Net.Mime.MediaTypeNames;

    /// <summary>
    /// Implements the ILogic interface
    /// </summary>
    public class Logic : ILogic
    {
        private readonly double width = 780;
        private readonly double height = 780;

        private readonly IRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="Logic"/> class.
        /// </summary>
        public Logic()
        {
            this.repository = new Repository();
        }

        /// <summary>
        /// Loads the level from a file
        /// </summary>
        /// <param name="numberOfPlayers">number of human players</param>
        /// <param name="battleField">number of the level</param>
        /// <param name="elements">list of elements on the level</param>
        public void LoadBattleField(int numberOfPlayers, int battleField, IList<Base> elements)
        {
            this.repository.LoadBattleField(numberOfPlayers, battleField, elements);
        }

        /// <summary>
        /// Moves the specified player tank
        /// </summary>
        /// <param name="players">list of players</param>
        /// <param name="elements">list of elements on the level</param>
        public void PlayerMove(IList<Player> players, IList<Base> elements)
        {
            if (players.Count() != 0 && players.ElementAt(0).Alive)
            {
                this.PlayerStep(players.ElementAt(0), elements);
            }

            if (players.Count() == 2 && players.ElementAt(1).Alive)
            {
                this.PlayerStep(players.ElementAt(1), elements);
            }
        }

        /// <summary>
        /// Fires a projectile from the specified player tank
        /// </summary>
        /// <param name="tank">the tank that shoots</param>
        /// <param name="bullets">list of all bullets currently on the level</param>
        /// <param name="elements">list of all elements on the level</param>
        public void Shoot(Tank tank, IList<Bullet> bullets, IList<Base> elements)
        {
            bool allowedToShoot = true;

            foreach (Bullet bullet in bullets)
            {
                if (bullet.Tank == tank)
                {
                    allowedToShoot = false;
                }
            }

            if (allowedToShoot)
            {
                elements.Insert(0, new Bullet(tank, (tank is Player) ? ((Player)tank).FacingDirection : tank.MovingDirection, tank.BulletSpeed, Bullet.X(tank), Bullet.Y(tank)));
            }
        }

        /// <summary>
        /// Spawns an enemy tank
        /// </summary>
        /// <param name="players">list of players</param>
        /// <param name="elements">list of all elements on the level</param>
        public void EnemyAppearance(IList<Player> players, IList<Base> elements)
        {
            Random random = new Random();

            switch (random.Next(3))
            {
                case 0:
                    Enemy e0 = new Enemy(0, 0);
                    if (!this.PlayerBlocked(e0, e0.Body.Left, e0.Body.Top, elements))
                    {
                        elements.Add(e0);
                    }

                    break;
                case 1:
                    Enemy e1 = new Enemy(360, 0);
                    if (!this.PlayerBlocked(e1, e1.Body.Left, e1.Body.Top, elements))
                    {
                        elements.Add(e1);
                    }

                    break;
                case 2:
                    Enemy e2 = new Enemy(720, 0);
                    if (!this.PlayerBlocked(e2, e2.Body.Left, e2.Body.Top, elements))
                    {
                        elements.Add(e2);
                    }

                    break;
            }
        }

        /// <summary>
        /// Moves an enemy tank
        /// </summary>
        /// <param name="enemies">list of enemy tanks</param>
        /// <param name="elements">list of all elements on the level</param>
        public void EnemyMove(IList<Enemy> enemies, IList<Base> elements)
        {
            Random random = new Random();

            foreach (Enemy enemy in enemies)
            {
                int rand = random.Next(351);

                if (rand < 4)
                {
                    if (rand == 0 && !this.EnemyBlocked(enemy, Direction.Up, 5, elements))
                    {
                        enemy.MovingDirection = Direction.Up;
                    }

                    if (rand == 1 && !this.EnemyBlocked(enemy, Direction.Down, 5, elements))
                    {
                        enemy.MovingDirection = Direction.Down;
                    }

                    if (rand == 2 && !this.EnemyBlocked(enemy, Direction.Left, 5, elements))
                    {
                        enemy.MovingDirection = Direction.Left;
                    }

                    if (rand == 3 && !this.EnemyBlocked(enemy, Direction.Right, 5, elements))
                    {
                        enemy.MovingDirection = Direction.Right;
                    }

                    this.EnemyStep(enemy);
                }
                else
                {
                    if (this.EnemyBlocked(enemy, enemy.MovingDirection, 1, elements))
                    {
                        rand = random.Next(1, 101);

                        switch (enemy.Where)
                        {
                            case Location.TopLeft:
                                if (rand < 41 && !this.EnemyBlocked(enemy, Direction.Down, 5, elements))
                                {
                                    enemy.MovingDirection = Direction.Down;
                                }
                                else if (rand < 81 && !this.EnemyBlocked(enemy, Direction.Right, 5, elements))
                                {
                                    enemy.MovingDirection = Direction.Right;
                                }
                                else if (rand < 91 && !this.EnemyBlocked(enemy, Direction.Left, 5, elements))
                                {
                                    enemy.MovingDirection = Direction.Left;
                                }
                                else if (rand < 101 && !this.EnemyBlocked(enemy, Direction.Up, 5, elements))
                                {
                                    enemy.MovingDirection = Direction.Up;
                                }

                                break;
                            case Location.BottomLeft:
                                if (rand < 11 && !this.EnemyBlocked(enemy, Direction.Down, 5, elements))
                                {
                                    enemy.MovingDirection = Direction.Down;
                                }
                                else if (rand < 81 && !this.EnemyBlocked(enemy, Direction.Right, 5, elements))
                                {
                                    enemy.MovingDirection = Direction.Right;
                                }
                                else if (rand < 91 && !this.EnemyBlocked(enemy, Direction.Left, 5, elements))
                                {
                                    enemy.MovingDirection = Direction.Left;
                                }
                                else if (rand < 101 && !this.EnemyBlocked(enemy, Direction.Up, 5, elements))
                                {
                                    enemy.MovingDirection = Direction.Up;
                                }

                                break;
                            case Location.TopRight:
                                if (rand < 41 && !this.EnemyBlocked(enemy, Direction.Left, 5, elements))
                                {
                                    enemy.MovingDirection = Direction.Left;
                                }
                                else if (rand < 81 && !this.EnemyBlocked(enemy, Direction.Down, 5, elements))
                                {
                                    enemy.MovingDirection = Direction.Down;
                                }
                                else if (rand < 91 && !this.EnemyBlocked(enemy, Direction.Right, 5, elements))
                                {
                                    enemy.MovingDirection = Direction.Right;
                                }
                                else if (rand < 101 && !this.EnemyBlocked(enemy, Direction.Up, 5, elements))
                                {
                                    enemy.MovingDirection = Direction.Up;
                                }

                                break;
                            case Location.BottomRight:
                                if (rand < 71 && !this.EnemyBlocked(enemy, Direction.Left, 5, elements))
                                {
                                    enemy.MovingDirection = Direction.Left;
                                }
                                else if (rand < 81 && !this.EnemyBlocked(enemy, Direction.Down, 5, elements))
                                {
                                    enemy.MovingDirection = Direction.Down;
                                }
                                else if (rand < 91 && !this.EnemyBlocked(enemy, Direction.Right, 5, elements))
                                {
                                    enemy.MovingDirection = Direction.Right;
                                }
                                else if (rand < 101 && !this.EnemyBlocked(enemy, Direction.Up, 5, elements))
                                {
                                    enemy.MovingDirection = Direction.Up;
                                }

                                break;
                        }
                    }
                    else
                    {
                        this.EnemyStep(enemy);
                    }
                }
            }
        }

        /// <summary>
        /// Fires a projectile from an enemy tank
        /// </summary>
        /// <param name="enemies">list of enemy tanks</param>
        /// <param name="bullets">list of projectiles on the map</param>
        /// <param name="elements">list of all elements on the level</param>
        public void EnemyShoot(IList<Enemy> enemies, IList<Bullet> bullets, IList<Base> elements)
        {
            Random random = new Random();

            foreach (Enemy enemy in enemies)
            {
                int rand = random.Next(131);

                if (rand < 2)
                {
                    this.Shoot(enemy, bullets, elements);
                }
            }
        }

        /// <summary>
        /// Handles the movement of projectiles
        /// </summary>
        /// <param name="bullets">list of projectiles on the map</param>
        public void BulletsMovement(IList<Bullet> bullets)
        {
            foreach (Bullet bullet in bullets)
            {
                int sebesseg = bullet.Speed;

                switch (bullet.Direction)
                {
                    case Direction.Up:
                        bullet.Move(0, -sebesseg);
                        break;
                    case Direction.Down:
                        bullet.Move(0, sebesseg);
                        break;
                    case Direction.Left:
                        bullet.Move(-sebesseg, 0);
                        break;
                    case Direction.Right:
                        bullet.Move(sebesseg, 0);
                        break;
                }
            }
        }

        /// <summary>
        /// Handles the hit detection of projectiles
        /// </summary>
        /// <param name="gameplay">the current gameplay element</param>
        /// <param name="bullets">list of projectiles on the map</param>
        /// <param name="elements">list of all elements on the level</param>
        public void Shot(Gameplay gameplay, IList<Bullet> bullets, IList<Base> elements)
        {
            Random rnd = new Random();

            foreach (Bullet bullet in bullets)
            {
                foreach (Base element in elements)
                {
                    if (bullet.Shot(element))
                    {
                        if (bullet.Tank is Player)
                        {
                            if (element is Player)
                            {
                                bullet.Life--;
                            }
                            else if (element is Enemy)
                            {
                                bullet.Life--;
                                element.Life--;
                            }
                            else if (element is Wall)
                            {
                                bullet.Life--;
                                element.Life--;
                            }
                            else if (element is Eagle)
                            {
                                bullet.Life--;
                                element.Life--;
                            }
                            else if (element is PowerUp)
                            {
                            }
                        }
                        else if (bullet.Tank is Enemy)
                        {
                            if (element is Enemy)
                            {
                                bullet.Life--;
                            }
                            else if (element is Player)
                            {
                                bullet.Life--;

                                if (((Player)element).Upgrade == 0)
                                {
                                    element.Life--;

                                    switch (((Player)element).Id)
                                    {
                                        case 0:
                                            if (this.PlayerBlocked((Player)element, 480, 730, elements))
                                            {
                                                int x = 240;

                                                while (this.PlayerBlocked((Player)element, x, 730, elements))
                                                {
                                                    x = rnd.Next(731);
                                                }

                                                element.PlayerRespawn(x, 730);
                                            }
                                            else
                                            {
                                                element.PlayerRespawn();
                                            }

                                            break;
                                        case 1:
                                            if (this.PlayerBlocked((Player)element, 240, 730, elements))
                                            {
                                                int x = 480;

                                                while (this.PlayerBlocked((Player)element, x, 730, elements))
                                                {
                                                    x = rnd.Next(731);
                                                }

                                                element.PlayerRespawn(x, 730);
                                            }
                                            else
                                            {
                                                element.PlayerRespawn();
                                            }

                                            break;
                                    }
                                }
                                else
                                {
                                    ((Player)element).Upgrade = 0;
                                    ((Player)element).Speed = Player.SPEED;
                                    ((Player)element).BulletSpeed = Player.BULLETSPEED;
                                }
                            }
                            else if (element is Wall)
                            {
                                bullet.Life--;
                                element.Life--;
                            }
                            else if (element is Eagle)
                            {
                                bullet.Life--;
                                element.Life--;
                            }
                            else if (element is PowerUp)
                            {
                            }
                        }

                        if (element is Bullet)
                        {
                            bullet.Life--;
                            element.Life--;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Deletes all inactive elements
        /// </summary>
        /// <param name="elements">list of all elements on the level</param>
        /// <param name="gameplay">the current gameplay element</param>
        public void Graveyard(IList<Base> elements, Gameplay gameplay)
        {
            List<Base> todeletes = new List<Base>();

            foreach (Base element in elements)
            {
                if (element is PowerUp)
                {
                    if (((PowerUp)element).SW.Elapsed.Seconds > 10)
                    {
                        ((PowerUp)element).SW.Stop();
                        todeletes.Add(element);
                    }
                }

                if (!element.Alive)
                {
                    todeletes.Add(element);
                }
                else if (element is Bullet && (element.Body.Left > this.width || element.Body.Right < 0 || element.Body.Top > this.height || element.Body.Bottom < 0))
                {
                    todeletes.Add(element);
                }
            }

            foreach (Base todelete in todeletes)
            {
                if (todelete is Enemy)
                {
                    gameplay.Score += ((Enemy)todelete).Value;
                }

                if (gameplay.Score > gameplay.HighScore)
                {
                    gameplay.HighScore = gameplay.Score;
                }

                elements.Remove(todelete);
            }
        }

        /// <summary>
        /// Adds a new powerup to the level
        /// </summary>
        /// <param name="elements">list of all elements on the level</param>
        public void PowerUp(IList<Base> elements)
        {
            Random random = new Random();

            elements.Add(new PowerUp(random.Next(0, 721), random.Next(0, 721)));
        }

        /// <summary>
        /// Terminates the game
        /// </summary>
        /// <param name="gameplay">the current gameplay element</param>
        public void Exit(Gameplay gameplay)
        {
            if (gameplay.Score == gameplay.HighScore)
            {
                File.WriteAllText("highscore.txt", gameplay.HighScore.ToString());
            }

            Environment.Exit(0);
        }

        /// <summary>
        /// Restarts the game
        /// </summary>
        /// <param name="gameplay">the current gameplay element</param>
        public void Restart(Gameplay gameplay)
        {
            if (gameplay.Score == gameplay.HighScore)
            {
                File.WriteAllText("highscore.txt", gameplay.HighScore.ToString());
            }
        }

        private void PlayerStep(Player player, IList<Base> elements)
        {
            switch (player.MovingDirection)
            {
                case Direction.Up:
                    if (!this.PlayerBlocked(player, player.Body.Left, player.Body.Top - player.Speed, elements))
                    {
                        player.Move(0, -player.Speed);
                    }

                    break;
                case Direction.Down:
                    if (!this.PlayerBlocked(player, player.Body.Left, player.Body.Top + player.Speed, elements))
                    {
                        player.Move(0, player.Speed);
                    }

                    break;
                case Direction.Left:
                    if (!this.PlayerBlocked(player, player.Body.Left - player.Speed, player.Body.Top, elements))
                    {
                        player.Move(-player.Speed, 0);
                    }

                    break;
                case Direction.Right:
                    if (!this.PlayerBlocked(player, player.Body.Left + player.Speed, player.Body.Top, elements))
                    {
                        player.Move(player.Speed, 0);
                    }

                    break;
                case Direction.Stay:
                    break;
            }
        }

        private bool PlayerBlocked(Tank tank, double next_x, double next_y, IList<Base> elements)
        {
            Rect next = new Rect(next_x, next_y, tank.Body.Width, tank.Body.Height);

            foreach (Base element in elements)
            {
                if (element is Bullet)
                {
                }
                else
                {
                    Geometry metszet = Geometry.Combine(new RectangleGeometry(next), new RectangleGeometry(element.Body), GeometryCombineMode.Intersect, null);

                    if (metszet.GetArea() > 0)
                    {
                        if (tank is Player && element is PowerUp)
                        {
                            switch (((PowerUp)element).Type)
                            {
                                case PowerUpType.Stronger:
                                    switch (((Player)tank).Upgrade < 2 ? ++((Player)tank).Upgrade : ((Player)tank).Upgrade)
                                    {
                                        case 1:
                                            ((Player)tank).Speed = Player.SPEED;
                                            ((Player)tank).BulletSpeed = 12;
                                            break;
                                        case 2:
                                            ((Player)tank).Speed = Player.SPEED + 2;
                                            ((Player)tank).BulletSpeed = Player.BULLETSPEED + 4;
                                            break;
                                    }

                                    break;
                                case PowerUpType.Bomb:
                                    foreach (Base item in elements)
                                    {
                                        if (item is Enemy)
                                        {
                                            item.Life = 0;
                                        }
                                    }

                                    break;
                                case PowerUpType.Life:
                                    if (tank is Player)
                                    {
                                        ((Player)tank).Life++;
                                    }

                                    break;
                            }

                            element.Life--;

                            return false;
                        }
                        else if (tank != element)
                        {
                            return true;
                        }
                    }
                    else if (next.Left < 0 || next.Right > this.width || next.Top < 0 || next.Bottom > this.height)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool EnemyBlocked(Tank tank, Direction direction, int deep, IList<Base> elements)
        {
            Rect next = default(Rect);

            switch (direction)
            {
                case Direction.Up:
                    next = new Rect(tank.Body.Left, tank.Body.Top - (tank.Speed * deep), tank.Body.Width, tank.Body.Height);
                    break;
                case Direction.Down:
                    next = new Rect(tank.Body.Left, tank.Body.Top + (tank.Speed * deep), tank.Body.Width, tank.Body.Height);
                    break;
                case Direction.Left:
                    next = new Rect(tank.Body.Left - (tank.Speed * deep), tank.Body.Top, tank.Body.Width, tank.Body.Height);
                    break;
                case Direction.Right:
                    next = new Rect(tank.Body.Left + (tank.Speed * deep), tank.Body.Top, tank.Body.Width, tank.Body.Height);
                    break;
            }

            foreach (Base element in elements)
            {
                if (element != tank && !(element is Bullet))
                {
                    Geometry metszet = Geometry.Combine(new RectangleGeometry(next), new RectangleGeometry(element.Body), GeometryCombineMode.Intersect, null);

                    if (metszet.GetArea() > 0)
                    {
                        if (element is PowerUp)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else if (next.Left < 0 || next.Right > this.width || next.Top < 0 || next.Bottom > this.height)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void EnemyStep(Enemy enemy)
        {
            switch (enemy.MovingDirection)
            {
                case Direction.Up:
                    enemy.Move(0, -enemy.Speed);
                    break;
                case Direction.Down:
                    enemy.Move(0, enemy.Speed);
                    break;
                case Direction.Left:
                    enemy.Move(-enemy.Speed, 0);
                    break;
                case Direction.Right:
                    enemy.Move(enemy.Speed, 0);
                    break;
            }
        }
    }
}
