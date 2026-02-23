using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SpriteZero.Enemies;

public class Goomba : IEnemy
{
    public Vector2 position { get; set; }
    private float speed = 2f;
    private float animationTimer = 0f;

    private TextureRegion goombaRight1Sprite;
    private TextureRegion goombaLeft1Sprite;
    private TextureRegion goombaFlat1Sprite;
    private AnimatedSprite goombaWalk1Sprite;
    private AnimatedSprite goombaHit1Sprite;
    private TextureRegion currentSprite;
    private AnimatedSprite currentASprite;


    public bool Dead { get; set; }

    public Goomba(TextureRegion goombaRight1, TextureRegion goombaLeft1, TextureRegion goombaFlat1, AnimatedSprite goombaWalk1, AnimatedSprite goombaHit1)
    {
        currentSprite = goombaRight1;
        goombaRight1Sprite = goombaRight1;
        goombaLeft1Sprite = goombaLeft1;
        goombaFlat1Sprite= goombaFlat1;
        goombaWalk1Sprite = goombaWalk1;
        goombaHit1Sprite = goombaHit1;
        currentASprite = goombaWalk1;
        Dead = false;
        position = new Vector2(600, 660);
    }

    public Goomba(TextureAtlas goombaTexture)
    {
     goombaRight1Sprite = goombaTexture.GetRegion("goombaRight1");
     goombaLeft1Sprite = goombaTexture.GetRegion("goombaLeft1");
     goombaFlat1Sprite = goombaTexture.GetRegion("goombaFlat1");
     goombaWalk1Sprite = goombaTexture.CreateAnimatedSprite("goombaWalk1");
     goombaHit1Sprite = goombaTexture.CreateAnimatedSprite("goombaHit1");
     position = new Vector2(600, 660);
    }
    public void Update(GameTime gameTime)
    {
        if(currentSprite == null)
        {
            currentSprite = goombaRight1Sprite;
        }

        animationTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (Dead == false)
        {
            position = new Vector2(position.X + speed, position.Y);
            if (animationTimer >= 0.5)
            {
                animationTimer = 0f;
                if(currentSprite == goombaRight1Sprite)
                {
                    currentSprite = goombaLeft1Sprite;
                }
                else
                {
                    currentSprite = goombaRight1Sprite;
                }
            }
            currentASprite = goombaWalk1Sprite;
            if (position.X > 700 || position.X < 500)
            {
                speed = -speed;
            }
        }
         else 
            {
            currentSprite = goombaFlat1Sprite;
            currentASprite = goombaHit1Sprite;
  
         }
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        if(currentSprite != null)
        {
            spriteBatch.Draw(currentSprite.Texture, position, currentSprite.SourceRectangle, Color.White, 0f, Vector2.Zero, 4f, SpriteEffects.None, 0f);

        }
        else if(currentASprite != null)
        {
            currentASprite.Draw(spriteBatch, position);

        }
    }
   
}