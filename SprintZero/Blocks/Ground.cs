using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SprintZero.blocks;
using SprintZero.Marios;

public class Ground : IBlock
{

    private TextureRegion sprite;
    public Vector2 location { get; set; }
    public Rectangle Collider { get; set; }
    private const float SCALE = 4f;
  
    public Ground(TextureRegion region, Vector2 pos)
    {
        sprite = region;
        location = pos;
        Collider = new Rectangle((int)location.X, (int)location.Y, (int)(sprite.Width * SCALE), (int)(sprite.Height * SCALE));
    }
    public void Update(GameTime gameTime)
    {
        Collider = new Rectangle(
            (int)location.X,
            (int)location.Y,
            (int)(sprite.Width * SCALE),
            (int)(sprite.Height * SCALE)
        );
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        sprite.Draw(spriteBatch, location, Color.White, 0f, Vector2.One, 4f, SpriteEffects.None, 0f);

    }

}