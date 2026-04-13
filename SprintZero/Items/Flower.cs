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
    public bool Collidable { get; set; } = false;
    private float spawnTimer = 0f;

    public Flower(AnimatedSprite animated)
    {
        sprite = animated;
        sprite.Scale = new Vector2(SCALE);
        _location = new Vector2(500, 600);
        //Collider = new HitBox((int)_location.X, (int)_location.Y, (int)sprite.Width, (int)sprite.Height);
        RectCollider = new Rectangle((int)_location.X, (int)_location.Y, (int)sprite.Width, (int)sprite.Height);
    }

    public Flower(AnimatedSprite animated, Vector2 pos)
    {
        sprite = animated;
        sprite.Scale = new Vector2(SCALE);
        _location = pos;
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
        spawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if(spawnTimer > 0.5f)
        {
            Collidable = true;
        }
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
