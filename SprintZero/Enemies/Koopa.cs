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


    private const float WALK_TIME = 0.25f;
	private const float CLOSE_AWAKEN_TIME = 3.0f;
	private const float AWAKEN_TIME = 5.0f;
	public enum KoopaType { Green, Red, Blue };
	public Vector2 position { get; set; }
	public bool Dead { get; set; }
	private bool FacingLeft { get; set; }
	private KoopaType Type { get; set; }
	private enum KoopaStates { Walk1, Walk2, ShellStill, ShellStill2, ShellMoving }
	private KoopaStates KoopaState { get; set; }
	private float KoopaTimer { get; set; }

	private readonly TextureRegion walk1;
	private readonly TextureRegion walk2;

	/*
		green: (451, 816, 16, 24); (481, 815, 16, 24);
		red: (451, 846, 16, 25); (481, 845, 16, 25); // why are these taller
		blue: (453, 875, 16, 24); (483, 874, 16, 24);
	*/

	public static void loadTextures(ContentManager content)
	{
		return; // ????
		TextureAtlas atlas = TextureAtlas.FromFile(content, "Images/koopa-definition.xml");
		green[(int)KoopaStates.Walk1] = atlas.GetRegion("greenWalk1");
		green[(int)KoopaStates.Walk2] = atlas.GetRegion("greenWalk2");
		green[(int)KoopaStates.ShellStill] = atlas.GetRegion("greenShell1");
		green[(int)KoopaStates.ShellStill2] = atlas.GetRegion("greenShell2");
		green[(int)KoopaStates.ShellMoving] = atlas.GetRegion("greenShell1");
        red[(int)KoopaStates.Walk1] = atlas.GetRegion("redWalk1");
        red[(int)KoopaStates.Walk2] = atlas.GetRegion("redWalk2");
        red[(int)KoopaStates.ShellStill] = atlas.GetRegion("redShell1");
        red[(int)KoopaStates.ShellStill2] = atlas.GetRegion("redShell2");
        red[(int)KoopaStates.ShellMoving] = atlas.GetRegion("redShell1");
        blue[(int)KoopaStates.Walk1] = atlas.GetRegion("blueWalk1");
        blue[(int)KoopaStates.Walk2] = atlas.GetRegion("blueWalk2");
        blue[(int)KoopaStates.ShellStill] = atlas.GetRegion("blueShell1");
        blue[(int)KoopaStates.ShellStill2] = atlas.GetRegion("blueShell2");
        blue[(int)KoopaStates.ShellMoving] = atlas.GetRegion("blueShell1");
    }

	public Koopa(KoopaType type)
	{
		Dead = false;
		position = new Vector2(0.0f, 0.0f);
		FacingLeft = true;
		KoopaState = KoopaStates.Walk1;
		KoopaTimer = WALK_TIME;
		Type = type;
	}

	public void Draw(SpriteBatch spriteBatch)
	{
		float scaleX = FacingLeft ? 1.0f : -1.0f; // Normal scale if facing left, otherwise negative scale for facing right.
		int offX = FacingLeft ? 0 : -16; // I suspect this is needed but idk for sure.
		TextureRegion[] sprites = null;
		if (Type == KoopaType.Green)
		{
			sprites = green;
		} else if (Type == KoopaType.Red)
		{
			sprites = red;
		} else
		{
			sprites = blue;
		}
		TextureRegion texture = sprites[(int)KoopaState];
		texture.Draw(spriteBatch, new Vector2(position.X + offX, position.Y), Color.White, 0.0f, new Vector2(0, 0), new Vector2(scaleX, 1.0f), SpriteEffects.None, 0.0f);
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
		this.KoopaTimer -= (float) gameTime.ElapsedGameTime.TotalSeconds;
		HandleTimer(); // Handles timed events.

		// TODO: implement physics.
	}
}

