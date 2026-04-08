using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using MonoGameLibrary.Graphics;
using SprintZero.blocks;
using SprintZero.Marios;
using SprintZero;
using SprintZero.Items;
using SoundManager;



public class questionMarkItem : IBlock
{
    private AnimatedSprite sprite;
    private int itemNum;

    public Vector2 location { get; set; }
    public Rectangle Collider { get; set; }

    private const float SCALE = 4f;
    private float startY;
    private float bounceHeight = 20f;
    private float bounceSpeed = 3f;

    private bool isHit = false;
    private bool movingUp = false;
    private bool movingDown = false;

    public questionMarkItem(AnimatedSprite animated, Vector2 pos, int itemNumber)
    {
        sprite = animated;
        sprite.Scale = new Vector2(SCALE);
        sprite.Pause();
        this.itemNum = itemNumber;
        location = pos;
        startY = location.Y;

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
                    sprite.Play();
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
                    sprite.PauseFrame(1);

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
                if(!isHit)
                {
                    isHit = true;
                    Music.blockSound.Play();
                    movingUp = true;
                    movingDown = false;
                    //SpawnItem.Spawn(location, itemNum);
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
