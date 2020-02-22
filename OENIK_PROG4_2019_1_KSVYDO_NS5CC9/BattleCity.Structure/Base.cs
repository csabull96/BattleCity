// <copyright file="Base.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BattleCity.Structure
{
    using System.Windows;
    using GalaSoft.MvvmLight;

    /// <summary>
    /// Provides a base for all game objects
    /// </summary>
    public abstract class Base : ObservableObject
    {
        private static int size = 60;

        private Rect body;
        private int life;

        /// <summary>
        /// Initializes a new instance of the <see cref="Base"/> class.
        /// </summary>
        /// <param name="left">the left coordinate of the object</param>
        /// <param name="top">the top coordinate of the object</param>
        public Base(double left, double top)
        {
            this.Body = new Rect(left, top, size, size);
        }

        /// <summary>
        /// Gets or sets the body of the object
        /// </summary>
        public Rect Body
        {
            get { return this.body; }
            set { this.Set(ref this.body, value); }
        }

        /// <summary>
        /// Gets or sets the life of the object
        /// </summary>
        public int Life
        {
            get { return this.life; }
            set { this.Set(ref this.life, value); }
        }

        /// <summary>
        /// Gets a value indicating whether the object is alive
        /// </summary>
        public bool Alive
        {
            get { return this.Life > 0; }
        }

        /// <summary>
        /// Moves the object
        /// </summary>
        /// <param name="dx">x component of the movement</param>
        /// <param name="dy">y component of the movement</param>
        public void Move(double dx, double dy)
        {
            this.body.X += dx;
            this.body.Y += dy;
        }

        /// <summary>
        /// Respawns the object if it is a player
        /// </summary>
        public void PlayerRespawn()
        {
            if (this is Player && this.Alive)
            {
                switch (((Player)this).Id)
                {
                    case 0:
                        this.body.X = 480;
                        this.body.Y = 720;
                        break;
                    case 1:
                        this.body.X = 240;
                        this.body.Y = 720;
                        break;
                }
            }
        }

        /// <summary>
        /// Respawns the object if it is a player the the given coordinates
        /// </summary>
        /// <param name="x">the x coordinate of the respawn</param>
        /// <param name="y">the y coordinate of the respawn</param>
        public void PlayerRespawn(int x, int y)
        {
            if (this is Player && this.Alive)
            {
                switch (((Player)this).Id)
                {
                    case 0:
                        this.body.X = x;
                        this.body.Y = y;
                        break;
                    case 1:
                        this.body.X = x;
                        this.body.Y = y;
                        break;
                }
            }
        }
    }
}