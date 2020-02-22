// <copyright file="PowerUp.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BattleCity.Structure
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Enumeration items for powerup types
    /// </summary>
    public enum PowerUpType
    {
        /// <summary>
        /// Powerup that makes your tank stronger
        /// </summary>
        Stronger,

        /// <summary>
        /// Powerup that destroys all enemy tanks
        /// </summary>
        Bomb,

        /// <summary>
        /// Powerup that gives your tank an extra life
        /// </summary>
        Life
    }

    /// <summary>
    /// Game object for powerups
    /// </summary>
    public class PowerUp : Base
    {
        private Random random = new Random();

        /// <summary>
        /// Initializes a new instance of the <see cref="PowerUp"/> class.
        /// </summary>
        /// <param name="left">the left coordinate of the object</param>
        /// <param name="top">the top coordinate of the object</param>
        public PowerUp(double left, double top)
            : base(left, top)
        {
            this.SW = new Stopwatch();
            this.SW.Start();

            this.Life = 1;

            switch (this.random.Next(3))
            {
                case 0:
                    this.Type = PowerUpType.Stronger;
                    break;
                case 1:
                    this.Type = PowerUpType.Bomb;
                    break;
                case 2:
                    this.Type = PowerUpType.Life;
                    break;
            }
        }

        /// <summary>
        /// Gets or sets the type of the powerup
        /// </summary>
        public PowerUpType Type { get; set; }

        /// <summary>
        /// Gets or sets the stopwatch of the powerup
        /// </summary>
        public Stopwatch SW { get; set; }
    }
}
