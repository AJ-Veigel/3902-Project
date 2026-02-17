using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SpriteZero.Marios;

public class BigMario : IMario
{
 
    private TextureRegion sprite;
    private AnimatedSprite aSprite;
    public Vector2 position {get;set;}
    public Boolean Sprint {get;set;}
    public Boolean Crouch {get;set;}
    public Boolean Swim {get;set;}
    public void MoveRight()
    {
        position = new Vector2(position.X + 4f,position.Y);
    }
    public void MoveLeft()
    {
        
    }
    public void Jump()
    {
        
    }
    public void Fireball()
    {
        
    }
    public BigMario(TextureRegion region)
    {
        sprite = region;
        position = new Vector2(300,600);
    }
    public BigMario(AnimatedSprite animated)
    {
        aSprite = animated;
        aSprite.Scale = new Vector2(4f);
        position = new Vector2(300, 664);
    }
    public void Update(GameTime gameTime)
    {
        if(aSprite != null) aSprite.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if(sprite != null)
        {
            sprite.Draw(spriteBatch,position,Color.White,0f,Vector2.One,4f,SpriteEffects.None,0f);
        }
        else if(aSprite != null)
        {
            aSprite.Draw(spriteBatch, position);
        }

    }
}