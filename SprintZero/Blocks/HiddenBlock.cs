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

public class HiddenBlock : IBlock
{
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
    private bool containsOneUp;

    public HiddenBlock(AnimatedSprite emptyAnim, Vector2 pos, TextureAtlas texture, List<ICollectable> currItems, bool hasStar, bool hasOneUp)
    {
        emptySprite = emptyAnim;
        emptySprite.Scale = new Vector2(SCALE);
        emptySprite.PauseFrame(1);

        location = pos;
        startY = location.Y;
        itemTexture = texture;
        items = currItems;
        containsStar = hasStar;
        containsOneUp = hasOneUp;

        Collider = new Rectangle(
            (int)location.X,
            (int)location.Y,
            (int)emptySprite.Width,
            (int)emptySprite.Height);
    }

    public bool GetisHit()
    {
        return isHit;
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
                }
            }
            emptySprite.Update(gameTime);
        }

        Collider = new Rectangle((int)location.X, (int)location.Y, (int)emptySprite.Width, (int)emptySprite.Height);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (isHit)
        {
            emptySprite.Draw(spriteBatch, location);
        }
    }

    public void onCollision(IMario mario, CollisionSide side)
    {
        if (!isHit)
        {
            if (side == CollisionSide.Bottom && mario.yVelocity < 0.0f)
            {
                mario.yVelocity = 0;
                isHit = true;
                Music.blockSound.Play();
                movingUp = true;
                movingDown = false;

                Vector2 aboveBlock = new Vector2(location.X, location.Y - 64);

                if (containsStar)
                {
                    SpawnItem.SpawnStar(itemTexture, items, aboveBlock);
                }
                else if (containsOneUp)
                {
                    SpawnItem.SpawnOneUp(itemTexture, items, aboveBlock);
                }
            }
        }
        else
        {
            if (side == CollisionSide.Bottom && mario.yVelocity < 0.0) { mario.yVelocity = 0; }
            else if (side == CollisionSide.Top)
            {
                if(isHit)
                {
                    mario.LandOnBlock(location.Y);
                }
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
}