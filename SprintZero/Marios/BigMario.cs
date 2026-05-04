using System;
using MarioMovement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SoundManager;
using SprintZero;
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
    public float currentPlatformY { get; set; }
    private Game1 game;
    public bool Jumping { get; set; }
    public bool isOnGround { get; set; }
    public bool Falling { get; set; }
    // If direction is True, mario is facing right, if direction is false, mario is facing left
    public bool Direction { get; set; }
    public bool Sprinting { get; set; }
    public bool Crouching { get; set; }
    public bool Swimming { get; set; }
    public bool throwing { get; set; } = false;
    public bool Moving { get; set; }
    public bool inPipe { get; set; }
    public bool Invincible { get; set; } = true;
    private float invincibilityTimer = 0f;
    private const float SCALE = 4f;
    private const float GRAVITY = 0.2f;
    private const float JUMP_POWER = -11f;
    public bool SlidingFlag { get; set; } = false;
    public bool AutoWalking { get; set; } = false;
    public bool WinState { get; set; } = false;
    public bool IsStarPower { get; set; } = false;
    private bool invincibleTint = false;

    public BigMario(TextureAtlas bigMarioTexture, ContentManager content, Game1 game)
    {
        Moving = false;
        this.game = game;
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
        MarioMovementManager.Move(this, marioSprites);
    }
    public void StopMove()
    {
        MarioMovementManager.StopMove(this, marioSprites);
    }
    public void LandOnBlock(float blockTopY)
    {
        MarioMovementManager.LandOnBlock(this, marioSprites, blockTopY);
    }

    public void Jump()
    {
        MarioMovementManager.Jump(this, marioSprites);
    }

    public void Bounce()
    {
        MarioMovementManager.Bounce(this, marioSprites);
    }
    public void Crouch()
    {
        if (!Falling && !Jumping && !Swimming)
        {
            if (Crouching)
            {
                location = new Vector2(location.X, location.Y + 10f * SCALE);
                marioSprites.SetLocation(location);
                marioSprites.SetSprite(Direction ? "crouchRight" : "crouchLeft");
            }
            else if (!Crouching)
            {
                location = new Vector2(location.X, location.Y - 10f * SCALE);
                marioSprites.SetLocation(location);
                marioSprites.SetSprite(Direction ? "crouchRight" : "crouchLeft");
            }
        }
    }
    public void Damage()
    {
        game.Damage();
    }
    public void Fireball()
    {

    }
    public void GrabFlagPole()
    {
        MarioMovementManager.GrabFlagPole(this, marioSprites);
    }
    public void EndFlagPole()
    {
        MarioMovementManager.EndFlagPole(this, marioSprites);
    }
    public void BecomeInvincible()
    {
        Invincible = true;
        IsStarPower = true;
        invincibilityTimer = -10f;
    }
    public void SetLocation(Vector2 pos)
    {
        location = pos;
        marioSprites.SetLocation(pos);
        Console.WriteLine(marioSprites.location);
    }

    public Vector2 GetLocation()
    {
        return location;
    }
    public void Update(GameTime gameTime)
    {
        invincibilityTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (invincibilityTimer > 1)
        {
            Invincible = false;
            if (IsStarPower)
            {
                IsStarPower = false;
                Music.PlayBackground();
            }
        }
        if(IsStarPower)
        {
            if(-(int)(invincibilityTimer * 6) % 2 == 0)
            {
                invincibleTint = true;
            }
            else
            {
                invincibleTint = false;
            }
        }
        else
        {
            invincibleTint = false;
        }

        if (SlidingFlag)
        {
            float slideSpeed = 2.5f;

            Vector2 nextPosition = new Vector2(location.X, location.Y + slideSpeed);
            if (nextPosition.Y >= currentPlatformY)
            {
                nextPosition.Y = currentPlatformY;
                location = nextPosition;
                marioSprites.SetLocation(location);

                EndFlagPole();
            }
            else
            {
                location = nextPosition;
                marioSprites.SetLocation(location);
            }

            MarioCollider = marioSprites.UpdateCollider();
            return;
        }
        if (AutoWalking)
        {
            float castleX = 400f;

            xVelocity = 2f;
            yVelocity = 0;

            location = new Vector2(location.X + xVelocity, location.Y);

            marioSprites.SetAnimatedSprite("moveRight");
            marioSprites.SetLocation(location);

            if (location.X >= castleX)
            {
                AutoWalking = false;
                xVelocity = 0;
                marioSprites.SetSprite("standRight");
            }

            MarioCollider = marioSprites.UpdateCollider();
            return;
        }

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

        if ((Jumping || Falling) && !isOnGround)
        {
            if (Direction)
                marioSprites.SetSprite("jumpRight");
            else
                marioSprites.SetSprite("jumpLeft");
        }

        marioSprites.Update(gameTime);

        MarioCollider = marioSprites.UpdateCollider();
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        marioSprites.Draw(spriteBatch, invincibleTint);
    }
}