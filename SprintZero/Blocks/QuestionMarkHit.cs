using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using MonoGameLibrary.Graphics;
using SprintZero.blocks;
using SprintZero.Marios;
using SoundManager;


public class questionMarkHit : IBlock
{
    private AnimatedSprite sprite;

    public Vector2 location { get; set; }
    public Rectangle Collider { get; set; }
   
    private const float SCALE = 4f;
    private float startY;
    private float bounceHeight = 20f;
    private float bounceSpeed = 3f;

    private bool isHit = false;
    private bool movingUp = false;
    private bool movingDown = false;

    public questionMarkHit(AnimatedSprite animated, Vector2 pos)
    {
        sprite = animated;
        sprite.Scale = new Vector2(SCALE);
        sprite.Pause();
        location = pos;
        startY = location.Y;
    
        Collider = new Rectangle(
            (int)location.X,
            (int)location.Y,
            (int)sprite.Width,
            (int)sprite.Height);
    }
    public void Update(GameTime gameTime)
    {

        if (isHit)
        {
            if (movingUp)
            {
                location = new Vector2(location.X, location.Y - bounceSpeed);
                if (location.Y <= startY - bounceHeight)
                {
                    movingUp = false;
                    movingDown = true;
                    sprite.Play();
                }

            }
            else if (movingDown)
            {
                location = new Vector2(location.X, startY);
                if (location.Y >= startY)
                {
                    location = new Vector2(location.X, startY);
                    movingDown = false;
                    isHit = true;
                    sprite.PauseFrame(1);

                }

            }
        }
        sprite.Update(gameTime);
        Collider = new Rectangle((int)location.X, (int)location.Y, (int)sprite.Width, (int)sprite.Height);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        sprite.Draw(spriteBatch, location);
    }

    public void onCollision(IMario mario, CollisionSide side)
    {
        if (side == CollisionSide.Bottom && mario.yVelocity < 0.0) { mario.yVelocity = 0; } // bump.
        if (!isHit)
        {
            if (side == CollisionSide.Bottom)
            {
                isHit = true;
                Music.blockSound.Play();
                movingUp = true;
                movingDown = false;
            }
            else if (side == CollisionSide.Top)
            {
                mario.LandOnBlock(location.Y);
            }
            else if (side == CollisionSide.Left)
            {
                mario.location = new Vector2(Collider.Left - mario.MarioCollider.Width, mario.location.Y);
            }
            else if (side == CollisionSide.Right)

            {
                mario.location = new Vector2(Collider.Right, mario.location.Y);
            }
        }
        else if (isHit)
        {
            if (side == CollisionSide.Top)
            {
                mario.LandOnBlock(location.Y);

            }
            else if (side == CollisionSide.Left)
            {
                mario.location = new Vector2(Collider.Left - mario.MarioCollider.Width, mario.location.Y);
            }
            else if (side == CollisionSide.Right)
            {
                mario.location = new Vector2(Collider.Right, mario.location.Y);
            }
        }
    }


}
