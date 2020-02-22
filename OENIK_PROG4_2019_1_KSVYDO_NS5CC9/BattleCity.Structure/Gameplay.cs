// <copyright file="Gameplay.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BattleCity.Structure
{
    using System.Collections.Generic;
    using System.IO;
    using GalaSoft.MvvmLight;

    /// <summary>
    /// Game object that contains information about the current game
    /// </summary>
    public class Gameplay : ObservableObject
    {
        private int score;
        private int highScore;

        /// <summary>
        /// Initializes a new instance of the <see cref="Gameplay"/> class.
        /// </summary>
        /// <param name="elements">list of game elements</param>
        public Gameplay(IList<Base> elements)
        {
            this.Players = new List<Player>();

            this.Seperate(elements);

            this.Score = 0;

            this.HighScore = int.Parse(File.ReadAllText("highscore.txt"));
        }

        /// <summary>
        /// Gets or sets the current score
        /// </summary>
        public int Score
        {
            get { return this.score; }

            set { this.Set(ref this.score, value); }
        }

        /// <summary>
        /// Gets or sets the high score
        /// </summary>
        public int HighScore
        {
            get { return this.highScore; }

            set { this.Set(ref this.highScore, value); }
        }

        /// <summary>
        /// Gets or sets the list of players in the game
        /// </summary>
        public List<Player> Players { get; set; }

        /// <summary>
        /// Gets or sets the Eagle(base) object
        /// </summary>
        public Eagle Eagle { get; set; }

        private void Seperate(IList<Base> elements)
        {
            foreach (Base element in elements)
            {
                if (element is Player)
                {
                    this.Players.Add((Player)element);
                }
                else if (element is Eagle)
                {
                    this.Eagle = (Eagle)element;
                }
            }
        }
    }
}