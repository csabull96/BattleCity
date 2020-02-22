// <copyright file="StartUpParameters.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BattleCity.WPF
{
    using System;
    using System.IO;
    using System.Windows;
    using System.Windows.Media;

    /// <summary>
    /// Class used for setting the startup parameters of the game
    /// </summary>
    public partial class StartUpParameters : Window
    {
        private static Random random = new Random();

        private int numberOfPlayers = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="StartUpParameters"/> class.
        /// </summary>
        public StartUpParameters()
        {
            this.InitializeComponent();
        }

        private void Singleplayer_Click(object sender, RoutedEventArgs e)
        {
            this.sp.Background = Brushes.Aqua;
            this.mp.Background = Brushes.LightGray;
            this.numberOfPlayers = 1;
        }

        private void Multiplayer_Click(object sender, RoutedEventArgs e)
        {
            this.sp.Background = Brushes.LightGray;
            this.mp.Background = Brushes.Aqua;
            this.numberOfPlayers = 2;
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            string parameters = this.numberOfPlayers.ToString() + ";" + random.Next(1, 2);

            File.WriteAllText("parameters.txt", parameters);

            MainWindow mw = new MainWindow();
            this.Close();
            mw.ShowDialog();
        }
    }
}
