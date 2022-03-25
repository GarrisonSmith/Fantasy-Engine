using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fantasy.Content.Logic.Drawing
{
    class Tile
    {
        //tile is 64x64 pixels large
        public string tileID; //Id for which tile this represents
        public string tileSetName; //name of the tile set this tile belongs to
        public int x; //top left x coordinate for reference tiles set
        public int y; //top left y coordinate for reference tiles set
        public int row; //row this tile occupies on its map
        public int column; //column this tile occupies on its map
        public int layer; //layer for the tile to be drawn on.
        public Texture2D tile;
        public Color color;

        public Tile(string tileID, int row, int column, int layer)
        {
            this.tileID = tileID;
            this.row = row;
            this.column = column;
            this.layer = layer;
            if (tileID == "BLACK")
            {
                this.tileSetName = tileID;
                x = 0;
                y = 0;
                color = Color.Black;
            }
            else
            {
                char[] tempChar = tileID.ToCharArray();
                int index = 0;
                string tileSetName = "";
                string[] coordinates = { "0", "0" };
                foreach (char i in tempChar)
                {
                    if (i == '(')
                    {
                        tileSetName = tileID.Substring(0, index);
                        coordinates = tileID.Substring(index).Replace("(", "").Replace(")", "").Split(",");
                        break;
                    }
                    index++;
                }
                this.tileSetName = tileSetName;
                x = int.Parse(coordinates[0]);
                y = int.Parse(coordinates[1]);
                color = Color.White;
            }
        }
        public void loadTile(Texture2D[] tileSets, GraphicsDevice device) 
        {
            foreach (Texture2D i in tileSets)
            {
                if (tileSetName == i.Name)
                {
                    tile = new Texture2D(device, 64, 64);
                    Color[] newColor = new Color[64 * 64];
                    Rectangle selectionArea = new Rectangle(x, y, 64, 64);

                    i.GetData(0, selectionArea, newColor, 0, newColor.Length);

                    tile.SetData(newColor);
                }
            }
        }
        public void loadTile(Texture2D tile)
        {
            this.tile = tile;
        }
        public void drawTile(SpriteBatch spriteBatch) 
        {
            spriteBatch.Draw(tile, new Vector2(row*64, column*64), Color.White);
        }
    }
}
