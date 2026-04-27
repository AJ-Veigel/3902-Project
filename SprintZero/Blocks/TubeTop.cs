using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SprintZero.blocks;
using SprintZero.Marios;
using SprintZero.Map;
using SprintZero;

public class TubeTop : IPipe
{
    private const float SCALE = 4f;
    private TextureRegion sprite;
    private string level = "";
    public int levelNum { get; set; }
    public int bonus { get; set; }
    public Vector2 marioSpawnPos { get; set; }
    public Vector2 location { get; set; }
    public Rectangle Collider { get; set; }


    public TubeTop(TextureRegion region, Vector2 pos)
    {
        sprite = region;

        location = pos;


        Collider = new Rectangle(
            (int)location.X,
            (int)location.Y,
            (int)(sprite.Width * SCALE),
            (int)(sprite.Height * SCALE)
        );
    }

    public TubeTop(TextureRegion region, Vector2 pos, string pipeLevel, Vector2 MarioPos, int levelNum, int bonus)
    {
        sprite = region;

        location = pos;

        marioSpawnPos = MarioPos;

        level = pipeLevel;

        this.levelNum = levelNum;

        this.bonus = bonus;

        Collider = new Rectangle(
            (int)location.X,
            (int)location.Y,
            (int)(sprite.Width * SCALE),
            (int)(sprite.Height * SCALE)
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

    public void onCollision(IMario mario, CollisionSide side, Game1 game)
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
                if (mario.Crouching && !level.Equals(""))
                {
                    LevelManager.SwapLevel(game, this);
                }
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
