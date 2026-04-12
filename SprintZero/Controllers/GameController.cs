using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SprintZero;

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
            game.play();


        if (currentState.IsKeyDown(Keys.D5) && previousState.IsKeyUp(Keys.D5))
            game.PauseGame();

        if (currentState.IsKeyDown(Keys.D6) && previousState.IsKeyUp(Keys.D6))
            game.UnpauseGame();

       
        if (currentState.IsKeyDown(Keys.M) && previousState.IsKeyUp(Keys.M))
            MediaPlayer.Pause();
    }
}