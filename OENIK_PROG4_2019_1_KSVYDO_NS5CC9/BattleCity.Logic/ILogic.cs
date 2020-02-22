// <copyright file="ILogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BattleCity.Logic
{
    using System.Collections.Generic;
    using BattleCity.Structure;

    /// <summary>
    /// Interface for the Logic class
    /// </summary>
    public interface ILogic
    {
        /// <summary>
        /// Loads the level from a file
        /// </summary>
        /// <param name="numberOfPlayers">number of human players</param>
        /// <param name="battleField">number of the level</param>
        /// <param name="elements">list of elements on the level</param>
        void LoadBattleField(int numberOfPlayers, int battleField, IList<Base> elements);

        /// <summary>
        /// Moves the specified player tank
        /// </summary>
        /// <param name="players">list of players</param>
        /// <param name="elements">list of elements on the level</param>
        void PlayerMove(IList<Player> players, IList<Base> elements);

        /// <summary>
        /// Fires a projectile from the specified player tank
        /// </summary>
        /// <param name="tank">the tank that shoots</param>
        /// <param name="bullets">list of all bullets currently on the level</param>
        /// <param name="elements">list of all elements on the level</param>
        void Shoot(Tank tank, IList<Bullet> bullets, IList<Base> elements);

        /// <summary>
        /// Spawns an enemy tank
        /// </summary>
        /// <param name="players">list of players</param>
        /// <param name="elements">list of all elements on the level</param>
        void EnemyAppearance(IList<Player> players, IList<Base> elements);

        /// <summary>
        /// Moves an enemy tank
        /// </summary>
        /// <param name="enemies">list of enemy tanks</param>
        /// <param name="elements">list of all elements on the level</param>
        void EnemyMove(IList<Enemy> enemies, IList<Base> elements);

        /// <summary>
        /// Fires a projectile from an enemy tank
        /// </summary>
        /// <param name="enemies">list of enemy tanks</param>
        /// <param name="bullets">list of projectiles on the map</param>
        /// <param name="elements">list of all elements on the level</param>
        void EnemyShoot(IList<Enemy> enemies, IList<Bullet> bullets, IList<Base> elements);

        /// <summary>
        /// Handles the movement of projectiles
        /// </summary>
        /// <param name="bullets">list of projectiles on the map</param>
        void BulletsMovement(IList<Bullet> bullets);

        /// <summary>
        /// Handles the hit detection of projectiles
        /// </summary>
        /// <param name="gameplay">the current gameplay element</param>
        /// <param name="bullets">list of projectiles on the map</param>
        /// <param name="elements">list of all elements on the level</param>
        void Shot(Gameplay gameplay, IList<Bullet> bullets, IList<Base> elements);

        /// <summary>
        /// Deletes all inactive elements
        /// </summary>
        /// <param name="elements">list of all elements on the level</param>
        /// <param name="gameplay">the current gameplay element</param>
        void Graveyard(IList<Base> elements, Gameplay gameplay);

        /// <summary>
        /// Adds a new powerup to the level
        /// </summary>
        /// <param name="elements">list of all elements on the level</param>
        void PowerUp(IList<Base> elements);

        /// <summary>
        /// Terminates the game
        /// </summary>
        /// <param name="gameplay">the current gameplay element</param>
        void Exit(Gameplay gameplay);

        /// <summary>
        /// Restarts the game
        /// </summary>
        /// <param name="gameplay">the current gameplay element</param>
        void Restart(Gameplay gameplay);
    }
}