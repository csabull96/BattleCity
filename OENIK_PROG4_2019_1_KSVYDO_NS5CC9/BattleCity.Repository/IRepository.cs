// <copyright file="IRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BattleCity.Repository
{
    using System.Collections.Generic;
    using BattleCity.Structure;

    /// <summary>y
    /// Interface for the repository
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Loads the level
        /// </summary>
        /// <param name="numberOfPlayers">the number of players</param>
        /// <param name="battleField">number of the level to load</param>
        /// <param name="elements">list of all elements in the level</param>
        void LoadBattleField(int numberOfPlayers, int battleField, IList<Base> elements);
    }
}
