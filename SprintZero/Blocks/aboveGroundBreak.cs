using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SpriteZero.Sprites;

public class AboveGroundBreak : ISprite
{
    private AnimatedSprite sprite;
    public Vector2 location{get;set;}
    private Vector2 velocity;
    private float gravity = 0.5f;
    

    public AboveGroundBreak(AnimatedSprite animated)
    {
      
        sprite = animated;
        sprite.Scale = new Vector2(4f);
 
        location = new Vector2(530,325);
        velocity = new Vector2(-6f,-8f);
    }
    public void Update(GameTime gameTime)
    {
        sprite.Update(gameTime);
        velocity.Y +=gravity;
        location +=velocity;
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        sprite.Draw(spriteBatch,location);
    }
}