using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SpriteZero.Marios;

public class standingBigMario : IMario
{
 
    private TextureRegion sprite;
    public Vector2 position {get;set;}
    public Boolean Sprint {get;set;}
    public Boolean Crouch {get;set;}
    public Boolean Swim {get;set;}
    public void MoveRight()
    {
        
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
    public standingBigMario(TextureRegion region)
    {
        sprite = region;
        position = new Vector2(300,600);
    }
    public void Update(GameTime gameTime){}

    public void Draw(SpriteBatch spriteBatch)
    {
        sprite.Draw(spriteBatch,position,Color.White,0f,Vector2.One,4f,SpriteEffects.None,0f);

    }
}