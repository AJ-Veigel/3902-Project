using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SprintZero;
using SoundManager;

public class GameController
{
    private Game1 game;
    private KeyboardState previousState, currentState;

    public GameController(KeyboardState prev, KeyboardState curr, Game1 game)
    {
        previousState = prev;
        currentState = curr;
        this.game = game;
    }

    public void UpdateGame()
    {

        if (currentState.IsKeyDown(Keys.Escape) && previousState.IsKeyUp(Keys.Escape))
            game.Exit();

        if (currentState.IsKeyDown(Keys.R) && previousState.IsKeyUp(Keys.R))
            game.Reset();

        if (currentState.IsKeyDown(Keys.N) && previousState.IsKeyUp(Keys.N))
            Music.PlayBackground();


        if (currentState.IsKeyDown(Keys.D5) && previousState.IsKeyUp(Keys.D5))
        {
            game.IsPaused = true;
            Music.PauseMusic();
        }

        if (currentState.IsKeyDown(Keys.D6) && previousState.IsKeyUp(Keys.D6))
        {
            game.IsPaused = false;
            Music.ResumeMusic();
        }

        if (currentState.IsKeyDown(Keys.M) && previousState.IsKeyUp(Keys.M))
            MediaPlayer.Pause();
    }
}