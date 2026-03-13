using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SpriteZero.blocks;
using SpriteZero.Marios;

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
        location = new Vector2(400, 400);
        startY = location.Y;

        Collider = new Rectangle(
            (int)location.X,
            (int)location.Y,
            (int)(sprite.Width * SCALE),
            (int)(sprite.Height * SCALE)
        );
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

                sprite.PauseFrame(2);
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

    public void onCollision(IMario mario, CollisionSide theSide)
    {
      
        // if (theSide == CollisionSide.Bottom && !isHit && mario.Jumping && (mario is SmallMario || mario is FireMario || mario is BigMario ))
        // {
        //     isHit = true;
        //     movingUp = true;

        //     mario.Falling = false;
        //     mario.isOnGround = true;
        //     mario.Jumping = true;
        // }

    }
}