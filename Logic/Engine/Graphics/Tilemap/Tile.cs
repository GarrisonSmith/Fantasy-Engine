using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Fantasy.Logic.Engine.Utility;
using Fantasy.Logic.Engine.Hitboxes;

namespace Fantasy.Logic.Engine.graphics.tilemap
{
    /// <summary>
    /// Describes a Tile in a given TileMapLayer.
    /// </summary>
    public class Tile
    {
        /// <summary>
        /// Reference graphical tile set used by this tile.
        /// </summary>
        public Texture2D tileSet;
        /// <summary>
        /// Top left coordinate of the tile collisionArea this tile describes inside of the given tile set.
        /// </summary>
        public Point tileSetCoordinate;
        /// <summary>
        /// Point this tile occupies in its TileMapLayer. The X value is the horizontal tile column and the Y value is the vertical tile row.
        /// </summary>
        public Point tileMapCoordinate;
        /// <summary>
        /// Describes position of visual area this Tile occupies..
        /// </summary>
        public Rectangle positionBox;
        /// <summary>
        /// 
        /// </summary>
        public Tilebox[] hitboxes;

        /// <summary>
        /// Generic inherited constructor.
        /// </summary>
        public Tile() { }
        /// <summary>
        /// Creates a Tile with the provided perameters.
        /// </summary>
        /// <param name="tileSet">The tileSet this Tile will use.</param>
        /// <param name="tileSetCoordinate">The coordinate of the tileSet this tiles graphic is located at.</param>
        /// <param name="tileMapCoordinate">Point containing the column (x value) and row (y value) this Tile occupies on its TileMapLayer.</param>
        /// <param name="positionBox">Describes where the tile is located (top right position) and the area this tile occupies.</param>
        /// <param name="hitboxes">Describes the hitboxes of this Tile.</param>
        public Tile(Texture2D tileSet, Point tileSetCoordinate, Point tileMapCoordinate, Rectangle positionBox, Tilebox[] hitboxes)
        {
            this.tileSet = tileSet;
            this.tileSetCoordinate = tileSetCoordinate;
            this.tileMapCoordinate = tileMapCoordinate;
            this.positionBox = positionBox;
            this.hitboxes = hitboxes;
        }
        
        /// <summary>
        /// Determines if the provided Entitybox collides with any of the Tiles hitboxes.
        /// </summary>
        /// <param name="entitybox">The Entitybox to be investigated.</param>
        /// <returns>True if the provided Entitybox collides with a Tiles hitbox, False if not.</returns>
        public bool TileCollision(Entitybox entitybox)
        {
            foreach (Tilebox foo in hitboxes)
            {
                if (foo.Collision(entitybox))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Draws the tile.
        /// </summary>
        public void DrawTile()
        {
            Global._spriteBatch.Draw(tileSet, Util.GetTopLeftVector(positionBox, true),
                new Rectangle(tileSetCoordinate.X, tileSetCoordinate.Y, 64, 64),
                Color.White, 0f, new Vector2(0, 0), new Vector2(1, 1), new SpriteEffects(), 0);
        }
        /// <summary>
        /// Draws all of this Tiles hitboxes.
        /// </summary>
        public void DrawHitboxes()
        {
            foreach (Tilebox foo in hitboxes)
            {
                foo.DrawHitbox();
            }
        }
    }
}