//All the packages I will be using 
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SpriteZero.Sprites;

public class animatedButNonMoving : ISprite
{
    private AnimatedSprite sprite;
    public Vector2 location{get;set;}

    public animatedButNonMoving(AnimatedSprite animated)
    {
        //Setting the sprite to the one that was passed in
        sprite = animated;
        sprite.Scale = new Vector2(4f);
        //Getting the location which should be the middle
        location = new Vector2(530,325);
    }
    public void Update(GameTime gameTime)
    {
        sprite.Update(gameTime);
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        //drawing the sprite in the location 
        sprite.Draw(spriteBatch,location);
    }
}