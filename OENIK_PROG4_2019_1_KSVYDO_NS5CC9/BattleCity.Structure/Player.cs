// <copyright file="Player.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BattleCity.Structure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Game object for player tanks
    /// </summary>
    public class Player : Tank
    {
        /// <summary>
        /// The SPEED of the tank
        /// </summary>
        public const int SPEED = 4;

        /// <summary>
        /// The SPEED of projectiles fired by the player
        /// </summary>
        public const int BULLETSPEED = 8;

        private static int id = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="left">the left coordinate of the tank</param>
        /// <param name="top">the top coordinate of the tank</param>
        public Player(double left, double top)
            : base(left, top)
        {
            this.Id = id++;
            this.Upgrade = 0;
            this.Speed = SPEED;
            this.BulletSpeed = BULLETSPEED;
            this.Life = 2;
        }

        /// <summary>
        /// Gets or sets the id of the player
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the current upgrade level
        /// </summary>
        public int Upgrade { get; set; }
    }
}
