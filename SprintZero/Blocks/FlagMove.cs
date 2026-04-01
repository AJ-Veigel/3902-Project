using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using Microsoft.Xna.Framework.Content;
using SprintZero.blocks;
using SprintZero.Marios;
using System;
using Microsoft.Xna.Framework.Audio;

public class FlagMove : IBlock
{
    private AnimatedSprite flagSprite;
    private const float SCALE = 4f;
    private const float marioSlideSpeed = 2f;

    private bool marioSliding = false;
    private IMario slidingMario;
    private SoundEffect flagSound;
    public Vector2 location { get; set; }
    public Rectangle Collider { get; set; }
    public bool alreadyActived = false;
    private float bottomY;

    public bool SlidingFlag { get; set; } = false;
    public FlagMove(AnimatedSprite sprite, ContentManager content)
    {
        flagSprite = sprite;
        flagSprite.Scale = new Vector2(SCALE);
        flagSprite.Pause();
        flagSound = content.Load<SoundEffect>("Music/flagpole");
        location = new Vector2(600, 70); 
        bottomY = 300f;

        UpdateCollider();
    }

   public void Update(GameTime gameTime)
{
    if (marioSliding && slidingMario != null)
    {
        // Slide Mario down
        Vector2 newMarioPos = slidingMario.location;
        newMarioPos.Y = Math.Min(newMarioPos.Y + marioSlideSpeed, bottomY - slidingMario.MarioCollider.Height);
        slidingMario.location = newMarioPos;

        // Update collider and animation
        slidingMario.MarioCollider = new Rectangle(
            (int)slidingMario.location.X,
            (int)slidingMario.location.Y,
            slidingMario.MarioCollider.Width,
            slidingMario.MarioCollider.Height
        );
        slidingMario.SlidingFlag = true; 

        flagSprite.Update(gameTime);

        if (slidingMario.location.Y >= bottomY - slidingMario.MarioCollider.Height)
        {
            slidingMario.SlidingFlag = false; 
            slidingMario.EndFlagPole();       
            marioSliding = false;
            slidingMario = null;
        }
    }

    UpdateCollider();
}

    public void Draw(SpriteBatch spriteBatch)
    {
        flagSprite.Draw(spriteBatch, location);
    }

    private void UpdateCollider()
    {
        Collider = new Rectangle(
            (int)location.X,
            (int)location.Y,
            (int)flagSprite.Width,
            (int)flagSprite.Height);
    }
public void onCollision(IMario mario, CollisionSide theSide)
{
    if (!marioSliding && mario.MarioCollider.Intersects(Collider))
    {
        marioSliding = true;
        slidingMario = mario;

        flagSprite.Play();
        flagSound.Play();
        slidingMario.GrabFlagPole();

        slidingMario.location = new Vector2(
            location.X - slidingMario.MarioCollider.Width,
            slidingMario.location.Y
        );

     
        slidingMario.SlidingFlag = true;

        
        slidingMario.isOnGround = false;
    }
}
}
