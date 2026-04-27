using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using MonoGameLibrary.Graphics;
using SprintZero.blocks;
using SprintZero.Marios;
using SprintZero;
using SprintZero.Items;
using SoundManager;
using System.Collections.Generic;

public class ItemBrick : IBlock
{
    private AnimatedSprite sprite;
    private AnimatedSprite emptySprite;
    private TextureAtlas itemTexture;
    private List<ICollectable> items;
    public Vector2 location { get; set; }
    public Rectangle Collider { get; set; }

    private const float SCALE = 4f;
    private float startY;
    private float bounceHeight = 20f;
    private float bounceSpeed = 3f;

    private bool isHit = false;
    private bool movingUp = false;
    private bool movingDown = false;
    private bool containsStar;

    public ItemBrick(AnimatedSprite brickAnim, AnimatedSprite emptyAnim, Vector2 pos, TextureAtlas texture, List<ICollectable> currItems, bool hasStar)
    {
        sprite = brickAnim;
        sprite.Scale = new Vector2(SCALE);
        sprite.Pause();

        emptySprite = emptyAnim;
        emptySprite.Scale = new Vector2(SCALE);
        emptySprite.PauseFrame(1);

        location = pos;
        startY = location.Y;
        itemTexture = texture;
        items = currItems;
        containsStar = hasStar;

        Collider = new Rectangle(
            (int)location.X,
            (int)location.Y,
            (int)sprite.Width,
            (int)sprite.Height);
    }

    public void Update(GameTime gameTime)
    {
        if (isHit)
        {
            if (movingUp)
            {
                location = new Vector2(location.X, location.Y - bounceSpeed);
                if (location.Y <= startY - bounceHeight)
                {
                    movingUp = false;
                    movingDown = true;
                }

            }
            else if (movingDown)
            {
                location = new Vector2(location.X, startY);
                if (location.Y >= startY)
                {
                    location = new Vector2(location.X, startY);
                    movingDown = false;
                    isHit = true;
                }

            }
        }
        sprite.Update(gameTime);
        Collider = new Rectangle((int)location.X, (int)location.Y, (int)sprite.Width, (int)sprite.Height);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        sprite.Draw(spriteBatch, location);
    }

    public void onCollision(IMario mario, CollisionSide side)
    {
        if (side == CollisionSide.Bottom && mario.yVelocity < 0.0) { mario.yVelocity = 0; }
        if (side == CollisionSide.Bottom)
        {
            if (!isHit)
            {
                isHit = true;
                Music.blockSound.Play();
                movingUp = true;
                movingDown = false;

                // Swap the exterior sprite to the inactive block
                sprite = emptySprite;

                Vector2 aboveBlock = new Vector2(location.X, location.Y - 64);

                // Dispense the correct item
                if (containsStar)
                {
                    SpawnItem.SpawnStar(itemTexture, items, aboveBlock);
                }
                else
                {
                    SpawnItem.SpawnOneUp(itemTexture, items, aboveBlock);
                }
            }
        }
        else if (side == CollisionSide.Top)
        {
            mario.LandOnBlock(location.Y);
        }
        else if (side == CollisionSide.Left)
        {
            mario.location = new Vector2(Collider.Left - mario.MarioCollider.Width, mario.location.Y);
        }
        else if (side == CollisionSide.Right)
        {
            mario.location = new Vector2(Collider.Right, mario.location.Y);
        }
    }
}