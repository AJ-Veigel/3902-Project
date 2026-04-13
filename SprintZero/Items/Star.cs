using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SoundManager;
using SprintZero.Items;
using SprintZero;
using SprintZero.Marios;

public class Star : ICollectable
{
    private AnimatedSprite sprite;
    public Vector2 location { get; set; }
    public Rectangle RectCollider { get; set; }
    public float VelocityX { get; set; }
    public float VelocityY { get; set; }
    private const float SCALE = 4f;
    public bool Collected { get; set; } = false;
    public bool onGround { get; set; }
    private float gravity = 0.3f;
    private float riseUp = 40f;
    private float startY;
    private bool rising = true;
    private float groundLevel = 500f;

    public Star(AnimatedSprite animated)
    {
        sprite = animated;
        sprite.Scale = new Vector2(SCALE);
        location = new Vector2(300, 700);
        startY = location.Y;
        //Collider = new Rectangle((int)location.X, (int)location.Y, (int)sprite.Width, (int)sprite.Height);
        RectCollider = new Rectangle((int)location.X, (int)location.Y, (int)(sprite.Width), (int)(sprite.Height));
    }

    public Star(AnimatedSprite animated, Vector2 pos)
    {
        sprite = animated;
        sprite.Scale = new Vector2(SCALE);
        location = pos;
        startY = location.Y;
        //Collider = new Rectangle((int)location.X, (int)location.Y, (int)sprite.Width, (int)sprite.Height);
        RectCollider = new Rectangle((int)location.X, (int)location.Y, (int)(sprite.Width), (int)(sprite.Height));
    }
    public void ReverseDirection()
    {
        VelocityX = -VelocityX;
    }
    public void Update(GameTime gameTime)
    {
        sprite.Update(gameTime);
        if (rising)
        {
            location = new Vector2(location.X, location.Y - 1f);
            if (startY - location.Y >= riseUp)
            {
                rising = false;
                VelocityY = 0f;
            }
        }
        else
        {
            VelocityY += gravity;
            location = new Vector2(location.X, location.Y + VelocityY);
        }
        if (location.Y >= groundLevel)
        {
            location = new Vector2(location.X, groundLevel);
            VelocityY = 0f;
            location = new Vector2(location.X + VelocityX, location.Y);
        }

        RectCollider = new Rectangle((int)location.X, (int)location.Y, (int)(sprite.Width), (int)(sprite.Height));
    }

    public bool CheckCollisions(IMario mario)
    {
        bool isCollected = false;
        if (!Collected)
        {
            if (RectCollider.Intersects(mario.MarioCollider))
            {
                Collected = true;
                Music.oneupSound.Play();
                isCollected = true;
            }
        }
        return isCollected;
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        if (!Collected)
            sprite.Draw(spriteBatch, location);
    }

}