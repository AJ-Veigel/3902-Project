using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SpriteZero.Enemies;

public class Koopa : IEnemy
{
	private const float WALK_TIME = 0.25f;
	private const float CLOSE_AWAKEN_TIME = 3.0f;
	private const float AWAKEN_TIME = 5.0f;
	public enum KoopaType { Green, Red, Blue };
	public Vector2 position { get; set; }
	public bool Dead { get; set; }
	private bool FacingLeft { get; set; }

	private enum KoopaStates { Walk1, Walk2, ShellStill, ShellStill2, ShellMoving}
	private KoopaStates KoopaState { get; set; }
	private float KoopaTimer { get; set; }

	private bool WillWalkOff { get; set; }

	private readonly TextureRegion walk1;
	private readonly TextureRegion walk2;

	/*
		green: (451, 816, 16, 24); (481, 815, 16, 24);
		red: (451, 846, 16, 25); (481, 845, 16, 25); // why are these taller
		blue: (453, 875, 16, 24); (483, 874, 16, 24);
	*/

	public Koopa(KoopaType type)
	{
		Dead = false;
		position = new Vector2(0.0f, 0.0f);
		FacingLeft = true;
		WillWalkOff = true;
		KoopaState = KoopaStates.Walk1;
		KoopaTimer = WALK_TIME;
		switch (type)
		{
			case KoopaType.Green:
				walk1 = new TextureRegion(); // We really need a content handler.
				walk2 = new TextureRegion();
				break;
			case KoopaType.Red:
				walk1 = new TextureRegion();
				walk2 = new TextureRegion();
				WillWalkOff = false;
				break;
			case KoopaType.Blue:
				walk1 = new TextureRegion();
				walk2 = new TextureRegion();
				break;
		}
	}

	public void Draw(SpriteBatch spriteBatch)
	{
		float scaleX = FacingLeft ? 1.0f : -1.0f; // Normal scale if facing left, otherwise negative scale for facing right.
		int offX = FacingLeft ? 0 : -16; // I suspect this is needed but idk for sure.
		switch (this.KoopaState)
		{
			case KoopaStates.Walk1:
				break;
			case KoopaStates.Walk2:
				break;
			default:
				throw new NotImplementedException();
		}
	}

	private void HandleTimer()
	{
        if (this.KoopaTimer < 0.0f)
        {
            switch (this.KoopaState)
            {
                case KoopaStates.Walk1:
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
		this.KoopaTimer -= (float) gameTime.ElapsedGameTime.TotalSeconds;
		HandleTimer(); // Handles timed events.

		// TODO: implement physics.
	}
}

