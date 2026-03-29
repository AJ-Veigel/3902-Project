using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SprintZero.Marios;
using SprintZero.blocks;
using System.Runtime.InteropServices.Marshalling;

public class AboveGroundBreak : IBlock
{
    private AnimatedSprite sprite;
    public Vector2 location { get; set; }
    public Rectangle Collider { get; set; }
    private const float SCALE = 4f;
    private Vector2 velocity;
    private float gravity = 0.5f;
    private bool isBroken = false;
    private bool playBreakingAnimation = false;

    public AboveGroundBreak(AnimatedSprite animated)
    {
        sprite = animated;
        sprite.Scale = new Vector2(SCALE);
        sprite.Pause();
        location = new Vector2(600, 700);
        velocity = Vector2.Zero;
        Collider = new Rectangle((int)location.X, (int)location.Y, (int)sprite.Width, (int)sprite.Height);

    }


    public void Update(GameTime gameTime)
    {
        if (playBreakingAnimation)
        {
            sprite.Play();
            sprite.Update(gameTime);

            velocity.Y += gravity;
            location += velocity;
        }
        else
        {
            sprite.Update(gameTime);
        }

        Collider = !isBroken
            ? new Rectangle((int)location.X, (int)location.Y, (int)sprite.Width, (int)sprite.Height)
            : Rectangle.Empty;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (!isBroken)
            sprite.Draw(spriteBatch, location);
    }
    
    public void onCollision(IMario mario, CollisionSide side)
    {
        bool shouldBreak = false;
    if (!isBroken){
        if(side == CollisionSide.Bottom && (mario is BigMario || mario is FireMario)){
            shouldBreak = true;

        } else
            {
                if (side == CollisionSide.Left)
                {
                    mario.location = new Vector2(Collider.Left - mario.MarioCollider.Width,mario.location.Y);
                }
                if (side == CollisionSide.Right)
                {
                    mario.location = new Vector2(Collider.Right,mario.location.Y);
                }
                else if (side == CollisionSide.Top)
                {
                    mario.LandOnBlock(location.Y);
                }
            }
            if (shouldBreak)
            {
                isBroken = true;
                playBreakingAnimation = true;
                velocity = new Vector2(-6f,-8f);
                mario.Jumping = true; 
                mario.Falling = true;
            }
    }
}
}
