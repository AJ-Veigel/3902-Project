
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SpriteZero.Sprites;

public class upAndDownS : ISprite
{
   private TextureRegion sprite;
   private float speed = 4f;
   public Vector2 location {get;set;}

   public upAndDownS(TextureRegion region)
    {
        sprite = region;
        location = new Vector2(530,325);
    }

    public void Update(GameTime gameTime)
    {
        location = new Vector2(location.X, location.Y +speed);
        if(location.Y > 500 || location.Y <100)
        speed*=-1;
    }
    
    public void Draw(SpriteBatch spriteBatch)
    {
        sprite.Draw(spriteBatch,location, Color.White,0f,Vector2.One,4f,SpriteEffects.None,0f);
    }
}