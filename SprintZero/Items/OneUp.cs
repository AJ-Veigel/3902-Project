using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SprintZero;
using SpriteZero.Sprites;

public class OneUp : ICollidable
{
   private TextureRegion sprite;
    public Vector2 location {get;set;}
    public Hitbox Collider {get; set;}
    private float horizontalSpeed = 2f;
    private float verticalSpeed=0f;
    private float gravity = 0.3f;
    private float riseUp = 40f;
    private float startY;
    private bool rising = true;

    public OneUp(TextureRegion region)
    {
        sprite = region;
        location = new Vector2(500,500);
        startY = location.Y;
        Collider = new Hitbox((int)location.X, (int)location.Y, (int)sprite.Width, (int)sprite.Height);
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
        //Change 1000 to viewport length
        if (location.X >= 1000 | location.X <= 0) 
        {
            horizontalSpeed *= -1;
        }
        Collider = new Hitbox((int)location.X, (int)location.Y, (int)sprite.Width, (int)sprite.Height);
    }

public void Draw(SpriteBatch spriteBatch)
{
   sprite.Draw(spriteBatch,location,Color.White,0f,Vector2.One,4f,SpriteEffects.None,0f);
}
}