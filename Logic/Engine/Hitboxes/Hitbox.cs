using Microsoft.Xna.Framework;

namespace Fantasy.Logic.Engine.Hitboxes
{
    /// <summary>
    /// Generic Hitbox inherited by other collision boxes.
    /// </summary>
    public abstract class Hitbox
    {
        /// <summary>
        /// Set of rectangles that describes the boxes area.
        /// </summary>
        public HitboxGeometry geometry;
        /// <summary>
        /// Describes the the visual area of the Hitbox this RectangleSet belong to.
        /// </summary>
        public Rectangle visualArea;

        /// <summary>
        /// Generic inherited constructor. 
        /// </summary>
        public Hitbox() { }

        /// <summary>
        /// Gets this Hitboxes position as a point.
        /// </summary>
        /// <returns>Point representing this Hitboxes position.</returns>
        public Point GetPointPosition()
        {
            return geometry.position;
        }
        /// <summary>
        /// Gets this Hitboxes position as a Vector2.
        /// </summary>
        /// <param name="invertY">Determines if the Y value of the returned Vector2 is inversed. Used for drawing.</param>
        /// <returns>Vector2 representing this Hitboxes position.</returns>
        public Vector2 GetVectorPosition(bool invertY = true)
        {
            if (invertY)
            {
                return new Vector2(geometry.position.X, -geometry.position.Y);
            }
            else
            {
                return new Vector2(geometry.position.X, geometry.position.Y);
            }
        }
        /// <summary>
        /// Determines if this Tilebox has collided with the provided Hitbox.
        /// </summary>
        /// <param name="foo">The Hitbox to be investigated.</param>
        /// <returns>True if this Tilebox collides with the provided Hitbox, False if not.</returns>
        public bool Collision(Hitbox foo)
        {
            return geometry.Intersection(foo.geometry);
        }
        /// <summary>
        /// Draws all of the rectangles inside of this Hitboxes collision area.
        /// </summary>
        /// <param name="drawSegments">True results in overlapping perimeters being drawn, False results in only unique perimeter values being drawn.</param>
        public void DrawHitbox(bool drawSegments = false)
        {
            geometry.Draw(drawSegments);
        }
    }
}