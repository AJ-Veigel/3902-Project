using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SpriteZero.Sprites;

public class Flower : ISprite
{
private AnimatedSprite sprite;
 public Vector2 location{get;set;}
private float startY;
private int rise = 40;
private float riseSpeed = 2f;
   
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

        if (location.Y > startY - rise)
        {
            location = new Vector2(location.X, location.Y-riseSpeed);
        } 
        if (location.Y < startY - rise)
        {
            location = new Vector2(location.X,startY-rise);
        }

    }
        public void Draw(SpriteBatch spriteBatch)
    {
        sprite.Draw(spriteBatch,location);
    }
}