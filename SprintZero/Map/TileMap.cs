using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using SpriteZero.blocks;


namespace SprintZero.Map
{
    public class TileMap
    {
        private Dictionary<Point, IBlock> map;

        /*
         *  It would probably be more efficient to instead map to like, 4x4 tiles containing blocks, or something.
         *  Doesn't really matter though, there can only be so many blocks on screen at once, so this won't be a bottleneck.
         *  (Unless we need to run at like, 10000 fps or something)
         */

        public TileMap()
        {
            map = new Dictionary<Point, IBlock>();
        }

        public void addBlockAt(Point p, IBlock block)
        {
            map[p] = block;
        }

        public void removeBlockAt(Point p)
        {
            map.Remove(p);
        }

        public IBlock getBlockAt(Point p)
        {
            return map[p];
        }
        
        public bool TryGetBlockAt(Point tilePos, out IBlock block)
        {
            return map.TryGetValue(tilePos, out block);
        }

        public List<IBlock> getBlocksInRectangle(Rectangle rect)
        {
            var list = new List<IBlock>();
            for (int x = rect.Left; x < rect.Right; x++)
            {
                for (int y = rect.Top; y < rect.Bottom; y++)
                {
                    Point p = new Point(x, y);
                    if (map.TryGetValue(p, out IBlock block))
                    {
                        list.Add(block);
                    }
                }
            }
            return list;
        }

        public void Draw(SpriteBatch batch, Rectangle cameraWorldBounds, int tileSize)
        {
            // TODO: work with camera system to not draw every block ever.
            int leftTile = cameraWorldBounds.Left / tileSize - 1;
            int rightTile = cameraWorldBounds.Right / tileSize + 1;
            int topTile = cameraWorldBounds.Top / tileSize - 1;
            int bottomTile = cameraWorldBounds.Bottom / tileSize + 1;

            for (int x = leftTile; x <= rightTile; x++)
            {
                for (int y = topTile; y <= bottomTile; y++)
                {
                    Point tilePos = new Point(x, y);

                    if (map.TryGetValue(tilePos, out IBlock block))
                    {
                        block.Draw(batch);
                    }
                }
            }
        }
    }
}
