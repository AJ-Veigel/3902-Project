using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SprintZero.blocks;

public class Axe : IBlock
{
    private AnimatedSprite sprite;
    public Vector2 location { get; set; }
    public Rectangle Collider { get; set; }
    private const float SCALE = 4f;

    public Axe(AnimatedSprite animation, Vector2 location)
    {
        sprite = animation;
        sprite.Scale = new Vector2(SCALE);
        this.location = location;

        Collider = new Rectangle(
            (int)location.X,
            (int)location.Y,
            (int)sprite.Width,
            (int)sprite.Height
        );
    }

    public void Update(GameTime gameTime)
    {
        sprite.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        sprite.Draw(spriteBatch, location);
    }
}