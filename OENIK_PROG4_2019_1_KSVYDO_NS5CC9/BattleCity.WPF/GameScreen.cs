// <copyright file="GameScreen.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BattleCity.WPF
{
    using System;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Threading;
    using BattleCity.Structure;
    using BattleCity.ViewModel;

    /// <summary>
    /// Defines how the display works
    /// </summary>
    public class GameScreen : FrameworkElement
    {
        private readonly ViewModel viewModel;

        private DispatcherTimer dt0;
        private DispatcherTimer dt1;
        private DispatcherTimer dt2;
        private DispatcherTimer fps;

        private ImageBrush eagle0;
        private ImageBrush eagle1;
        private ImageBrush eagle2;
        private ImageBrush eagle3;
        private ImageBrush eagle4;

        private ImageBrush wall;
        private ImageBrush walldemaged0;
        private ImageBrush walldemaged1;
        private ImageBrush walldemaged2;
        private ImageBrush walldemaged3;
        private ImageBrush wallunbreakable;

        private ImageBrush yellowup;
        private ImageBrush yellowdown;
        private ImageBrush yellowleft;
        private ImageBrush yellowright;

        private ImageBrush yellowstrongup;
        private ImageBrush yellowstrongdown;
        private ImageBrush yellowstrongleft;
        private ImageBrush yellowstrongright;

        private ImageBrush greenup;
        private ImageBrush greendown;
        private ImageBrush greenleft;
        private ImageBrush greenright;

        private ImageBrush greenstrongup;
        private ImageBrush greenstrongdown;
        private ImageBrush greenstrongleft;
        private ImageBrush greenstrongright;

        private ImageBrush enemyup;
        private ImageBrush enemydown;
        private ImageBrush enemyleft;
        private ImageBrush enemyright;

        private ImageBrush enemystrongup;
        private ImageBrush enemystrongdown;
        private ImageBrush enemystrongbal;
        private ImageBrush enemystrongjobb;

        private ImageBrush bullet;

        private ImageBrush powerupstronger;
        private ImageBrush powerupbomb;
        private ImageBrush poweruplife;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameScreen"/> class.
        /// </summary>
        public GameScreen()
        {
            this.viewModel = (ViewModel)App.Current.Resources["VM"];

            this.Loaded += this.GameScreen_Loaded;
        }

        /// <summary>
        /// Handles drawing all the game objects
        /// </summary>
        /// <param name="drawingContext">The current DrawingContext</param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            Random rnd = new Random();

            foreach (Base element in this.viewModel.O2)
            {
                if (element is Bullet)
                {
                    drawingContext.DrawRectangle(this.bullet, null, element.Body);
                }

                if (element is PowerUp)
                {
                    switch (((PowerUp)element).Type)
                    {
                        case PowerUpType.Stronger:
                            drawingContext.DrawRectangle(this.powerupstronger, null, element.Body);
                            break;
                        case PowerUpType.Bomb:
                            drawingContext.DrawRectangle(this.powerupbomb, null, element.Body);
                            break;
                        case PowerUpType.Life:
                            drawingContext.DrawRectangle(this.poweruplife, null, element.Body);
                            break;
                    }
                }

                if (element is Wall)
                {
                    if (((Wall)element).Unbreakable)
                    {
                        drawingContext.DrawRectangle(this.wallunbreakable, null, element.Body);
                    }
                    else
                    {
                        switch (((Wall)element).Life)
                        {
                            default:
                                drawingContext.DrawRectangle(this.wall, null, element.Body);
                                break;
                            case 1:
                                switch (((Wall)element).Demaged)
                                {
                                    case 0:
                                        drawingContext.DrawRectangle(this.walldemaged0, null, element.Body);
                                        break;
                                    case 1:
                                        drawingContext.DrawRectangle(this.walldemaged1, null, element.Body);
                                        break;
                                    case 2:
                                        drawingContext.DrawRectangle(this.walldemaged2, null, element.Body);
                                        break;
                                    case 3:
                                        drawingContext.DrawRectangle(this.walldemaged3, null, element.Body);
                                        break;
                                }

                                break;
                        }
                    }
                }

                if (element is Enemy)
                {
                    switch (((Enemy)element).Type)
                    {
                        case EnemyType.Normal:
                            switch (((Enemy)element).MovingDirection)
                            {
                                case Direction.Up:
                                    drawingContext.DrawRectangle(this.enemyup, null, element.Body);
                                    break;
                                case Direction.Down:
                                    drawingContext.DrawRectangle(this.enemydown, null, element.Body);
                                    break;
                                case Direction.Left:
                                    drawingContext.DrawRectangle(this.enemyleft, null, element.Body);
                                    break;
                                case Direction.Right:
                                    drawingContext.DrawRectangle(this.enemyright, null, element.Body);
                                    break;
                            }

                            break;
                        case EnemyType.Strong:
                            switch (((Enemy)element).MovingDirection)
                            {
                                case Direction.Up:
                                    drawingContext.DrawRectangle(this.enemystrongup, null, element.Body);
                                    break;
                                case Direction.Down:
                                    drawingContext.DrawRectangle(this.enemystrongdown, null, element.Body);
                                    break;
                                case Direction.Left:
                                    drawingContext.DrawRectangle(this.enemystrongbal, null, element.Body);
                                    break;
                                case Direction.Right:
                                    drawingContext.DrawRectangle(this.enemystrongjobb, null, element.Body);
                                    break;
                            }

                            break;
                    }
                }
            }

            switch (this.viewModel.Gameplay.Eagle.Life)
            {
                case 4:
                    drawingContext.DrawRectangle(this.eagle0, null, this.viewModel.Gameplay.Eagle.Body);
                    break;
                case 3:
                    drawingContext.DrawRectangle(this.eagle1, null, this.viewModel.Gameplay.Eagle.Body);
                    break;
                case 2:
                    drawingContext.DrawRectangle(this.eagle2, null, this.viewModel.Gameplay.Eagle.Body);
                    break;
                case 1:
                    drawingContext.DrawRectangle(this.eagle3, null, this.viewModel.Gameplay.Eagle.Body);
                    break;
                case 0:
                    drawingContext.DrawRectangle(this.eagle4, null, this.viewModel.Gameplay.Eagle.Body);
                    break;
            }

            foreach (Player player in this.viewModel.Gameplay.Players)
            {
                if (player.Upgrade == 0)
                {
                    if (player.Alive && player.Id == 0)
                    {
                        switch (player.FacingDirection)
                        {
                            case Direction.Up:
                                drawingContext.DrawRectangle(this.yellowup, null, player.Body);
                                break;
                            case Direction.Down:
                                drawingContext.DrawRectangle(this.yellowdown, null, player.Body);
                                break;
                            case Direction.Left:
                                drawingContext.DrawRectangle(this.yellowleft, null, player.Body);
                                break;
                            case Direction.Right:
                                drawingContext.DrawRectangle(this.yellowright, null, player.Body);
                                break;
                        }
                    }

                    if (player.Alive && player.Id == 1)
                    {
                        switch (player.FacingDirection)
                        {
                            case Direction.Up:
                                drawingContext.DrawRectangle(this.greenup, null, player.Body);
                                break;
                            case Direction.Down:
                                drawingContext.DrawRectangle(this.greendown, null, player.Body);
                                break;
                            case Direction.Left:
                                drawingContext.DrawRectangle(this.greenleft, null, player.Body);
                                break;
                            case Direction.Right:
                                drawingContext.DrawRectangle(this.greenright, null, player.Body);
                                break;
                        }
                    }
                }
                else
                {
                    if (player.Alive && player.Id == 0)
                    {
                        switch (player.FacingDirection)
                        {
                            case Direction.Up:
                                drawingContext.DrawRectangle(this.yellowstrongup, null, player.Body);
                                break;
                            case Direction.Down:
                                drawingContext.DrawRectangle(this.yellowstrongdown, null, player.Body);
                                break;
                            case Direction.Left:
                                drawingContext.DrawRectangle(this.yellowstrongleft, null, player.Body);
                                break;
                            case Direction.Right:
                                drawingContext.DrawRectangle(this.yellowstrongright, null, player.Body);
                                break;
                        }
                    }

                    if (player.Alive && player.Id == 1)
                    {
                        switch (player.FacingDirection)
                        {
                            case Direction.Up:
                                drawingContext.DrawRectangle(this.greenstrongup, null, player.Body);
                                break;
                            case Direction.Down:
                                drawingContext.DrawRectangle(this.greenstrongdown, null, player.Body);
                                break;
                            case Direction.Left:
                                drawingContext.DrawRectangle(this.greenstrongleft, null, player.Body);
                                break;
                            case Direction.Right:
                                drawingContext.DrawRectangle(this.greenstrongright, null, player.Body);
                                break;
                        }
                    }
                }
            }
        }

        private void GameScreen_Loaded(object sender, RoutedEventArgs e)
        {
            this.dt0 = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(15) };
            this.dt0.Tick += new EventHandler(this.viewModel.PlayerMove);
            this.dt0.Tick += new EventHandler(this.viewModel.EnemyMove);
            this.dt0.Tick += new EventHandler(this.viewModel.EnemyShoot);
            this.dt0.Tick += new EventHandler(this.viewModel.BulletsMove);
            this.dt0.Tick += new EventHandler(this.viewModel.Shot);
            this.dt0.Tick += new EventHandler(this.viewModel.Graveyard);
            this.dt0.Start();

            this.dt1 = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(4) };
            this.dt1.Tick += new EventHandler(this.viewModel.EnemyAppear);
            this.dt1.Start();

            this.dt2 = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(30) };
            this.dt2.Tick += new EventHandler(this.viewModel.PowerUp);
            this.dt2.Start();

            this.fps = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(15) };
            this.fps.Tick += new EventHandler(this.GameScreen_Refresh);
            this.fps.Start();

            Window.GetWindow(this).KeyDown += this.GameScreen_KeyDown;

            this.LoadImages();
        }

        private void GameScreen_KeyDown(object sender, KeyEventArgs e)
        {
            this.viewModel.PlayerShoot(e.Key);
        }

        private void GameScreen_Refresh(object sender, EventArgs e)
        {
            if (this.viewModel.Gameplay.Eagle.Alive)
            {
                this.InvalidateVisual();
            }
            else
            {
                this.dt0.IsEnabled = false;
                this.dt1.IsEnabled = false;
                this.dt2.IsEnabled = false;
                this.fps.IsEnabled = false;

                this.viewModel.O2.Clear();

                this.viewModel.Gameplay.Players.Clear();

                this.InvalidateVisual();

                MessageBox.Show("GAME OVER");
            }
        }

        private void LoadImages()
        {
            this.eagle0 = new ImageBrush() { ImageSource = new BitmapImage(new Uri(@"nyersanyag\oe1.png", UriKind.RelativeOrAbsolute)) };
            this.eagle1 = new ImageBrush() { ImageSource = new BitmapImage(new Uri(@"nyersanyag\oe2.png", UriKind.RelativeOrAbsolute)) };
            this.eagle2 = new ImageBrush() { ImageSource = new BitmapImage(new Uri(@"nyersanyag\oe3.png", UriKind.RelativeOrAbsolute)) };
            this.eagle3 = new ImageBrush() { ImageSource = new BitmapImage(new Uri(@"nyersanyag\oe4.png", UriKind.RelativeOrAbsolute)) };
            this.eagle4 = new ImageBrush() { ImageSource = new BitmapImage(new Uri(@"nyersanyag\oe5.png", UriKind.RelativeOrAbsolute)) };

            this.wall = new ImageBrush() { ImageSource = new BitmapImage(new Uri(@"nyersanyag\fal.png", UriKind.RelativeOrAbsolute)) };
            this.walldemaged0 = new ImageBrush() { ImageSource = new BitmapImage(new Uri(@"nyersanyag\falrombolt0.png", UriKind.RelativeOrAbsolute)) };
            this.walldemaged1 = new ImageBrush() { ImageSource = new BitmapImage(new Uri(@"nyersanyag\falrombolt1.png", UriKind.RelativeOrAbsolute)) };
            this.walldemaged2 = new ImageBrush() { ImageSource = new BitmapImage(new Uri(@"nyersanyag\falrombolt2.png", UriKind.RelativeOrAbsolute)) };
            this.walldemaged3 = new ImageBrush() { ImageSource = new BitmapImage(new Uri(@"nyersanyag\falrombolt3.png", UriKind.RelativeOrAbsolute)) };
            this.wallunbreakable = new ImageBrush() { ImageSource = new BitmapImage(new Uri(@"nyersanyag\faltorhetetlen.png", UriKind.RelativeOrAbsolute)) };

            this.yellowup = new ImageBrush() { ImageSource = new BitmapImage(new Uri(@"nyersanyag\sargafel.png", UriKind.RelativeOrAbsolute)) };
            this.yellowdown = new ImageBrush() { ImageSource = new BitmapImage(new Uri(@"nyersanyag\sargale.png", UriKind.RelativeOrAbsolute)) };
            this.yellowleft = new ImageBrush() { ImageSource = new BitmapImage(new Uri(@"nyersanyag\sargabal.png", UriKind.RelativeOrAbsolute)) };
            this.yellowright = new ImageBrush() { ImageSource = new BitmapImage(new Uri(@"nyersanyag\sargajobb.png", UriKind.RelativeOrAbsolute)) };

            this.yellowstrongup = new ImageBrush() { ImageSource = new BitmapImage(new Uri(@"nyersanyag\sargabengafel.png", UriKind.RelativeOrAbsolute)) };
            this.yellowstrongdown = new ImageBrush() { ImageSource = new BitmapImage(new Uri(@"nyersanyag\sargabengale.png", UriKind.RelativeOrAbsolute)) };
            this.yellowstrongleft = new ImageBrush() { ImageSource = new BitmapImage(new Uri(@"nyersanyag\sargabengabal.png", UriKind.RelativeOrAbsolute)) };
            this.yellowstrongright = new ImageBrush() { ImageSource = new BitmapImage(new Uri(@"nyersanyag\sargabengajobb.png", UriKind.RelativeOrAbsolute)) };

            this.greenup = new ImageBrush() { ImageSource = new BitmapImage(new Uri(@"nyersanyag\zoldfel.png", UriKind.RelativeOrAbsolute)) };
            this.greendown = new ImageBrush() { ImageSource = new BitmapImage(new Uri(@"nyersanyag\zoldle.png", UriKind.RelativeOrAbsolute)) };
            this.greenleft = new ImageBrush() { ImageSource = new BitmapImage(new Uri(@"nyersanyag\zoldbal.png", UriKind.RelativeOrAbsolute)) };
            this.greenright = new ImageBrush() { ImageSource = new BitmapImage(new Uri(@"nyersanyag\zoldjobb.png", UriKind.RelativeOrAbsolute)) };

            this.greenstrongup = new ImageBrush() { ImageSource = new BitmapImage(new Uri(@"nyersanyag\zoldbengafel.png", UriKind.RelativeOrAbsolute)) };
            this.greenstrongdown = new ImageBrush() { ImageSource = new BitmapImage(new Uri(@"nyersanyag\zoldbengale.png", UriKind.RelativeOrAbsolute)) };
            this.greenstrongleft = new ImageBrush() { ImageSource = new BitmapImage(new Uri(@"nyersanyag\zoldbengabal.png", UriKind.RelativeOrAbsolute)) };
            this.greenstrongright = new ImageBrush() { ImageSource = new BitmapImage(new Uri(@"nyersanyag\zoldbengajobb.png", UriKind.RelativeOrAbsolute)) };

            this.enemyup = new ImageBrush() { ImageSource = new BitmapImage(new Uri(@"nyersanyag\ellensegfel.png", UriKind.RelativeOrAbsolute)) };
            this.enemydown = new ImageBrush() { ImageSource = new BitmapImage(new Uri(@"nyersanyag\ellensegle.png", UriKind.RelativeOrAbsolute)) };
            this.enemyleft = new ImageBrush() { ImageSource = new BitmapImage(new Uri(@"nyersanyag\ellensegbal.png", UriKind.RelativeOrAbsolute)) };
            this.enemyright = new ImageBrush() { ImageSource = new BitmapImage(new Uri(@"nyersanyag\ellensegjobb.png", UriKind.RelativeOrAbsolute)) };

            this.enemystrongup = new ImageBrush() { ImageSource = new BitmapImage(new Uri(@"nyersanyag\bengafel.png", UriKind.RelativeOrAbsolute)) };
            this.enemystrongdown = new ImageBrush() { ImageSource = new BitmapImage(new Uri(@"nyersanyag\bengale.png", UriKind.RelativeOrAbsolute)) };
            this.enemystrongbal = new ImageBrush() { ImageSource = new BitmapImage(new Uri(@"nyersanyag\bengabal.png", UriKind.RelativeOrAbsolute)) };
            this.enemystrongjobb = new ImageBrush() { ImageSource = new BitmapImage(new Uri(@"nyersanyag\bengajobb.png", UriKind.RelativeOrAbsolute)) };

            this.bullet = new ImageBrush() { ImageSource = new BitmapImage(new Uri(@"nyersanyag\lovedek.png", UriKind.RelativeOrAbsolute)) };

            this.powerupstronger = new ImageBrush() { ImageSource = new BitmapImage(new Uri(@"nyersanyag\powerupstronger.png", UriKind.RelativeOrAbsolute)) };
            this.powerupbomb = new ImageBrush() { ImageSource = new BitmapImage(new Uri(@"nyersanyag\powerupbomb.png", UriKind.RelativeOrAbsolute)) };
            this.poweruplife = new ImageBrush() { ImageSource = new BitmapImage(new Uri(@"nyersanyag\poweruplife.png", UriKind.RelativeOrAbsolute)) };
        }
    }
}