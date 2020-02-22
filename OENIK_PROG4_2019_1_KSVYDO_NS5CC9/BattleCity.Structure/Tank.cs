// <copyright file="Tank.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BattleCity.Structure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;

    /// <summary>
    /// Enumeration items for movement directions
    /// </summary>
    public enum Direction
    {
        /// <summary>
        /// Tank going up
        /// </summary>
        Up,

        /// <summary>
        /// Tank going down
        /// </summary>
        Down,

        /// <summary>
        /// Tank going left
        /// </summary>
        Left,

        /// <summary>
        /// Tank going right
        /// </summary>
        Right,

        /// <summary>
        /// Tank staying in it's place
        /// </summary>
        Stay
    }

    /// <summary>
    /// Game object for all tanks
    /// </summary>
    public class Tank : Base
    {
        private static int size = 50;

        /// <summary>
        /// Initializes a new instance of the <see cref="Tank"/> class.
        /// </summary>
        /// <param name="left">left coordinate of the tank</param>
        /// <param name="top">top coordinate of the tank</param>
        public Tank(double left, double top)
            : base(left, top)
        {
            this.Body = new Rect(left, top, size, size);
        }

        /// <summary>
        /// Gets or sets the moving direction of the tank
        /// </summary>
        public Direction MovingDirection { get; set; }

        /// <summary>
        /// Gets or sets the facing direction of the tank
        /// </summary>
        public Direction FacingDirection { get; set; }

        /// <summary>
        /// Gets or sets the SPEED of the tank
        /// </summary>
        public int Speed { get; set; }

        /// <summary>
        /// Gets or sets the SPEED of projectiles fired by the tank
        /// </summary>
        public int BulletSpeed { get; set; }
    }
}