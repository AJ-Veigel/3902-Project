using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SpriteZero.Sprites;

   public class Flower : ISprite
{
    private AnimatedSprite sprite;


    private Vector2 _location;
    public Vector2 location
    {
        get { return _location; }
        set { _location = value; }
    }

    private float startY;
    private float riseSpeed = 2f;
    private float riseHeight = 100f; 

    private bool rising = true;

    public Flower(AnimatedSprite animated)
    {
        sprite = animated;
        sprite.Scale = new Vector2(4f);
        _location = new Vector2(300, 300);
        startY = _location.Y;
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
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        sprite.Draw(spriteBatch, _location);
    }
}
