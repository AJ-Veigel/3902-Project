
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
using SprintZero.background;

namespace SprintZero.Map
{
    public class LevelFour : ILevel
    {
        private const int TileSize = 64; // 64 in screen coordinates

        private const int FIREBALL_COUNT = 6; // How many fireballs are in a fire bar.

        private const int FIREBALL_DISTANCE = 32; // How much further out each fireball is in a fire bar.

        public Color BGColor { get; set; }
        private ContentManager content { get; set; }
        private TextureAtlas blockTextures { get; set; }
        private TextureAtlas itemTextures;
        private List<ICollectable> items;
        private string filename;
        private TextureRegion brick, bridge, firebarBlock, chain, lava;
        private AnimatedSprite qBlock, axe;


        public List<IEnemy> spawnedEnemies;

        private TextureAtlas goombaTexture;
        private TextureAtlas bowserTexture;
        private Game1 game;

        public LevelFour(ContentManager content, TextureAtlas blockTextures, TextureAtlas itemTextures, List<ICollectable> currItems, string filename, Game1 game)
        {
            this.content = content;
            this.blockTextures = blockTextures;
            spawnedEnemies = new List<IEnemy>();
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
        private static void placeBrickAt(TileMap map, TextureRegion brick, Point tilePos)
        {
            Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);
            IBlock block = new CastleBrick(brick, location);
            map.addBlockAt(tilePos, block);
        }

        private void placeQBlockAt(TileMap map, TextureAtlas hqTexture, Point tilePos)
        {
            Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);
            AnimatedSprite newSprite = hqTexture.CreateAnimatedSprite("hit-Question");
            IBlock block = new questionMarkHit(newSprite, location, itemTextures, items, game);

            map.addBlockAt(tilePos, block);
        }

        private void placeFireBarBlockAt(TileMap map, TextureRegion firebarBlock, Point tilePos)
        {
            Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);
            IBlock block = new FireBarBlock(firebarBlock, location);
            map.addBlockAt(tilePos, block);

            Vector2 blockCenter = location + new Vector2(TileSize / 2.0f, TileSize / 2.0f);
            for (int i = 0; i < FIREBALL_COUNT; i++)
            {
                IEnemy Fireball = new BarFireball(blockCenter, FIREBALL_DISTANCE * i);
                spawnedEnemies.Add(Fireball);
            }
        }

        private static void placeBridgeAt(TileMap map, TextureRegion bridge, Point tilePos)
        {
            Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);
            IBlock block = new Bridge(bridge, location);
            map.addBlockAt(tilePos, block);
        }

        private static void placeChainAt(TileMap map, TextureRegion chain, Point tilePos)
        {
            Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);
            IBackground block = new Chain(chain, location);
            map.addBackgroundAt(tilePos, block);
        }

        private static void placeLavaAt(TileMap map, TextureRegion lava, Point tilePos)
        {
            Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);
            IBackground block = new Lava(lava, location);
            map.addBackgroundAt(tilePos, block);
        }

        private void placeItemQBlockAt(TileMap map, AnimatedSprite qBlock, Point tilePos)
        {
            Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);
            IBlock block = new questionMarkItem(qBlock, location, itemTextures, items);
            map.addBlockAt(tilePos, block);
        }
        private static void placeAxeAt(TileMap map, AnimatedSprite axe, Point tilepos)
        {
            Vector2 location = new Vector2(tilepos.X * TileSize, tilepos.Y * TileSize);
            IBlock block = new Axe(axe, location);
            map.addBlockAt(tilepos, block);
        }

        public void LoadContent()
        {
            axe = blockTextures.CreateAnimatedSprite("axe");
            qBlock = blockTextures.CreateAnimatedSprite("hit-Question");
            brick = blockTextures.GetRegion("castleBlock");
            bridge = blockTextures.GetRegion("bridge");
            chain = blockTextures.GetRegion("chain");
            lava = blockTextures.GetRegion("lavaTop");
            firebarBlock = blockTextures.GetRegion("firebarBase");
            //fireballTexture = TextureAtlas.FromFile(this.content, "images/fireBall-definition.xml");
            goombaTexture = TextureAtlas.FromFile(this.content, "images/goomba-definition.xml");
            bowserTexture = TextureAtlas.FromFile(this.content, "images/bowser-definition.xml");
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

                    XElement marioSpawnElement = root.Element("MarioPos");

                    string[] split = marioSpawnElement.Value.Trim().Split('\n', StringSplitOptions.RemoveEmptyEntries);
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

                            switch (tilesetIndex)
                            {
                                case 1:
                                    {
                                        placeBrickAt(tilemap, brick, p);
                                        break;
                                    }
                                case 2:
                                    {
                                        placeFireBarBlockAt(tilemap, firebarBlock, p);
                                        break;
                                    }
                                case 3:
                                    {
                                        placeQBlockAt(tilemap, blockTextures, p);
                                        break;
                                    }
                                case 4:
                                    {
                                        placeBridgeAt(tilemap, bridge, p);
                                        break;
                                    }
                                case 5:
                                    {
                                        placeChainAt(tilemap, chain, p);
                                        break;
                                    }
                                case 6:
                                    {
                                        placeAxeAt(tilemap, axe, p);
                                        break;
                                    }
                                case 7:
                                    {
                                        placeLavaAt(tilemap, lava, p);
                                        break;
                                    }
                                case 8:
                                    {
                                        break;
                                    }
                                case 9:
                                    {
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
                                        break;
                                    }
                                case 15:
                                    {
                                        break;
                                    }
                                case 16:
                                    {
                                        break;
                                    }
                                case 17:
                                    {
                                        break;
                                    }
                                case 18:
                                    {
                                        Bowser bowser = new Bowser(bowserTexture, goombaTexture, game, pos);
                                        spawnedEnemies.Add(bowser);
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
