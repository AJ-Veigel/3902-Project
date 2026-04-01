using SprintZero;
using SprintZero.Marios;

namespace playerItemCollision;

public class playerItemCollisions{

    public void CheckCollisions(IMario mario, ICollidable item, int currentItemCount,int currentMarioNum, System.Action<int> setMario)
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
 
 
