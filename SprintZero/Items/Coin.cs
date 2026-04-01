using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using SprintZero;

public class Coin : ICollectable
{
    private AnimatedSprite sprite;
    private SoundEffect coinSound;
    public Vector2 location { get; set; }
    public Hitbox Collider { get; set; }
    public Rectangle RectCollider { get; set; }
    private float riseSpeed = 2f;
    private int rise = 40;
    private float startY;

    private bool rising = true;



    public Coin(AnimatedSprite animated,ContentManager content)
    {
        sprite = animated;
        sprite.Scale = new Vector2(4f);
        location = new Vector2(400, 700);
        startY = location.Y;
        //update to play sound to happen during collision 
        coinSound = content.Load<SoundEffect>("Music/coin");
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