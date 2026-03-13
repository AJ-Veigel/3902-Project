using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SprintZero;
using SpriteZero.Sprites;

   public class Flower : ICollidable
{
    private AnimatedSprite sprite;


    private Vector2 _location;
    public Vector2 location
    {
        get { return _location; }
        set { _location = value; }
    }
    public Hitbox Collider {get; set;}
    private const float SCALE = 4f;

    private float startY;
    private float riseSpeed = 2f;
    private float riseHeight = 100f; 

    private bool rising = true;

    public Flower(AnimatedSprite animated)
    {
        sprite = animated;
        sprite.Scale = new Vector2(SCALE);
        _location = new Vector2(300, 300);
        startY = _location.Y;
        Collider = new Hitbox((int)_location.X, (int)_location.Y, (int)sprite.Width, (int)sprite.Height);
    }

    public Boolean GetCollidable()
    {
        return true;
    }

    public void Update(GameTime gameTime)
    {
        sprite.Update(gameTime);

        if (rising)
        {
          
            _location.Y -= riseSpeed;

            if (_location.Y <= startY - riseHeight)
            {
                _location.Y = startY - riseHeight;
                rising = false; 
            }
        }
        Collider = new Hitbox((int)_location.X, (int)_location.Y, (int)sprite.Width, (int)sprite.Height);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        sprite.Draw(spriteBatch, _location);
    }
}
