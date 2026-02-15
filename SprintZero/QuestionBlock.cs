using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SpriteZero.Sprites;

public class QuestionBlock : ISprite
{
    private AnimatedSprite sprite;
    public Vector2 location{get;set;}

    public QuestionBlock(AnimatedSprite animated)
    {
      
        sprite = animated;
        sprite.Scale = new Vector2(4f);
 
        location = new Vector2(530,325);
    }
    public void Update(GameTime gameTime)
    {
        sprite.Update(gameTime);
    }
    public void Draw(SpriteBatch spriteBatch)
    {
    
        sprite.Draw(spriteBatch,location);
    }
}