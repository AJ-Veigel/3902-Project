using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended.Tiled;
using MonoGameLibrary.Graphics;
using SpriteZero.Enemies;

namespace SprintZero.Map
{
    public class TestLevel : ILevel
    {

        private const int TileSize = 64; // 64 in screen coordinates
        public Color BGColor { get; set;}

        private ContentManager content { get; set; }

        public TestLevel(ContentManager content)
        {
            this.BGColor = Color.AliceBlue;
            this.content = content;
        }
        public List<IEnemy> GetEnemies()
        {
            return new List<IEnemy>(); // Return empty list.
        }

        private static void placeGroundAt(TileMap map, TextureRegion ground, Point tilePos)
        {
            var block = new Ground(ground);
            block.location = new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize);
            map.addBlockAt(tilePos, block);
        }

        public void Populate(TileMap tileMap)
        {
            TextureAtlas blocksTexture = TextureAtlas.FromFile(this.content, "images/block-definition.xml");
            TextureRegion ground = blocksTexture.GetRegion("ground");
            for (int x = 0; x < 50; x++)
            {
                for (int y = 12; y < 24; y++) 
                {
                    if (x%2 == 0 && x > 12 && x < 38 && y < 15) { continue; }
                    placeGroundAt(tileMap, ground, new Point(x, y));
                }
            }
        }
    }
}
