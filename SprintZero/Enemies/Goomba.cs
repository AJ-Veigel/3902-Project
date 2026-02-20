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

    public Goomba(TextureRegion region)
    {
        currentSprite = region;
        position = new Vector2(400, 500);
    }

    public Goomba(AnimatedSprite animated)
    {
        currentASprite = animated;
        currentASprite.Scale = new Vector2(4f);
        position = new Vector2(400, 500);
    }
    public Goomba  (TextureRegion goombaRight1, TextureRegion goombaLeft1, TextureRegion goombaFlat1, AnimatedSprite goombaWalk1, AnimatedSprite goombaHit1)
    {
        currentSprite = goombaRight1;
        goombaRight1Sprite = goombaRight1;
        goombaLeft1Sprite = goombaLeft1;
        goombaFlat1Sprite= goombaFlat1;
        goombaWalk1Sprite = goombaWalk1;
        goombaHit1Sprite = goombaHit1;
        currentASprite = goombaWalk1;
        startXPosition = 400;
    }
    public void Update(GameTime gameTime)
    {
        //TODO: add animations
    }
    public void Draw(SpriteBatch spriteBatch)
    {

        //TODO: add drawing logic
    }
    public bool Dead { get; set; }
}