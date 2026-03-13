using Microsoft.Xna.Framework;
using MonoGameLibrary.Graphics;
using SprintZero.Map;
using SpriteZero.blocks;

public class Level1 : ILevel
{
    private const int TileSize = 16;

    public void Populate(TileMap tileMap)
    {
        for (int x = 0; x < 50; x++)
        {
            Point tilePos = new Point(x, 12);

            tileMap.addBlockAt(
                tilePos,
                new Ground(,tilePos.X * TileSize, tilePos.Y * TileSize))
            );
        }
    }
}