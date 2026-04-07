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
    public class LevelOneBonus : ILevel
    {
        private const int TileSize = 64; // 64 in screen coordinates

        public Color BGColor { get; set; }
        private ContentManager content { get; set; }
        private TextureAtlas blockTextures { get; set; }
        private string filename;
        private TextureRegion ground, solid;
        private AnimatedSprite qBlock, brick;

        public LevelOneBonus(ContentManager content, TextureAtlas blockTextures, string filename)
        {
            this.blockTextures = blockTextures;
            LoadContent();
            BGColor = Color.AliceBlue;
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

        private static void placeBrickAt(TileMap map, AnimatedSprite brick, Point tilePos)
        {
            Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);
            IBlock block = new AboveGroundBreak(brick, location);
            map.addBlockAt(tilePos, block);
        }

        private static void placeQBlockAt(TileMap map, AnimatedSprite qBlock, Point tilePos)
        {
            Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);
            IBlock block = new questionMarkHit(qBlock, location);
            map.addBlockAt(tilePos, block);
        }

        private static void placeSolidAt(TileMap map, TextureRegion solid, Point tilePos)
        {
            Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);
            IBlock block = new SolidBlock(solid, location);
            map.addBlockAt(tilePos, block);
        }

        public void LoadContent()
        {
            ground = blockTextures.GetRegion("ground");
            brick = blockTextures.CreateAnimatedSprite("aboveGroundBreak");
            qBlock = blockTextures.CreateAnimatedSprite("hit-Question");
            solid = blockTextures.GetRegion("solidBlock");
        }

        public void Populate(TileMap tileMap)
        {
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

                            Point p = new Point(column, row);
                            Vector2 pos = new Vector2(column * 64, row * 64);

                            // Get the tileset index for this location
                            int tilesetIndex = int.Parse(columns[column]);

                            switch(tilesetIndex)
                            {
                                case 1:
                                    {
                                        placeGroundAt(tilemap, ground, p);
                                        break;
                                    }
                                case 2:
                                    {
                                        placeBrickAt(tilemap, brick, p);
                                        break;
                                    }
                                case 3:
                                    {
                                        placeSolidAt(tilemap, solid, p);
                                        break;
                                    }
                                case 4:
                                    {
                                        placeQBlockAt(tilemap, qBlock, p);
                                        break;
                                    }
                                default:
                                    {
                                        break;
                                    }
                            }
                        }
                    }
                }
            }
        }
    }
}