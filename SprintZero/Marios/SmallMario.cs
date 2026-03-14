using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SprintZero.Marios;

public class SmallMario : IMario
{
    private MarioSprite marioSprites;
    public Vector2 location { get; set; }
    public Rectangle MarioCollider { get; set; }
    public float yVelocity { get; set; }
    public float xVelocity { get; set; }
    public float jumpStartHeight { get; set; }
    public Boolean Jumping { get; set; }
    public Boolean Falling { get; set; }
    public Boolean isOnGround { get; set; }
    public Boolean Direction { get; set; }
    public Boolean Sprinting { get; set; }
    public Boolean Crouching { get; set; }
    public Boolean Swimming { get; set; }
    private const float DefaultMoveSpeed = 4f;
    private const float SCALE = 4f;
    private const float GRAVITY = 1f;
    private float groundY;
    private float currentPlatformY;
    private const float JUMP_POWER = -8f;

    public void Move()
    {
        if (Direction)
        {
            xVelocity = DefaultMoveSpeed;
        }
        else
        {
            xVelocity = -DefaultMoveSpeed;
        }
        if (Jumping)
        {
            if (Direction)
            {
                location = new Vector2(location.X + xVelocity, location.Y);
                marioSprites.SetLocation(location);
                marioSprites.SetSprite("jumpRight");
            }
            else if (!Direction)
            {
                location = new Vector2(location.X + xVelocity, location.Y);
                marioSprites.SetLocation(location);
                marioSprites.SetSprite("jumpLeft");
            }
        }
        else if (!Jumping)
        {
            if (Direction)
            {
                marioSprites.SetAnimatedSprite("moveRight");
                location = new Vector2(location.X + xVelocity, location.Y);
                marioSprites.SetLocation(location);
            }
            else if (!Direction)
            {
                marioSprites.SetAnimatedSprite("moveLeft");
                location = new Vector2(location.X + xVelocity, location.Y);
                marioSprites.SetLocation(location);
            }
        }
    }
    public void StopMove()
    {
        xVelocity = 0;
        if (!Jumping)
        {
            if (Direction)
            {
                marioSprites.SetSprite("standRight");
            }
            else if (!Direction)
            {
                marioSprites.SetSprite("standLeft");
            }
        }
    }
    public void Jump()
    {
        if (isOnGround)
        {
            Jumping = true;
            Falling = false;
            jumpStartHeight = location.Y;
            isOnGround = false;

            if (Direction)
            {
                marioSprites.SetSprite("jumpRight");
            }
            else if (!Direction)
            {
                marioSprites.SetSprite("jumpLeft");
            }

        }
    }
    public void Crouch()
    {

    }
    public void Damage()
    {
        marioSprites.SetSprite("death");
    }
    public void Fireball()
    {

    }
    public void GrabFlagPole()
    {
        Jumping = false;
        Falling = false;
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
    public SmallMario(TextureAtlas smallMarioTexture)
    {
        // default location
        location = new Vector2(300, 664);
        groundY = location.Y;
        currentPlatformY = groundY;

        // jump height based on location
        jumpStartHeight = location.Y;

        marioSprites = new MarioSprite(smallMarioTexture, 0, location);

        // Set Mario Collider
        MarioCollider = marioSprites.UpdateCollider();

        isOnGround = true;
    }
    public void LandOnBlock(float blockTopY)
    {
        location = new Vector2(location.X, blockTopY - marioSprites.GetSprite().Height * SCALE);
        currentPlatformY = location.Y;
        Jumping = false;
        Falling = false;
        isOnGround = true;
        jumpStartHeight = location.Y;

        if (Direction)
            marioSprites.SetSprite("standRight");
        else
            marioSprites.SetSprite("standLeft");

        MarioCollider = marioSprites.UpdateCollider();
    }
    public SmallMario(TextureAtlas smallMarioTexture, Vector2 pos)
    {
        // set location to the passed Vector2
        location = pos;

        // jump height based on location
        jumpStartHeight = pos.Y;

        marioSprites = new MarioSprite(smallMarioTexture, 0, location);

        // Set Mario Collider
        MarioCollider = marioSprites.UpdateCollider();

        isOnGround = true;
    }
    public void Update(GameTime gameTime)
    {
        if (Jumping && !Falling)
        {

            location = new Vector2(location.X, location.Y - SCALE);
            marioSprites.SetLocation(location);


            if (location.Y <= jumpStartHeight - 100)
                Falling = true;
        }

        if (Falling)
        {
            location = new Vector2(location.X, location.Y + SCALE);
            marioSprites.SetLocation(location);


            if (location.Y >= groundY)
            {
                location = new Vector2(location.X, currentPlatformY);
                Jumping = false;
                Falling = false;
                isOnGround = true;

                if (Direction)
                    marioSprites.SetSprite("standRight");
                else
                    marioSprites.SetSprite("standLeft");
            }
        }


        if (marioSprites != null)
            MarioCollider = marioSprites.UpdateCollider();


        if ((Jumping || Falling) && marioSprites != null)
        {
            if (Direction)
                marioSprites.SetSprite("jumpRight");
            else
                marioSprites.SetSprite("jumpLeft");
        }

        if (marioSprites != null)
            marioSprites.Update(gameTime);

        MarioCollider = marioSprites.UpdateCollider();
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        marioSprites.Draw(spriteBatch);
    }
}