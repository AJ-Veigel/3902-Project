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

    public Star(AnimatedSprite animated, Vector2 pos)
    {
        sprite = animated;
        sprite.Scale = new Vector2(SCALE);
        location = pos;
        //Collider = new Rectangle((int)location.X, (int)location.Y, (int)sprite.Width, (int)sprite.Height);
        RectCollider = new Rectangle((int)location.X, (int)location.Y, (int)(sprite.Width), (int)(sprite.Height));
        VelocityX = 2f;

    }
    public void ReverseDirection()
    {
        VelocityX = -VelocityX;
    }
    public void Update(GameTime gameTime)
    {
        if (Collected) return;

        onGround = false;
        sprite.Update(gameTime);
        spawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (spawnTimer < 0.5f)
        {
            Collidable = false;
            return;
        }

        Collidable = true;
        if (!onGround)
        {
            VelocityY = MathHelper.Clamp(VelocityY + Gravity, -10f, 12f);
        }
        else
        {
            VelocityY = -6f;
        }
        location = new Vector2(location.X + VelocityX, location.Y + VelocityY);
        RectCollider = new Rectangle((int)location.X, (int)location.Y, (int)(sprite.Width), (int)(sprite.Height));
    }

    public void Update(GameTime gameTime, int coins, int score)
    {
        sprite.Update(gameTime);
    } 

    public void Draw(SpriteBatch spriteBatch)
    {
        if (!Collected)
        {
            Vector2 lockedPos = new Vector2((int)location.X, (int)location.Y);
            sprite.Draw(spriteBatch, lockedPos);
        }
    }

}