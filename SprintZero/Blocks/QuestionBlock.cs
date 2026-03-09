using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SpriteZero.blocks;
using SpriteZero.Marios;

public class QuestionBlock : IBlock
{
    private AnimatedSprite sprite;
    public Vector2 location{get;set;}
    public Rectangle Collider {get; set;}
    private const float SCALE = 4f;

    public QuestionBlock(AnimatedSprite animated)
    {
      
        sprite = animated;
        sprite.Scale = new Vector2(4f);
 
        location = new Vector2(530,325);
        Collider = new Rectangle((int)location.X, (int)location.Y, (int)sprite.Width, (int)sprite.Height);
    }
    public void Update(GameTime gameTime)
    {
        sprite.Update(gameTime);
    }
    public void Draw(SpriteBatch spriteBatch)
    {
    
        sprite.Draw(spriteBatch,location);
    }
     public  void onHit(IMario mario, CollisionSide theSide){}
}