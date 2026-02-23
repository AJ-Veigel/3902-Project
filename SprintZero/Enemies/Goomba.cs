using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SpriteZero.Enemies;

public class Goomba : IEnemy
{
    public Vector2 position { get; set; }
    private float speed = 4f;

    private TextureRegion goombaRight1Sprite;
    private TextureRegion goombaLeft1Sprite;
    private TextureRegion goombaFlat1Sprite;
    private AnimatedSprite goombaWalk1Sprite;
    private AnimatedSprite goombaHit1Sprite;
    private TextureRegion currentSprite;
    private AnimatedSprite currentASprite;

    public float startXPosition { get; set; }

    public bool Dead { get; set; }

    public Goomba(TextureRegion region)
    {
        currentSprite = region;
        position = new Vector2(400, 500);
    }

    public Goomba(AnimatedSprite animated)
    {
        currentASprite = animated;
        currentASprite.Scale = new Vector2(4f);
        position = new Vector2(400, 300);
    }
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
        startXPosition = 400;
    }

    public Goomba(TextureAtlas goombaTexture)
    {
    TextureRegion goombaRight1Sprite = goombaTexture.GetRegion("goombaRight1");
    TextureRegion goombaLeft1Sprite = goombaTexture.GetRegion("goombaLeft1");
    TextureRegion goombaFlat1Sprite = goombaTexture.GetRegion("goombaFlat1");
    AnimatedSprite goombaWalk1Sprite = goombaTexture.CreateAnimatedSprite("goombaWalk1");
    AnimatedSprite goombaHit1Sprite = goombaTexture.CreateAnimatedSprite("goombaHit1");
    }
    public void Update(GameTime gameTime)
    {

        if (Dead == false)
        {
            currentSprite = goombaRight1Sprite;
            currentASprite = goombaWalk1Sprite;
            startXPosition += speed;
            if (startXPosition > 600 || startXPosition < 200)
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