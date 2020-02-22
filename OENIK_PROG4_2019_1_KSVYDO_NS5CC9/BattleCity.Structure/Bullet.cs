// <copyright file="Bullet.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BattleCity.Structure
{
    using System.Windows;
    using System.Windows.Media;

    /// <summary>
    /// Game object for projectiles
    /// </summary>
    public class Bullet : Base
    {
        private static int size = 10;

        /// <summary>
        /// Initializes a new instance of the <see cref="Bullet"/> class.
        /// </summary>
        /// <param name="tank">the tank that fired the projectile</param>
        /// <param name="direction">the direction of the projectile</param>
        /// <param name="speed">the SPEED of the projectile</param>
        /// <param name="left">the left coordinate of the projectile</param>
        /// <param name="top">the top coordinate of the projectile</param>
        public Bullet(Tank tank, Direction direction, int speed, double left, double top)
            : base(left, top)
        {
            this.Body = new Rect(left, top, size, size);
            this.Tank = tank;
            this.Direction = direction;
            this.Life = 1;
            this.Speed = speed;
        }

        /// <summary>
        /// Gets or sets the projectile's direction
        /// </summary>
        public Direction Direction { get; set; }

        /// <summary>
        /// Gets or sets the projectile's parent tank
        /// </summary>
        public Tank Tank { get; set; }

        /// <summary>
        /// Gets or sets the SPEED of the projectile
        /// </summary>
        public int Speed { get; set; }

        /// <summary>
        /// Calculates the x coordinate of the projectile
        /// </summary>
        /// <param name="t">the parent tank</param>
        /// <returns>the initial x coordinate of the projectile</returns>
        public static double X(Tank t)
        {
            return t.Body.Left + ((t.Body.Width - size) / 2);
        }

        /// <summary>
        /// Calculates the y coordinate of the projectile
        /// </summary>
        /// <param name="t">the parent tank</param>
        /// <returns>the initial y coordinate of the projectile</returns>
        public static double Y(Tank t)
        {
            return t.Body.Top + ((t.Body.Height - size) / 2);
        }

        /// <summary>
        /// Handles hit detection of the bullet
        /// </summary>
        /// <param name="element">the element to check collision with</param>
        /// <returns>whether the projectile is touching the element or not</returns>
        public bool Shot(Base element)
        {
            if (this != element && this.Tank != element)
            {
                Geometry metszet = Geometry.Combine(new RectangleGeometry(this.Body), new RectangleGeometry(element.Body), GeometryCombineMode.Intersect, null);

                if (metszet.GetArea() > 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}