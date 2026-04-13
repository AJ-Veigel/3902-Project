using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using SprintZero.Collisions;
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

	private const float WALK_SPEED = 75.0f; // In per second scale
	private const float SHELL_SPEED = 384.0f;
	public enum KoopaType { Green, Red, Blue };
	public Vector2 position { get; set; }
	private bool isDead = false;
	public bool Dead {
		get { return isDead; }
		set
		{
			if(value && !isDead)
			{
				isDead = true;
				KoopaState = KoopaStates.ShellStill; 
				VelocityX = 0;
				VelocityY = -GRAVITY * 0.5f;
				EnemyCollider = new Rectangle(0, 0, 0, 0);
            }
		}
	}

	public bool Despawn { get; set; }
    public bool onGround { get; set; }
	private bool FacingLeft { get; set; }
	private bool isShell { get; set; }
	private KoopaType Type { get; set; }
	public enum KoopaStates { Walk1, Walk2, ShellStill, ShellStill2, ShellMoving }
	public KoopaStates KoopaState { get; set; }
	private float KoopaTimer { get; set; }
	public Rectangle EnemyCollider { get; set; }
	public float VelocityX { get; set; }
	public float VelocityY { get; set; }

	public EnemyEnemyCollision.EnemyAction ActionState
	{
		get
		{
			if (KoopaState == KoopaStates.ShellMoving)
			{
				return EnemyEnemyCollision.EnemyAction.Kill;
			}
			return EnemyEnemyCollision.EnemyAction.Bounce;
		}
	}

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
		isShell = false;
		KoopaState = KoopaStates.Walk1;
		VelocityX = -WALK_SPEED;
		KoopaTimer = WALK_TIME;
		Type = type;
	}

	public void ReverseDirection()
	{
		FacingLeft = !FacingLeft;
		VelocityX = -VelocityX;
		
	}

	private void UpdateCollider()
	{
		switch (KoopaState)
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
            point = new Point((int)position.X, (int)position.Y + 12*4);
			EnemyCollider = new Rectangle(point, new Point(16 * 4, 12 * 4));
		}
		else
		{
            point = new Point((int)position.X, (int)position.Y);
            EnemyCollider = new Rectangle(point, new Point(16 * 4, 24 * 4));
		}
	}

    public void Stomped()
    {
        if (KoopaState == KoopaStates.Walk1 || KoopaState == KoopaStates.Walk2 || KoopaState == KoopaStates.ShellMoving)
        {
            KoopaState = KoopaStates.ShellStill;
            VelocityX = 0;
            KoopaTimer = AWAKEN_TIME;

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

            position += new Vector2(kickRight ? 8.0f : -8.0f, 0);

            UpdateCollider();
        }
    }

	public void CollideWithEnemy(IEnemy enemy)
	{
		// Todo: implement koopa behavior on enemy collision
		switch (enemy.ActionState)
		{
			case EnemyEnemyCollision.EnemyAction.None: // i dont think this should ever happen. idk.
				break;
			case EnemyEnemyCollision.EnemyAction.Bounce:
				if (KoopaState == KoopaStates.Walk1 || KoopaState == KoopaStates.Walk2)
				{
					this.ReverseDirection();
				}
				// Otherwise ignore other enemy: they die or bounce on their own.
                break;
            case EnemyEnemyCollision.EnemyAction.Kill:
				this.Dead = true;
                break;
        }
	}

    public void Draw(SpriteBatch spriteBatch)
	{
		if(Despawn) return;

		SpriteEffects effect = FacingLeft ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

		if (Dead)
		{
			effect |= SpriteEffects.FlipVertically;
        }
		int offX = FacingLeft ? 0 : -16; // I suspect this is needed but idk for sure.
		TextureRegion[] sprites;
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
		if (KoopaTimer < 0.0f)
		{
			switch (KoopaState)
			{
				case KoopaStates.Walk1: // These animate the walking.
					KoopaState = KoopaStates.Walk2;
					KoopaTimer += WALK_TIME;
					break;
				case KoopaStates.Walk2:
					KoopaState = KoopaStates.Walk1;
					KoopaTimer += WALK_TIME;
					break;
				case KoopaStates.ShellStill:
					KoopaState = KoopaStates.ShellStill2; // Awaken from shell.
					KoopaTimer += CLOSE_AWAKEN_TIME;
					break;
				case KoopaStates.ShellStill2:
					KoopaState = KoopaStates.Walk1;
					KoopaTimer += WALK_TIME;
					break;
				default: // Moving shell has no timed events I don't think.
					break;
			}
		}
	}

	public void Update(GameTime gameTime)
	{
		float timeSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
		KoopaTimer -= timeSeconds;
		HandleTimer(); // Handles timed events.

		if (Dead)
		{
			VelocityY += GRAVITY * timeSeconds;
			position += new Vector2(VelocityX, VelocityY) * timeSeconds;

			if (position.Y > 2000f) //value is arbitrary for despawn
            {
				Despawn = true;
			}
			return;
        }

		// Try move.
		if (!onGround) {
			VelocityY += GRAVITY * timeSeconds;
		}
		position += new Vector2(VelocityX, VelocityY) * timeSeconds;

		UpdateCollider();

		if (onGround)
		{
			switch (KoopaState)
			{
                case KoopaStates.Walk1:
				case KoopaStates.Walk2:
					VelocityX = WALK_SPEED;
					break;
				case KoopaStates.ShellMoving:
					VelocityX = SHELL_SPEED;
					break;
				default:
					VelocityX = 0;
					break;
            }
			if (FacingLeft) { VelocityX = -VelocityX; }
			VelocityY = 0;
		}
	}
}

