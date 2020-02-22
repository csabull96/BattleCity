// <copyright file="Enemy.cs" company="PlaceholderCompany">
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
    using System.Windows.Media;

    /// <summary>
    /// Enumeration items for item corners
    /// </summary>
    public enum Location
    {
        /// <summary>
        /// the top left corner of the element
        /// </summary>
        TopLeft,

        /// <summary>
        /// the bottom left corner of the element
        /// </summary>
        BottomLeft,

        /// <summary>
        /// the top right corner of the element
        /// </summary>
        TopRight,

        /// <summary>
        /// the bottom right corner of the element
        /// </summary>
        BottomRight
    }

    /// <summary>
    /// Enumeration items for enemy types
    /// </summary>
    public enum EnemyType
    {
        /// <summary>
        /// Normal enemy tank
        /// </summary>
        Normal,

        /// <summary>
        /// Strong enemy tank
        /// </summary>
        Strong
    }

    /// <summary>
    /// Game object for enemy tanks
    /// </summary>
    public class Enemy : Tank
    {
        private Rect tl = new Rect(0, 0, 390, 390);
        private Rect bl = new Rect(0, 390, 390, 390);
        private Rect tr = new Rect(390, 0, 390, 390);
        private Rect br = new Rect(390, 390, 390, 390);

        /// <summary>
        /// Initializes a new instance of the <see cref="Enemy"/> class.
        /// </summary>
        /// <param name="left">the left coordinate of the element</param>
        /// <param name="top">the top coordinate of the element</param>
        public Enemy(int left, int top)
           : base(left, top)
        {
            Random rnd = new Random();

            switch (rnd.Next(5))
            {
                case 0:
                    this.Type = EnemyType.Strong;
                    this.Speed = 2;
                    this.BulletSpeed = 8;
                    this.Life = 3;
                    this.Value = 300;
                    break;
                default:
                    this.Type = EnemyType.Normal;
                    this.Speed = 4;
                    this.BulletSpeed = 8;
                    this.Life = 1;
                    this.Value = 100;
                    break;
            }
        }

        /// <summary>
        /// Gets or sets the value of the enemy
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// Gets or sets the type of the enemy
        /// </summary>
        public EnemyType Type { get; set; }

        /// <summary>
        /// Gets the location of the enemy
        /// </summary>
        public Location Where
        {
            get
            {
                double min = -1;

                Location position = default(Location);

                Rect enemy = new Rect(this.Body.Left, this.Body.Top, this.Body.Width, this.Body.Right);

                Geometry tl_enemy = Geometry.Combine(new RectangleGeometry(enemy), new RectangleGeometry(this.tl), GeometryCombineMode.Intersect, null);
                Geometry bl_enemy = Geometry.Combine(new RectangleGeometry(enemy), new RectangleGeometry(this.bl), GeometryCombineMode.Intersect, null);
                Geometry tr_enemy = Geometry.Combine(new RectangleGeometry(enemy), new RectangleGeometry(this.tr), GeometryCombineMode.Intersect, null);
                Geometry br_enemy = Geometry.Combine(new RectangleGeometry(enemy), new RectangleGeometry(this.br), GeometryCombineMode.Intersect, null);

                if (tl_enemy.GetArea() > min)
                {
                    min = tl_enemy.GetArea();
                    position = Location.TopLeft;
                }

                if (bl_enemy.GetArea() > min)
                {
                    min = bl_enemy.GetArea();
                    position = Location.BottomLeft;
                }

                if (tr_enemy.GetArea() > min)
                {
                    min = tr_enemy.GetArea();
                    position = Location.TopRight;
                }

                if (br_enemy.GetArea() > min)
                {
                    min = br_enemy.GetArea();
                    position = Location.BottomRight;
                }

                return position;
            }
        }
    }
}