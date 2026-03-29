using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SprintZero;

public class Coin : ICollidable
{
    private AnimatedSprite sprite;
    public Vector2 location { get; set; }
    public Hitbox Collider { get; set; }
    public Rectangle RectCollider { get; set; }
    private float riseSpeed = 2f;
    private int rise = 40;
    private float startY;

    private bool rising = true;



    public Coin(AnimatedSprite animated)
    {
        sprite = animated;
        sprite.Scale = new Vector2(4f);
        location = new Vector2(400, 600);
        startY = location.Y;
    }

    public void Update(GameTime gameTime)
    {
        sprite.Update(gameTime);
        if (rising)
        {
            location = new Vector2(location.X, location.Y - riseSpeed);
            if (startY - location.Y >= rise)
            {
                rising = false;
            }
        }
        else
        {
            location = new Vector2(location.X, location.Y + riseSpeed);
            if (location.Y >= startY)
            {
                location = new Vector2(location.X, startY);
                rising = true;
            }
        }
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        sprite.Draw(spriteBatch, location);
    }
}