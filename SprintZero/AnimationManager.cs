using System;
using System.Numerics;
using SprintZero.blocks;
using SprintZero.Marios;

namespace SprintZero
{
    public static class AnimationManager
    {
        public static void PipeAnimation(IMario mario, IPipe pipe)
        {
            if (pipe is TubeTop)
            {
                int MarioFeet = mario.MarioCollider.Bottom;
                while (mario.MarioCollider.Top < MarioFeet)
                {
                    Vector2 marioDown = new Vector2(mario.MarioCollider.X, mario.MarioCollider.Y + 4);
                    mario.SetLocation(marioDown);
                }
            }
            else if(pipe is TubeLeft)
            {
                int MarioRight = mario.MarioCollider.Right;
                while (mario.MarioCollider.Left < MarioRight)
                {
                    Vector2 marioDown = new Vector2(mario.MarioCollider.X + 4,  mario.MarioCollider.Y);
                    mario.SetLocation(marioDown);
                }
            }
        }
    }
}