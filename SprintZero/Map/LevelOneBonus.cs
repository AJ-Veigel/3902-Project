using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MonoGameLibrary.Graphics;
using SprintZero.blocks;
using SpriteZero.Enemies;
using SprintZero.Marios;
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
        private TextureRegion ground, solid, tubeTop, tubeMid, tubeLeft, tubeInter;
        private AnimatedSprite qBlock, brick;
        private Game1 game;

        public LevelOneBonus(ContentManager content, TextureAtlas blockTextures, string filename, Game1 game)
        {
            this.blockTextures = blockTextures;
            LoadContent();
            BGColor = Color.AliceBlue;
            this.filename = filename;
            this.content = content;
            this.game = game;
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

      private static void placeBrickAt(TileMap map, TextureAtlas agbTexture, Point tilePos, Game1 game)
        {
            Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);
            AnimatedSprite newSprite = agbTexture.CreateAnimatedSprite("aboveGroundBreak");
            IBlock block = new AboveGroundBreak(newSprite, location, game);
            map.addBlockAt(tilePos, block);
        }
    // private  void placeQBlockAt(TileMap map, TextureAtlas hqTexture, Point tilePos)
    //     {
    //         Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);
    //         AnimatedSprite newSprite = hqTexture.CreateAnimatedSprite("hit-Question");
    //         IBlock block = new questionMarkHit(newSprite, location, itemTexture, );

    //         map.addBlockAt(tilePos, block);
    //     }

        private static void placeSolidAt(TileMap map, TextureRegion solid, Point tilePos)
        {
            Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);
            IBlock block = new SolidBlock(solid, location);
            map.addBlockAt(tilePos, block);
        }

        private static void placeTubeTopAt(TileMap map, TextureRegion tube, Point tilePos)
        {
            Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);
            IPipe block = new TubeTop(tube, location);
            map.addPipeAt(tilePos, block);
        }

        private static void placeTubeLeftAt(TileMap map, TextureRegion tube, Point tilePos)
        {
            Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);
            IPipe block = new TubeLeft(tube, location);
            map.addPipeAt(tilePos, block);
        }

        private static void placeTubeLeftAt(TileMap map, TextureRegion tube, Point tilePos, string pipeLevel, Vector2 marioSpawnPos)
        {
            Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);
            IPipe block = new TubeLeft(tube, location, pipeLevel, marioSpawnPos, 1, 0);
            map.addPipeAt(tilePos, block);
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

                    XElement pipeElement = root.Element("PipeData");

                    string[] split = pipeElement.Value.Trim().Split('\n', StringSplitOptions.RemoveEmptyEntries);
                    int pipeNum = int.Parse(split[0]);
                    string pipeLevel = split[1];
                    int marioX = int.Parse(split[2]);
                    int marioY = int.Parse(split[3]);

                    XElement marioSpawnElement = root.Element("MarioPos");

                    split = marioSpawnElement.Value.Trim().Split('\n', StringSplitOptions.RemoveEmptyEntries);
                    int marioSpawnX = int.Parse(split[0]);
                    int marioSpawnY = int.Parse(split[1]);

                    Vector2 marioSpawnPos = new Vector2(marioSpawnX, marioSpawnY);

                    tilemap.setSpawn(marioSpawnPos);

                    tilemap.SetBackgroundColor(Color.Black);

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
                                        placeBrickAt(tilemap, blockTextures, p, game);
                                        break;
                                    }
                                case 3:
                                    {
                                        placeSolidAt(tilemap, solid, p);
                                        break;
                                    }
                                // case 4:
                                //     {
                                //         placeQBlockAt(tilemap, blockTextures, p);
                                //         break;
                                //     }
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
                                        if(tilesetIndex == pipeNum)
                                        {
                                            Vector2 marioPipePos = new Vector2(marioX, marioY);
                                            placeTubeLeftAt(tilemap, tubeLeft, p, pipeLevel, marioPipePos);
                                        }
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