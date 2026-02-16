using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SpriteZero.Sprites;

public class Star : ISprite
{
   private AnimatedSprite sprite;
 public Vector2 location{get;set;}
   private float horizontalSpeed = 2f;
   private float  verticalSpeed = 0f;
   private float gravity  = 0.3f;
   private float riseUp = 40f;
   private float startY;
   private bool rising = true;
   private float groundLevel = 500f;
   
   public Star(AnimatedSprite animated)
    {
        sprite = animated;
        sprite.Scale = new Vector2(2f);
        location = new Vector2(300,300);
        startY = location.Y;
    }
    public void Update(GameTime gameTime)
    {
        sprite.Update(gameTime);
        if (rising)
        {
            location = new Vector2(location.X, location.Y-1f);
            if (startY-location.Y>=riseUp){
                rising = false;
                verticalSpeed = 0f;
            }
        }
        else
        {
            verticalSpeed +=gravity;
            location = new Vector2(location.X,location.Y+verticalSpeed);
        }
        if (location.Y >= groundLevel)
        {
            location = new Vector2(location.X,groundLevel);
            verticalSpeed = 0f;
            location = new Vector2(location.X+horizontalSpeed,location.Y);
        }
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        sprite.Draw(spriteBatch,location);
    }

}