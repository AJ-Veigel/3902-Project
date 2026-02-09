
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SpriteZero.Sprites;

public class LRAnimated : ISprite
{

   private AnimatedSprite sprite;
  public Vector2 location {get;set;}
   private float speed = 4f;
   public  LRAnimated(AnimatedSprite animated)
    {
        //setting sprite to the passed in sprite
        sprite = animated;
       //setting its location
        location = new Vector2(530,325);
    }

    public void Update(GameTime gameTime)
    {
        sprite.Update(gameTime);
        location = new Vector2(location.X + speed, location.Y);
        if (location.X > 1280)
        {
            location = new Vector2(-sprite.Width * 4,location.Y);
        }
    }
 
    public void Draw(SpriteBatch spriteBatch)
    {
        sprite.Draw(spriteBatch,location);
    }
}