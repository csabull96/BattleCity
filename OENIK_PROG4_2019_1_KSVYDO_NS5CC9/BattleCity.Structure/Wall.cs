// <copyright file="Wall.cs" company="PlaceholderCompany">
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
    /// Game object for walls
    /// </summary>
    public class Wall : Obstacle
    {
        private readonly Random random = new Random();

        /// <summary>
        /// Initializes a new instance of the <see cref="Wall"/> class.
        /// </summary>
        /// <param name="left">left coordinate of the wall</param>
        /// <param name="top">top coordinate of the wall</param>
        /// <param name="unbreakable">whether the wall is unbreakable or not</param>
        public Wall(double left, double top, bool unbreakable)
           : base(left, top)
        {
            this.Demaged = this.random.Next(4);

            this.Unbreakable = unbreakable;

            if (this.Unbreakable)
            {
                this.Life = int.MaxValue;
            }
            else
            {
                this.Life = 2;
            }
        }

        /// <summary>
        /// Gets or sets the amount the wall is damaged
        /// </summary>
        public int Demaged { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the wall is unbreakable
        /// </summary>
        public bool Unbreakable { get; set; }
    }
}