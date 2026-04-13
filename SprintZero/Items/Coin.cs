using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SprintZero.Items;
using SprintZero;
using SoundManager;
using SprintZero.Marios;

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
    }

    public void Update(GameTime gameTime)
    {
        sprite.Update(gameTime);
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
        if (!Collected)
            sprite.Draw(spriteBatch, location);
    }
}