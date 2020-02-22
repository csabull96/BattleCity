// <copyright file="ViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BattleCity.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Windows.Input;
    using BattleCity.Logic;
    using BattleCity.Structure;
    using CommonServiceLocator;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;

    /// <summary>
    /// Viewmodel that handles the connection between the display and the logic
    /// </summary>
    public class ViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModel"/> class.
        /// </summary>
        public ViewModel()
        {
            string[] parameters = File.ReadAllText("parameters.txt").Split(';');

            this.O2 = new BindingList<Base>();

            this.Logic.LoadBattleField(int.Parse(parameters[0]), int.Parse(parameters[1]), this.O2);

            this.Gameplay = new Gameplay(this.O2);

            this.ExitCommand = new RelayCommand(() => this.Logic.Exit(this.Gameplay));

            this.RestartCommand = new RelayCommand(() => this.Logic.Restart(this.Gameplay));
        }

        /// <summary>
        /// Gets or sets the list of objects
        /// </summary>
        public BindingList<Base> O2 { get; set; }

        /// <summary>
        /// Gets the current logic object
        /// </summary>
        public ILogic Logic
        {
            get { return ServiceLocator.Current.GetInstance<ILogic>(); }
        }

        /// <summary>
        /// Gets or sets the current gameplay object
        /// </summary>
        public Gameplay Gameplay { get; set; }

        /// <summary>
        /// Gets the restart command
        /// </summary>
        public ICommand RestartCommand { get; private set; }

        /// <summary>
        /// Gets the exit command
        /// </summary>
        public ICommand ExitCommand { get; private set; }

        private Direction Direction_Player0
        {
            get
            {
                Direction k = default(Direction);

                if (Keyboard.IsKeyDown(Key.Up))
                {
                    k = Direction.Up;
                }
                else if (Keyboard.IsKeyDown(Key.Down))
                {
                    k = Direction.Down;
                }
                else if (Keyboard.IsKeyDown(Key.Left))
                {
                    k = Direction.Left;
                }
                else if (Keyboard.IsKeyDown(Key.Right))
                {
                    k = Direction.Right;
                }
                else
                {
                    k = Direction.Stay;
                }

                return k;
            }
        }

        private Direction Direction_Player1
        {
            get
            {
                Direction k = default(Direction);

                if (Keyboard.IsKeyDown(Key.W))
                {
                    k = Direction.Up;
                }
                else if (Keyboard.IsKeyDown(Key.S))
                {
                    k = Direction.Down;
                }
                else if (Keyboard.IsKeyDown(Key.A))
                {
                    k = Direction.Left;
                }
                else if (Keyboard.IsKeyDown(Key.D))
                {
                    k = Direction.Right;
                }
                else
                {
                    k = Direction.Stay;
                }

                return k;
            }
        }

        private List<Enemy> Enemies
        {
            get
            {
                List<Enemy> enemies = new List<Enemy>();

                foreach (Base item in this.O2)
                {
                    if (item is Enemy)
                    {
                        enemies.Add((Enemy)item);
                    }
                }

                return enemies;
            }
        }

        private List<Wall> Walls
        {
            get
            {
                List<Wall> walls = new List<Wall>();

                foreach (Base item in this.O2)
                {
                    if (item is Wall)
                    {
                        walls.Add((Wall)item);
                    }
                }

                return walls;
            }
        }

        private List<Bullet> Bullets
        {
            get
            {
                List<Bullet> seged = new List<Bullet>();

                foreach (Base item in this.O2)
                {
                    if (item is Bullet)
                    {
                        seged.Add((Bullet)item);
                    }
                }

                return seged;
            }
        }

        /// <summary>
        /// Handles player movement
        /// </summary>
        /// <param name="sender">the sender of the event</param>
        /// <param name="e">the event arguments passed by the sender</param>
        public void PlayerMove(object sender, EventArgs e)
        {
            if (this.Gameplay.Players.Count() != 0 && this.Gameplay.Players.ElementAt(0).Alive)
            {
                if (this.Direction_Player0 != Direction.Stay)
                {
                    this.Gameplay.Players.ElementAt(0).FacingDirection = this.Direction_Player0;
                }

                this.Gameplay.Players.ElementAt(0).MovingDirection = this.Direction_Player0;
            }

            if (this.Gameplay.Players.Count() == 2 && this.Gameplay.Players.ElementAt(1).Alive)
            {
                if (this.Gameplay.Players.Count() == 2 && this.Direction_Player1 != Direction.Stay)
                {
                    this.Gameplay.Players.ElementAt(1).FacingDirection = this.Direction_Player1;
                }

                this.Gameplay.Players.ElementAt(1).MovingDirection = this.Direction_Player1;
            }

            this.Logic.PlayerMove(this.Gameplay.Players, this.O2);
        }

        /// <summary>
        /// Handles the firing by player tanks
        /// </summary>
        /// <param name="key">the key that was pressed</param>
        public void PlayerShoot(Key key)
        {
            switch (key)
            {
                case Key.Space:
                    if (this.Gameplay.Players.ElementAt(0).Alive)
                    {
                        this.Logic.Shoot(this.Gameplay.Players.ElementAt(0), this.Bullets, this.O2);
                    }

                    break;
                case Key.G:
                    if (this.Gameplay.Players.ElementAt(1).Alive)
                    {
                        this.Logic.Shoot(this.Gameplay.Players.ElementAt(1), this.Bullets, this.O2);
                    }

                    break;
            }
        }

        /// <summary>
        /// Event for spawning enemies
        /// </summary>
        /// <param name="sender">the sender object</param>
        /// <param name="e">the event arguments</param>
        public void EnemyAppear(object sender, EventArgs e)
        {
            this.Logic.EnemyAppearance(this.Gameplay.Players, this.O2);
        }

        /// <summary>
        /// Event for moving enemies
        /// </summary>
        /// <param name="sender">the sender object></param>
        /// <param name="e">the event arguments</param>
        public void EnemyMove(object sender, EventArgs e)
        {
            this.Logic.EnemyMove(this.Enemies, this.O2);
        }

        /// <summary>
        /// Event for enemies shooting
        /// </summary>
        /// <param name="sender">the sender object</param>
        /// <param name="e">the event arguments</param>
        public void EnemyShoot(object sender, EventArgs e)
        {
            this.Logic.EnemyShoot(this.Enemies, this.Bullets, this.O2);
        }

        /// <summary>
        /// Event for bullet movement
        /// </summary>
        /// <param name="sender">the sender object</param>
        /// <param name="e">the event arguments</param>
        public void BulletsMove(object sender, EventArgs e)
        {
            this.Logic.BulletsMovement(this.Bullets);
        }

        /// <summary>
        /// Event for bullet collision detection
        /// </summary>
        /// <param name="sender">the sender object</param>
        /// <param name="e">the event arguments</param>
        public void Shot(object sender, EventArgs e)
        {
            this.Logic.Shot(this.Gameplay, this.Bullets, this.O2);
        }

        /// <summary>
        /// Event for cleaning up destroyed game objects
        /// </summary>
        /// <param name="sender">the sender object</param>
        /// <param name="e">the event arguments</param>
        public void Graveyard(object sender, EventArgs e)
        {
            this.Logic.Graveyard(this.O2, this.Gameplay);
        }

        /// <summary>
        /// Event for powerup spawning
        /// </summary>
        /// <param name="sender">the sender object</param>
        /// <param name="e">the event arguments</param>
        public void PowerUp(object sender, EventArgs e)
        {
            this.Logic.PowerUp(this.O2);
        }
    }
}