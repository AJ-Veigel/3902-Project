using System.Collections.Generic;
using SprintZero.Items;
using SprintZero.Marios;

namespace playerItemCollision;

public class playerItemCollisions
{

    public void CheckCollisions(IMario mario, List<ICollectable> items, int currentMarioNum, System.Action<int> setMario)
    {
        foreach (ICollectable item in items)
        {
            if (item.RectCollider.Intersects(mario.MarioCollider))
            {
                if(item.GetType() == typeof(Flower) && currentMarioNum <= 1)
                {
                    setMario(2);
                }
                else if (item.GetType() == typeof(Mushroom) && currentMarioNum == 0)
                {
                    setMario(1);
                }
            }
        }
    }
}