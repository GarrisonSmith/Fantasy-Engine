﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Xml;
using Fantasy.Logic.Engine.hitboxes;

namespace Fantasy.Logic.Engine.graphics.tilemap
{
    /// <summary>
    /// Desribes a layer of tiles in a given <c>TileMap</c>.
    /// </summary>
    class TileMapLayer
    {
        /// <summary>
        /// List containing this layers tiles.
        /// </summary>
        public List<Tile> map;
        /// <summary>
        /// The layer this TileMapLayer will be drawn on.
        /// </summary>
        public int layer;
        /// <summary>
        /// The width of this TileMapLayer.
        /// </summary>
        public int width;
        /// <summary>
        /// The height of this TileMapLayer.
        /// </summary>
        public int height;

        /// <summary>
        /// Constructs a TileMapLayer from the provided string.
        /// </summary>
        /// <param name="layer">The layer this TileMapLayer occupies in its TileMap</param>
        /// <param name="initialize">string to be parsed to describe the Tiles in the TileMapLayer.</param>
        public TileMapLayer(int layer, string initialize)
        {
            this.map = new List<Tile>();
            this.layer = layer;

            XmlDocument animatedList = new XmlDocument();
            XmlDocument hitboxList = new XmlDocument();
            animatedList.Load(@"Content\tile-sets\animated_tiles_config.xml");
            hitboxList.Load(@"Content\tile-sets\tile_hitboxes_config.xml");

            string[] columnTemp;
            string[] rowTemp;
            int row = 1;

            rowTemp = initialize.Split(";");
            Array.Reverse(rowTemp);
            for (int i = 0; i < rowTemp.Length; i++)
            {
                int column = 1;
                columnTemp = rowTemp[i].Split(":");
                if (rowTemp[i] != "")
                {
                    foreach (string j in columnTemp)
                    {
                        if (j != "")
                        {
                            bool animated = false;
                            int frames = 1;
                            int minDuration = 0;
                            int maxDuration = 0;
                            foreach (XmlElement foo in animatedList.GetElementsByTagName("tile"))
                            {
                                if (j == foo.GetAttribute("name"))
                                {
                                    animated = true;
                                    frames = int.Parse(foo.ChildNodes[0].InnerText);
                                    minDuration = int.Parse(foo.ChildNodes[1].InnerText);
                                    maxDuration = int.Parse(foo.ChildNodes[2].InnerText);
                                    break;
                                }
                            }

                            bool hitbox = false;
                            foreach (XmlElement foo in hitboxList.GetElementsByTagName("tile"))
                            {
                                if (j == foo.GetAttribute("name"))
                                {
                                    hitbox = true;
                                }
                            }

                            if (animated)
                            {
                                map.Add(new AnimatedTile(j, column, row, frames, minDuration, maxDuration, hitbox));
                            }
                            else
                            {
                                map.Add(new Tile(j, column, row, hitbox));
                            }


                            if (column > this.width)
                            {
                                this.width = column;
                            }
                            if (row > this.height)
                            {
                                this.height = row;
                            }
                        }
                        column++;
                    }

                    row++;
                }
            }
        }
        /// <summary>
        /// Returns the tile with the given index from the TileMaplayer. If the index is invalid returns null.
        /// </summary>
        /// <param name="index">The index of the tile to be returned.</param>
        /// <returns>The tile with the corresponding index. If not tile with that index exists then null.</returns>
        public Tile GetTile(int index)
        {
            if (map.Count - 1 >= index)
            {
                return map[index];
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Returns the tile with the given coordinate from the TileMaplayer. If the coordiante is invalid returns null.
        /// </summary>
        /// <param name="coordinate">The coordinate of the tile to be returned. coordinate.X is the column and coordinate.Y is the row the tile occupies in the TileMapLayer.</param>
        /// <returns></returns>
        public Tile GetTile(Point coordinate)
        {
            foreach (Tile i in map)
            {
                if (i.tileMapCoordinate == coordinate)
                {
                    return i;
                }
            }
            return null;
        }
        /// <summary>
        /// Returns the layer dimensions in as a point containing width and height.
        /// </summary>
        /// <returns>Point where the X value is the TileMapLayer width and the Y is the TileMapLayer height.</returns>
        public Point GetLayerDimensions()
        {
            return new Point(this.width, this.height);
        }
        /// <summary>
        /// Checks if the provided Hitbox with the provided position collide with any tiles that have hitboxes in this TileMapLayer.
        /// </summary>
        /// <param name="pos">The position of the Hitbox to be checked.</param>
        /// <param name="box">The Hitbox to be checked.</param>
        /// <param name="tileHitboxes">The reference Hitboxes for the tiles in this TileMapLayer.</param>
        /// <returns></returns>
        public bool CheckLayerCollision(Point pos, Hitbox box, List<Hitbox> tileHitboxes)
        {
            foreach (Tile j in map)
            {
                string tileReference = j.tileSetName + '(' + j.tileSetCoordinate.X.ToString() + ',' + j.tileSetCoordinate.Y.ToString() + ')';
                foreach (Hitbox k in tileHitboxes)
                {
                    if (tileReference == k.reference)
                    {
                        if (k.Collision(pos, new Point(j.tileMapCoordinate.X * 64, (j.tileMapCoordinate.Y+1) * 64), box.area))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}