
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SpriteZero.Sprites;

public class notMoving : ISprite
{

    private TextureRegion sprite;
    public Vector2 location {get;set;}
    public notMoving(TextureRegion region)
    {
        sprite = region;
        location = new Vector2(530,325);
    }

    public void Update(GameTime gameTime){}

    public void Draw(SpriteBatch spriteBatch)
    {
        sprite.Draw(spriteBatch,location,Color.White,0f,Vector2.One,4f,SpriteEffects.None,0f);

    }
}