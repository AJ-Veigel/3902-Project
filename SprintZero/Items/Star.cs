using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SoundManager;
using SprintZero;
using SprintZero.Marios;

public class Star : ICollectable
{
    private AnimatedSprite sprite;
    public Vector2 location { get; set; }
    public Hitbox Collider { get; set; }
    public Rectangle RectCollider { get; set; }
    private const float SCALE = 4f;
    private float horizontalSpeed = 2f;
    public bool Collected {get;set;} =false;
    private float verticalSpeed = 0f;
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
    public void Update(GameTime gameTime)
    {
        sprite.Update(gameTime);
        if (rising)
        {
            location = new Vector2(location.X, location.Y - 1f);
            if (startY - location.Y >= riseUp)
            {
                rising = false;
                verticalSpeed = 0f;
            }
        }
        else
        {
            verticalSpeed += gravity;
            location = new Vector2(location.X, location.Y + verticalSpeed);
        }
        if (location.Y >= groundLevel)
        {
            location = new Vector2(location.X, groundLevel);
            verticalSpeed = 0f;
            location = new Vector2(location.X + horizontalSpeed, location.Y);
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
        if(!Collected)
        sprite.Draw(spriteBatch, location);
    }

}