using SprintZero.Map;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using SpriteZero.Enemies;
using System.ComponentModel;
using SprintZero.Marios;

namespace SprintZero.Map
{
    public interface ILevel
    {
        Color BGColor { get; set; }
        List<IEnemy> GetEnemies();
        void FromFile(TileMap tilemap);
    
    }

}