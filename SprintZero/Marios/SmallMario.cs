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
    public Vector2 position { get; set; }
     public Vector2 velocity {get;set;}
    public Rectangle MarioCollider {get; set;}
    public float yVelocity {get; set;}
    public float xVelocity {get; set;}
    public float jumpStartHeight { get; set; }
    public Boolean Jumping { get; set; }
    public Boolean Falling { get; set; }
    public Boolean isOnGround {get;set;}
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
                currentSprite = jumpingRightSprite;
                currentASprite = null;
            }
            else if (!Direction)
            {
                position = new Vector2(position.X + xVelocity, position.Y);
                currentSprite = jumpingLeftSprite;
                currentASprite = null;
            }
        }
        else if (!Jumping)
        {
            if (Direction)
            {
                currentASprite = moveRightSprite;
                currentASprite.Scale = new Vector2(SCALE);
                position = new Vector2(position.X + xVelocity, position.Y);
                currentSprite = null;
            }
            else if (!Direction)
            {
                currentASprite = moveLeftSprite;
                currentASprite.Scale = new Vector2(SCALE);
                position = new Vector2(position.X + xVelocity, position.Y);
                currentSprite = null;
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
                currentSprite = standingRightSprite;
                currentASprite = null;
            }
            else if (!Direction)
            {
                currentSprite = standingLeftSprite;
                currentASprite = null;
            }
        }
    }
    public void Jump()
    {
        Jumping = true;
        yVelocity = 4f;
        if (Direction)
        {
            currentSprite = jumpingRightSprite;
        }
        else if (!Direction)
        {
            currentSprite = jumpingLeftSprite;
        }
    }
    public void Damage()
    {
        currentSprite = deathSprite;
        currentASprite = null;
    }
    public void Fireball()
    {

    }

    public void LoadMarioSprites(TextureAtlas smallMarioTexture)
    {
        standingLeftSprite = smallMarioTexture.GetRegion("standingLeftSmallMario");
        standingRightSprite = smallMarioTexture.GetRegion("standingRightSmallMario");
        jumpingLeftSprite = smallMarioTexture.GetRegion("jumpingLeftSmallMario");
        jumpingRightSprite = smallMarioTexture.GetRegion("jumpingRightSmallMario");
        deathSprite = smallMarioTexture.GetRegion("deadMario");
        moveRightSprite = smallMarioTexture.CreateAnimatedSprite("smallRightMove");
        moveLeftSprite = smallMarioTexture.CreateAnimatedSprite("smallLeftMove");
        swimRightSprite = smallMarioTexture.CreateAnimatedSprite("smallRightSwim");
        swimLeftSprite = smallMarioTexture.CreateAnimatedSprite("smallLeftSwim");
        leftFlagpoleSprite = smallMarioTexture.CreateAnimatedSprite("smallLeftFlag");
        rightFlagpoleSprite = smallMarioTexture.CreateAnimatedSprite("smallRightFlag");
    }

    public SmallMario(TextureAtlas smallMarioTexture)
    {
        // Load all of the sprites
        LoadMarioSprites(smallMarioTexture);

        // Default Sprite
        currentSprite = standingLeftSprite;

        // default jump position
        jumpStartHeight = 664;

        // default position
        position = new Vector2(300, 664);

        // Set Mario Collider
        MarioCollider = new Rectangle((int)position.X, (int)position.Y, currentSprite.Width * (int)SCALE, currentSprite.Height * (int)SCALE);
    }
    public SmallMario(TextureAtlas smallMarioTexture, Vector2 pos)
    {
        // Load all of the sprites
        LoadMarioSprites(smallMarioTexture);

        // Default Sprite
        currentSprite = standingLeftSprite;

        // jump height based on position
        jumpStartHeight = pos.Y;

        // set position to the passed Vector2
        position = pos;

        // Set Mario Collider
        MarioCollider = new Rectangle((int)position.X, (int)position.Y, currentSprite.Width * (int)SCALE, currentSprite.Height * (int)SCALE);
    }
    public void Update(GameTime gameTime)
    {
        if (Jumping)
        {
            if (!Falling)
            {
                position = new Vector2(position.X, position.Y - 4f);
                if (position.Y <= jumpStartHeight - 100)
                {
                    Falling = true;
                }
            }
            else if (Falling)
            {
                position = new Vector2(position.X, position.Y + 4f);
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
            MarioCollider = new Rectangle((int)position.X, (int)position.Y, currentSprite.Width * (int)SCALE, currentSprite.Height * (int)SCALE);
        }
        else if(currentASprite != null)
        {
            currentASprite.Update(gameTime);
            MarioCollider = new Rectangle((int)position.X, (int)position.Y, (int)currentASprite.Width, (int)currentASprite.Height); 
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (currentSprite != null)
        {
            currentSprite.Draw(spriteBatch, position, Color.White, 0f, Vector2.One, 4f, SpriteEffects.None, 0f);
        }
        else if (currentASprite != null)
        {
            currentASprite.Draw(spriteBatch, position);
        }

    }
}