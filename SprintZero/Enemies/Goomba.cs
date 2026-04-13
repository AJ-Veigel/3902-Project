using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SprintZero.Collisions;
using SpriteZero.Enemies;

public class Goomba : IEnemy
{
    public Vector2 position { get; set; }
    private const float SCALE = 4f;
    private float animationTimer = 0f;
    public Rectangle EnemyCollider { get; set; }
    public float VelocityX { get; set; } = -2f;
    public float VelocityY { get; set; } = 0f;
    public bool onGround { get; set; } = false;
    public bool Despawn { get; set; }

    public EnemyEnemyCollision.EnemyAction ActionState { get => EnemyEnemyCollision.EnemyAction.Bounce; }

    private const float Gravity = 0.5f;
    private float deathTimer = 0f; 

    private TextureRegion goombaRight1Sprite;
    private TextureRegion goombaLeft1Sprite;
    private TextureRegion goombaFlat1Sprite;
    private AnimatedSprite goombaWalk1Sprite;
    private AnimatedSprite goombaHit1Sprite;
    private TextureRegion currentSprite;
    private AnimatedSprite currentASprite;


    public bool Dead { get; set; }

    public Goomba(TextureAtlas goombaTexture)
    {
        goombaRight1Sprite = goombaTexture.GetRegion("goombaRight1");
        goombaLeft1Sprite = goombaTexture.GetRegion("goombaLeft1");
        goombaFlat1Sprite = goombaTexture.GetRegion("goombaFlat1");
        goombaWalk1Sprite = goombaTexture.CreateAnimatedSprite("goombaWalk1");
        goombaHit1Sprite = goombaTexture.CreateAnimatedSprite("goombaHit1");
        position = new Vector2(600, 650);
        EnemyCollider = new Rectangle((int)position.X, (int)position.Y, goombaRight1Sprite.SourceRectangle.Width * (int)SCALE, goombaRight1Sprite.SourceRectangle.Height * (int)SCALE);
    }

    public void ReverseDirection()
    {
        VelocityX = -VelocityX;
    }

    public void CollideWithEnemy(IEnemy enemy)
    {
        
    }
    public void Update(GameTime gameTime)
    {
        if (currentSprite == null && !Dead)
        {
            currentSprite = goombaRight1Sprite;
        }

        animationTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (!Dead)
        {
            if (!onGround)
            {
                VelocityY = MathHelper.Clamp(VelocityY + Gravity, -10f, 12f);
            }
            else
            {
                VelocityY = 0f;
            }
            position = new Vector2(position.X + VelocityX, position.Y + VelocityY);

            EnemyCollider = new Rectangle((int)position.X, (int)position.Y,
            currentSprite.SourceRectangle.Width * (int)SCALE,
            currentSprite.SourceRectangle.Height * (int)SCALE);

            if (animationTimer >= 0.5f)
            {
                animationTimer = 0f;
                if (currentSprite == goombaRight1Sprite)
                {
                    currentSprite = goombaLeft1Sprite;
                }
                else
                {
                    currentSprite = goombaRight1Sprite;
                }
            }
            currentASprite = goombaWalk1Sprite;

        }
        else
        {
            EnemyCollider = new Rectangle(0, 0, 0, 0);
            if (!Despawn)
            {
                deathTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (deathTimer < 1.0f)
                {
                    currentSprite = goombaFlat1Sprite;
                    currentASprite = goombaHit1Sprite;
                }
                else
                {
                    Despawn = true;
                    currentSprite = null;
                    currentASprite = null;
                }
            }

        }
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        if (currentSprite != null)
        {

            spriteBatch.Draw(currentSprite.Texture, position, currentSprite.SourceRectangle, Color.White, 0f, Vector2.Zero, 4f, SpriteEffects.None, 0f);

        }
        
        else if (currentASprite != null)
        {

            currentASprite.Draw(spriteBatch, position);

        }
    }

}