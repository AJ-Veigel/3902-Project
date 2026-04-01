using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SprintZero.Marios;
using SprintZero.blocks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using SoundManager;


public class AboveGroundBreak : IBlock
{
  
    private AnimatedSprite sprite;
    private Music theSound;
    public Vector2 location { get; set; }
    public Rectangle Collider { get; set; }
    private const float SCALE = 4f;
    private float startY; 
    private Vector2 velocity;
    private float gravity = 0.5f;
    private bool isBroken = false;
    private bool playBreakingAnimation = false;
    private bool movingUp = false;
    private bool movingDown = false;
    private float bounceHeight =20f; 
    private float bounceSpeed = 2f;

    public AboveGroundBreak(AnimatedSprite animated)
    {
        sprite = animated;
        sprite.Scale = new Vector2(SCALE);
        sprite.Pause();
        location = new Vector2(600, 600);
        startY = location.Y;
        velocity = Vector2.Zero;
        Collider = new Rectangle((int)location.X, (int)location.Y, (int)sprite.Width, (int)sprite.Height);
        
    }


    public void Update(GameTime gameTime)
    {
        if (playBreakingAnimation)
        {
            sprite.Play();
            sprite.Update(gameTime);
            velocity.Y += gravity;
            location += velocity;
        }
         else if (movingUp)
        {
            location = new Vector2(location.X, location.Y - bounceSpeed);
            if (location.Y <= startY- bounceHeight)
            {

                movingUp = false;
                movingDown = true;
                
            }
        
        }
        
         if (movingDown)
        {
            location = new Vector2(location.X, location.Y + bounceSpeed);
            if (location.Y >= startY)
            {
                location = new Vector2(location.X, startY);
                movingDown = false;
                sprite.PauseFrame(0);
             
            }
          

        }
        sprite.Update(gameTime);
    if (!isBroken)
        {
            Collider = new Rectangle((int) location.X, (int)location.Y, (int) sprite.Width,(int)sprite.Height);
        }
        else
        {
            Collider = Rectangle.Empty;
        }
      
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (!isBroken)
            sprite.Draw(spriteBatch, location);
    }
    
    public void onCollision(IMario mario, CollisionSide side)
    {
        bool shouldBreak = false;
    if (!isBroken){
        if(side == CollisionSide.Bottom && (mario is BigMario || mario is FireMario)){
            shouldBreak = true;

        } else{
                if (side == CollisionSide.Left)
                {
                    mario.location = new Vector2(Collider.Left - mario.MarioCollider.Width, mario.location.Y);
                }
                if (side == CollisionSide.Right)
                {
                    mario.location = new Vector2(Collider.Right,mario.location.Y);
                }
                else if (side == CollisionSide.Top)
                {
                    mario.LandOnBlock(location.Y);
                }
                else if (side == CollisionSide.Bottom)
                {
                    movingUp = true; 
                    Music.blockSound.Play();
                    movingDown = false;
                  
                }
            }
            if (shouldBreak)
            {
                isBroken = true;
                playBreakingAnimation = true;
                Music.blockSound.Play();
                mario.Jumping = true; 
                mario.Falling = true;
            }
    }
}
}
