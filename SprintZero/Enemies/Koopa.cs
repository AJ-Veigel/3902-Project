using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using SpriteZero.Enemies;

public class Koopa : IEnemy
{
	private static TextureRegion[] green;
	private static TextureRegion[] red;
	private static TextureRegion[] blue;


	private const float WALK_TIME = 0.25f; // in seconds
	private const float CLOSE_AWAKEN_TIME = 3.0f;
	private const float AWAKEN_TIME = 5.0f;

	private const float GRAVITY = 384.0f;

	private const float WALK_SPEED = 192.0f; // In per second scale
	private const float SHELL_SPEED = 384.0f;
	public enum KoopaType { Green, Red, Blue };
	public Vector2 position { get; set; }
	public bool Dead { get; set; }
	public bool onGround { get; set; }
	private bool FacingLeft { get; set; }
	private KoopaType Type { get; set; }
	public enum KoopaStates { Walk1, Walk2, ShellStill, ShellStill2, ShellMoving }
	public KoopaStates KoopaState { get; set; }
	private float KoopaTimer { get; set; }
	public Rectangle EnemyCollider { get; set; }
	public float VelocityX { get; set; }
	public float VelocityY { get; set; }

	public static void LoadTextures(ContentManager content)
	{
		TextureAtlas atlas = TextureAtlas.FromFile(content, "Images/koopa-definition.xml");
		const int StateCount = 5;
		green = new TextureRegion[StateCount];
		red = new TextureRegion[StateCount];
		blue = new TextureRegion[StateCount];
		green[(int)KoopaStates.Walk1] = atlas.GetRegion("greenWalk1");
		green[(int)KoopaStates.Walk2] = atlas.GetRegion("greenWalk2");
		green[(int)KoopaStates.ShellStill] = atlas.GetRegion("greenShell1");
		green[(int)KoopaStates.ShellStill2] = atlas.GetRegion("greenShell2");
		green[(int)KoopaStates.ShellMoving] = atlas.GetRegion("greenShell2");
		red[(int)KoopaStates.Walk1] = atlas.GetRegion("redWalk1");
		red[(int)KoopaStates.Walk2] = atlas.GetRegion("redWalk2");
		red[(int)KoopaStates.ShellStill] = atlas.GetRegion("redShell1");
		red[(int)KoopaStates.ShellStill2] = atlas.GetRegion("redShell2");
		red[(int)KoopaStates.ShellMoving] = atlas.GetRegion("redShell2");
		blue[(int)KoopaStates.Walk1] = atlas.GetRegion("blueWalk1");
		blue[(int)KoopaStates.Walk2] = atlas.GetRegion("blueWalk2");
		blue[(int)KoopaStates.ShellStill] = atlas.GetRegion("blueShell1");
		blue[(int)KoopaStates.ShellStill2] = atlas.GetRegion("blueShell2");
		blue[(int)KoopaStates.ShellMoving] = atlas.GetRegion("blueShell2");
	}

	public Koopa(KoopaType type = KoopaType.Green)
	{
		Dead = false;
		onGround = false;
		position = new Vector2(600.0f, 660.0f);
		FacingLeft = true;
		KoopaState = KoopaStates.Walk1;
		VelocityX = -WALK_SPEED;
		KoopaTimer = WALK_TIME;
		Type = type;
	}

	public void ReverseDirection()
	{
		FacingLeft = !FacingLeft;
	}

	private void UpdateCollider()
	{
		bool isShell = false;

		switch (this.KoopaState)
		{
			case KoopaStates.ShellStill:
			case KoopaStates.ShellStill2:
			case KoopaStates.ShellMoving:
				isShell = true;
				break;
			default:
				break;
		}

		Point point;
		if (isShell)
		{
            point = new Point((int)this.position.X, (int)this.position.Y + 12*4);
			this.EnemyCollider = new Rectangle(point, new Point(16 * 4, 12 * 4));
		}
		else
		{
            point = new Point((int)this.position.X, (int)this.position.Y);
            this.EnemyCollider = new Rectangle(point, new Point(16 * 4, 24 * 4));
		}
	}

    public void Stomped()
    {
        if (KoopaState == KoopaStates.Walk1 || KoopaState == KoopaStates.Walk2 || KoopaState == KoopaStates.ShellMoving)
        {
            KoopaState = KoopaStates.ShellStill;
            VelocityX = 0;
            KoopaTimer = AWAKEN_TIME; // Reset the timer to wake up

            UpdateCollider();
        }
    }
    public void Kicked(bool kickRight)
    {
        if (KoopaState == KoopaStates.ShellStill || KoopaState == KoopaStates.ShellStill2)
        {
            KoopaState = KoopaStates.ShellMoving;
            FacingLeft = !kickRight;
            VelocityX = kickRight ? SHELL_SPEED : -SHELL_SPEED;

            this.position += new Vector2(kickRight ? 8.0f : -8.0f, 0);

            UpdateCollider();
        }
    }

    public void Draw(SpriteBatch spriteBatch)
	{
		SpriteEffects effect = FacingLeft ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
		int offX = FacingLeft ? 0 : -16; // I suspect this is needed but idk for sure.
		TextureRegion[] sprites = null;
		if (Type == KoopaType.Green)
		{
			sprites = green;
		}
		else if (Type == KoopaType.Red)
		{
			sprites = red;
		}
		else
		{
			sprites = blue;
		}
		TextureRegion texture = sprites[(int)KoopaState];
		texture.Draw(spriteBatch, new Vector2(position.X + offX, position.Y), Color.White, 0.0f, new Vector2(0, 0), new Vector2(4.0f, 4.0f), effect, 0.0f);
	}

	private void HandleTimer()
	{
		if (this.KoopaTimer < 0.0f)
		{
			switch (this.KoopaState)
			{
				case KoopaStates.Walk1: // These animate the walking.
					this.KoopaState = KoopaStates.Walk2;
					this.KoopaTimer += WALK_TIME;
					break;
				case KoopaStates.Walk2:
					this.KoopaState = KoopaStates.Walk1;
					this.KoopaTimer += WALK_TIME;
					break;
				case KoopaStates.ShellStill:
					this.KoopaState = KoopaStates.ShellStill2; // Awaken from shell.
					this.KoopaTimer += CLOSE_AWAKEN_TIME;
					break;
				case KoopaStates.ShellStill2:
					this.KoopaState = KoopaStates.Walk1;
					this.KoopaTimer += WALK_TIME;
					break;
				default: // Moving shell has no timed events I don't think.
					break;
			}
		}
	}

	public void Update(GameTime gameTime)
	{
		float timeSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
		this.KoopaTimer -= timeSeconds;
		HandleTimer(); // Handles timed events.

		// Try move.
		if (!onGround) {
			this.VelocityY += GRAVITY * timeSeconds;
		}
		this.position += new Vector2(this.VelocityX, this.VelocityY) * timeSeconds;

		this.UpdateCollider();

		if (onGround)
		{
			switch (this.KoopaState)
			{
                case KoopaStates.Walk1:
				case KoopaStates.Walk2:
					this.VelocityX = WALK_SPEED;
					break;
				case KoopaStates.ShellMoving:
					this.VelocityX = SHELL_SPEED;
					break;
				default:
					this.VelocityX = 0;
					break;
            }
			if (FacingLeft) { this.VelocityX = -this.VelocityX; }
			this.VelocityY = 0;
		}
	}
}

