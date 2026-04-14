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
    public bool onGround { get; set; } = false;
    private const float SCALE = 4f;
    private float Gravity = 0.3f;
    public bool Collected { get; set; } = false;
    public bool Collidable { get; set; } = false;
    private float spawnTimer = 0f;
    public Mushroom(TextureRegion region)
    {
        sprite = region;
        location = new Vector2(300, 700);
        RectCollider = new Rectangle((int)location.X, (int)location.Y, (int)(sprite.Width * SCALE), (int)(sprite.Height * SCALE));
        VelocityX = 2f;
    }
    public Mushroom(TextureRegion region, Vector2 pos)
    {
        sprite = region;
        location = pos;
        RectCollider = new Rectangle((int)location.X, (int)location.Y, (int)(sprite.Width * SCALE), (int)(sprite.Height * SCALE));
        VelocityX = 2f;
    }

    public void ReverseDirection()
    {
        VelocityX = -VelocityX;
    }
    public void Update(GameTime gameTime)
    {
        spawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if(spawnTimer > 0.75f)
        {
            Collidable = true;
        }
        if (!onGround)
        {
            VelocityY = MathHelper.Clamp(VelocityY + Gravity, -10f, 12f);
        }
        else
        {
            VelocityY = 0f;
        }
        location = new Vector2(location.X + VelocityX, location.Y);
        RectCollider = new Rectangle((int)location.X, (int)location.Y, (int)(sprite.Width * SCALE), (int)(sprite.Height * SCALE));
    }
    public void Update(GameTime gameTime, int coins, int score)
    {
        spawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if(spawnTimer > 0.75f)
        {
            Collidable = true;
        }
        if (!onGround)
        {
            VelocityY = MathHelper.Clamp(VelocityY + Gravity, -10f, 12f);
        }
        else
        {
            VelocityY = 0f;
        }
        location = new Vector2(location.X + VelocityX, location.Y);
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