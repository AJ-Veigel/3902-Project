using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SpriteZero.blocks;
using SpriteZero.Marios;

public class Ground : IBlock
{
 
    private TextureRegion sprite;
    public Vector2 location {get;set;}
    public Rectangle Collider {get; set;}
    private const float SCALE = 4f;
    public Ground(TextureRegion region)
    {
        sprite = region;
        location = new Vector2(530,300);
        Collider = new Rectangle((int)location.X, (int)location.Y, (int)sprite.Width, (int)sprite.Height);
    }
    public void Update(GameTime gameTime){}

    public void Draw(SpriteBatch spriteBatch)
    {
        sprite.Draw(spriteBatch,location,Color.White,0f,Vector2.One,4f,SpriteEffects.None,0f);

    }
     public  void onHit(IMario mario, CollisionSide theSide){}
}