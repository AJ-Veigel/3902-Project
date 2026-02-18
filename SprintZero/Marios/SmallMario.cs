using System;
using System.Security.Cryptography.X509Certificates;
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
    public Vector2 position {get;set;}
    public Boolean Jumping {get; set;}
    public Boolean Direction {get; set;}
    public Boolean Sprint {get;set;}
    public Boolean Crouch {get;set;}
    public Boolean Swim {get;set;}
    public void Move()
    {
        if(Direction)
        {
            currentASprite = moveRightSprite;
            currentASprite.Scale = new Vector2(4f);
            position = new Vector2(position.X + 4f,position.Y);
            currentSprite = null;
        }
        else if (!Direction)
        {
            currentASprite = moveLeftSprite;
            currentASprite.Scale = new Vector2(4f);
            position = new Vector2(position.X - 4f,position.Y);
            currentSprite = null;
        }
        
    }
    public void StopMove()
    {
        if(Direction)
        {
            currentSprite = standingRightSprite;
            currentASprite = null;
        }
        else if(!Direction)
        {
            currentSprite = standingLeftSprite;
            currentASprite = null;    
        }
        
    }
    public void Jump()
    {
        
    }
    public void Fireball()
    {
        
    }
    public SmallMario(TextureRegion region)
    {
        currentSprite = region;
        position = new Vector2(300,664);
    }
    public SmallMario(AnimatedSprite animated)
    {
        currentASprite = animated;
        currentASprite.Scale = new Vector2(4f);
        position = new Vector2(300, 664);
    }
    public SmallMario(TextureRegion standingLeft, TextureRegion standingRight, TextureRegion jumpingLeft, TextureRegion jumpingRight, TextureRegion death, AnimatedSprite rightMove, AnimatedSprite leftMove, AnimatedSprite rightSwim, AnimatedSprite leftSwim, AnimatedSprite leftFlagpole, AnimatedSprite rightFlagpole)
    {
        currentSprite = standingLeft;
        standingLeftSprite = standingLeft;
        standingRightSprite = standingRight;
        jumpingLeftSprite = jumpingLeft;
        jumpingRightSprite = jumpingRight;
        deathSprite = death;
        moveRightSprite = rightMove;
        moveLeftSprite = leftMove;
        swimRightSprite = rightSwim;
        swimLeftSprite = leftSwim;
        leftFlagpoleSprite = leftFlagpole;
        rightFlagpoleSprite = rightFlagpole;
        position = new Vector2(300, 664);
    }
    public void Update(GameTime gameTime)
    {
        if(currentASprite != null) currentASprite.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if(currentSprite != null)
        {
            currentSprite.Draw(spriteBatch,position,Color.White,0f,Vector2.One,4f,SpriteEffects.None,0f);
        }
        else if(currentASprite != null)
        {
            currentASprite.Draw(spriteBatch, position);
        }

    }
}