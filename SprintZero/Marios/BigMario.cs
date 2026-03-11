using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SpriteZero.Marios;

public class BigMario : IMario
{   
    public Vector2 position {get;set;}
    private MarioSprite marioSprites;
    public Rectangle MarioCollider {get; set;}
    public float yVelocity {get; set;}
    public float xVelocity {get; set;}
    public float jumpStartHeight {get; set;}
    public Boolean Jumping {get; set;}
    public Boolean Falling {get; set;}
    // If direction is True, mario is facing right, if direction is false, mario is facing left
    public Boolean Direction {get; set;}
    public Boolean Sprinting {get;set;}
    public Boolean Crouching {get;set;}
    public Boolean Swimming {get;set;}
    private float DefaultMoveSpeed = 4f;
    private const float SCALE = 4f;
    public void Move()
    {
        if(!Crouching)
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
        if(Crouching)
        {
            if(Direction)
            {
                position = new Vector2(position.X, position.Y + 10f * (SCALE));
                marioSprites.SetLocation(position);
                marioSprites.SetSprite("crouchRight");
            }
            else if(!Direction)
            {
                position = new Vector2(position.X, position.Y + 10f * (SCALE));
                marioSprites.SetLocation(position);
                marioSprites.SetSprite("crouchLeft");
            }
        }
        else if(!Crouching)
        {
            if(Direction)
            {
                position = new Vector2(position.X, position.Y - 10f * (SCALE));
                marioSprites.SetLocation(position);
                marioSprites.SetSprite("standRight");
            }
            else if(!Direction)
            {
                position = new Vector2(position.X, position.Y - 10f * (SCALE));
                marioSprites.SetLocation(position);
                marioSprites.SetSprite("standLeft");
            }
        }
    }
    public void Damage()
    {
        
    }
    public void Fireball()
    {
        
    }
    public BigMario(TextureAtlas bigMarioTexture)
    {
        // default position
        position = new Vector2(300, 664);

        // jump height based on position
        jumpStartHeight = position.Y;

        marioSprites = new MarioSprite(bigMarioTexture, 1, position);

        // Set Mario Collider
        MarioCollider = new Rectangle((int)position.X, (int)position.Y, marioSprites.GetSprite().Width * (int)SCALE, marioSprites.GetSprite().Height * (int)SCALE);
    }
    public BigMario(TextureAtlas bigMarioTexture, Vector2 pos)
    {
        // default position
        position = pos;

        // jump height based on position
        jumpStartHeight = position.Y;

        marioSprites = new MarioSprite(bigMarioTexture, 1, position);

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