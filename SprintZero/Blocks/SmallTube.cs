using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SpriteZero.blocks;
using SpriteZero.Marios;

public class smallTube : IBlock
{
 
    private TextureRegion sprite;
    public Vector2 location {get;set;}
    public Rectangle Collider {get; set;}
    public smallTube(TextureRegion region)
    {
        sprite = region;
        location = new Vector2(400,500);
        Collider = new Rectangle((int)location.X, (int)location.Y, (int)sprite.Width, (int)sprite.Height);
    }
    public void Update(GameTime gameTime){}

    public void Draw(SpriteBatch spriteBatch)
    {
        sprite.Draw(spriteBatch,location,Color.White,0f,Vector2.One,4f,SpriteEffects.None,0f);

    }
     public  void onCollision(IMario mario, CollisionSide theSide)
    {
        if (theSide == CollisionSide.Top) {
           mario.position = new Vector2(mario.position.X,location.Y- mario.MarioCollider.Height);
           mario.Falling = false;
           mario.Jumping = false;
        }
           else if (theSide == CollisionSide.Left)
            {
                mario.position  = new Vector2(location.X-mario.MarioCollider.Width,mario.position.Y);
            }
            else if (theSide == CollisionSide.Bottom)
            {
                mario.position = new Vector2(mario.position.X,location.Y+Collider.Height);
            }
        }
}