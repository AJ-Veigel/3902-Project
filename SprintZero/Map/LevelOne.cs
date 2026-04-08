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
    public class LevelOne : ILevel
    {
        private const int TileSize = 64; // 64 in screen coordinates

        public Color BGColor { get; set; }
        private ContentManager content { get; set; }
        private TextureAtlas blockTextures { get; set; }
        private string filename;
        private TextureRegion ground, solid, tubeTop, tubeLeft, tubeMid, tubeInter;
        private AnimatedSprite qBlock, brick;

        public LevelOne(ContentManager content, TextureAtlas blockTextures, string filename)
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

        private static void placeBrickAt(TileMap map, TextureAtlas agbTexture, Point tilePos)
        {
            Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);
            AnimatedSprite newSprite = agbTexture.CreateAnimatedSprite("aboveGroundBreak");
            IBlock block = new AboveGroundBreak(newSprite, location);
            map.addBlockAt(tilePos, block);
        }

        private static void placeQBlockAt(TileMap map, TextureAtlas hqTexture, Point tilePos)
        {
            Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);
            AnimatedSprite newSprite = hqTexture.CreateAnimatedSprite("hit-Question");
            IBlock block = new questionMarkHit(newSprite, location);

            map.addBlockAt(tilePos, block);
        }

        private static void placeSolidAt(TileMap map, TextureRegion solid, Point tilePos)
        {
            Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);
            IBlock block = new SolidBlock(solid, location);
            map.addBlockAt(tilePos, block);
        }

        private static void placeTubeTopAt(TileMap map, TextureRegion tube, Point tilePos)
        {
            Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);
            IBlock block = new TubeTop(tube, location);
            map.addBlockAt(tilePos, block);
        }

        private static void placeTubeLeftAt(TileMap map, TextureRegion tube, Point tilePos)
        {
            Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);
            IBlock block = new TubeLeft(tube, location);
            map.addBlockAt(tilePos, block);
        }

        private static void placeTubeMidAt(TileMap map, TextureRegion tube, Point tilePos)
        {
            Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);
            IBlock block = new TubeMid(tube, location);
            map.addBlockAt(tilePos, block);
        }

        private static void placeTubeInterAt(TileMap map, TextureRegion tube, Point tilePos)
        {
            Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);
            IBlock block = new TubeIntersect(tube, location);
            map.addBlockAt(tilePos, block);
        }

        public void LoadContent()
        {
            ground = blockTextures.GetRegion("ground");
            brick = blockTextures.CreateAnimatedSprite("aboveGroundBreak");
            qBlock = blockTextures.CreateAnimatedSprite("hit-Question");
            solid = blockTextures.GetRegion("solidBlock");
            tubeTop = blockTextures.GetRegion("tubeTop");
            tubeLeft = blockTextures.GetRegion("tubeLeft");
            tubeMid = blockTextures.GetRegion("tubeMid");
            tubeInter = blockTextures.GetRegion("tubeIntersect");
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
                                        placeBrickAt(tilemap, blockTextures, p);
                                        break;
                                    }
                                case 3:
                                    {
                                        placeSolidAt(tilemap, solid, p);
                                        break;
                                    }
                                case 4:
                                    {
                                        placeQBlockAt(tilemap, blockTextures, p);
                                        break;
                                    }
                                case 5:
                                    {
                                        placeTubeTopAt(tilemap, tubeTop, p);
                                        break;
                                    }
                                case 6:
                                    {
                                        placeTubeMidAt(tilemap, tubeMid, p);
                                        break;
                                    }
                                case 7:
                                    {
                                        placeTubeLeftAt(tilemap, tubeLeft, p);
                                        break;
                                    }
                                case 8:
                                    {
                                        placeTubeInterAt(tilemap, tubeInter, p);
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