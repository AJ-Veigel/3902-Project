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
    private float Gravity = 0.3f;
    public bool Collidable { get; set; } = false;
    private float spawnTimer = 0f;

    public Star(AnimatedSprite animated)
    {
        sprite = animated;
        sprite.Scale = new Vector2(SCALE);
        location = new Vector2(300, 700);
        //Collider = new Rectangle((int)location.X, (int)location.Y, (int)sprite.Width, (int)sprite.Height);
        RectCollider = new Rectangle((int)location.X, (int)location.Y, (int)(sprite.Width), (int)(sprite.Height));
    }

    public Star(AnimatedSprite animated, Vector2 pos)
    {
        sprite = animated;
        sprite.Scale = new Vector2(SCALE);
        location = pos;
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
        spawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (spawnTimer > 0.5f)
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
        RectCollider = new Rectangle((int)location.X, (int)location.Y, (int)(sprite.Width), (int)(sprite.Height));
    }

    public void Update(GameTime gameTime, int coins, int score)
    {
        sprite.Update(gameTime);
        spawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (spawnTimer > 0.5f)
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