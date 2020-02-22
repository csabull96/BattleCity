// <copyright file="Eagle.cs" company="PlaceholderCompany">
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
    /// Game object for the Eagle
    /// (acts as the player base in-game)
    /// </summary>
    public class Eagle : Obstacle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Eagle"/> class.
        /// </summary>
        /// <param name="left">the left coordinate of the element</param>
        /// <param name="top">the top coordinate of the element</param>
        public Eagle(double left, double top)
            : base(left, top)
        {
            this.Life = 4;
        }
    }
}