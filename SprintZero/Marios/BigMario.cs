using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SoundManager;
using SprintZero.Marios;

public class BigMario : IMario
{
    public Vector2 location { get; set; }
    private MarioSprite marioSprites;
    public Rectangle MarioCollider { get; set; }
    public float yVelocity { get; set; }
    public float xVelocity { get; set; }
    public float jumpStartHeight { get; set; }
    private float groundY;
    private float currentPlatformY;
    public Boolean Jumping { get; set; }
    public Boolean isOnGround { get; set; }
    public Boolean Falling { get; set; }
    // If direction is True, mario is facing right, if direction is false, mario is facing left
    public Boolean Direction { get; set; }
    public Boolean Sprinting { get; set; }
    public Boolean Crouching { get; set; }
    public Boolean Swimming { get; set; }
    public Boolean Moving { get; set; }
    private float DefaultMoveSpeed = 4f;
    private const float SCALE = 4f;
    private const float GRAVITY = 0.2f;
    private const float JUMP_POWER = -8f;
 public bool SlidingFlag { get; set; } = false;
    public BigMario(TextureAtlas bigMarioTexture,ContentManager content)
    {
        Moving = false;
        
        location = new Vector2(300, 600);
        Direction = true;

        groundY = location.Y;
        currentPlatformY = groundY;

        yVelocity = 0f;
        xVelocity = 0f;

        marioSprites = new MarioSprite(bigMarioTexture, 1, location);

       
        MarioCollider = marioSprites.UpdateCollider();

        isOnGround = false;
    }
    public BigMario(TextureAtlas bigMarioTexture, Vector2 pos)
    {
        Moving = false;
       
        location = pos;
        Direction = true;

        groundY = location.Y;
        currentPlatformY = groundY;

        yVelocity = 0f;
        xVelocity = 0f;

        marioSprites = new MarioSprite(bigMarioTexture, 1, location);
        MarioCollider = marioSprites.UpdateCollider();
        isOnGround = false;


    }
    public void Move()
    {
        if (!Crouching)
        {
            Moving = true;
            Moving = true;
            // If you’re throwing, you might want to ignore Move animation for a split second.
            // Up to you—this keeps movement but doesn’t override the throw pose.
            location = new Vector2(
                location.X + (Direction ? DefaultMoveSpeed : -DefaultMoveSpeed),
                location.Y
            );
            marioSprites.SetLocation(location);

            // Only set run animation if we’re not in a higher-priority pose
            if (!Jumping && !Swimming && !Falling)
            {
                marioSprites.SetAnimatedSprite(Direction ? "moveRight" : "moveLeft");
            }
        }
    }
    public void StopMove()
    {
        Moving = false;
        xVelocity = 0;
        if (!Jumping && !Crouching && !Falling)
        {
            marioSprites.SetSprite(Direction ? "standRight" : "standLeft");
        }
    }
    public void LandOnBlock(float blockTopY)
    {
        location = new Vector2(location.X, blockTopY - MarioCollider.Height);
        isOnGround = true;
        Jumping = false;
        Falling = false;
        jumpStartHeight = location.Y;

        marioSprites.SetSprite(Direction ? "standRight" : "standLeft");

        MarioCollider = marioSprites.UpdateCollider();

    }


    public void Jump()
    {
        if (isOnGround)
        {
            yVelocity = JUMP_POWER;
            Jumping = true;
            Falling = false;
            jumpStartHeight = location.Y;
            isOnGround = false;

            // Update sprite
            marioSprites.SetSprite(Direction ? "jumpRight" : "jumpLeft");
            
  
        }
        Music.jumpBigSound.Play();
    }
    public void Crouch()
    {
        if (!Falling && !Jumping && !Swimming)
        {
            if (Crouching)
            {
                location = new Vector2(location.X, location.Y + 10f * (SCALE));
                marioSprites.SetLocation(location);
                marioSprites.SetSprite(Direction ? "crouchRight" : "crouchLeft");
            }
            else if (!Crouching)
            {
                location = new Vector2(location.X, location.Y - 10f * (SCALE));
                marioSprites.SetLocation(location);
                marioSprites.SetSprite(Direction ? "crouchRight" : "crouchLeft");
            }
        }
    }
    public void Damage()
    {

    }
    public void Fireball()
    {

    }
    public void GrabFlagPole()
    {
        Jumping = false;
        Falling = true;
        if (Direction)
        {
            marioSprites.SetAnimatedSprite("flagpoleRight");
        }
        else
        {
            marioSprites.SetAnimatedSprite("flagpoleLeft");
        }
    }
    public void EndFlagPole()
    {
        if (Direction)
            marioSprites.SetSprite("standRight");
        else
            marioSprites.SetSprite("standLeft");

        isOnGround = true;
    }

    public void Update(GameTime gameTime)
    {
        if (Jumping && !Falling)
        {
            yVelocity += GRAVITY;
            location = new Vector2(location.X, location.Y + yVelocity);
            marioSprites.SetLocation(location);

            if (yVelocity <= 0)
                Falling = true;
        }

        if (Falling)
        {
            if (yVelocity <= -JUMP_POWER)
                yVelocity += GRAVITY;
            location = new Vector2(location.X, location.Y + yVelocity);
            marioSprites.SetLocation(location);
        

            if (isOnGround)
            {
                yVelocity = 0;
                location = new Vector2(location.X, currentPlatformY);
                Jumping = false;
                Falling = false;

                if (Direction)
                    marioSprites.SetSprite("standRight");
                else
                    marioSprites.SetSprite("standLeft");
            }
        }


        if (isOnGround)
        {
            if (!Moving) StopMove();
            Falling = false;
            yVelocity = 0f;
        }

        MarioCollider = marioSprites.UpdateCollider();


        if ((Jumping || Falling) && !isOnGround)
        {
            if (Direction)
                marioSprites.SetSprite("jumpRight");
            else
                marioSprites.SetSprite("jumpLeft");
        }

        marioSprites.Update(gameTime);

        MarioCollider = marioSprites.UpdateCollider();


        marioSprites.Update(gameTime);
        MarioCollider = marioSprites.UpdateCollider();
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        marioSprites.Draw(spriteBatch);
    }
}