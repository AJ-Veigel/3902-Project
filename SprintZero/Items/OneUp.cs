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
    public float VelocityX { get; set; }
    public float VelocityY { get; set; }
    public bool Collected {get;set;} =false;
    public bool onGround { get; set; }
    private float gravity = 0.3f;
    private float riseUp = 40f;
    private float startY;
    private bool rising = true;

    public OneUp(TextureRegion region)
    {
        sprite = region;
        location = new Vector2(400, 700);
        startY = location.Y;
    }

    public OneUp(TextureRegion region, Vector2 pos)
    {
        sprite = region;
        location = pos;
        startY = location.Y;
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
        //Change 1000 to viewport length
        if (location.X >= 1000 | location.X <= 0)
        {
            VelocityX *= -1;
        }
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