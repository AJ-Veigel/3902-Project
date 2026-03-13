using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SprintZero;
using SpriteZero.Marios;

public class SmallMario : IMario
{
    private MarioSprite marioSprites;
    public Vector2 location { get; set; }
    public Hitbox Collider {get; set; }
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
    private float DefaultMoveSpeed = 4f;
    private const float SCALE = 4f;
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
            yVelocity = 4f;
            jumpStartHeight = location.Y;

            if (Direction)
            {
                marioSprites.SetSprite("jumpRight");
            }
            else if (!Direction)
            {
                marioSprites.SetSprite("jumpLeft");
            }
            isOnGround = false;
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
        location = new Vector2(300, 650);

        // jump height based on location
        jumpStartHeight = location.Y;

        marioSprites = new MarioSprite(smallMarioTexture, 0, location);

        // Set Mario Collider
        MarioCollider = new Rectangle(
            (int)location.X,
            (int)location.Y,
            marioSprites.GetSprite().Width * (int)SCALE,
            marioSprites.GetSprite().Height * (int)SCALE
        );

        Collider = marioSprites.UpdateCollider();

        isOnGround = true;
    }
    public void LandOnBlock(float blockTopY)
    {
        location = new Vector2(location.X, blockTopY - marioSprites.GetSprite().Height * SCALE);
        isOnGround = true;
        Jumping = false;
        Falling = false;
        jumpStartHeight = location.Y;

        if (Direction)
        {
            marioSprites.SetSprite("standRight");
        }   
        else
        {
            marioSprites.SetSprite("standLeft");
        }

        //MarioCollider = marioSprites.UpdateCollider();
        Collider = marioSprites.UpdateCollider();
 
    }
    public SmallMario(TextureAtlas smallMarioTexture, Vector2 pos)
    {
        // set location to the passed Vector2
        location = pos;

        // jump height based on location
        jumpStartHeight = pos.Y;

        marioSprites = new MarioSprite(smallMarioTexture, 0, location);

        // Set Mario Collider
        MarioCollider = new Rectangle((int)location.X, (int)location.Y, marioSprites.GetSprite().Width * (int)SCALE, marioSprites.GetSprite().Height * (int)SCALE);
        Collider = marioSprites.UpdateCollider();
        isOnGround = true;
    }

    public Boolean GetCollidable()
    {
        return true;
    }
    public void Update(GameTime gameTime)
    {
        if (Jumping)
        {
            if (!Falling)
            {
                location = new Vector2(location.X, location.Y - SCALE); // move up
                marioSprites.SetLocation(location);

                if (location.Y <= jumpStartHeight - 100) // fixed jump height
                    Falling = true;
            }
            else
            {
                location = new Vector2(location.X, location.Y + SCALE); // move down
                marioSprites.SetLocation(location);

                if (location.Y >= jumpStartHeight)
                {
                    location = new Vector2(location.X, jumpStartHeight);
                    Jumping = false;
                    Falling = false;
                    isOnGround = true;

                    if (Direction)
                        marioSprites.SetSprite("standRight");
                    else
                        marioSprites.SetSprite("standLeft");
                }
            }
        }
        if(!isOnGround)
        {

        }

        // Update sprite animations / collider
        if (marioSprites != null)
            marioSprites.Update(gameTime);

        //MarioCollider = marioSprites != null ? marioSprites.UpdateCollider() : MarioCollider;
        Collider = marioSprites.UpdateCollider();
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        marioSprites.Draw(spriteBatch);
    }
}