using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SpriteZero.Sprites;

public class GoombaWalk1 : ISprite
{
    private AnimatedSprite sprite;
    public Vector2 location { get; set; }
    private float speed = 4f;

    public GoombaWalk1(AnimatedSprite animated)
    {

        sprite = animated;
        sprite.Scale = new Vector2(4f);

        location = new Vector2(180, 120);
    }
    public void Update(GameTime gameTime)
    {
        sprite.Update(gameTime);
        location = new Vector2(location.X + speed, location.Y);
        if (location.X > 1280)
        {
            location = new Vector2(-sprite.Width * 4, location.Y);
        }
    }
    public void Draw(SpriteBatch spriteBatch)
    {

        sprite.Draw(spriteBatch, location);
    }
}