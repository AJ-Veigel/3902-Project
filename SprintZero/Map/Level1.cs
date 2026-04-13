using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MonoGameLibrary.Graphics;
using SprintZero.blocks;
using SpriteZero.Enemies;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System;

namespace SprintZero.Map
{
    public class Level1 : ILevel
    {
        private const int TileSize = 64; // 64 in screen coordinates

        public Color BGColor { get; set; }
        private ContentManager content { get; set; }
        private string filename;

        public Level1(ContentManager content, string filename)
        {
               this.BGColor = Color.AliceBlue;
            this.filename = filename;
            this.content = content;
        }

        public List<IEnemy> GetEnemies()
        {
            return new List<IEnemy>(); // Return empty list, for now.
        }
        private static void placeGroundAt(TileMap map, TextureRegion ground, Point tilePos)
        {
            Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);
            IBlock block = new Ground(ground, location);
            map.addBlockAt(tilePos, block);
        }

        public void Populate(TileMap tileMap)
        {
            TextureAtlas blocksTexture = TextureAtlas.FromFile(this.content, "images/block-definition.xml");
            TextureRegion ground = blocksTexture.GetRegion("ground");
            for (int x = -200; x < 50; x++)
            {
                placeGroundAt(tileMap, ground, new Point(x, 13));
            }

            for (int x = 50; x < 100; x++)
            {
                placeGroundAt(tileMap, ground, new Point(x, x + -37));
            }
        }

        public void FromFile(TileMap tilemap)
        {
            string filePath = Path.Combine(content.RootDirectory, filename);

            using (Stream stream = TitleContainer.OpenStream(filePath))
            {
                using (XmlReader reader = XmlReader.Create(stream))
                {
                    XDocument doc = XDocument.Load(reader);
                    XElement root = doc.Root;


                    XElement tilesElement = root.Element("Blocks");

                    // Split the value of the tiles data into rows by splitting on
                    // the new line character
                    string[] rows = tilesElement.Value.Trim().Split('\n', StringSplitOptions.RemoveEmptyEntries);

                    // Split the value of the first row to determine the total number of columns
                    int columnCount = rows[0].Split(" ", StringSplitOptions.RemoveEmptyEntries).Length;

                    // Process each row
                    for (int row = 0; row < rows.Length; row++)
                    {
                        // Split the row into individual columns
                        string[] columns = rows[row].Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);

                        // Process each column of the current row
                        for (int column = 0; column < columnCount; column++)
                        {

                            Point p = new Point(row, column);
                            Vector2 pos = new Vector2(row * 64, column * 64);

                            // Get the tileset index for this location
                            int tilesetIndex = int.Parse(columns[column]);

                            //IBlock block = getBlockFromInt(tilesetIndex, pos);

                            //addBlockAt(p, block);

                            // Add that region to the tilemap at the row and column location
                            //tilemap.SetTile(column, row, tilesetIndex);
                        }
                    }
                }
            }
        }

    }
}