using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Xna.Framework.Graphics;
using SpriteZero.Sprites;

namespace SprintZero.Map
{
    public class TileMap
    {
        private Dictionary<Point, ISprite> map; // We really should have a separate interface for blocks

        /*
         *  It would probably be more efficient to instead map to like, 4x4 tiles containing blocks, or something.
         *  Doesn't really matter though, there can only be so many blocks on screen at once, so this won't be a bottleneck.
         *  (Unless we need to run at like, 10000 fps or something)
         */

        public TileMap()
        {
            map = new Dictionary<Point, ISprite>();
        }

        public void addBlockAt(Point p, ISprite block)
        {
            map.Add(p, block);
        }

        public void removeBlockAt(Point p)
        {
            map.Remove(p);
        }

        public ISprite getBlockAt(Point p)
        {
            return map[p];
        }

        public List<ISprite> getBlocksInRectangle(Rectangle rect)
        {
            var list = new List<ISprite>();
            for (int x = rect.Left; x < rect.Right; x++)
            {
                for (int y = rect.Top; y < rect.Bottom; y++)
                {
                    if (map.ContainsKey(new Point(x, y)))
                    {
                        list.Add(map[new Point(x, y)]);
                    }
                }
            }
            return list;
        }

        public void Draw(SpriteBatch batch)
        {
            // TODO: work with camera system to not draw every block ever.
            Dictionary<Point, ISprite>.ValueCollection values = map.Values;
            foreach (ISprite s in values)
            {
                s.Draw(batch);
            }
        }
    }
}
