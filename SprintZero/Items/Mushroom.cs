using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SoundManager;
using SprintZero.Items;
using SprintZero;
using SprintZero.Marios;
using System.Collections.Generic;

public class Mushroom : ICollectable
{
    private TextureRegion sprite;
    public Vector2 location { get; set; }
    public Rectangle RectCollider { get; set; }

    public float VelocityX { get; set; }
    public float VelocityY { get; set; }

    public bool onGround { get; set; } = false;

    private const float SCALE = 4f;
    private float Gravity = 0.3f;

    public bool Collected { get; set; } = false;
    public bool Collidable { get; set; } = false;

    private float spawnTimer = 0f;

    public Mushroom(TextureRegion region, Vector2 pos)
    {
        sprite = region;
        location = pos;

        RectCollider = new Rectangle(
            (int)location.X,
            (int)location.Y,
            (int)(sprite.Width * SCALE),
            (int)(sprite.Height * SCALE)
        );

        VelocityX = 2f;
    }

    public void ReverseDirection()
    {
        VelocityX = -VelocityX;
    }

    public void Update(GameTime gameTime)
    {
        // ✅ stop everything after collected
        if (Collected) return;

        onGround = false;

        spawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (spawnTimer < 0.5f)
        {
            Collidable = false;
            return;
        }

        Collidable = true;

        // Gravity
        if (!onGround)
        {
            VelocityY = MathHelper.Clamp(VelocityY + Gravity, -10f, 12f);
        }
        else
        {
            VelocityY = 0f;
        }

        // Movement
        location = new Vector2(
            location.X + VelocityX,
            location.Y + VelocityY
        );

        // Update collider
        RectCollider = new Rectangle(
            (int)location.X,
            (int)location.Y,
            (int)(sprite.Width * SCALE),
            (int)(sprite.Height * SCALE)
        );
    }

    public bool CheckCollisions(IMario mario)
    {
        // ✅ prevent multiple triggers
        if (!Collidable || Collected)
            return false;

        if (RectCollider.Intersects(mario.MarioCollider))
        {
            Collected = true;
            Collidable = false; // 🔥 stops repeat hits
            Music.oneupSound.Play();
            return true;
        }

        return false;
    }

    public void Update(GameTime gameTime, int coins, int score)
    {
        Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (!Collected)
        {
            Vector2 lockedPos = new Vector2((int)location.X, (int)location.Y);
            sprite.Draw(
                spriteBatch,
                lockedPos,
                Color.White,
                0f,
                Vector2.One,
                SCALE,
                SpriteEffects.None,
                0f
            );
        }
    }
}