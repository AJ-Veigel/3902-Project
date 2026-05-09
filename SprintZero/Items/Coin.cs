using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SprintZero.Items;
using SprintZero;
using SoundManager;

public class Coin : ICollectable
{
    private AnimatedSprite sprite;
    public Game1 game;
    public Vector2 location { get; set; }
    public Rectangle RectCollider { get; set; }
    public float VelocityX { get; set; }
    public float VelocityY { get; set; }
    public bool Collected { get; set; } = false;
    public bool onGround { get; set; }
    public bool Collidable { get; set; } = true;
    private float gravity = 0.4f;
    private float bounceVelocity = -8f;
    private float groundY;
    private bool endSound = false;
    private bool IsPopUpCoin = false;

    public Coin(AnimatedSprite animated, Vector2 pos, bool stationary)
    {
        sprite = animated;
        sprite.Scale = new Vector2(4f);
        location = pos;
        groundY = pos.Y;
        VelocityY = bounceVelocity;

        if (!stationary) IsPopUpCoin = true;
        Collidable = false;
    }

    public Coin(AnimatedSprite animated, Vector2 pos, bool stationary, Game1 game)
    {
        sprite = animated;
        sprite.Scale = new Vector2(4f);
        location = pos;
        groundY = pos.Y;
        VelocityY = bounceVelocity;

        if (!stationary) IsPopUpCoin = true;
        this.game = game;
        Collidable = false;
    }

    public void Update(GameTime gameTime)
    {
        if (Collected)
        {
            RectCollider = Rectangle.Empty;
            return;
        }

        sprite.Update(gameTime);

        if (IsPopUpCoin)
        {
            VelocityY += gravity;
            location = new Vector2(location.X, location.Y + VelocityY);

            if (location.Y >= groundY && VelocityY > 0)
            {
                location = new Vector2(location.X, groundY);
                VelocityY = 0;

                if (!endSound)
                {
                    Music.coinSound.Play();
                    if (game != null)
                    {
                        ScoreManager.CollectCoin(game);
                        game.coinCount++;
                    }
                    endSound = true;
                }

                Collected = true;
            }
        }
        else
        {
            RectCollider = new Rectangle(
                (int)location.X,
                (int)location.Y,
                (int)sprite.Width,
                (int)sprite.Height
                        );
        }
    }
    public void Update(GameTime gameTime, int coins, int score)
    {
        sprite.Update(gameTime);

    }
    public void ReverseDirection()
    {
        VelocityX = -VelocityX;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (!Collected)
        {
            sprite.Draw(spriteBatch, location);
        }
    }
}