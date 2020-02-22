// <copyright file="Obstacle.cs" company="PlaceholderCompany">
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
    /// Game objects for obstacles (game objects that prohibit movement)
    /// </summary>
    public class Obstacle : Base
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Obstacle"/> class.
        /// </summary>
        /// <param name="left">left coordinate of the obstacle</param>
        /// <param name="top">top coordinate of the obstacle</param>
        public Obstacle(double left, double top)
            : base(left, top)
        {
        }
    }
}