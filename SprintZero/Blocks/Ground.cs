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
        location = new Vector2(600, 600);
        Collider = new Rectangle((int)location.X, (int)location.Y, (int)sprite.Width, (int)sprite.Height);
    }
    public void Update(GameTime gameTime)
{
    Collider = new Rectangle(
        (int)location.X,
        (int)location.Y,
        (int)(sprite.Width * SCALE),
        (int)(sprite.Height * SCALE)
    );
}

    public void Draw(SpriteBatch spriteBatch)
    {
        sprite.Draw(spriteBatch,location,Color.White,0f,Vector2.One,4f,SpriteEffects.None,0f);

    }
public void onCollision(IMario mario, CollisionSide theSide)
{
    if (theSide == CollisionSide.Top)
    {
        mario.position = new Vector2(
            mario.position.X,
            Collider.Top - mario.MarioCollider.Height
        );

        mario.Falling = false;
        mario.Jumping = false;
        mario.jumpStartHeight = mario.position.Y;
    }
}
}