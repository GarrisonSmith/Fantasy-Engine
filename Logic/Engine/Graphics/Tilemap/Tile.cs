﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Fantasy.Logic.Engine.utility;

namespace Fantasy.Logic.Engine.graphics.tilemap
{
    /// <summary>
    /// Describes A tile in a given TileMapLayer.
    /// </summary>
    class Tile
    {
        /// <summary>
        /// Name of the tile set this tile describes.
        /// </summary>
        public string tileSetName;
        /// <summary>
        /// Top left coordinate of the tile collisionArea this tile describes inside of the given tile set.
        /// </summary>
        public Point tileSetCoordinate;
        /// <summary>
        /// Point this tile occupies in its TileMapLayer. The X value is the horizontal position and the Y value is the vertical position.
        /// </summary>
        public Point tileMapCoordinate;
        /// <summary>
        /// Color this tile loads when drawn.
        /// </summary>
        public Color color;
        /// <summary>
        /// The index of the graphic in the TileMap for this tile.
        /// </summary>
        public int graphicsIndex;
        /// <summary>
        /// Determines if this Tile has a corresponding Hitbox or not.
        /// </summary>
        public bool hasHitbox;

        public Tile() { }
        /// <summary>
        /// Constructs a tile with the given properties.
        /// <param name="tileID">is parsed to get the tiles tileSetName and tiles x and y values.</param>
        /// <param name="column">the column this tile occupies on its TileMapLayer.</param>
        /// <param name="row">the row this tile occupies on its TileMapLayer.</param>
        /// </summary>
        public Tile(string tileID, int column, int row, bool hasHitbox)
        {
            tileMapCoordinate = new Point(column, row);
            this.hasHitbox = hasHitbox;
            if (tileID == "BLACK")
            {
                this.tileSetName = tileID;
                tileSetCoordinate = new Point(0, 0);
                color = Color.Black;
            }
            else
            {
                
                this.tileSetName = tileID.Substring(0, tileID.IndexOf('{'));
                tileSetCoordinate = Util.PointFromString(tileID);
                color = Color.White;
            }
        }
        /// <summary>
        /// Draws the tile with the provided stretch.
        /// </summary>
        /// <param name="tileSet">the reference tileSet this tiles graphic references.</param>
        public void DrawTile(Texture2D tileSet)
        {
            Global._spriteBatch.Draw(tileSet, new Vector2(tileMapCoordinate.X * 64, -(tileMapCoordinate.Y+1) * 64),
                new Rectangle(tileSetCoordinate.X, tileSetCoordinate.Y, 64, 64),
                color, 0f, new Vector2(0, 0), new Vector2(1, 1), new SpriteEffects(), 0);
        }
    }
}