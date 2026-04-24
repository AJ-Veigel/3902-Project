using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using EnemyCollisions;
using SpriteZero.Enemies;

public class FlyingKoopa : IEnemy
{
	private static TextureRegion[] green;
	private static TextureRegion[] red;
	private static TextureRegion[] blue;


	private const float ANIMATION_TIME = 0.25f; // in seconds

	private const float GRAVITY = 384.0f;

	private const float WALK_SPEED = 75.0f; // In per second scale
	private const float SHELL_SPEED = 384.0f;
	public enum KoopaType { Green, Red, Blue };
	public Vector2 position { get; set; }
	private bool isDead = false;
	public bool Dead
	{
		get { return isDead; }
		set
		{
			if (value && !isDead)
			{
				isDead = true;
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
	public enum KoopaStates { Flying1, Flying2 }
	public KoopaStates KoopaState { get; set; }
	private float KoopaTimer { get; set; }
	public Rectangle EnemyCollider { get; set; }
	public float VelocityX { get; set; }
	public float VelocityY { get; set; }

	public CheckEnemyCollisions.EnemyAction ActionState
	{
		get
		{
			return CheckEnemyCollisions.EnemyAction.None;
		}
	}

	public static void LoadTextures(ContentManager content)
	{
		TextureAtlas atlas = TextureAtlas.FromFile(content, "Images/koopa-definition.xml");
		const int StateCount = 5;
		green = new TextureRegion[StateCount];
		red = new TextureRegion[StateCount];
		blue = new TextureRegion[StateCount];
		green[(int)KoopaStates.Flying1] = atlas.GetRegion("greenWalk1");
		green[(int)KoopaStates.Flying2] = atlas.GetRegion("greenWalk2");
		red[(int)KoopaStates.Flying1] = atlas.GetRegion("redWalk1");
		red[(int)KoopaStates.Flying2] = atlas.GetRegion("redWalk2");
		blue[(int)KoopaStates.Flying1] = atlas.GetRegion("blueWalk1");
		blue[(int)KoopaStates.Flying2] = atlas.GetRegion("blueWalk2");
	}

	public FlyingKoopa(KoopaType type = KoopaType.Green)
	{
		Dead = false;
		onGround = false;
		position = new Vector2(600.0f, 660.0f);
		FacingLeft = true;
		isShell = false;
		KoopaState = KoopaStates.Flying1;
		VelocityX = 0;
		KoopaTimer = ANIMATION_TIME;
		Type = type;
	}

	public void ReverseDirection()
	{
		FacingLeft = !FacingLeft;
		VelocityX = -VelocityX;

	}

	private void UpdateCollider()
	{
		var point = new Point((int)position.X, (int)position.Y);
		EnemyCollider = new Rectangle(point, new Point(16 * 4, 24 * 4));
	}

	public void Stomped()
	{
		// TODO: spawn koopa.
	}


	public void CollideWithEnemy(IEnemy enemy)
	{
		// Todo: implement koopa behavior on enemy collision
		switch (enemy.ActionState)
		{
			case CheckEnemyCollisions.EnemyAction.None: // i dont think this should ever happen. idk.
				break;
			case CheckEnemyCollisions.EnemyAction.Bounce:
				this.ReverseDirection();
				break;
			case CheckEnemyCollisions.EnemyAction.Kill:
				this.Dead = true;
				break;
		}
	}

	public void Draw(SpriteBatch spriteBatch)
	{
		if (Despawn) return;

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
				case KoopaStates.Flying1: // These animate the flight.
					KoopaState = KoopaStates.Flying2;
					KoopaTimer += ANIMATION_TIME;
					break;
				case KoopaStates.Flying2:
					KoopaState = KoopaStates.Flying1;
					KoopaTimer += ANIMATION_TIME;
					break;
				default:
					break;
			}
		}
	}

	public void Update(GameTime gameTime)
	{
		float timeSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
		KoopaTimer -= timeSeconds;
		HandleTimer(); // Handles timed events.

		// TODO: fill stuff in
	}
}

