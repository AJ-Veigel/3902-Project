using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using SpriteZero.Enemies;
using SpriteZero.Sprites;
using SprintZero;
using Microsoft.Xna.Framework.Graphics;

namespace SprintZero.Map
{
    public class MapManager
    {
        // Handles collisions, updates, and drawing within one 'map' (i.e, the above ground section of 1-1)
        private TileMap map;
        private Color BGcolor;
        private List<IEnemy> enemies;
        private List<IProjectile> projectiles;

        public MapManager(ILevel Level)
        {
            map = new TileMap();
            Level.FromFile(map);
            BGcolor = Level.BGColor;
            enemies = new List<IEnemy>();
            projectiles = new List<IProjectile>();
        }

        public void Update(GameTime dt)
        {
            // STUB
            foreach (var enemy in enemies)
            {
                enemy.Update(dt);
            }
        }

        public void Draw(SpriteBatch batch, Rectangle cameraBounds)
        {
            // STUB
            foreach (var enemy in this.enemies)
            {
                enemy.Draw(batch);
            }
            map.Draw(batch, cameraBounds, 64);
        }

    }
}
