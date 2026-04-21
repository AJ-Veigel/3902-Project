using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SprintZero.Items;
using SprintZero;
using SoundManager;
using SprintZero.Marios;
using System.Runtime.Serialization;
using System.Runtime.CompilerServices;

public class Coin : ICollectable
{
    private AnimatedSprite sprite;
    public Vector2 location { get; set; }
    public Rectangle RectCollider { get; set; }
    public float VelocityX { get; set; }
    public float VelocityY { get; set; }
    public bool Collected { get; set; } = false;
    public bool onGround { get; set; }
    public bool Collidable { get; set; } = true;
    private float gravity = 0.4f;
    private float bounceVelocity =-8f;
    private float groundY;
    private bool shouldLeave = false;
    private bool endSound = false;
    public Coin(AnimatedSprite animated)
    {
        sprite = animated;
        sprite.Scale = new Vector2(4f);
        location = new Vector2(400, 700);
    }

    public Coin(AnimatedSprite animated, Vector2 pos)
    {
        sprite = animated;
        sprite.Scale = new Vector2(4f);
        location = pos;
        groundY = pos.Y;
        VelocityY = bounceVelocity;
    }

  public void Update(GameTime gameTime)
{
    sprite.Update(gameTime);
    VelocityY += gravity;

    location = new Vector2(location.X, location.Y + VelocityY);
    if (location.Y >= groundY)
    {
        location = new Vector2(location.X, groundY);
        VelocityY = 0;
        if (!endSound)
            {
             Music.coinSound.Play();
             endSound = true;   
            }
        shouldLeave=true;
    }

    RectCollider = new Rectangle(
        (int)location.X,
        (int)location.Y,
        (int)sprite.Width,
        (int)sprite.Height
    );
}
    public void Update(GameTime gameTime, int coins, int score)
    {
        sprite.Update(gameTime);
        coins++;
        score += 100;
    }
    public void ReverseDirection()
    {
        VelocityX = -VelocityX;
    }
    public bool CheckCollisions(IMario mario)
    {
        bool isCollected = false;
        if (!Collected)
        {
            if (RectCollider.Intersects(mario.MarioCollider))
            {
                Collected = true;
                Music.coinSound.Play();
                isCollected = true;

            }
        }
        return isCollected;

    }
    public void Draw(SpriteBatch spriteBatch)
    {
        if (!shouldLeave)
            sprite.Draw(spriteBatch, location);
    }
}