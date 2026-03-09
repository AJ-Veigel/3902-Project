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
    private float riseSpeed = 3f;
    private bool isHit = false;
    private bool rising = false;

    public questionMarkHit(AnimatedSprite animated)
    {
        sprite = animated;
        sprite.Scale = new Vector2(SCALE);
        sprite.Pause(); 
        location = new Vector2(300, 450);
        startY = location.Y;

        Collider = new Rectangle((int)location.X, (int)location.Y, (int)sprite.Width, (int)sprite.Height);
    }

    public void Update(GameTime gameTime)
    {
        sprite.Update(gameTime);

        if (rising)
        {
  
            location = new Vector2(location.X, location.Y - riseSpeed);
            if (location.Y <= startY - bounceHeight)
            {
                location = new Vector2(location.X, startY - bounceHeight);
                rising = false; 
            }
        }
        else if (isHit && location.Y < startY)
        {
         
            location = new Vector2(location.X, location.Y + riseSpeed);
            if (location.Y >= startY)
            {
                location = new Vector2(location.X, startY);
                sprite.PauseFrame(2); 
            }
        }

        Collider = new Rectangle((int)location.X, (int)location.Y, (int)sprite.Width, (int)sprite.Height);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        sprite.Draw(spriteBatch, location);
    }

    public void onHit(IMario mario, CollisionSide theSide)
    {
        if (theSide == CollisionSide.Bottom && !isHit)
        {
            isHit = true;
            rising = true;
               
        }
    }
}