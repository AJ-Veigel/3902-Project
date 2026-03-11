using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SpriteZero.Marios;

public class SmallMario : IMario
{

    private TextureRegion standingLeftSprite;
    private TextureRegion standingRightSprite;
    private TextureRegion jumpingLeftSprite;
    private TextureRegion jumpingRightSprite;
    private TextureRegion deathSprite;
    private AnimatedSprite moveRightSprite;
    private AnimatedSprite moveLeftSprite;
    private AnimatedSprite swimRightSprite;
    private AnimatedSprite swimLeftSprite;
    private AnimatedSprite leftFlagpoleSprite;
    private AnimatedSprite rightFlagpoleSprite;
    private TextureRegion currentSprite;
    private AnimatedSprite currentASprite;
    private MarioSprite marioSprites;
    public Vector2 position { get; set; }
    public Rectangle MarioCollider {get; set;}
    public float yVelocity {get; set;}
    public float xVelocity {get; set;}
    public float jumpStartHeight { get; set; }
    public Boolean Jumping { get; set; }
    public Boolean Falling { get; set; }
    public Boolean Direction { get; set; }
    public Boolean Sprint { get; set; }
    public Boolean Crouch { get; set; }
    public Boolean Swim { get; set; }
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
                marioSprites.SetAnimatedSprite("");
            }
            else if (!Direction)
            {
                position = new Vector2(position.X + xVelocity, position.Y);
                marioSprites.SetLocation(position);
                marioSprites.SetSprite("jumpLeft");
                marioSprites.SetAnimatedSprite("");
            }
        }
        else if (!Jumping)
        {
            if (Direction)
            {
                marioSprites.SetAnimatedSprite("moveRight");
                position = new Vector2(position.X + xVelocity, position.Y);
                marioSprites.SetLocation(position);
                marioSprites.SetSprite("");
            }
            else if (!Direction)
            {
                marioSprites.SetAnimatedSprite("moveLeft");
                position = new Vector2(position.X + xVelocity, position.Y);
                marioSprites.SetLocation(position);
                marioSprites.SetSprite("");
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
                marioSprites.SetAnimatedSprite("");
            }
            else if (!Direction)
            {
                marioSprites.SetSprite("standLeft");
                marioSprites.SetAnimatedSprite("");
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
    public void Damage()
    {
        marioSprites.SetSprite("death");
        marioSprites.SetAnimatedSprite("");
    }
    public void Fireball()
    {

    }
    public SmallMario(TextureAtlas smallMarioTexture)
    {

        // default jump position
        jumpStartHeight = 664;

        // default position
        position = new Vector2(300, 664);

        marioSprites = new MarioSprite(smallMarioTexture, 0, position);

        // Set Mario Collider
        MarioCollider = new Rectangle((int)position.X, (int)position.Y, marioSprites.GetSprite().Width * (int)SCALE, marioSprites.GetSprite().Height * (int)SCALE);
    }
    public SmallMario(TextureAtlas smallMarioTexture, Vector2 pos)
    {

        // jump height based on position
        jumpStartHeight = pos.Y;

        // set position to the passed Vector2
        position = pos;

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
                        currentSprite = standingRightSprite;
                    }
                    else if (!Direction)
                    {
                        currentSprite = standingLeftSprite;
                    }
                }
            }
        }
        if(currentSprite != null)
        {
            MarioCollider = new Rectangle((int)position.X, (int)position.Y, marioSprites.GetSprite().Width * (int)SCALE, marioSprites.GetSprite().Height * (int)SCALE);
        }
        else if(currentASprite != null)
        {
            currentASprite.Update(gameTime);
            MarioCollider = new Rectangle((int)position.X, (int)position.Y, (int)marioSprites.GetAnimatedSprite().Width, (int)marioSprites.GetAnimatedSprite().Height); 
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        marioSprites.Draw(spriteBatch);
    }
}