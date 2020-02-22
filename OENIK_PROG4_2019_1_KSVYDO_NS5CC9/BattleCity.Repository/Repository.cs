// <copyright file="Repository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BattleCity.Repository
{
    using System.Collections.Generic;
    using System.IO;
    using BattleCity.Structure;

    /// <summary>
    /// Interface for the repository
    /// </summary>
    public class Repository : IRepository
    {
        /// <summary>
        /// Loads the level
        /// </summary>
        /// <param name="numberOfPlayers">the number of players</param>
        /// <param name="battleField">number of the level to load</param>
        /// <param name="elements">list of all elements in the level</param>
        public void LoadBattleField(int numberOfPlayers, int battleField, IList<Base> elements)
        {
            string[] bf = File.ReadAllLines(battleField + ".lvl");

            int x = int.Parse(bf[0]);
            int y = int.Parse(bf[1]);

            elements.Add(new Player(480, 720));
            if (numberOfPlayers > 1)
            {
                elements.Add(new Player(240, 720));
            }

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if (bf[j + 2][i] == 'w')
                    {
                        elements.Add(new Wall(i * 60, j * 60, false));
                    }
                    else if (bf[j + 2][i] == 's')
                    {
                        elements.Add(new Wall(i * 60, j * 60, true));
                    }
                    else if (bf[j + 2][i] == 'e')
                    {
                        elements.Add(new Eagle(i * 60, j * 60));
                    }
                }
            }
        }
    }
}
