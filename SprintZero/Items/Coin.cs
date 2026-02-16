using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SpriteZero.Sprites;

public class Coin : ISprite
{
   private AnimatedSprite sprite;
  public Vector2 location {get;set;}
   private float riseSpeed = 2f;
   private int rise = 40;
   private float startY;

   private bool rising=true;


 
   public Coin(AnimatedSprite animated)
    {
        sprite = animated;
        sprite.Scale = new Vector2(4f);
        location = new Vector2(300,300);
        startY = location.Y;
    }
  
    public void Update(GameTime gameTime)
    {
        sprite.Update(gameTime);
        if (rising)
        {
            location = new Vector2(location.X,location.Y-riseSpeed);
            if (startY -location.Y >= rise)
            {
                rising = false;
            }
        }
        else
        {
            location = new Vector2(location.X,location.Y +riseSpeed);
            if (location.Y >= startY)
            {
                location = new Vector2(location.X,startY);
                rising = true;
            }
        }
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        sprite.Draw(spriteBatch,location);
    }
}