using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SoundManager;
using SprintZero.Items;
using SprintZero;
using SprintZero.Marios;
using System.Dynamic;

public class Mushroom : ICollectable
{
    private TextureRegion sprite;
    public Vector2 location { get; set; }
    public Hitbox Collider { get; set; }
    public Rectangle RectCollider { get; set; }
    public float VelocityX { get; set; }
    public float VelocityY { get; set; }
    public bool onGround { get; set; }
    private const float SCALE = 4f;
    private float gravity = 0.3f;
    public bool Collected { get; set; } = false;
    private float riseUp = 40f;
    private float startY;
    private bool rising = true;
    public Mushroom(TextureRegion region)
    {
        sprite = region;
        location = new Vector2(300, 700);
        startY = location.Y;
        RectCollider = new Rectangle((int)location.X, (int)location.Y, (int)(sprite.Width * SCALE), (int)(sprite.Height * SCALE));
        VelocityX = 2f;
    }
    public Mushroom(TextureRegion region, Vector2 pos)
    {
        sprite = region;
        location = pos;
        startY = location.Y;
        RectCollider = new Rectangle((int)location.X, (int)location.Y, (int)(sprite.Width * SCALE), (int)(sprite.Height * SCALE));
        VelocityX = 2f;
    }

    public void ReverseDirection()
    {
        VelocityX = -VelocityX;
    }
    public void Update(GameTime gameTime)
    {
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
        if (location.Y >= 500)
        {
            location = new Vector2(location.X, 500);
            VelocityY = 0f;
            location = new Vector2(location.X + VelocityX, location.Y);
        }
        RectCollider = new Rectangle((int)location.X, (int)location.Y, (int)(sprite.Width * SCALE), (int)(sprite.Height * SCALE));
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
            sprite.Draw(spriteBatch, location, Color.White, 0f, Vector2.One, SCALE, SpriteEffects.None, 0f);
    }
}