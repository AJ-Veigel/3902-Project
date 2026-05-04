using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SprintZero.blocks;
using SprintZero.background;
using SprintZero.Items;


namespace SprintZero.Map
{
    public class TileMap
    {
        private Dictionary<Point, IBlock> blockMap;
        private Dictionary<Point, IPipe> pipeMap;
        private Dictionary<Point, ICollectable> itemMap;
        private Dictionary <Point, IBackground> backgroundMap;
        private Vector2 marioSpawnPos = new Vector2(600, 600);
        private Color backgroundColor = Color.CornflowerBlue;

        private List<IBlock> cachedBlockList = new List<IBlock>();
        private List<IPipe> cachedPipeList = new List<IPipe>();
        private List<ICollectable> cachedItemList = new List<ICollectable>();

        /*
         *  It would probably be more efficient to instead map to like, 4x4 tiles containing blocks, or something.
         *  Doesn't really matter though, there can only be so many blocks on screen at once, so this won't be a bottleneck.
         *  (Unless we need to run at like, 10000 fps or something)
         */

        public TileMap()
        {
            blockMap = new Dictionary<Point, IBlock>();
            pipeMap = new Dictionary<Point, IPipe>();
            itemMap = new Dictionary<Point, ICollectable>();
            backgroundMap = new Dictionary<Point, IBackground>();
        }

        public void addBlockAt(Point p, IBlock block)
        {
            blockMap.Add(p, block);
        }

        public void addPipeAt(Point p, IPipe pipe)
        {
            pipeMap.Add(p, pipe);
        }

        public void addItemAt(Point p, ICollectable item)
        {
            itemMap.Add(p, item);
        }

        public void addBackgroundAt(Point p, IBackground background)
        {
            backgroundMap.Add(p, background);
        }

        public void removeBlockAt(Point p)
        {
            blockMap.Remove(p);
        }

        public IBlock getBlockAt(Point p)
        {
            return blockMap[p];
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

        public void ClearMaps()
        {
            blockMap.Clear();
            pipeMap.Clear();
            itemMap.Clear();
            backgroundMap.Clear();
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

                    if (blockMap.ContainsKey(p))
                    {
                        cachedBlockList.Add(blockMap[p]);
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

        public List<ICollectable> getItemsInRectangle(Rectangle rect)
        {
            cachedItemList.Clear();

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

                    if (itemMap.ContainsKey(p))
                    {
                        cachedItemList.Add(itemMap[p]);
                    }
                }
            }

            return cachedItemList;
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

                    if (blockMap.TryGetValue(tilePos, out IBlock block))
                    {
                        block.Draw(batch);
                    }
                    if (pipeMap.TryGetValue(tilePos, out IPipe pipe))
                    {
                        pipe.Draw(batch);
                    }
                    if (itemMap.TryGetValue(tilePos, out ICollectable item))
                    {
                        item.Draw(batch);
                    }
                }
            }
        }
        public void DrawBackground(SpriteBatch batch, Rectangle cameraWorldBounds, int tileSize)
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

                    if (backgroundMap.TryGetValue(tilePos, out IBackground back))
                    {
                        back.Draw(batch);
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

                    if (blockMap.TryGetValue(tilePos, out IBlock block))
                    {
                        block.Update(gametime);
                    }
                    if (pipeMap.TryGetValue(tilePos, out IPipe pipe))
                    {
                        pipe.Update(gametime);
                    }
                    if (itemMap.TryGetValue(tilePos, out ICollectable item))
                    {
                        item.Update(gametime);
                    }
                    if (backgroundMap.TryGetValue(tilePos, out IBackground background))
                    {
                        background.Update(gametime);
                    }
                }
            }
        }

        public List<IBlock> getAllBlocks()
        {
            return new List<IBlock>(blockMap.Values);
        }
    }
}
