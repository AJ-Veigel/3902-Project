using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SprintZero.Items;

public class ItemBlockCollisions
{
    public void CheckCollisions(ICollectable item, List<Rectangle> blocks)
    {
        bool onGround = false;

        foreach (var block in blocks)
        {
            if (item.RectCollider.Intersects(block))
            {
                Rectangle overlap = Rectangle.Intersect(item.RectCollider, block);

                if (overlap.Height < overlap.Width)
                {
                    if (item.RectCollider.Bottom > block.Top)
                    {
                        item.location = new Vector2(
                            item.location.X,
                            block.Top - item.RectCollider.Height
                        );

                        item.VelocityY = 0;
                        onGround = true;
                    }
                }
                else
                {
               
                    item.ReverseDirection();
                }
            }
        }

        item.onGround = onGround;
    }
}