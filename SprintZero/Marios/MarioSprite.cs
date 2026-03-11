using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SpriteZero.Sprites;

public class MarioSprite : ISprite
{
    public Rectangle Collider {get; set;}
    public Vector2 location {get; set;}
    private TextureRegion standingLeftSprite;
    private TextureRegion standingRightSprite;
    private TextureRegion jumpingLeftSprite;
    private TextureRegion jumpingRightSprite;
    private TextureRegion crouchingLeftSprite;
    private TextureRegion crouchingRightSprite;
    private TextureRegion deathSprite;
    private AnimatedSprite moveRightSprite;
    private AnimatedSprite moveLeftSprite;
    private AnimatedSprite swimRightSprite;
    private AnimatedSprite swimLeftSprite;
    private AnimatedSprite flagpoleLeftSprite;
    private AnimatedSprite flagpoleRightSprite;
    private AnimatedSprite throwLeftSprite;
    private AnimatedSprite throwRightSprite;
    private TextureRegion currentSprite;
    private AnimatedSprite currentAnimatedSprite;

    public TextureRegion GetSprite()
    {
        return currentSprite;
    }

    public void SetSprite(string marioState)
    {
        switch (marioState)
        {
            case "standLeft":
                currentSprite = standingLeftSprite;
                break;
            case "standRight":
                currentSprite = standingRightSprite;
                break;
            case "jumpLeft":
                currentSprite = jumpingLeftSprite;
                break;
            case "jumpRight":
                currentSprite = jumpingRightSprite;
                break;
            case "death":
                currentSprite = deathSprite;
                break;
            case "crouchLeft":
                currentSprite = crouchingLeftSprite;
                break;
            case "crouchRight":
                currentSprite = crouchingRightSprite;
                break;
            default:
                currentSprite = null;
                break;
        }
    }

    public AnimatedSprite GetAnimatedSprite()
    {
        return currentAnimatedSprite;
    }

    public void SetAnimatedSprite(string marioState)
    {
        switch (marioState)
        {
            case "moveLeft":
                currentAnimatedSprite = moveLeftSprite;
                break;
            case "moveRight":
                currentAnimatedSprite = moveRightSprite;
                break;
            case "swimLeft":
                currentAnimatedSprite = swimLeftSprite;
                break;
            case "swimRight":
                currentAnimatedSprite = swimRightSprite;
                break;
            case "flagpoleLeft":
                currentAnimatedSprite = flagpoleLeftSprite;
                break;
            case "flagpoleRight":
                currentAnimatedSprite = flagpoleRightSprite;
                break;
            case "throwLeft":
                currentAnimatedSprite = throwLeftSprite;
                break;
            case "throwRight":
                currentAnimatedSprite = throwRightSprite;
                break;
            default:
                currentAnimatedSprite = null;
                break;
        }
    }

    public Vector2 GetLocation()
    {
        return location;
    }

    public void SetLocation(Vector2 loc)
    {
        location = loc;
    }

    public MarioSprite(TextureAtlas marioTexture, int marioNumber, Vector2 position)
    {
        location = position;
        LoadContent(marioTexture, marioNumber);
    }

