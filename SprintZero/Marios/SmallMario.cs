using System;
using System.Resources;
using System.Threading;
using MarioMovement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGameLibrary.Graphics;
using SoundManager;
using SprintZero;
using SprintZero.Marios;
using SprintZero.blocks;


public class SmallMario : IMario
{
    private MarioSprite marioSprites;

    public Vector2 location { get; set; }
    private Game1 game;
    public Rectangle MarioCollider { get; set; }
    public float yVelocity { get; set; }
    public float xVelocity { get; set; }
    public float jumpStartHeight { get; set; }
    public bool Jumping { get; set; }
    public bool Falling { get; set; }
    public bool isOnGround { get; set; }
    public bool Direction { get; set; }
    public bool Sprinting { get; set; }
    public bool Crouching { get; set; }
    public bool Swimming { get; set; }
    public bool Moving { get; set; }
    public bool throwing { get; set; } = false;
    public bool SlidingFlag { get; set; }
    public bool inPipe { get; set; }
    public bool Invincible { get; set; } = true;
    private float invincibilityTimer = 0f;
    private const float DefaultMoveSpeed = 4f;
    private const float SCALE = 4f;
    private const float GRAVITY = 0.2f;
    private float groundY;
    private float pipeHeight = 64;
    public float currentPlatformY { get; set; }
    private const float JUMP_POWER = -11f;
    public bool AutoWalking { get; set; } = false;
    public bool WinState { get; set; } = false;
    public bool IsStarPower { get; set; } = false;
    public IPipe pipeStorage { get; set; }
    private bool invincibleTint = false;
    public SmallMario(TextureAtlas smallMarioTexture, Vector2 pos, Game1 game)
    {
        Moving = false;

        location = pos;

        groundY = location.Y;
        currentPlatformY = groundY;

        jumpStartHeight = pos.Y;

        yVelocity = 0f;
        xVelocity = 0f;

        marioSprites = new MarioSprite(smallMarioTexture, 0, location);

        MarioCollider = marioSprites.UpdateCollider();

        isOnGround = false;
        this.game = game;
    }

    public void Move()
    {
        MarioMovementManager.Move(this, marioSprites);
    }
    public void StopMove()
    {
        MarioMovementManager.StopMove(this, marioSprites);
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

    }
    public void Damage()
    {
        marioSprites.SetSprite("death");
        Music.deathSound.Play();
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
    public void LandOnBlock(float blockTopY)
    {
        MarioMovementManager.LandOnBlock(this, marioSprites, blockTopY);
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
        if (IsStarPower)
        {
            if (-(int)(invincibilityTimer * 6) % 2 == 0)
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


        if (AutoWalking)
        {
            float castleX = 100f;

            xVelocity = 2f;
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

        if (inPipe)
        {
            if(pipeHeight > 0)
            {
                pipeHeight -= 4;
                if(pipeStorage is TubeTop)
                {
                    location = new Vector2(location.X, location.Y + 4);
                }
                else
                {
                    location = new Vector2(location.X + 4, location.Y);
                }
                marioSprites.SetLocation(location);
                MarioCollider = marioSprites.UpdateCollider();
                return;
            }
            else
            {
                SetLocation(pipeStorage.marioSpawnPos);
                game.toggleMap(pipeStorage.levelNum + pipeStorage.bonus - 1);
                pipeStorage = null;
                pipeHeight = 64;
                inPipe = false;
            }
        }

        if (Jumping)
        {
            yVelocity += GRAVITY;
            location = new Vector2(location.X, location.Y + yVelocity);

            if (yVelocity > 0)
            {
                Falling = true;
                Jumping = false;
            }
        }

        if (Falling)
        {
            if (yVelocity <= -JUMP_POWER)
                yVelocity += GRAVITY;
            location = new Vector2(location.X, location.Y + yVelocity);

            if (isOnGround)
            {
                yVelocity = 0;
                location = new Vector2(location.X, currentPlatformY);
                Jumping = false;
                Falling = false;

                marioSprites.SetSprite(Direction ? "standRight" : "standLeft");
            }
        }

        if ((Jumping || Falling) && !isOnGround)
        {
            marioSprites.SetSprite(Direction ? "jumpRight" : "jumpLeft");
        }

        marioSprites.SetLocation(location);

        marioSprites.Update(gameTime);

        MarioCollider = marioSprites.UpdateCollider();
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        marioSprites.Draw(spriteBatch, invincibleTint);
    }
}