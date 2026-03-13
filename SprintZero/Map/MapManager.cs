using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using SpriteZero.Enemies;
using SpriteZero.Sprites;
using SprintZero;


namespace SprintZero.Map
{
    internal class MapManager
    {
        // Handles collisions, updates, and drawing within one 'map' (i.e, the above ground section of 1-1)
        private TileMap map;
        private Color BGcolor;
        private List<IEnemy> enemies;
        private List<IProjectile> projectiles;

        public MapManager(Color BGColor)
        {
            this.map = new TileMap();
            this.BGcolor = BGColor;
            this.enemies = new List<IEnemy>();
            this.projectiles = new List<IProjectile>();
        }

        public void Load(ContentManager loader)
        {
            // Load in a file and do stuff? idk.
        }

        public void Update(GameTime dt)
        {
            // STUB
            foreach (var enemy in this.enemies)
            {
                enemy.Update(dt);
            }
        }

        public void Draw(GameTime dt)
        {
            // STUB
            foreach(var enemy in this.enemies)
            {
                enemy.Update(dt);
            }
        }

    }
}
