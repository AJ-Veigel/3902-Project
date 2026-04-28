using System;
using System.Collections.Generic;
using System.Text;
using EnemyCollisions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Particles.Modifiers.Interpolators;
using MonoGameLibrary.Graphics;
using SpriteZero.Enemies;


public class BarFireball : IEnemy
{
    private static TextureAtlas texture;

    private readonly AnimatedSprite rolling;

    private const float SCALE = 4.0f;

    private const float SPIN_TIME = 1.0f; // Full spin every this many seconds.

    private const int SECTORS = 16; // how many different 'rotations' there are. Idk if this is right but it's probably close.
    public Vector2 position { get; set; } // should only be used to get, it'll instantly reset otherwise.
    public bool Dead { get; set; } // unused
    public bool onGround { get; set; } // unused
    public bool Despawn { get; set; }
    public Rectangle EnemyCollider { get; set; }
    public float VelocityX { get; set; } // unused
    public float VelocityY { get; set; } // unused


    private Vector2 Center { get; set; }
    
    private float Radius { get; set; }
    private float Rotation { get; set; } // 0 = to the right. range [0, 1).

    public CheckEnemyCollisions.EnemyAction ActionState
    {
        get
        {
            return CheckEnemyCollisions.EnemyAction.None;
        }
    }

    public static void LoadTextures(ContentManager content)
    {
        texture = TextureAtlas.FromFile(content, "images/Fireball-definition.xml");
    }

    public BarFireball(Vector2 Center, float Radius)
    {
        this.Center = Center;
        this.Radius = Radius;
        this.Rotation = 0.0f;
        this.VelocityX = 0.0f;
        this.VelocityY = 0.0f;
        this.position = this.Center; // Start at center so all activate at once.
        this.rolling = texture.CreateAnimatedSprite("FireballRolling");
        rolling.Scale = new Vector2(SCALE, SCALE);
    }

    public void CollideWithEnemy(IEnemy enemy) // Enemies don't collide with firebars I don't think?
    {
        return;
    }

    public void ReverseDirection() // Not applicable
    {
        return;
    }

    public void Stomped() // Not applicable
    {
        return;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        rolling.Draw(spriteBatch, this.position);
    }

    public void Update(GameTime gameTime) // Unloading should be handled based on the center of this firebar.
    {
        float timeSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

        this.Rotation += timeSeconds / SPIN_TIME; // "true" rotation, 
        this.Rotation %= 1.0f; // Keep in [0, 1) range.

        // We round to a valid rotation.
        float realRotation = MathF.Floor(this.Rotation * SECTORS) / SECTORS;

        this.position = new Vector2(
            this.Center.X + this.Radius * MathF.Cos(realRotation * 2.0f * MathF.PI) - 4.0f*SCALE,
            this.Center.Y + this.Radius * MathF.Sin(realRotation * 2.0f * MathF.PI) - 4.0f*SCALE
        );

        rolling.Update(gameTime);

        this.EnemyCollider = new Rectangle((int)this.position.X, (int)this.position.Y, 8 * (int)SCALE, 8 * (int)SCALE);

    }
}

