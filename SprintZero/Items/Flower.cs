using System.Net.Mime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using Microsoft.Xna.Framework.Content;
using SprintZero;
using SoundManager;
using SprintZero.Marios;

public class Flower : ICollectable
{
    private AnimatedSprite sprite;
    private Vector2 _location;
    public Vector2 location
    {
        get { return _location; }
        set { _location = value; }
    }
    public Hitbox Collider { get; set; }
    public Rectangle RectCollider { get; set; }
    private const float SCALE = 4f;
    public bool Collected {get;set;} =false;
    private float startY;
    private float riseSpeed = 2f;
    private float riseHeight = 20f;

    private bool rising = true;

    public Flower(AnimatedSprite animated)
    {
        sprite = animated;
        sprite.Scale = new Vector2(SCALE);
        _location = new Vector2(500, 600);
        startY = _location.Y;
        //Collider = new HitBox((int)_location.X, (int)_location.Y, (int)sprite.Width, (int)sprite.Height);
        RectCollider = new Rectangle((int)_location.X, (int)_location.Y, (int)sprite.Width, (int)sprite.Height);

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
        RectCollider = new Rectangle((int)_location.X, (int)_location.Y, (int)sprite.Width, (int)sprite.Height);
    }
    
    public bool CheckCollisions(IMario mario)
{
    bool isCollected = false;

    if (!Collected)
    {
        if (RectCollider.Intersects(mario.MarioCollider))
        {
            Collected = true;
            Music.itemSound.Play(); // plays the flower collection sound
            isCollected = true;
        }
    }

    return isCollected;
}

    public void Draw(SpriteBatch spriteBatch)
    {
        if(!Collected   )
        sprite.Draw(spriteBatch, _location);
    }
}
