using SprintZero.Map;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using SpriteZero.Enemies;
using System.ComponentModel;

namespace SprintZero.Map
{
    public interface ILevel
    {
        Color BGColor { get; set; }
        void Populate(TileMap tileMap);

        List<IEnemy> GetEnemies();
    }

}