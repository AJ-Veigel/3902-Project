using System.Net.Mime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using Microsoft.Xna.Framework.Content;
using SprintZero.Items;
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
    public Rectangle RectCollider { get; set; }
    public float VelocityX { get; set; }
    public float VelocityY { get; set; }
    private const float SCALE = 4f;
    public bool Collected { get; set; } = false;
    public bool onGround { get; set; }
    private float startY;
    private float riseSpeed = 2f;
    private float riseHeight = 20f;

    private bool rising = false;

    public Flower(AnimatedSprite animated)
    {
        sprite = animated;
        sprite.Scale = new Vector2(SCALE);
        _location = new Vector2(500, 600);
        startY = _location.Y;
        //Collider = new HitBox((int)_location.X, (int)_location.Y, (int)sprite.Width, (int)sprite.Height);
        RectCollider = new Rectangle((int)_location.X, (int)_location.Y, (int)sprite.Width, (int)sprite.Height);
    }

    public Flower(AnimatedSprite animated, Vector2 pos)
    {
        sprite = animated;
        sprite.Scale = new Vector2(SCALE);
        _location = pos;
        startY = _location.Y;
        //Collider = new HitBox((int)_location.X, (int)_location.Y, (int)sprite.Width, (int)sprite.Height);
        RectCollider = new Rectangle((int)_location.X, (int)_location.Y, (int)sprite.Width, (int)sprite.Height);

    }
    public void ReverseDirection()
    {
        VelocityX = -VelocityX;
    }

    public void Update(GameTime gameTime)
    {
        sprite.Update(gameTime);
    }

    public void Update(GameTime gameTime, int coins, int score)
    {
        sprite.Update(gameTime);
        score += 400;
    }

    public bool CheckCollisions(IMario mario)
    {
        bool isCollected = false;

        if (!Collected)
        {
            if (RectCollider.Intersects(mario.MarioCollider))
            {
                Collected = true;
                Music.itemSound.Play();
                isCollected = true;
            }
        }

        return isCollected;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (!Collected)
            sprite.Draw(spriteBatch, _location);
    }
}
