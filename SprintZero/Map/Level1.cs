using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SprintZero.blocks;
using SprintZero.Map;
using SpriteZero.Enemies;

namespace SprintZero.Map
{
    public class Level1 : ILevel
    {
        private const int TileSize = 64; // 64 in screen coordinates

        public Color BGColor { get; set; }
        private ContentManager content { get; set; }

        public Level1(ContentManager content)
        {
            this.BGColor = Color.AliceBlue;
            this.content = content;
        }

        public List<IEnemy> GetEnemies()
        {
            return new List<IEnemy>(); // Return empty list, for now.
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
            for (int x = -200; x < 50; x++)
            {
                placeGroundAt(tileMap, ground, new Point(x, 13));
            }

            for (int x = 50; x < 100; x++)
            {
                placeGroundAt(tileMap, ground, new Point(x, x + -37));
            }
        }

    }
}