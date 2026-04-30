using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SprintZero.background;
using SprintZero.blocks;
using SprintZero.Marios;

public class Chain : IBackground
{

    private TextureRegion sprite;
    public Vector2 location { get; set; }
    private const float SCALE = 4f;
  
    public Chain(TextureRegion region, Vector2 pos)
    {
        sprite = region;
        location = pos;
    }
    public void Update(GameTime gameTime)
    {

    }

    public void Draw(SpriteBatch spriteBatch)
    {
        sprite.Draw(spriteBatch, location, Color.White, 0f, Vector2.One, SCALE, SpriteEffects.None, 0f);

    }
}