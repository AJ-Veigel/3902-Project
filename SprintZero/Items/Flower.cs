using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SpriteZero.Sprites;

public class Flower : ISprite
{
private AnimatedSprite sprite;
public Vector2 location{get;set;}

private float startY;

private float riseSpeed = 3f;
private float targetY= 10f;

private bool rising = true;
   
public Flower(AnimatedSprite animated)
    {
        sprite = animated;
        sprite.Scale = new Vector2(4f);
        location = new Vector2(300,300);
        startY = location.Y;
    }
  

   public void Update(GameTime gameTime)
{
    sprite.Update(gameTime);

    if (rising && location.Y > targetY)
    {
        location = new Vector2(location.X, location.Y - riseSpeed);

        if (location.Y <= targetY)
        {
            location = new Vector2(location.X, targetY);
            rising = false;
        }
    }
}
   
        public void Draw(SpriteBatch spriteBatch)
    {
        sprite.Draw(spriteBatch,location);
    }
}