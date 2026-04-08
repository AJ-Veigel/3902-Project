using SprintZero.Items;
using SprintZero.Marios;

namespace playerItemCollision;

public class playerItemCollisions{

    public void CheckCollisions(IMario mario, ICollectable item, int currentItemCount,int currentMarioNum,System.Action<int> setMario)
    {
        if (item.RectCollider.Intersects(mario.MarioCollider))
        {
            if (currentItemCount == 0 && currentMarioNum <= 1)
            {
                setMario(2);
            }
            else if (currentItemCount == 3 && currentMarioNum ==0)
            {
                setMario(1);
            }
        }
    }
 }
 
 
