using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SprintZero;
using SpriteZero.Sprites;

public class Mushroom : ICollidable
{
   private TextureRegion sprite;
    public Vector2 location {get;set;}
    public Hitbox Collider {get; set;}
    private const float SCALE = 4f;
    private float horizontalSpeed = 2f;
    private float verticalSpeed=0f;
    private float gravity = 0.3f;
    private float riseUp = 40f;
    private float startY;
    private bool rising = true;
    public Mushroom(TextureRegion region)
    {
        sprite = region;
        location = new Vector2(300,300);
        startY = location.Y;
        Collider = new Hitbox((int)location.X, (int)location.Y, sprite.Width * (int)SCALE, sprite.Height * (int)SCALE);
    }

    public Boolean GetCollidable()
    {
        return true;
    }
    public void Update(GameTime gameTime)
    {
        if (rising)
        {
            location = new Vector2(location.X,location.Y-1f);
            if (startY - location.Y >= riseUp)
            {
                rising=false;
                verticalSpeed = 0f;
            }
        }
        else
        {
            verticalSpeed +=gravity;
            location = new Vector2(location.X,location.Y + verticalSpeed);
        }
        if (location.Y >= 500)
        {
            location = new Vector2(location.X,500);
            verticalSpeed = 0f;
            location = new Vector2(location.X+horizontalSpeed,location.Y);
        }
        Collider = new Hitbox((int)location.X, (int)location.Y, sprite.Width * (int)SCALE, sprite.Height * (int)SCALE);
    }

public void Draw(SpriteBatch spriteBatch)
{
   sprite.Draw(spriteBatch,location,Color.White,0f,Vector2.One,SCALE,SpriteEffects.None,0f); 
}
}