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
    public Hitbox Collider { get; set; }
    public Rectangle RectCollider { get; set; }
    private float horizontalSpeed = 2f;
    private float verticalSpeed = 0f;
    public bool Collected {get;set;} =false;
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
    public void Update(GameTime gameTime)
    {
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
        if (location.Y >= 500)
        {
            location = new Vector2(location.X, 500);
            verticalSpeed = 0f;
            location = new Vector2(location.X + horizontalSpeed, location.Y);
        }
        //Change 1000 to viewport length
        if (location.X >= 1000 | location.X <= 0)
        {
            horizontalSpeed *= -1;
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