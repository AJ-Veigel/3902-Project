using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SprintZero.blocks;
using SprintZero.Marios;

public class MediumTube : IBlock
{
    private const float SCALE = 4f;
    private TextureRegion sprite;
    public Vector2 location { get; set; }
    public Rectangle Collider { get; set; }
    
    public bool GetCollidable() => true;
    public MediumTube(TextureRegion region)
    {
        sprite = region;

        location = new Vector2(0, 0);

    
        Collider = new Rectangle(
            (int)location.X,
            (int)location.Y,
            (int)(sprite.Width * SCALE),
            (int)(sprite.Height * SCALE)
        );
    }


    public void Update(GameTime gameTime)
    {
        UpdateCollider();
    }
private void UpdateCollider()
    {
          Collider = new Rectangle(
            (int)location.X,
            (int)location.Y,
            (int)(sprite.Width * SCALE),
            (int)(sprite.Height * SCALE));
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        sprite.Draw(spriteBatch, location, Color.White, 0f, Vector2.One, 4f, SpriteEffects.None, 0f);

    }

}
