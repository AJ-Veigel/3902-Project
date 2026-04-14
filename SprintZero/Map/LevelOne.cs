
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
        private TextureRegion ground, solid, tubeTop, tubeLeft, tubeMid, tubeInter,castle;
        private AnimatedSprite qBlock, brick;


        public List<IEnemy> spawnedEnemies;

        private TextureAtlas goombaTexture;
        public LevelOne(ContentManager content, TextureAtlas blockTextures, TextureAtlas itemTextures, List<ICollectable> currItems, string filename, TextureAtlas bigBlockTexturePt2, TextureAtlas bigBlockTexture)
        {
            this.content = content;
            this.blockTextures = blockTextures;
            this.bigBlockTexturePt2 = bigBlockTexturePt2;
            this.spawnedEnemies = new List<IEnemy>();
            this.bigBlockTexture = bigBlockTexture;
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

        private void placeItemQBlockAt(TileMap map, AnimatedSprite qBlock, Point tilePos)
        {
            Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);
            IBlock block = new questionMarkItem(qBlock, location, itemTextures, items);
            map.addBlockAt(tilePos, block);
        }
        private static void placeFlagAt(TileMap map, TextureAtlas flagTexture, Point tilePos)
    {
    Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);
    AnimatedSprite sprite = flagTexture.CreateAnimatedSprite("flagMove");
    IBlock block = new FlagMove(sprite, location);
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
            flagMove = bigBlockTexturePt2.CreateAnimatedSprite("flagMove");
            castle = bigBlockTexture.GetRegion("castle");
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

                            switch (tilesetIndex)
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
                                case 9:
                                    {
                                        placeItemQBlockAt(tilemap, qBlock, p);
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
                                        placeFlagAt(tilemap,bigBlockTexturePt2,p);
                                        break;
                                    }
                                    case 15:
                                    {
                                        placeCastleAt(tilemap, castle,p);
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
