using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SpriteZero.blocks;
using SpriteZero.Marios;

public class questionMarkHit : IBlock
{
    private AnimatedSprite sprite;
    public Vector2 location{get;set;}
    public Rectangle Collider {get; set;}
    private const float SCALE = 4f;
    private float startY;
    private float bounceHeight=20f;
    private float riseSpeed=3f;
    private bool rise = true;

    

    public questionMarkHit(AnimatedSprite animated)
    {      
        sprite = animated;
        sprite.Scale = new Vector2(4f);
        location = new Vector2(530,325);
        startY = location.Y;
        Collider = new Rectangle((int)location.X, (int)location.Y, (int)sprite.Width, (int)sprite.Height);
    }

    public void Update(GameTime gameTime)
    {
        sprite.Update(gameTime);
        if (rise)
        {
           location = new Vector2(location.X,location.Y-riseSpeed);
           if (location.Y <= startY - bounceHeight)
            {
                location = new Vector2(location.X,startY-bounceHeight);
                rise = false;
            } 
        }
        else
        {
            location = new Vector2(location.X,location.Y+riseSpeed);
            if (location.Y >= startY)
            {
                location = new Vector2(location.X,startY);
                rise=false;
                sprite.PauseFrame(3);
                
            }
        }
    }
    public void Draw(SpriteBatch spriteBatch)
    {
    
        sprite.Draw(spriteBatch,location);
    }
     public  void onHit(IMario mario, CollisionSide theSide){}
}