    // marioNumber = 0 : small mario
    // marioNumber = 1 : big mario
    // marioNumber = 2 : fire mario
    public void LoadContent(TextureAtlas marioTexture, int marioNumber)
    {
        if(marioNumber == 0)
        {
            // Store Still Sprites
            standingLeftSprite = marioTexture.GetRegion("standingLeftSmallMario");
            standingRightSprite = marioTexture.GetRegion("standingRightSmallMario");
            jumpingLeftSprite = marioTexture.GetRegion("jumpingLeftSmallMario");
            jumpingRightSprite = marioTexture.GetRegion("jumpingRightSmallMario");
            deathSprite = marioTexture.GetRegion("deadMario");

            // Store Animated Sprites
            moveRightSprite = marioTexture.CreateAnimatedSprite("smallRightMove");
            moveLeftSprite = marioTexture.CreateAnimatedSprite("smallLeftMove");
            swimRightSprite = marioTexture.CreateAnimatedSprite("smallRightSwim");
            swimLeftSprite = marioTexture.CreateAnimatedSprite("smallLeftSwim");
            flagpoleLeftSprite = marioTexture.CreateAnimatedSprite("smallLeftFlag");
            flagpoleRightSprite = marioTexture.CreateAnimatedSprite("smallRightFlag");

            // Set Sprites not in Small Mario Sheet to null
            crouchingLeftSprite = null;
            crouchingRightSprite = null;
            throwLeftSprite = null;
            throwRightSprite = null;
        }
        else if(marioNumber == 1)
        {
            // Store Still Sprites
            standingLeftSprite = marioTexture.GetRegion("standingLeftBigMario");
            standingRightSprite = marioTexture.GetRegion("standingRightBigMario");
            jumpingLeftSprite = marioTexture.GetRegion("jumpingLeftBigMario");
            jumpingRightSprite = marioTexture.GetRegion("jumpingRightBigMario");
            crouchingLeftSprite = marioTexture.GetRegion("crouchLeftBigMario");
            crouchingRightSprite = marioTexture.GetRegion("crouchRightBigMario");

            // Store Animated Sprites
            moveRightSprite = marioTexture.CreateAnimatedSprite("bigRightMove");
            moveLeftSprite = marioTexture.CreateAnimatedSprite("bigLeftMove");
            swimRightSprite = marioTexture.CreateAnimatedSprite("bigRightSwim");
            swimLeftSprite = marioTexture.CreateAnimatedSprite("bigLeftSwim");
            flagpoleLeftSprite = marioTexture.CreateAnimatedSprite("bigLeftFlag");
            flagpoleRightSprite = marioTexture.CreateAnimatedSprite("bigRightFlag");

            // Set sprites not in Big Mario Sheet to null
            throwLeftSprite = null;
            throwRightSprite = null;
            deathSprite = null;
        }
        else if(marioNumber == 2)
        {
            // Store Still Sprites
            standingLeftSprite = marioTexture.GetRegion("standingLeftFireMario");
            standingRightSprite = marioTexture.GetRegion("standingRightFireMario");
            jumpingLeftSprite = marioTexture.GetRegion("jumpingLeftFireMario");
            jumpingRightSprite = marioTexture.GetRegion("jumpingRightFireMario");
            crouchingLeftSprite = marioTexture.GetRegion("crouchLeftFireMario");
            crouchingRightSprite = marioTexture.GetRegion("crouchRightFireMario");

            // Store Animated Sprites
            moveRightSprite = marioTexture.CreateAnimatedSprite("fireRightMove");
            moveLeftSprite = marioTexture.CreateAnimatedSprite("fireLeftMove");
            swimRightSprite = marioTexture.CreateAnimatedSprite("fireRightSwim");
            swimLeftSprite = marioTexture.CreateAnimatedSprite("fireLeftSwim");
            flagpoleLeftSprite = marioTexture.CreateAnimatedSprite("fireLeftFlag");
            flagpoleRightSprite = marioTexture.CreateAnimatedSprite("fireRightFlag");

            // Store Throwing Sprites
            throwLeftSprite = marioTexture.CreateAnimatedSprite("fireThrowLeft");
            throwRightSprite = marioTexture.CreateAnimatedSprite("fireThrowRight");

            // Set sprites not in Fire Mario Sheet to null
            deathSprite = null;
        }
        currentSprite = standingLeftSprite;
        currentAnimatedSprite = null;
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        if (currentSprite != null)
        {
            currentSprite.Draw(spriteBatch, location, Color.White, 0f, Vector2.One, 4f, SpriteEffects.None, 0f);
        }
        else if (currentAnimatedSprite != null)
        {
            currentAnimatedSprite.Scale = new Vector2(4f);
            currentAnimatedSprite.Draw(spriteBatch, location);
        }
    }

    public void Update(GameTime gametime)
    {
        
    }
}