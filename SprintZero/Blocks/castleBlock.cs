using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SprintZero.blocks;
using SprintZero.Marios;

public class CastleBlock : IBlock
{

    private TextureRegion sprite;
    public Vector2 location { get; set; }
    public Rectangle Collider { get; set; }
    private const float SCALE = 4f;
    public CastleBlock(TextureRegion region, Vector2 location)
    {
        sprite = region;
        this.location = location;
        Collider = new Rectangle((int)location.X, (int)location.Y, (int)sprite.Width, (int)sprite.Height);
    }
    public void Update(GameTime gameTime) { }

    public void Draw(SpriteBatch spriteBatch)
    {
        sprite.Draw(spriteBatch, location, Color.White, 0f, Vector2.One, SCALE, SpriteEffects.None, 0f);

    }
}