using EnemyCollisions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SprintZero;
using SpriteZero.Enemies;
using System;
using System.Collections.Generic;
using System.Text;

namespace SprintZero.Enemies
{
    public class Bowser : IEnemy
    {
        public Vector2 position { get; set; }
        public bool onGround { get; set; } = false;
        public bool Despawn { get; set; } = false;
        public Rectangle EnemyCollider { get; set; }
        public float VelocityX { get; set; }
        public float VelocityY { get; set; }

        private const float MOVE_TIMING = 1.5f;
        private const float JUMP_TIMING = 3.0f;
        private const float FIRE_TIMING = 4.0f;
        private const float HAMMER_TIMING = 1.2f;
        private const float ANIMATION_TIMING = 0.3f;
        private const float FIRE_ANIM_COOLDOWN = 0.5f;
        private const float DAMAGE_COOLDOWN_DURATION = 0.5f;





        private bool isDead = false;
        public bool Dead
        {
            get { return isDead; }
            set
            {
                if (value && !isDead)
                {
                    isDead = true;

                    currentSprite = grayGoombaSprite;

                    // Adjusts position so the Goomba is centered where Bowser was standing
                    position = new Vector2(position.X + 34f, position.Y + 64f);

                    EnemyCollider = new Rectangle(0, 0, 0, 0);
                    VelocityX = 0;
                    VelocityY = -8f;
                }
            }
        }

        public CheckEnemyCollisions.EnemyAction ActionState => CheckEnemyCollisions.EnemyAction.Bounce;

        private Game1 game;
        private const float SCALE = 4f;
        private const float GRAVITY = 0.5f;

        private TextureRegion mouthOpen1;
        private TextureRegion mouthClosed1;
        private TextureRegion grayGoombaSprite; // Fake Goomba sprite for when Bowser dies
        private TextureRegion currentSprite;

        private float fireTimer = 0f;
        private float hammerTimer = 0f;
        private float jumpTimer = 0f;
        private float moveTimer = 0f;
        private float animationTimer = 0f;

        public int health = 5;
        private bool facingLeft = true;

        private Random rng = new Random();

        // Cooldown timer for invincibility frames
        private float damageCooldown = 0f;

        public Bowser(TextureAtlas bowserTexture, TextureAtlas goombaTexture, Game1 gameInstance, Vector2 startPos)
        {
            game = gameInstance;
            position = startPos;

            mouthOpen1 = bowserTexture.GetRegion("mouthOpen1");
            mouthClosed1 = bowserTexture.GetRegion("mouthClosed1");

            grayGoombaSprite = goombaTexture.GetRegion("goombaRight3");

            currentSprite = mouthClosed1;
            VelocityX = -3.0f;

            EnemyCollider = new Rectangle((int)position.X, (int)position.Y,
                (int)(currentSprite.SourceRectangle.Width * SCALE),
                (int)(currentSprite.SourceRectangle.Height * SCALE));
        }

        public void CollideWithEnemy(IEnemy enemy)
        {
            if (enemy.ActionState == CheckEnemyCollisions.EnemyAction.Bounce)
            {
                ReverseDirection();
            }
            else if (enemy.ActionState == CheckEnemyCollisions.EnemyAction.Kill)
            {
                TakeDamage();
            }
        }

        public void TakeDamage()
        {
            // If the cooldown is active, ignore the hit
            if (damageCooldown > 0) return;

            health--;

            // Give Bowser half a second of invincibility per hit
            damageCooldown = DAMAGE_COOLDOWN_DURATION;

            if (health <= 0)
            {
                Dead = true;
            }
        }

        public void ReverseDirection()
        {
            VelocityX = -VelocityX;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Despawn) return;

            SpriteEffects effect = facingLeft ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            if (Dead) effect |= SpriteEffects.FlipVertically;

            if (currentSprite != null)
            {
                // Optional: You could make Bowser flash red or disappear briefly when damageCooldown > 0 here to show he took a hit!
                spriteBatch.Draw(currentSprite.Texture, position, currentSprite.SourceRectangle, Color.White, 0f, Vector2.Zero, SCALE, effect, 0f);
            }
        }

        public void Stomped()
        {
            // Stomping on Bowser does nothing
        }

        public void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Count down the invincibility frames
            if (damageCooldown > 0) damageCooldown -= dt;

            if (Dead)
            {
                VelocityY += GRAVITY;
                position += new Vector2(VelocityX, VelocityY);
                if (position.Y > 2000f) Despawn = true;
                return;
            }

            facingLeft = game.currentMario.location.X < position.X;

            fireTimer += dt;
            hammerTimer += dt;
            jumpTimer += dt;
            moveTimer += dt;
            animationTimer += dt;

            if (moveTimer > MOVE_TIMING)
            {
                VelocityX = -VelocityX;
                moveTimer = 0f;
            }

            if (jumpTimer > JUMP_TIMING && onGround)
            {
                VelocityY = -12f;
                onGround = false;
                jumpTimer = 0f;

                bool jumpBackwards = rng.Next(0, 2) == 0;
                float jumpSpeed = 3.0f;

                if (facingLeft)
                {
                    VelocityX = jumpBackwards ? jumpSpeed : -jumpSpeed;
                }
                else
                {
                    VelocityX = jumpBackwards ? -jumpSpeed : jumpSpeed;
                }

                moveTimer = 0f;
            }

            if (fireTimer > FIRE_TIMING)
            {
                bool shootRight = !facingLeft;
                Vector2 spawnPos = new Vector2(position.X + (facingLeft ? -30f : 100f), position.Y + 20f);
                game.SpawnBowserFireball(spawnPos, shootRight);

                currentSprite = mouthOpen1;
                fireTimer = 0f;
                animationTimer = 0f;
            }

            if (hammerTimer > HAMMER_TIMING)
            {
                bool shootRight = !facingLeft;
                Vector2 spawnPos = new Vector2(position.X + (facingLeft ? -10f : 80f), position.Y - 20f);
                game.SpawnHammer(spawnPos, shootRight);

                hammerTimer = 0f;
            }

            if (animationTimer > ANIMATION_TIMING && fireTimer > FIRE_ANIM_COOLDOWN)
            {
                animationTimer = 0f;
                currentSprite = (currentSprite == mouthClosed1) ? mouthOpen1 : mouthClosed1;
            }

            if (!onGround)
            {
                VelocityY = MathHelper.Clamp(VelocityY + GRAVITY, -15f, 15f);
            }
            else
            {
                VelocityY = 0f;
            }

            position += new Vector2(VelocityX, VelocityY);
            onGround = false;

            EnemyCollider = new Rectangle((int)position.X, (int)position.Y,
                (int)(currentSprite.SourceRectangle.Width * SCALE),
                (int)(currentSprite.SourceRectangle.Height * SCALE));
        }
    }
}