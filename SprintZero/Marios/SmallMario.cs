using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SpriteZero.Marios;

public class SmallMario : IMario
{
    private MarioSprite marioSprites;
    public Vector2 position { get; set; }
    public Rectangle MarioCollider {get; set;}
    public float yVelocity {get; set;}
    public float xVelocity {get; set;}
    public float jumpStartHeight { get; set; }
    public Boolean Jumping { get; set; }
    public Boolean Falling { get; set; }
    public Boolean isOnGround {get;set;}
    public Boolean Direction { get; set; }
    public Boolean Sprinting { get; set; }
    public Boolean Crouching { get; set; }
    public Boolean Swimming { get; set; }
    private float DefaultMoveSpeed = 4f;
    private const float SCALE = 4f;
    public void Move()
    {
        if(Direction)
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
                position = new Vector2(position.X + xVelocity, position.Y);
                marioSprites.SetLocation(position);
                marioSprites.SetSprite("jumpRight");
            }
            else if (!Direction)
            {
                position = new Vector2(position.X + xVelocity, position.Y);
                marioSprites.SetLocation(position);
                marioSprites.SetSprite("jumpLeft");
            }
        }
        else if (!Jumping)
        {
            if (Direction)
            {
                marioSprites.SetAnimatedSprite("moveRight");
                position = new Vector2(position.X + xVelocity, position.Y);
                marioSprites.SetLocation(position);
            }
            else if (!Direction)
            {
                marioSprites.SetAnimatedSprite("moveLeft");
                position = new Vector2(position.X + xVelocity, position.Y);
                marioSprites.SetLocation(position);
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
        Jumping = true;
        yVelocity = 4f;
        if (Direction)
        {
            marioSprites.SetSprite("jumpRight");
        }
        else if (!Direction)
        {
            marioSprites.SetSprite("jumpLeft");
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
    public SmallMario(TextureAtlas smallMarioTexture)
    {
        // default position
        position = new Vector2(300, 664);

        // jump height based on position
        jumpStartHeight = position.Y;

        marioSprites = new MarioSprite(smallMarioTexture, 0, position);

        // Set Mario Collider
        MarioCollider = new Rectangle((int)position.X, (int)position.Y, marioSprites.GetSprite().Width * (int)SCALE, marioSprites.GetSprite().Height * (int)SCALE);
    }
    public SmallMario(TextureAtlas smallMarioTexture, Vector2 pos)
    {
        // set position to the passed Vector2
        position = pos;

        // jump height based on position
        jumpStartHeight = pos.Y;

        marioSprites = new MarioSprite(smallMarioTexture, 0, position);

        // Set Mario Collider
        MarioCollider = new Rectangle((int)position.X, (int)position.Y, marioSprites.GetSprite().Width * (int)SCALE, marioSprites.GetSprite().Height * (int)SCALE);
    }
    public void Update(GameTime gameTime)
    {
        if (Jumping)
        {
            if (!Falling)
            {
                position = new Vector2(position.X, position.Y - 4f);
                marioSprites.SetLocation(position);
                if (position.Y <= jumpStartHeight - 100)
                {
                    Falling = true;
                }
            }
            else if (Falling)
            {
                position = new Vector2(position.X, position.Y + 4f);
                marioSprites.SetLocation(position);
                if (position.Y >= jumpStartHeight - 16)
                {
                    Falling = false;
                    Jumping = false;
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
        }
        marioSprites.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        marioSprites.Draw(spriteBatch);
    }
}