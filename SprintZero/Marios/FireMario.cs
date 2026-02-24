using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SpriteZero.Marios;

public class FireMario : IMario
{
    private const float SCALE = 4f;
    private const float MOVE_SPEED = 4f;
    private const float GRAVITY = 4f;
    private const float JUMP_VELOCITY = 4f;
    private float jumpStartHeight;


    // Static poses
    private readonly TextureRegion standingLeftSprite;
    private readonly TextureRegion standingRightSprite;
    private readonly TextureRegion jumpingLeftSprite;
    private readonly TextureRegion jumpingRightSprite;
    private readonly TextureRegion crouchLeftSprite;
    private readonly TextureRegion crouchRightSprite;

    // Animated sprites
    private readonly AnimatedSprite moveRightSprite;
    private readonly AnimatedSprite moveLeftSprite;
    private readonly AnimatedSprite swimRightSprite;
    private readonly AnimatedSprite swimLeftSprite;
    private readonly AnimatedSprite flagpoleLeftSprite;
    private readonly AnimatedSprite flagpoleRightSprite;

    // Throw pose
    private readonly AnimatedSprite throwLeftSprite;
    private readonly AnimatedSprite throwRightSprite;

    // Currently drawing 
    private TextureRegion currentSprite;
    private AnimatedSprite currentASprite;

    public Vector2 position { get; set; }

    // State flags 
    public bool Jumping { get; set; }
    public bool Falling { get; set; }
    public bool Direction { get; set; } = true; 
    public bool Sprint { get; set; }
    public bool Crouch { get; set; }
    public bool Swim { get; set; }

    // Throw Timer
    private bool throwing;
    private double throwTimerMs;
    private const double THROW_DURATION_MS = 180;
    private const float groundLevel = 600f;

    public FireMario(
        TextureRegion standingLeft,
        TextureRegion standingRight,
        TextureRegion jumpingLeft,
        TextureRegion jumpingRight,
        TextureRegion crouchLeft,
        TextureRegion crouchRight,
        AnimatedSprite rightMove,
        AnimatedSprite leftMove,
        AnimatedSprite rightSwim,
        AnimatedSprite leftSwim,
        AnimatedSprite leftFlagpole,
        AnimatedSprite rightFlagpole,
        AnimatedSprite throwLeftFireMario,
        AnimatedSprite throwRightFireMario
    )
    {
        // Store static poses
        standingLeftSprite = standingLeft;
        standingRightSprite = standingRight;
        jumpingLeftSprite = jumpingLeft;
        jumpingRightSprite = jumpingRight;
        crouchLeftSprite = crouchLeft;
        crouchRightSprite = crouchRight;

        // Store animated sprites
        moveRightSprite = rightMove;
        moveLeftSprite = leftMove;
        swimRightSprite = rightSwim;
        swimLeftSprite = leftSwim;
        flagpoleLeftSprite = leftFlagpole;
        flagpoleRightSprite = rightFlagpole;

        // Store throw spriteswe
        throwLeftSprite = throwLeftFireMario;
        throwRightSprite = throwRightFireMario;

        // Set scale
        ApplyScale(moveRightSprite);
        ApplyScale(moveLeftSprite);
        ApplyScale(swimRightSprite);
        ApplyScale(swimLeftSprite);
        ApplyScale(flagpoleLeftSprite);
        ApplyScale(flagpoleRightSprite);
        ApplyScale(throwLeftSprite);
        ApplyScale(throwRightSprite);

        // Defaults
        position = new Vector2(300, 600);
        Direction = true;

        // Start in standing-right pose (or left if you prefer)
        currentSprite = standingRightSprite;
        currentASprite = null;
    }

    private static void ApplyScale(AnimatedSprite sprite)
    {
        if (sprite != null)
        {
            sprite.Scale = new Vector2(SCALE);
        }
    }

    private void SetRegion(TextureRegion region)
    {
        currentSprite = region;
        currentASprite = null;
    }

    private void SetAnimated(AnimatedSprite anim)
    {
        currentASprite = anim;
        currentSprite = null;
    }

    public void Move()
    {
        // If you’re throwing, you might want to ignore Move animation for a split second.
        // Up to you—this keeps movement but doesn’t override the throw pose.
        position = new Vector2(
            position.X + (Direction ? MOVE_SPEED : -MOVE_SPEED),
            position.Y
        );

        // Only set run animation if we’re not in a higher-priority pose
        if (!Jumping && !Crouch && !Swim && !throwing && !Falling)
        {
            SetAnimated(Direction ? moveRightSprite : moveLeftSprite);
        }
    }

    public void setAppropriate()
    {
        if (Swim)
            SetAnimated(Direction ? swimRightSprite : swimLeftSprite);
        else if (Crouch)
            SetRegion(Direction ? crouchRightSprite : crouchLeftSprite);
        else if (Jumping)
            SetRegion(Direction ? jumpingRightSprite : jumpingLeftSprite);
        else
            SetRegion(Direction ? standingRightSprite : standingLeftSprite);
    }

    public void StopMove()
    {
        if (!Jumping && !Crouch && !Swim && !throwing)
        {
            SetRegion(Direction ? standingRightSprite : standingLeftSprite);
        }
    }

    public void UpdateAirSpriteForDirection()
    {
        if(throwing) return;
        if (Jumping || Falling)
        {
            SetRegion(Direction ? jumpingRightSprite : jumpingLeftSprite);
        }    
    }

    public void Jump()
    {
        if(Jumping || Falling)
            return;
        jumpStartHeight = position.Y;
        Jumping = true;
        SetRegion(Direction ? jumpingRightSprite : jumpingLeftSprite);
    }

    public void Fireball()
    {
        throwing = true;
        throwTimerMs = 0;
        SetAnimated(Direction ? throwRightSprite : throwLeftSprite);
    }
    public Vector2 FireballSpawnPosition
    {
        get
        {
            float offsetX = Direction ? 40f : -10f;
            float offsetY = 40f;
            return new Vector2(position.X + offsetX, position.Y + offsetY);
        }
    }

    public void Update(GameTime gameTime)
    {
        if (Jumping || Falling)
            UpdateAirSpriteForDirection();

        if (Jumping)
        {
            position = new Vector2(position.X, position.Y - JUMP_VELOCITY);
            if(position.Y <= jumpStartHeight - 96)
            {
                Jumping = false;
                Falling = true;
            }
        }
        if (Falling)
        {
            position = new Vector2(position.X, position.Y + GRAVITY);
            if(position.Y >= groundLevel-32)
            {
                Falling = false;
                setAppropriate();
            }
        }

        if (throwing)
        {
            throwTimerMs += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (throwTimerMs >= THROW_DURATION_MS)
            {
                throwing = false;
                // Return to the most appropriate pose after throwing
                setAppropriate();
            }
        }


        if (currentASprite != null)
            currentASprite.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (currentSprite != null)
        {
            currentSprite.Draw(
                spriteBatch,
                position,
                Color.White,
                0f,
                Vector2.One,
                SCALE,
                SpriteEffects.None,
                0f
            );
        }
        else if (currentASprite != null)
        {
            currentASprite.Draw(spriteBatch, position);
        }
    }
}