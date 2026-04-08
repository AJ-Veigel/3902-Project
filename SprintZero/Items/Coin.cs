using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SprintZero.Items;
using SprintZero;
using SoundManager;
using System.Runtime.CompilerServices;
using SprintZero.Marios;

public class Coin : ICollectable
{
    private AnimatedSprite sprite;
    public Vector2 location { get; set; }
    public Hitbox Collider { get; set; }
    public Rectangle RectCollider { get; set; }
    public bool Collected {get;set;} =false;
    private float riseSpeed = 2f;
    private int rise = 40;
    private float startY;

    private bool rising = true;



    public Coin(AnimatedSprite animated)
    {
        sprite = animated;
        sprite.Scale = new Vector2(4f);
        location = new Vector2(400, 700);
        startY = location.Y;
    }

    public void Update(GameTime gameTime)
    {
        sprite.Update(gameTime);
        if (rising)
        {
            location = new Vector2(location.X, location.Y - riseSpeed);
            if (startY - location.Y >= rise)
            {
           
                rising = false;
            }
        }
        else
        {
            location = new Vector2(location.X, location.Y + riseSpeed);
            if (location.Y >= startY)
            {
                location = new Vector2(location.X, startY);
                rising = true;
            }
        }
        RectCollider = new Rectangle((int)location.X, (int)location.Y,(int)sprite.Width,(int)sprite.Height);
    }
public bool CheckCollisions(IMario mario)
    {
       bool isCollected = false;
       if (!Collected)
        {
            if(RectCollider.Intersects(mario.MarioCollider))
            {
                Collected= true;
                Music.coinSound.Play();
                isCollected = true;

            }
        } 
    return isCollected;
     
    }
    public void Draw(SpriteBatch spriteBatch)
    {
       if (!Collected)
        sprite.Draw(spriteBatch,location);
        

    }
}