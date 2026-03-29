using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGameLibrary.Graphics;
using SprintZero.blocks;
using SprintZero.Marios;
using System;

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

    public questionMarkHit(AnimatedSprite animated)
    {
        sprite = animated;
        sprite.Scale = new Vector2(SCALE);
        sprite.PauseFrame(0);
        location = new Vector2(600, 800);
        startY = location.Y;

        Collider = new Rectangle(
            (int)location.X,
            (int)location.Y,
            (int)(sprite.Width * SCALE),
            (int)(sprite.Height * SCALE)
        );
    }

    public Boolean GetCollidable()
    {
        return true;
    }

    public void Update(GameTime gameTime)
    {
        sprite.Update(gameTime);


        if (movingUp)
        {
            location = new Vector2(location.X, location.Y - bounceSpeed);

            if (location.Y <= startY - bounceHeight)
            {
                location = new Vector2(location.X, startY - bounceHeight);
                movingUp = false;
                movingDown = true;
            }
        }

        if (movingDown)
        {
            location = new Vector2(location.X, location.Y + bounceSpeed);

            if (location.Y >= startY)
            {
                location = new Vector2(location.X, startY);
                movingDown = false;

                sprite.PauseFrame(3);
            }
        }

        Collider = new Rectangle(
            (int)location.X,
            (int)location.Y,
            (int)(sprite.Width * SCALE),
            (int)(sprite.Height * SCALE)
        );
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        sprite.Draw(spriteBatch, location);
    }

   
   public void onCollision(IMario mario, CollisionSide side)
    {

    if (!isHit && side == CollisionSide.Bottom){
            isHit = true;
            movingUp = true;
            sprite.Stop();
            sprite.PauseFrame(3);
            mario.Jumping = true;
            mario.Jumping = true;
    
        }  else
        {
            if (side == CollisionSide.Left)
            {
                mario.location = new Vector2(Collider.Left - mario.MarioCollider.Width, mario.location.Y);
            } else if (side == CollisionSide.Right)
            {
                mario.location = new Vector2(Collider.Right,mario.location.Y);
    
                }
            else if (side == CollisionSide.Top)
            {
                mario.LandOnBlock(location.Y);
            }
        }

    }

}