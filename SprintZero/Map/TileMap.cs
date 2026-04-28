using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SprintZero.blocks;


namespace SprintZero.Map
{
    public class TileMap
    {
        private Dictionary<Point, IBlock> map;
        private Dictionary<Point, IPipe> pipeMap;
        private Vector2 marioSpawnPos = new Vector2(600, 600);
        private Color backgroundColor = Color.CornflowerBlue;

        private List<IBlock> cachedBlockList = new List<IBlock>();
        private List<IPipe> cachedPipeList = new List<IPipe>();

        /*
         *  It would probably be more efficient to instead map to like, 4x4 tiles containing blocks, or something.
         *  Doesn't really matter though, there can only be so many blocks on screen at once, so this won't be a bottleneck.
         *  (Unless we need to run at like, 10000 fps or something)
         */

        public TileMap()
        {
            map = new Dictionary<Point, IBlock>();
            pipeMap = new Dictionary<Point, IPipe>();
        }

        public void addBlockAt(Point p, IBlock block)
        {
            map.Add(p, block);
        }

        public void addPipeAt(Point p, IPipe pipe)
        {
            pipeMap.Add(p, pipe);
        }

        public void removeBlockAt(Point p)
        {
            map.Remove(p);
        }

        public IBlock getBlockAt(Point p)
        {
            return map[p];
        }

        public Vector2 getSpawn()
        {
            return marioSpawnPos;
        }

        public void setSpawn(Vector2 pos)
        {
            marioSpawnPos = pos;
        }

        public Color GetBackgroundColor()
        {
            return backgroundColor;
        }

        public void SetBackgroundColor(Color color)
        {
            backgroundColor = color;
        }

        public List<IBlock> getBlocksInRectangle(Rectangle rect)
        {
            cachedBlockList.Clear();
            int tileSize = 64;

            int leftTile = rect.Left / tileSize;
            int rightTile = rect.Right / tileSize;
            int topTile = rect.Top / tileSize;
            int bottomTile = rect.Bottom / tileSize;

            for (int x = leftTile; x <= rightTile; x++)
            {
                for (int y = topTile; y <= bottomTile; y++)
                {
                    Point p = new Point(x, y);

                    if (map.ContainsKey(p))
                    {
                        cachedBlockList.Add(map[p]);
                    }
                }
            }

            return cachedBlockList;
        }

        public List<IPipe> getPipesInRectangle(Rectangle rect)
        {
            cachedPipeList.Clear();

            int tileSize = 64;

            int leftTile = rect.Left / tileSize;
            int rightTile = rect.Right / tileSize;
            int topTile = rect.Top / tileSize;
            int bottomTile = rect.Bottom / tileSize;

            for (int x = leftTile; x <= rightTile; x++)
            {
                for (int y = topTile; y <= bottomTile; y++)
                {
                    Point p = new Point(x, y);

                    if (pipeMap.ContainsKey(p))
                    {
                        cachedPipeList.Add(pipeMap[p]);
                    }
                }
            }

            return cachedPipeList;
        }

        public List<IBlock> getBlocksInRectangle(Rectangle rect, int tolerance)
        {
            rect.Inflate(tolerance, tolerance);
            return this.getBlocksInRectangle(rect);
        }

        public List<IPipe> getPipesInRectangle(Rectangle rect, int tolerance)
        {
            rect.Inflate(tolerance, tolerance);
            return this.getPipesInRectangle(rect);
        }
        public void Draw(SpriteBatch batch, Rectangle cameraWorldBounds, int tileSize)
        {
            int leftTile = cameraWorldBounds.Left / tileSize - 2;
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
                    if (pipeMap.TryGetValue(tilePos, out IPipe pipe))
                    {
                        pipe.Draw(batch);
                    }
                }
            }
        }
        public void Update(GameTime gametime, Rectangle cameraWorldBounds, int tileSize)
        {
            int leftTile = cameraWorldBounds.Left / tileSize - 2;
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
                        block.Update(gametime);
                    }
                    if (pipeMap.TryGetValue(tilePos, out IPipe pipe))
                    {
                        pipe.Update(gametime);
                    }
                }
            }
        }

        public List<IBlock> getAllBlocks()
        {
            return new List<IBlock>(map.Values);
        }
    }
}
