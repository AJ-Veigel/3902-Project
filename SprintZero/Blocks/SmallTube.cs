using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SprintZero.blocks;
using SprintZero.Marios;
using System;

public class smallTube : IBlock
{

     private const float SCALE = 4f;
    private TextureRegion sprite;
    public Vector2 location { get; set; }
    public Rectangle Collider { get; set; }
    
    public bool GetCollidable() => true;
    public smallTube(TextureRegion region)
    {
        sprite = region;

        location = new Vector2(600, 700);

    
        Collider = new Rectangle(
            (int)location.X,
            (int)location.Y,
            (int)sprite.Width ,
            (int)sprite.Height
        );
    }


    public void Update(GameTime gameTime)
    {
        UpdateCollider();
    }
private void UpdateCollider()
    {
          Collider = new Rectangle(
            (int)location.X,
            (int)location.Y,
            (int)(sprite.Width * SCALE),
            (int)(sprite.Height * SCALE));
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        sprite.Draw(spriteBatch, location, Color.White, 0f, Vector2.One, 4f, SpriteEffects.None, 0f);

    }
 public void onCollision(IMario mario, CollisionSide side)
    {
            switch (side)
            {
                case CollisionSide.Left:
                    if (mario.xVelocity < 0) { break; }
                    mario.location = new Vector2(Collider.Left - mario.MarioCollider.Width, mario.location.Y);
                    mario.xVelocity = 0;
                    break;
                case CollisionSide.Right:
                    if (mario.xVelocity > 0) { break; }
                    mario.location = new Vector2(Collider.Right, mario.location.Y);
                    mario.xVelocity = 0;
                    break;
                case CollisionSide.Top:
                   break;
                case CollisionSide.Bottom:
                    if (mario.yVelocity > 0) { break; }
                    mario.location = new Vector2(mario.location.X, Collider.Bottom);
                    mario.yVelocity = 0;
                    break;
                default: throw new System.Exception("Invalid collision side for collision.");
            }
            return;
    }
}
