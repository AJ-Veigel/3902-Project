using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SprintZero.Map;
using SprintZero.blocks;
using SpriteZero.Enemies;

namespace SprintZero.Map
{
    public class Level1 : ILevel
    {
        private const int TileSize = 64; // 64 in screen coordinates

        public Color BGColor { get; set; }
        public Level1()
        {
            this.BGColor = Color.AliceBlue; // Load whatever we need to from file here?
        }
        public void Populate(TileMap tileMap)
        {
            for (int x = 0; x < 50; x++)
            {
                Point tilePos = new Point(x, 12);


                // Once we're able to place blocks more efficiently, we could begin building the levels.
                // Below is an example of how it might work. 

                // tileMap.addBlockAt(
                //     tilePos,
                //     new Ground(region, tilePos.X * TileSize, tilePos.Y * TileSize)
                // );
            }
        }

        public List<IEnemy> GetEnemies()
        {
            var list = new List<IEnemy>();

            return list;
        }

    }
}