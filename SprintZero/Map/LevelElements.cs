using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MonoGameLibrary.Graphics;
using SprintZero.blocks;
using SprintZero.Map;
using SprintZero.background;
using SpriteZero.Enemies;
using SprintZero.Items;
using SprintZero.Marios;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System;

namespace SprintZero.LevelElements
{
    public static class LevelElements
    {
        public const int TileSize = 64; // 64 in screen coordinates
        public static void placeGroundAt(TileMap map, TextureRegion ground, Point tilePos)
        {
            Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);
            IBlock block = new Ground(ground, location);
            map.addBlockAt(tilePos, block);
        }

        public static void placeBrickAt(TileMap map, TextureRegion brick, Point tilePos)
        {
            Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);
            IBlock block = new Brick(brick, location);
            map.addBlockAt(tilePos, block);
        }

        public static void placeCoinAt(TileMap map, TextureAtlas coinTexture, Point tilePos, Game1 game)
        {
            Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);

            AnimatedSprite coin = coinTexture.CreateAnimatedSprite("coins");

            ICollectable item = new Coin(coin, location, true, game);
            map.addItemAt(tilePos, item);
        }

        public static void placeSolidAt(TileMap map, TextureRegion solid, Point tilePos)
        {
            Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);
            IBlock block = new SolidBlock(solid, location);
            map.addBlockAt(tilePos, block);
        }

        public static void placeTubeTopAt(TileMap map, TextureRegion tube, Point tilePos)
        {
            Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);
            IPipe block = new TubeTop(tube, location);
            map.addPipeAt(tilePos, block);
        }

        public static void placeTubeLeftAt(TileMap map, TextureRegion tube, Point tilePos)
        {
            Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);
            IPipe block = new TubeLeft(tube, location);
            map.addPipeAt(tilePos, block);
        }

        public static void placeTubeLeftAt(TileMap map, TextureRegion tube, Point tilePos, string pipeLevel, Vector2 marioSpawnPos)
        {
            Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);
            IPipe block = new TubeLeft(tube, location, pipeLevel, marioSpawnPos, 1, 0);
            map.addPipeAt(tilePos, block);
        }

        public static void placeTubeMidAt(TileMap map, TextureRegion tube, Point tilePos)
        {
            Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);
            IBlock block = new TubeMid(tube, location);
            map.addBlockAt(tilePos, block);
        }

        public static void placeTubeInterAt(TileMap map, TextureRegion tube, Point tilePos)
        {
            Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);
            IBlock block = new TubeIntersect(tube, location);
            map.addBlockAt(tilePos, block);
        }

        public static void placeBrickAt(TileMap map, TextureAtlas agbTexture, Point tilePos, Game1 game)
        {
            Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);
            AnimatedSprite newSprite = agbTexture.CreateAnimatedSprite("aboveGroundBreak");
            IBlock block = new AboveGroundBreak(newSprite, location, game);
            map.addBlockAt(tilePos, block);
        }

        public static void placeQBlockAt(TileMap map, TextureAtlas hqTexture, TextureAtlas itemTextures, List<ICollectable> items, Point tilePos, Game1 game)
        {
            Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);
            AnimatedSprite newSprite = hqTexture.CreateAnimatedSprite("hit-Question");
            IBlock block = new questionMarkHit(newSprite, location, itemTextures, items, game);

            map.addBlockAt(tilePos, block);
        }

        public static void placeTubeTopAt(TileMap map, TextureRegion tube, Point tilePos, string pipeLevel, Vector2 MarioPos)
        {
            Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);
            IPipe block = new TubeTop(tube, location, pipeLevel, MarioPos, 1, 1);
            map.addPipeAt(tilePos, block);
        }

        public static void placeItemQBlockAt(TileMap map, TextureAtlas hqTexture, TextureAtlas itemTextures, List<ICollectable> items, Point tilePos)
        {
            Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);
            AnimatedSprite newSprite = hqTexture.CreateAnimatedSprite("hit-Question");
            IBlock block = new questionMarkItem(newSprite, location, itemTextures, items);
            map.addBlockAt(tilePos, block);
        }
        public static void placeItemBrickAt(TileMap map, TextureAtlas blockAtlas, TextureAtlas itemTextures, List<ICollectable> items, Point tilePos, bool containsStar)
        {
            Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);

            AnimatedSprite brickSprite = blockAtlas.CreateAnimatedSprite("aboveGroundBreak");

            AnimatedSprite emptySprite = blockAtlas.CreateAnimatedSprite("hit-Question");

            IBlock block = new ItemBrick(brickSprite, emptySprite, location, itemTextures, items, containsStar);
            map.addBlockAt(tilePos, block);
        }
        public static void placeHiddenBlockAt(TileMap map, TextureAtlas blockAtlas, TextureAtlas itemTextures, List<ICollectable> items, Point tilePos, bool containsStar)
        {
            Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);

            AnimatedSprite emptySprite = blockAtlas.CreateAnimatedSprite("hit-Question");

            IBlock block = new HiddenBlock(emptySprite, location, itemTextures, items, containsStar, true);
            map.addBlockAt(tilePos, block);
        }
        public static void placeFlagAt(TileMap map, TextureRegion flag, Point tilepos)
        {
            Vector2 location = new Vector2(tilepos.X * TileSize, tilepos.Y * TileSize);
            IBlock block = new Flag(flag, location);
            map.addBlockAt(tilepos, block);
        }
        public static void placePoleTop(TileMap map, TextureRegion poleTop, Point tilePos)
        {
            Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);
            IBlock block = new FlagTop(poleTop, location);
            map.addBlockAt(tilePos, block);
        }
        public static void placePoleMiddle(TileMap map, TextureRegion poleMid, Point tilePos)
        {
            Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);
            IBlock block = new FlagMiddle(poleMid, location);
            map.addBlockAt(tilePos, block);
        }

        public static void placeCastleAt(TileMap map, TextureRegion castle, Point tilePos)
        {
            Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);
            IBlock block = new CastleBlock(castle, location);
            map.addBlockAt(tilePos, block);
        }

        public static void placeBackgroundAt(TileMap map, TextureRegion background, Point tilePos)
        {
            Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);
            IBackground back = new LevelOneBackground(background, location);
            map.addBackgroundAt(tilePos, back);
        }

        public static void placeBridgeAt(TileMap map, TextureRegion bridge, Point tilePos)
        {
            Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);
            IBlock block = new Bridge(bridge, location);
            map.addBlockAt(tilePos, block);
        }

        public static void placeChainAt(TileMap map, TextureRegion chain, Point tilePos)
        {
            Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);
            IBackground block = new Chain(chain, location);
            map.addBackgroundAt(tilePos, block);
        }

        public static void placeLavaAt(TileMap map, TextureRegion lava, Point tilePos)
        {
            Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);
            IBackground block = new Lava(lava, location);
            map.addBackgroundAt(tilePos, block);
        }

        public static void placeLavaBodyAt(TileMap map, TextureRegion lava, Point tilePos)
        {
            Vector2 location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);
            IBackground block = new LavaBody(lava, location);
            map.addBackgroundAt(tilePos, block);
        }

        public static void placeAxeAt(TileMap map, AnimatedSprite axe, Point tilepos)
        {
            Vector2 location = new Vector2(tilepos.X * TileSize, tilepos.Y * TileSize);
            IBlock block = new Axe(axe, location);
            map.addBlockAt(tilepos, block);
        }
    }
}