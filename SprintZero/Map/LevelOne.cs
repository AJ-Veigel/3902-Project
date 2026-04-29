
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
using System.Net.NetworkInformation;
using SprintZero.Items;
using MonoGame.Extended;
using System.Reflection.Metadata;
using SprintZero.Marios;
using SprintZero.Enemies;

namespace SprintZero.Map
{
    public class LevelOne : ILevel
    {
        private const int TileSize = 64; // 64 in screen coordinates

        public Color BGColor { get; set; }
        private ContentManager content { get; set; }
        private TextureAtlas blockTextures { get; set; }
        private TextureAtlas itemTextures;
        private TextureAtlas bigBlockTexture;
        private AnimatedSprite flagMove;
        private TextureAtlas bigBlockTexturePt2;
        private List<ICollectable> items;
        private string filename;
        private TextureRegion ground, solid, tubeTop, tubeLeft, tubeMid, tubeInter, castle;
        private AnimatedSprite qBlock, brick;
        private TextureAtlas flagpoleTexture;
        private TextureRegion flagRegion, flag, poleTop, poleMiddle;


        public List<IEnemy> spawnedEnemies;

        private TextureAtlas goombaTexture;
        private TextureAtlas bowserTexture;
        private Game1 game;

        public LevelOne(ContentManager content, TextureAtlas blockTextures, TextureAtlas itemTextures, List<ICollectable> currItems, string filename, TextureAtlas bigBlockTexturePt2, TextureAtlas bigBlockTexture, Game1 game)
        {
            this.content = content;
            this.blockTextures = blockTextures;
            this.bigBlockTexturePt2 = bigBlockTexturePt2;
            this.spawnedEnemies = new List<IEnemy>();
            this.bigBlockTexture = bigBlockTexture;
            this.game = game;
            LoadContent();
            BGColor = Color.AliceBlue;
            this.filename = filename;
            items = currItems;
            this.itemTextures = itemTextures;
        }

        public List<IEnemy> GetEnemies()
        {
            return spawnedEnemies;
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

        private void placeQBlockAt(TileMap map, TextureAtlas hqTexture, Point tilePos)
        {
            Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);
            AnimatedSprite newSprite = hqTexture.CreateAnimatedSprite("hit-Question");
            IBlock block = new questionMarkHit(newSprite, location, itemTextures, items, game);

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
            IPipe block = new TubeTop(tube, location);
            map.addPipeAt(tilePos, block);
        }

        private static void placeTubeTopAt(TileMap map, TextureRegion tube, Point tilePos, string pipeLevel, Vector2 MarioPos)
        {
            Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);
            IPipe block = new TubeTop(tube, location, pipeLevel, MarioPos, 1, 1);
            map.addPipeAt(tilePos, block);
        }

        private static void placeTubeLeftAt(TileMap map, TextureRegion tube, Point tilePos)
        {
            Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);
            IPipe block = new TubeLeft(tube, location);
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

        private void placeItemQBlockAt(TileMap map, TextureAtlas hqTexture, Point tilePos)
        {
            Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);
            AnimatedSprite newSprite = hqTexture.CreateAnimatedSprite("hit-Question");
            IBlock block = new questionMarkItem(newSprite, location, itemTextures, items);
            map.addBlockAt(tilePos, block);
        }
        private void placeItemBrickAt(TileMap map, TextureAtlas blockAtlas, Point tilePos, bool containsStar)
        {
            Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);

            AnimatedSprite brickSprite = blockAtlas.CreateAnimatedSprite("aboveGroundBreak");

            AnimatedSprite emptySprite = blockAtlas.CreateAnimatedSprite("hit-Question");

            IBlock block = new ItemBrick(brickSprite, emptySprite, location, itemTextures, items, containsStar);
            map.addBlockAt(tilePos, block);
        }
        private void placeHiddenBlockAt(TileMap map, TextureAtlas blockAtlas, Point tilePos, bool containsStar)
        {
            Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);

            AnimatedSprite emptySprite = blockAtlas.CreateAnimatedSprite("hit-Question");

            IBlock block = new HiddenBlock(emptySprite, location, itemTextures, items, containsStar);
            map.addBlockAt(tilePos, block);
        }
        private static void placeFlagAt(TileMap map, TextureRegion flag, Point tilepos)
        {
            Vector2 location = new Vector2(tilepos.X * TileSize, tilepos.Y * TileSize);
            IBlock block = new Flag(flag, location);
            map.addBlockAt(tilepos, block);
        }
        private static void placePoleTop(TileMap map, TextureRegion poleTop, Point tilePos)
        {
            Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);
            IBlock block = new FlagTop(poleTop, location);
            map.addBlockAt(tilePos, block);
        }
        private static void placePoleMiddle(TileMap map, TextureRegion poleMid, Point tilePos)
        {
            Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);
            IBlock block = new FlagMiddle(poleMid, location);
            map.addBlockAt(tilePos, block);
        }

        private static void placeCastleAt(TileMap map, TextureRegion castle, Point tilePos)
        {
            Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);
            IBlock block = new CastleBlock(castle, location);
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
            goombaTexture = TextureAtlas.FromFile(this.content, "images/goomba-definition.xml");
            bowserTexture = TextureAtlas.FromFile(this.content, "images/bowser-definition.xml");
            flagMove = bigBlockTexturePt2.CreateAnimatedSprite("flagMove");
            castle = bigBlockTexture.GetRegion("castle");
            flagpoleTexture = TextureAtlas.FromFile(content, "Images/flag.xml");
            flag = flagpoleTexture.GetRegion("flag");
            poleTop = flagpoleTexture.GetRegion("poleTop");
            poleMiddle = flagpoleTexture.GetRegion("poleMiddle");
        }

        public void FromFile(TileMap tilemap)
        {
            tilemap.ClearMaps();
            spawnedEnemies.Clear();

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

                            switch (tilesetIndex)
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
                                case 9:
                                    {
                                        placeItemQBlockAt(tilemap, blockTextures, p);
                                        break;
                                    }
                                case 12:
                                    {
                                        Goomba goomba = new Goomba(goombaTexture);
                                        goomba.position = pos;
                                        spawnedEnemies.Add(goomba);
                                        break;
                                    }
                                case 13:
                                    {
                                        Koopa koopa = new Koopa(Koopa.KoopaType.Green);
                                        koopa.position = pos;
                                        spawnedEnemies.Add(koopa);
                                        break;
                                    }
                                case 14:
                                    {
                                        placeFlagAt(tilemap, flag, p);
                                        break;
                                    }
                                case 15:
                                    {
                                        placeCastleAt(tilemap, castle, p);
                                        break;
                                    }
                                case 16:
                                    {
                                        placePoleTop(tilemap, poleTop, p);
                                        break;
                                    }
                                case 17:
                                    {
                                        placePoleMiddle(tilemap, poleMiddle, p);
                                        break;
                                    }
                                case 18:
                                    {
                                        Bowser bowser = new Bowser(bowserTexture, goombaTexture, game, pos);
                                        spawnedEnemies.Add(bowser);
                                        break;
                                    }
                                case 19:
                                    {
                                        placeItemBrickAt(tilemap, blockTextures, p, true);
                                        break;
                                    }
                                case 20:
                                    {
                                        placeHiddenBlockAt(tilemap, blockTextures, p, false);
                                        break;
                                    }
                                default:
                                    {
                                        if (tilesetIndex == pipeNum)
                                        {
                                            Vector2 marioPipePos = new Vector2(marioX, marioY);
                                            placeTubeTopAt(tilemap, tubeTop, p, pipeLevel, marioPipePos);
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
