using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SprintZero.blocks;
using SprintZero.Marios;
using System;

public class smallTube : IBlock
{

    private TextureRegion sprite;
    public Vector2 location { get; set; }
    public Rectangle Collider { get; set; }
    public smallTube(TextureRegion region)
    {
        sprite = region;
        location = new Vector2(800, 650);
        Collider = new Rectangle((int)location.X, (int)location.Y, (int)sprite.Width, (int)sprite.Height);
    }
    public void Update(GameTime gameTime) { }

    public Boolean GetCollidable()
    {
        return true;
    }
  public void onCollision(IMario mario, CollisionSide theSide)
{
    if (theSide == CollisionSide.Top)
    {
          
       // Console.WriteLine($"[Collision Debug] Mario landed on SmallTube at {Collider.Location}");
    }
    else if (theSide == CollisionSide.Left)
    {
        mario.location = new Vector2(Collider.Left - mario.MarioCollider.Width, mario.location.Y);
    }
    else if (theSide == CollisionSide.Right)
    {
        mario.location = new Vector2(Collider.Right, mario.location.Y);
    }
}
    public void Draw(SpriteBatch spriteBatch)
    {
        sprite.Draw(spriteBatch, location, Color.White, 0f, Vector2.One, 4f, SpriteEffects.None, 0f);

    }
  
}