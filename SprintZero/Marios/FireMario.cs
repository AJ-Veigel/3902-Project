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
     public Vector2 velocity {get;set;}


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
    public Rectangle MarioCollider {get; set;}

    // State flags 
    public bool Jumping { get; set; }
    public bool Falling { get; set; }
    public bool Direction { get; set; } = true; 
    public bool Sprinting { get; set; }
    public bool Crouching { get; set; }
    public bool Swimming { get; set; }

    // Throw Timer
    private bool throwing;
    private double throwTimerMs;
    private const double THROW_DURATION_MS = 180;
    private const float groundLevel = 600f;

    public FireMario( TextureAtlas fireMarioTexture )
    {
        // Store static poses
        standingLeftSprite = fireMarioTexture.GetRegion("standingLeftFireMario");
        standingRightSprite = fireMarioTexture.GetRegion("standingRightFireMario");
        jumpingLeftSprite = fireMarioTexture.GetRegion("jumpingLeftFireMario");
        jumpingRightSprite = fireMarioTexture.GetRegion("jumpingRightFireMario");
        crouchLeftSprite = fireMarioTexture.GetRegion("crouchLeftFireMario");
        crouchRightSprite = fireMarioTexture.GetRegion("crouchRightFireMario");

        // Store animated sprites
        moveRightSprite = fireMarioTexture.CreateAnimatedSprite("fireRightMove");
        moveLeftSprite = fireMarioTexture.CreateAnimatedSprite("fireLeftMove");
        swimRightSprite = fireMarioTexture.CreateAnimatedSprite("fireRightSwim");
        swimLeftSprite = fireMarioTexture.CreateAnimatedSprite("fireLeftSwim");
        flagpoleLeftSprite = fireMarioTexture.CreateAnimatedSprite("fireLeftFlag");
        flagpoleRightSprite = fireMarioTexture.CreateAnimatedSprite("fireRightFlag");

        // Store throw sprites
        throwLeftSprite = fireMarioTexture.CreateAnimatedSprite("fireThrowLeft");
        throwRightSprite = fireMarioTexture.CreateAnimatedSprite("fireThrowRight");

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

        // Set Mario Collider
        MarioCollider = new Rectangle((int)position.X, (int)position.Y, currentSprite.Width * (int)SCALE, currentSprite.Height * (int)SCALE);
    }

    public FireMario( TextureAtlas fireMarioTexture , Vector2 pos)
    {
        // Store static poses
        standingLeftSprite = fireMarioTexture.GetRegion("standingLeftFireMario");
        standingRightSprite = fireMarioTexture.GetRegion("standingRightFireMario");
        jumpingLeftSprite = fireMarioTexture.GetRegion("jumpingLeftFireMario");
        jumpingRightSprite = fireMarioTexture.GetRegion("jumpingRightFireMario");
        crouchLeftSprite = fireMarioTexture.GetRegion("crouchLeftFireMario");
        crouchRightSprite = fireMarioTexture.GetRegion("crouchRightFireMario");

        // Store animated sprites
        moveRightSprite = fireMarioTexture.CreateAnimatedSprite("fireRightMove");
        moveLeftSprite = fireMarioTexture.CreateAnimatedSprite("fireLeftMove");
        swimRightSprite = fireMarioTexture.CreateAnimatedSprite("fireRightSwim");
        swimLeftSprite = fireMarioTexture.CreateAnimatedSprite("fireLeftSwim");
        flagpoleLeftSprite = fireMarioTexture.CreateAnimatedSprite("fireLeftFlag");
        flagpoleRightSprite = fireMarioTexture.CreateAnimatedSprite("fireRightFlag");

        // Store throw sprites
        throwLeftSprite = fireMarioTexture.CreateAnimatedSprite("fireThrowLeft");
        throwRightSprite = fireMarioTexture.CreateAnimatedSprite("fireThrowRight");

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
        position = pos;
        Direction = true;

        // Start in standing-right pose (or left if you prefer)
        currentSprite = standingRightSprite;
        currentASprite = null;

        // Set Mario Collider
        MarioCollider = new Rectangle((int)position.X, (int)position.Y, currentSprite.Width * (int)SCALE, currentSprite.Height * (int)SCALE);
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
        if (!Jumping && !Crouching && !Swimming && !throwing && !Falling)
        {
            SetAnimated(Direction ? moveRightSprite : moveLeftSprite);
        }
    }

    public void setAppropriate()
    {
        if (Swimming)
            SetAnimated(Direction ? swimRightSprite : swimLeftSprite);
        else if (Crouching)
            SetRegion(Direction ? crouchRightSprite : crouchLeftSprite);
        else if (Jumping)
            SetRegion(Direction ? jumpingRightSprite : jumpingLeftSprite);
        else
            SetRegion(Direction ? standingRightSprite : standingLeftSprite);
    }

    public void StopMove()
    {
        if (!Jumping && !Crouching && !Swimming && !throwing)
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

    public void Crouch()
    {
        
    }

    public void Fireball()
    {
        throwing = true;
        throwTimerMs = 0;
        SetAnimated(Direction ? throwRightSprite : throwLeftSprite);
    }

    public void Damage()
    {
        
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