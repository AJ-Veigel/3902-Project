using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SprintZero.Items;
using SprintZero;
using SprintZero.Marios;
using SoundManager;

public class OneUp : ICollectable
{
    private TextureRegion sprite;
    public Vector2 location { get; set; }
    public Rectangle RectCollider { get; set; }
    private const float Gravity = 0.3f;
    private const float SCALE = 4f;
    public float VelocityX { get; set; }
    public float VelocityY { get; set; }
    public bool Collected {get;set;} =false;
    public bool onGround { get; set; }
    public bool Collidable { get; set; } = false;
    private float spawnTimer = 0f;

    public OneUp(TextureRegion region)
    {
        sprite = region;
        location = new Vector2(400, 700);
    }

    public OneUp(TextureRegion region, Vector2 pos)
    {
        sprite = region;
        location = pos;
    }
    public void ReverseDirection()
    {
        VelocityX = -VelocityX;
    }
    public void Update(GameTime gameTime)
    {
        spawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if(spawnTimer > 0.5f)
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
        if(!Collected)
            sprite.Draw(spriteBatch, location, Color.White, 0f, Vector2.One, 4f, SpriteEffects.None, 0f);
    }
}